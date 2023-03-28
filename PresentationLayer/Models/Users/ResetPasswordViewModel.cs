using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Users;
public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Type existing password")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Type existing password")]
    public string CurrentPassword { get; set; }


    [Required(ErrorMessage = "Type new password")]
    public string NewPassword { get; set; }


    [Required(ErrorMessage = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "Passwords must match")]
    public string ConfirmNewPassword { get; set; }
    public string Token { get; set; }
}
