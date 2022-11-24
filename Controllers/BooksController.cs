using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace matthew_prac.controllers;

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
    
    [HttpGet]
    public ActionResult<List<AllBooksRs>> GetBooks()
    {
        try
        {
            List<Books> books = _context.Books.ToList<Books>();

            if(books == null || books.Count == 0)
            {
                return NotFound("No Books In The Library");
            }

            List<AllBooksRs> allBooks = new List<AllBooksRs>();

            books.ForEach(b => {
                Authors author = _context.Authors.FirstOrDefault<Authors>(o => o.AuthorId == b.Author);
                string authorName = "No Author";
                bool hasAuthor = false;

                if(author != null)
                {
                    authorName = author.AuthorName;
                    hasAuthor = true;
                }

                allBooks.Add(new AllBooksRs(b.BookName, authorName, hasAuthor));
            });

            Console.WriteLine("The values in allBooks are:\n======================");
            allBooks.ForEach(a => Console.WriteLine(a.BookAuthorName + " - " + a.BookName));

            return Ok(allBooks);
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could not get books from Library Database");
        }
    }

    [HttpGet("{authorId}")]
    public async Task<ActionResult<List<AllBooksByAuthorRs>>> GetBooksByAuthorId(Guid authorId)
    {
        try
        {
            if(authorId == null)
            {
                return BadRequest("Invalid Author ID");
            }

            List<Books> books = await _context.Books.Where<Books>(o => o.Author == authorId).ToListAsync<Books>(); 

            if(books == null || books.Count == 0)
            {
                return NotFound("This Author Has No Books");
            }

            List<AllBooksByAuthorRs> allBooks = new List<AllBooksByAuthorRs>();

            books.ForEach(b => {
                allBooks.Add(new AllBooksByAuthorRs(b.BookName, b.DatePublished));
            });

            return Ok(allBooks);
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could not get books by author id from library database");
        }
    }
    
    [HttpGet("{authorId}/{bookId}")]
    public async Task<ActionResult<List<AllBooksByAuthorBookRs>>> GetBooksByAuthorIdAndBookId([FromRoute] Guid authId, [FromRoute] Guid bookId)
    {
        try
        {
            Console.WriteLine("AuthorId and Book Id\n=======================\n" + authId + " - " + bookId);
            if(authId == null || bookId == null)
            {
                return BadRequest("Invalid Author ID Or Book Id");
            }

            List<Books> books = await _context.Books.Where<Books>(o => o.Author == authId && o.BookId == bookId).ToListAsync<Books>(); 

            if(books == null || books.Count() == 0)
            {
                return NotFound("The Book Does Not Exist");
            }      

            List<AllBooksByAuthorBookRs> allBooks = new List<AllBooksByAuthorBookRs>();

            books.ForEach(async b => {
                Accounts account = await _context.Accounts.FirstOrDefaultAsync<Accounts>(a => a.UserId == b.CreatedBy);
                string name = "Anonymous";

                if(account != null)
                {
                    name  = account.UserName;
                }

                allBooks.Add(new AllBooksByAuthorBookRs(b.BookName, b.DatePublished, b.Publisher, b.CopiesSold, name));
            });  

            return Ok(allBooks);   
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could not get books by author id from library database");
        }
    }

    [HttpPost("{authorId}")]
    public async Task<ActionResult<string>> CreateBook(Guid authorId, [FromBody] BooksRq book)
    {
        try
        {
            if(authorId == null)
            {
                return BadRequest("Invalid Author ID");
            }

            string tempId = GetCurrentUser();

            if(tempId == "WhatEvenIsAToken?" || tempId == "NotLoggedIn")
            {
                return BadRequest("Bad Token");
            }

            Guid.TryParse(tempId, out Guid cUId);

            Books newBook = new Books(book.BookName, book.Publisher, book.DatePublished, book.CopiesSold, cUId, authorId); 
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return Ok("Book Added Successfully");
        }
        catch(Exception e)
        {
            Console.WriteLine("Woops, Something Went Wrong: \n" + e.Message);
            return BadRequest("Could Add Book To the Database");
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