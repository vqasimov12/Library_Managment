using Domain.Entities;
using Domain.Enums;

namespace Repository.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id, int deletedBy);
    Task<IQueryable<User>> GetAll();
    Task<IEnumerable<User>> GetByNameAsync(string name);
    Task<User> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetByFathernameAsync(string fathername);
    Task<IEnumerable<User>> GetByGenderAsync(Gender gender);
}
