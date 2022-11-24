using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace matthew_prac;

public class Books
{
    [Key]
    public Guid BookId{get; set;}

    [ForeignKey("Accounts")]
    public Guid CreatedBy{get; set;}

    [ForeignKey("Authors")]
    public Guid Author{get; set;}

    private string bookName;
    private string publisher;
    private DateTime datePublished;
    private int copiesSold;

    public Books()
    {
        BookId = Guid.NewGuid();
        bookName = "";
        publisher = "";
        copiesSold = 0;
    }

    public Books(string bN, string pub, DateTime dPub, int cS, Guid uid, Guid aid)
    {
        BookId = Guid.NewGuid();
        bookName = bN;
        publisher = pub;
        datePublished = dPub;
        copiesSold = cS;
        CreatedBy = uid;
        Author = aid;
    }

    public string BookName{get{return bookName;} set{bookName = value;}}
    public string Publisher{get{return publisher;} set{publisher = value;}}
    public DateTime DatePublished{get{return datePublished;} set{datePublished = value;}}
    public int CopiesSold{get{return copiesSold;} set{copiesSold = value;}}
}