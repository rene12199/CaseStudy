using CaseStudy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Api;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateTransaction()
    {
        _transactionService.CreateTransaction();
        return Created();
    }
}