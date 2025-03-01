using Application.CQRS.User.Commands.Requests;
using Application.CQRS.User.Commands.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using Common.Security;
using FluentValidation;
using MediatR;
using Repository.Common;

namespace Application.CQRS.User.Handlers.CommandHandlers;

public class UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateUserRequest> validator) : IRequestHandler<UpdateUserRequest, ResponseModel<UpdateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateUserRequest> _validator = validator;

    public async Task<ResponseModel<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var checkUser =await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id);
        if (checkUser == null)
            throw new BadRequestException("User not found with provided id");
        var checkUsername = await _unitOfWork.UserRepository.GetByUsernameAsync(request.Username);

        if(checkUsername != null && checkUsername.Id != request.Id)
            throw new BadRequestException("Username already exists");

        checkUser=await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
        if (checkUser != null && checkUser.Id != request.Id)
            throw new BadRequestException("Email already exists");

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.UpdatedDate = DateTime.Now;
        user.UpdatedBy = 1;
        user.PasswordHash=PasswordHasher.ComputeStringToSha256Hash(request.Password);

        await _unitOfWork.UserRepository.UpdateUserAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseModel<UpdateUserResponse>
        {
            Data = _mapper.Map<UpdateUserResponse>(user),
            Errors= [],
            IsSuccess = true
        };
    }
}
