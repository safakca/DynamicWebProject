using EntityLayer.Common;

namespace EntityLayer.Concrete;
public class Article : BaseEntity, ICreatedDate, IUpdatedDate
{
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public virtual Author Author { get; set; }
}