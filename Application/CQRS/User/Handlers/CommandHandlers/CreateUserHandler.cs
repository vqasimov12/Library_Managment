using Application.CQRS.User.Commands.Requests;
using Application.CQRS.User.Commands.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using Common.Security;
using FluentValidation;
using MediatR;
using Repository.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.User.Handlers.CommandHandlers;

public class CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateUserRequest> validator) : IRequestHandler<CreateUserRequest, ResponseModel<CreateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateUserRequest> _validator = validator;


    public async Task<ResponseModel<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {

        var userExist = await _unitOfWork.UserRepository.GetByUsernameAsync(request.Username);
        if (userExist != null)
            throw new BadRequestException("User already exist with provided username");

        userExist = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
        if (userExist != null)
            throw new BadRequestException("User already exist with provided email");

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            throw new BadRequestException("Validation is not valid");
        var user = _mapper.Map<Domain.Entities.User>(request);

        user.CreatedDate = DateTime.UtcNow;
        user.CreatedBy = 1;
        user.UpdatedDate = null;
        user.UpdatedBy = null;
        user.DeletedDate = null;
        user.DeletedBy = null;
        user.PasswordHash=PasswordHasher.ComputeStringToSha256Hash(request.Password);


        await _unitOfWork.UserRepository.AddUserAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return new ResponseModel<CreateUserResponse>
        {
            Data = _mapper.Map<CreateUserResponse>(user),
            Errors = [],
            IsSuccess = true
        };


    }
}
