using Domain.BaseEntities;
using Domain.Enums;

namespace Domain.Entities;

public class Book : BaseEntity
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CoverPhoto { get; set; }
    public int UserId { get; set; }
    public bool? ShowOnFirstScreen { get; set; }
    public Language? Language { get; set; }
}