using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete;
public class AppUser : IdentityUser<int>
{ 
    public string Username { get; set; }
    public string Password { get; set; } 
    public string MailCode { get; set; }
}

