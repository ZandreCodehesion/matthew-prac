/* using Microsoft.AspNetCore.Mvc;

namespace matthew_prac.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    public AccountsController(ILogger<AccountsController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "PostAccounts")]
    public IEnumerable<Accounts> Post()
    {
        return Enumerable.Range(1, 5).Select(index => new Accounts
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
} */