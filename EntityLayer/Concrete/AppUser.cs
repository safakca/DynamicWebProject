using EntityLayer.Common; 

namespace EntityLayer.Concrete;
public class AppUser : BaseEntity
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public int AppRoleId { get; set; }
    public AppRole? AppRole { get; set; }
}

