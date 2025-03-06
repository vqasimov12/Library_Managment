namespace Application.CQRS.Image.Queries.Responses;

public sealed class GetImagesResponse
{
    public string ImagePath { get; set; }
    public string ImageId{ get; set; }
}
