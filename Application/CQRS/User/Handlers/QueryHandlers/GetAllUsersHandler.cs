using Application.CQRS.User.Queries.Requests;
using Application.CQRS.User.Queries.Responses;
using AutoMapper;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.User.Handlers.QueryHandlers;

public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUsersRequest, ResponseModelPagination<GetAllUsersResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModelPagination<GetAllUsersResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAll();
        if (!users.Any())
            return new ResponseModelPagination<GetAllUsersResponse>() { Data = null, Errors = [], IsSuccess = true };
        var totalCount = users.Count();
        users = users.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
        var mappedCategories = new List<GetAllUsersResponse>();
        foreach (var user in users)
            mappedCategories.Add(_mapper.Map<GetAllUsersResponse>(user));
        
        var response = new Pagination<GetAllUsersResponse>
        {
            Data = mappedCategories,
            TotalCount = totalCount
        };

        return new ResponseModelPagination<GetAllUsersResponse>
        {
            Data = response,
            Errors = [],
        };


    }
}
