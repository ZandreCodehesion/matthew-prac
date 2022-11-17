using System.ComponentModel.DataAnnotations;

namespace matthew_prac;

public class Accounts
{
    [Key]
    public Guid uid {get;set;}

    private string uname;
    private string pword;

    private Accounts()
    {

    }

    public Accounts(string u, string p)
    {
        uid = new Guid();
        Username = u;
        Password = p;
    }

    public string Username {get; set;}
    public string Password {get; set;}
}
