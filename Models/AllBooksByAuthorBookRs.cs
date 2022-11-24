namespace matthew_prac;

public class AllBooksByAuthorBookRs
{
    private string bookName;
    private DateTime datePublished;
    private string publisher;
    private int copiesSold;
    private string creatorName;

    public AllBooksByAuthorBookRs(string bN, DateTime dP, string p, int cS, string cN)
    {
        bookName = bN;
        datePublished = dP;
        publisher = p;
        copiesSold = cS;
        creatorName = cN;
    }

    public string BookName{get{return bookName;} set{bookName = value;}}
    public DateTime DatePublished{get{return datePublished;} set{datePublished = value;}}
    public string Publisher{get{return publisher;} set{publisher = value;}}
    public int CopiesSold{get{return copiesSold;} set{copiesSold = value;}}
    public string CreatorName{get{return creatorName;} set{creatorName = value;}}
}