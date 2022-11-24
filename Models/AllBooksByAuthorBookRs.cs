namespace matthew_prac;

public class AllBooksByAuthorBookRs
{
    public string bookName;
    public DateTime datePublished;
    public string publisher;
    public int copiesSold;
    public string creatorName;

    public AllBooksByAuthorBookRs(string bN, DateTime dP, string p, int cS, string cN)
    {
        bookName = bN;
        datePublished = dP;
        publisher = p;
        copiesSold = cS;
        creatorName = cN;
    }
}