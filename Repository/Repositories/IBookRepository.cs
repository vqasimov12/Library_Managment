using Domain.Entities;
using Domain.Enums;

namespace Repository.Repositories;

public interface IBookRepository
{
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> GetBookByNameAsync(string name);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id, int deletedBy);
    Task<IQueryable<Book>> GetAll();
    Task<IEnumerable<Book>> GetByAuthorAsync(string author);
    Task<IEnumerable<Book>> GetByLanguagesync(Language language);
    Task<IEnumerable<Book>> GetByUserIdAsync(int id);
}
