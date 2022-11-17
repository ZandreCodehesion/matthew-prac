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

    //[HttpPost(Name = "UserRegistration")]
    public void PostNewUser(string name, string password)
    {
        Accounts acc = new Accounts(name, password);
        //Add To user to database/jsonfile
    }

    //[HttpPost(Name = "UserLogon")]
    public Accounts PostUserLogin(string name, string password) //Return JWT token?
    {
        return new Accounts(name, password);
    }
}