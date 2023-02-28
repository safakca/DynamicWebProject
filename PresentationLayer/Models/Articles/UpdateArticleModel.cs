namespace PresentationLayer.Models;
public class UpdateArticleModel
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime UpdatedDate { get; set; }
}
