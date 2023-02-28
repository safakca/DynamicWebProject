using System;
using EntityLayer.Common;
using EntityLayer.Enums;

namespace EntityLayer.Concrete;

public class Todo : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TodoStatus Status { get; set; }
}

