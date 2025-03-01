using Application.CQRS.User.Commands.Requests;
using Application.CQRS.User.Commands.Responses;
using Application.CQRS.User.Queries.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserByIdResponse>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, GetAllUsersResponse>().ReverseMap();
        CreateMap<User, UpdateUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserRequest>().ReverseMap();
    }


}
