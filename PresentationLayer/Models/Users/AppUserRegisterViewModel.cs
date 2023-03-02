namespace PresentationLayer.Models.Users;
public class AppUserRegisterViewModel
{
    //[Required(ErrorMessage = "name is required")]
    public string Name { get; set; }

    //[Required(ErrorMessage = "surname is required")]
    public string Surname { get; set; }

    //[Required(ErrorMessage = "username is required")]
    public string Username { get; set; }

    //[Required(ErrorMessage = "mail is required")]
    public string Mail { get; set; }

    //[Required(ErrorMessage = "password is required")]
    public string Password { get; set; }

    //[Required(ErrorMessage = "password confirm is required")]
    public string ConfirmPassword { get; set; }

    public string Gender { get; set; }
    public string MailCode { get; set; }
}
