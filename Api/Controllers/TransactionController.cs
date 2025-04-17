using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IConverter<TransactionDto, Transaction> _transactionConverter;
    private readonly ITransactionService _transactionService;

    public TransactionController(IConverter<TransactionDto, Transaction> transactionConverter,
        ITransactionService transactionService)
    {
        _transactionConverter = transactionConverter;
        _transactionService = transactionService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RecordTransaction(TransactionDto transactionDto)
    {
        // todo DTO Validation
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        _transactionService.CreateTransaction(_transactionConverter.Convert(transactionDto));

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Created();
    }
}