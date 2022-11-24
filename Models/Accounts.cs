using System.ComponentModel.DataAnnotations;
namespace matthew_prac;

public class Accounts
{
    private string uname = "";
    private string pword = "";

    [Key]
    public Guid UserId{get; set;}
    public string UserName{get{return uname;} set{uname = value;}}
    public string Password{get{return pword;} set{pword = value;}}

    public Accounts()
    {
        UserId = Guid.NewGuid();
    }

    public Accounts(string u, string p)
    {
        UserId = Guid.NewGuid();
        uname = u;
        pword = p;
    }
}