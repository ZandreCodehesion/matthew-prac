using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace matthew_prac.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private IConfiguration _configuration;
    private readonly ILogger<BooksController> _logger;
    private readonly LibraryDBContext _context;

    public BooksController(IConfiguration config,ILogger<BooksController> logger, LibraryDBContext context)
    {
        _configuration = config;
        _logger = logger;
        _context = context;
    }
}