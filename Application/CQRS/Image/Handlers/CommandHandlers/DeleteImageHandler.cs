
using Application.CQRS.Image.Commands.Requests;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Image.Handlers.CommandHandlers;

public class DeleteImageHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteImageRequest, ResponseModel<string>>
{
    public async Task<ResponseModel<string>> Handle(DeleteImageRequest request, CancellationToken cancellationToken)
    {
        var image = await unitOfWork.ImageRepository.GetImageByIdAsync(request.ImageId);
        if (image == null)
            throw new BadRequestException("Image not found with provided id");

        await unitOfWork.ImageRepository.DeleteImageAsync(image.PhotoId);
        await unitOfWork.SaveChangesAsync();

        File.Delete(image.ImagePath);
        return new ResponseModel<string>
        {
            Data = "Image deleted successfully",
            IsSuccess = true,
            Errors = []
        };
    }
}
