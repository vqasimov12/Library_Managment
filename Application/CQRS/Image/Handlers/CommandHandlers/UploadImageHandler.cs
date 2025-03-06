using Application.CQRS.Image.Commands.Requests;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Image.Handlers.CommandHandlers;


public class UploadImageHandler(IUnitOfWork unitOfWork) : IRequestHandler<UploadImageRequest, ResponseModel<string>>
{
    public async Task<ResponseModel<string>> Handle(UploadImageRequest request, CancellationToken cancellationToken)
    {

        var image = new Domain.Entities.Image()
        {
            ImagePath = request.ImagePath,
            PhotoId = Guid.NewGuid().ToString()
        };

        await unitOfWork.ImageRepository.AddImageAsync(image);
        await unitOfWork.SaveChangesAsync();

        return new ResponseModel<string>()
        {
            Data = image.PhotoId,
            Errors = [],
            IsSuccess = true
        };


    }
}
