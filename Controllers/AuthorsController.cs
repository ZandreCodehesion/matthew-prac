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
public class AuthorsController : ControllerBase
{
    private IConfiguration _configuration;
    private readonly ILogger<AuthorsController> _logger;
    private readonly LibraryDBContext _context;

    public AuthorsController(IConfiguration config,ILogger<AuthorsController> logger, LibraryDBContext context)
    {
        _configuration = config;
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Authors>> GetAuthors()
    {
        try
        {
            List<Authors> authors = _context.Authors.ToList<Authors>();

            if(authors.Count == 0)
            {
                return NotFound();
            }

            return Ok(authors);
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could not get authors from Library Database");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Authors>> GetAuthorByID(Guid id)
    {
        try
        {
            if(id == null)
            {
                return BadRequest("Invalid ID");
            }

            Authors author = await _context.Authors.FirstOrDefaultAsync<Authors>(a => a.AuthorId == id);

            if(author == null)
            {
                return NotFound("There are no Authors in the Library Database");
            }

            return Ok(author);
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could not get authors from Library Database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateAuthor([FromBody] AuthorsRq author)
    {
        try
        {
            string tempId = GetCurrentUser();

            if(tempId == "WhatEvenIsAToken?" || tempId == "NotLoggedIn")
            {
                return BadRequest("Bad Token");
            }

            if(author.AuthorName.IsNullOrEmpty() || author.ActiveFrom == null || author.ActiveTo == null)
            {
                return BadRequest("Incorrect Author Details");
            }

            Guid.TryParse(tempId, out Guid cUId);
            Authors auth = new Authors(author.AuthorName, author.ActiveFrom, author.ActiveTo, cUId);

            await _context.Authors.AddAsync(auth);
            await _context.SaveChangesAsync();

            return Ok("Author Added Successfully");
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could Not Create Author");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateAuthorByID(Guid id, [FromBody] AuthorsRq author)
    {
        try
        {
            //Get ID of Current user and convert it to Guid
            string tempId = GetCurrentUser();

            if(tempId == "WhatEvenIsAToken?" || tempId == "NotLoggedIn")
            {
                return BadRequest("Bad Token");
            }

            if(author.AuthorName.IsNullOrEmpty() || author.ActiveFrom == null || author.ActiveTo == null)
            {
                return BadRequest("Incorrect Author Details");
            }

            Guid.TryParse(tempId, out Guid cUId);

            //Get Author by id
            Authors authById = await _context.Authors.FirstOrDefaultAsync<Authors>(o => o.AuthorId == id);
            //Check who created the author
            if(authById == null)
            {
                return BadRequest("Author Does Not Exist");
            }

            Guid authCreatorId = authById.CreatedBy;
            //if creator id is same as current id: update author, else return error message
            if(authCreatorId == cUId)
            {
                authById.AuthorName = author.AuthorName;
                authById.ActiveFrom = author.ActiveFrom;
                authById.ActiveTo = author.ActiveTo;
                
                _context.Entry(authById).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Author Updated");
               
            }

            return BadRequest("You Did Not Create This Author");

        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could Not Update Author");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteAuthorByID(Guid id)
    {
        try
        {
            //Get ID of Current user and convert it to Guid
            string tempId = GetCurrentUser();

            if(tempId == "WhatEvenIsAToken?" || tempId == "NotLoggedIn")
            {
                return BadRequest("Bad Token");
            }

            Guid.TryParse(tempId, out Guid cUId);

            //Get Author by id
            Authors authById = await _context.Authors.FirstOrDefaultAsync<Authors>(o => o.AuthorId == id);
            //Check who created the author
            if(authById == null)
            {
                return BadRequest("Author Does Not Exist");
            }

            Guid authCreatorId = authById.CreatedBy;
            //if creator id is same as current id: update author, else return error message
            if(authCreatorId == cUId)
            {
                _context.Entry(authById).State = EntityState.Deleted;
                _context.SaveChanges();
                
                return Ok("Author Deleted Successfully");
            }

            return BadRequest("You Did Not Create This Author");
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could Not Delete Author");
        }
    }

    private string GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if(identity != null)
        {
            string uid = identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Sid).Value;

            return (uid == null)? "WhatEvenIsAToken?" : uid;
        }

        return "NotLoggedIn";
    }
}