using Application.CQRS.Book.Commands.Requests;
using Application.CQRS.Book.Commands.Responses;
using Application.CQRS.Book.Queries.Responses;
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


        CreateMap<Book, GetAllBooksResponse>().ReverseMap();
        CreateMap<Book, CreateBookRequest>().ReverseMap();
        CreateMap<Book, CreateBookResponse>().ReverseMap();
        CreateMap<Book, UpdateBookRequest>().ReverseMap();
        CreateMap<Book, UpdateBookResponse>().ReverseMap();
        CreateMap<Book, GetBookByIdResponse>().ReverseMap();
    }
}