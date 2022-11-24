using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace matthew_prac.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private IConfiguration _configuration;
    private readonly ILogger<AccountsController> _logger;
    private readonly LibraryDBContext _context;

    public AccountsController(IConfiguration config,ILogger<AccountsController> logger, LibraryDBContext context)
    {
        _configuration = config;
        _logger = logger;
        _context = context;
    }

    [HttpGet("AllAccounts")]
    public ActionResult<List<Accounts>> GetAllAccounts()
    {
        try
        {
            List<Accounts> accounts = _context.Accounts.ToList<Accounts>();

            if(accounts.Count == 0)
            {
                return NotFound();
            }

            return Ok(accounts);
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could not get authors from Library Database");
        }
    }

    [HttpPost("register")]
    public async Task<StatusCodeResult> Register([FromBody] AccountsRq user)
    {
        //Console.WriteLine("Register User Details:\n=======================\nName: " + user.Name + "\nPassword: " + user.Password);

        try
        {
            if(!String.IsNullOrWhiteSpace(user.Name) || user.Name != "")
            {
                Accounts acc = new Accounts(user.Name, user.Password);

                //Console.WriteLine("Account Details To Be Added:\n=============================\nUser ID: " + acc.UserId + "\nUser Name: " + acc.UserName + "\nPassword: " + acc.Password);

                await _context.Accounts.AddAsync(acc);
                await _context.SaveChangesAsync();

                return new StatusCodeResult(204);
            }
            else
            {
                return new StatusCodeResult(400);
            }
        }
        catch/* (Exception e) */ 
        {
            //Console.WriteLine("Something went wrong: \n" + e.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] AccountsRq user)
    {
        try
        {
            if(!String.IsNullOrWhiteSpace(user.Name) || user.Name != "")
            {
                Accounts acc = await _context.Accounts.SingleOrDefaultAsync<Accounts>(a => a.UserName == user.Name);

                if(acc != null)
                {
                    if(acc.Password == user.Password)
                    {
                        string token = GenerateToken(acc);

                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest("Incorrect Password");
                    }
                }

                return NotFound("Account Does Not Exist");
            }
            else
            {
                return BadRequest("Invalid Account Name");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Something went Wrong in Login:\n" + e.Message);
            return BadRequest("Server could not generate token or something");
        }
    }

    private string GenerateToken(Accounts user)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Sid, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)/* ,
            new Claim("Password", user.Password) */
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: signIn);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
