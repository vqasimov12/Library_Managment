using Application.CQRS.Image.Queries.Requests;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Image.Handlers.QueryHandlers;

public sealed class GetImageHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetImageRequest, ResponseModel<string>>
{
    public async Task<ResponseModel<string>> Handle(GetImageRequest request, CancellationToken cancellationToken)
    {
        var image = await unitOfWork.ImageRepository.GetImageByIdAsync(request.ImageId);
        if (image == null)
            throw new BadRequestException("Image can not be found with provided id");

        return new ResponseModel<string>
        {
            Data = image.ImagePath,
            Errors = [],
            IsSuccess = true
        };
    }
}
