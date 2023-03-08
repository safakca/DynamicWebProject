using EntityLayer.Common;
using EntityLayer.Enums;

namespace EntityLayer.Concrete;

public class Todo : BaseEntity, ICreatedDate, IUpdatedDate
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TodoStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

