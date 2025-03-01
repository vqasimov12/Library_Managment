using Application.CQRS.User.Queries.Requests;
using Application.CQRS.User.Queries.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.User.Handlers.QueryHandlers;

public class GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdRequest, ResponseModel<GetUserByIdResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<GetUserByIdResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id);
        if (user is null)
            throw new BadRequestException("User with provided id can not be found");
        return new ResponseModel<GetUserByIdResponse>
        {
            Data =_mapper.Map<GetUserByIdResponse>(user),
            Errors = [],
            IsSuccess = true
        };
    }
}
