namespace matthew_prac;

public class AccountsRq
{
    private string name;
    private string password;

    public AccountsRq(){}

    public AccountsRq(string n, string p)
    {
        name = n;
        password = p;
    }

    public string Name{get{return name;} set{name = value;}}
    public string Password{get{return password;} set{password = value;}}
}