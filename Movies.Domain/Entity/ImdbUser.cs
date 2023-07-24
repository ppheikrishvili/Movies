using System.ComponentModel.DataAnnotations;
using Movies.Domain.Interface;

namespace Movies.Domain.Entity;

public class ImdbUser : IEntity
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
    [StringLength(126, ErrorMessage = "Must be between 5 and 126 characters", MinimumLength = 5)]
    [RegularExpression(
        "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{5,126}$",
        ErrorMessage =
            "Passwords must be between 5-126 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
    public string Password { get; set; }

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "The email address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EMail { get; set; }
}