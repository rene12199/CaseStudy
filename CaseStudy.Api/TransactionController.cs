using Microsoft.AspNetCore.Mvc;

namespace CaseStudy;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{

    [HttpPost()]
    public IActionResult CreateTransaction()
    {
        return Ok();
    }
}