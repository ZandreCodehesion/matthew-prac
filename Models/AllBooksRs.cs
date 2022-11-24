namespace matthew_prac;

public class AllBooksRs
{
    public string bookName;
    public string bookAuthorName;
    public bool ownsAuthor;

    public AllBooksRs(string bN, string bA, bool oA)
    {
        bookName = bN;
        bookAuthorName = bA;
        ownsAuthor = oA;
    }
}