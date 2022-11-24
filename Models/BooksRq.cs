namespace matthew_prac;

public class BooksRq
{
    private string bookName;
    private string publisher;
    private DateTime datePublished;
    private int copiesSold;
    private Guid author;
    private Guid createdBy;

    public BooksRq()
    {
        bookName = "";
        publisher = "";
        copiesSold = 0;
    }

    public BooksRq(string bN, string pub, DateTime dPub, int cS, Guid uid, Guid aid)
    {
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
    public Guid CreatedBy{get{return createdBy;} set{createdBy = value;}}
    public Guid Author{get{return author;} set{author = value;}}
}