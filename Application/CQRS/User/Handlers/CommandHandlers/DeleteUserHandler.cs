using Application.CQRS.User.Commands.Requests;
using Application.CQRS.User.Commands.Responses;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.User.Handlers.CommandHandlers;

public class DeleteUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserRequest, ResponseModel<DeleteUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseModel<DeleteUserResponse>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id);
        if (user == null)
            throw new BadRequestException("User not found with provided id");
        await _unitOfWork.UserRepository.DeleteUserAsync(user.Id, 1);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseModel<DeleteUserResponse>
        {
            Data = new DeleteUserResponse
            {
                Message = "User deleted successfully"
            },
            Errors = [],
            IsSuccess = true
        };
    }
}
