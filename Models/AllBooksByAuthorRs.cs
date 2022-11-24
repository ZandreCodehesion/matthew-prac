namespace matthew_prac;

public class AllBooksByAuthorRs
{
    private string bookName;
    private DateTime datePublished;

    public AllBooksByAuthorRs(string bN, DateTime dP)
    {
        bookName = bN;
        datePublished = dP;
    }

    public string BookName{get{return bookName;} set{bookName = value;}}
    public DateTime DatePublished{get{return datePublished;} set{datePublished = value;}}
}