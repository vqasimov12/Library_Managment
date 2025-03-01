using Application.CQRS.User.Commands.Responses;
using Common.GlobalResponses.Generics;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.User.Commands.Requests;

public class UpdateUserRequest : IRequest<ResponseModel<UpdateUserResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Fathername { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; }
    public string Address { get; set; }
    public string MobilePhone { get; set; }
    public string CardNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime DateOfDismissal { get; set; }
    public string Note { get; set; }
    public int CreatedBy { get; set; }
    public Gender Gender { get; set; }
    public UserType UserType { get; set; }
}
