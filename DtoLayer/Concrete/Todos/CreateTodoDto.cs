﻿namespace DtoLayer.Concrete.Todos;
public class CreateTodoDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
}
