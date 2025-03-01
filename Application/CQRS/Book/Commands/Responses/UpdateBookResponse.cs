using Domain.Enums;

namespace Application.CQRS.Book.Commands.Responses;

public class UpdateBookResponse
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
    public bool ShowOnFirstScreen { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int UpdatedBy { get; set; }
}
