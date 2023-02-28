namespace DtoLayer.Concrete.Authors;
public record UpdateAuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public DateTime UpdatedDate { get; set; }
}
