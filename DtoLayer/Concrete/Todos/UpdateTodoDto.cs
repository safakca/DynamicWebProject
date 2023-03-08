namespace DtoLayer.Concrete.Todos;
public class UpdateTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime UpdatedDate { get; set; }
}
