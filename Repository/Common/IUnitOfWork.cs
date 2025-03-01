using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    IUserRepository UserRepository { get; }
    IImageRepository ImageRepository { get; }
    Task<int> SaveChangesAsync();
}