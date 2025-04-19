using CaseStudy.Api.Converter;
using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IConverter<TransactionFilterDto, TransactionFilter> _filterConverter;
    private readonly ITransactionConverter _transactionConverter;
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionConverter transactionConverter,
        IConverter<TransactionFilterDto, TransactionFilter> filterConverter,
        ITransactionService transactionService)
    {
        _transactionConverter = transactionConverter;
        _filterConverter = filterConverter;
        _transactionService = transactionService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RecordTransaction(CreateTransactionDto createTransactionDto)
    {
        // todo DTO Validation
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        _transactionService.CreateTransaction(_transactionConverter.Convert(createTransactionDto));

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Created();
    }

    [HttpGet]
    [Route("filter")]
    [ProducesResponseType(typeof(TransactionViewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetFilteredTransactions([FromQuery] TransactionFilterDto transactionDto)
    {
        // todo DTO Validation
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        var transactions = _transactionService.FilterTransactions(_filterConverter.Convert(transactionDto));

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Ok(transactions.Select(t => _transactionConverter.Convert(t)));
    }

    [HttpGet]
    [Route("summary")]
    [ProducesResponseType(typeof(TransactionViewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetMonthSummary(int month, int year)
    {
        // todo DTO Validation
        if(month < 1 || (month > 12 && year < 1))
        {
            return BadRequest();
        }

        var transactions = _transactionService.FilterTransactions(new TransactionFilter
        {
            CreatedAfter = new DateTime(year, month, 1),
            CreatedBefore = new DateTime(year, month, DateTime.DaysInMonth(year, month))
        });

        var categories = transactions.GroupBy(t => t.Category?.CategoryId);
        var summary = new List<TransactionSummaryDto>();

        foreach (var category in categories)
        {
            summary.Add(_transactionConverter.Convert(category));
        }

        return Ok(summary);
    }
}