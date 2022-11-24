namespace matthew_prac;

public class AuthorsRq
{
    private string authorName;
    private DateTime activeFrom;
    private DateTime activeTo;

    public AuthorsRq()
    {
        authorName = "";
        activeFrom = DateTime.Now;
        activeTo = DateTime.Now.AddDays(5);
    }

    public AuthorsRq(string n, DateTime aF, DateTime aT)
    {
        authorName = n;
        activeFrom = aF;
        activeTo = aT;
    }

    public string AuthorName{get{return authorName;} set{authorName = value;}}
    public DateTime ActiveFrom{get{return activeFrom;} set{activeFrom = value;}}
    public DateTime ActiveTo{get{return activeTo;} set{activeTo = value;}}
}