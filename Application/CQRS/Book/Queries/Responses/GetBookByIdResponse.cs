using Domain.Enums;

namespace Application.CQRS.Book.Queries.Responses;

public class GetBookByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public Language Language { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool ShowOnFirstScreen { get; set; }
}
