using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete;
public class AppUser : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ImageURL { get; set; }
    public string? Gender { get; set; }
    public string? MailCode { get; set; }
}

