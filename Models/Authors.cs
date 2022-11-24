using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace matthew_prac;

public class Authors
{
    [Key]
    public Guid AuthorId{get; set;}

    private string authorName;
    private DateTime activeFrom;
    private DateTime activeTo;

    [ForeignKey("Accounts")]
    public Guid CreatedBy{get; set;}
    //public virtual Account Account{get;set;}

    public Authors()
    {
        AuthorId = Guid.NewGuid();
    }

    public Authors(string n, DateTime aF, DateTime aT, Guid uId)
    {
        authorName = n;
        activeFrom = aF;
        activeTo = aT;
        AuthorId = Guid.NewGuid();
        CreatedBy = uId;
    }

    public string AuthorName{get{return authorName;} set{authorName = value;}}
    public DateTime ActiveFrom{get{return activeFrom;} set{activeFrom = value;}}
    public DateTime ActiveTo{get{return activeTo;} set{activeTo = value;}}
}