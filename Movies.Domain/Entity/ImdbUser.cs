using System.ComponentModel.DataAnnotations;

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

    [Key, MaxLength(32)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(126, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    public string Password { get; set; }

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "The email address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EMail { get; set; }
}