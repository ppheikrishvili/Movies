
namespace Movies.Domain.Entity;

public class ImdbUser
{
    public ImdbUser(string name, string password, string eMail)
    {
        Name = name;
        Password = password;
        EMail = eMail;
    }

    public ImdbUser()
    {
        Name = "";
        Password = "";
        EMail = "";
    }

    public string Name { get; set; }
    public string Password { get; set; }
    public string EMail { get; set; }
}