using EntityLayer.Common; 

namespace EntityLayer.Concrete; 
public class AppRole : BaseEntity 
{ 
    public string? Defination { get; set; }
    public List<AppUser>? AppUsers { get; set; }
}

