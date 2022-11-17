using Microsoft.AspNetCore.Mvc;

namespace matthew_prac.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    public AccountsController(ILogger<AccountsController> logger)
    {
        _logger = logger;
    }

    [HttpPost("UserRegistration/{username}/{password}")]
    public void UserRegistration(string name, string password)
    {
        Accounts acc = new Accounts(name, password);
        //Add acc to user database
        //Void method returns Code 204
    }

    /* [HttpPost("UserLogon/{username}/{password}")]
    public Accounts UserLogin(string name, string password) //Return JWT
    {
        return new Accounts(name, password);
    } */
}