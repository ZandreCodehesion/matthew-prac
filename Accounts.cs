using System;

namespace matthew_prac;

public class Accounts
{
    private Guid uid;
    private string uname;
    private string pword;

    public Accounts(string u, string p)
    {
        uid = new Guid();
        Username = u;
        Password = p;
    }

    public string Username {get; set;}
    public string Password {get; set;}
}
