namespace matthew_prac;

public class AllBooksRs
{
    private string bookName;
    private string bookAuthorName;
    private bool ownsAuthor;

    public AllBooksRs(string bN, string bA, bool oA)
    {
        bookName = bN;
        bookAuthorName = bA;
        ownsAuthor = oA;
    }

    public string BookName{get{return bookName;} set{bookName = value;}}
    public string BookAuthorName{get{return bookAuthorName;} set{bookAuthorName = value;}}
    public bool OwnsAuthor{get{return ownsAuthor;} set{ownsAuthor = value;}}
}