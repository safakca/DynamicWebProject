namespace DtoLayer.Concrete.Authors;
public record CreateAuthorDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public DateTime CreatedDate { get; set; }
}
