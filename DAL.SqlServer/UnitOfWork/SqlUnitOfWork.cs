using DAL.SqlServer.Context;
using DAL.SqlServer.Infrustructure;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork( string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private readonly string _connectionString = connectionString;

    public SqlBookRepository _bookRepository;
    public SqlUserRepository _userRepository;
    public SqlImageRepository _imageRepository;

    public IBookRepository BookRepository => _bookRepository ??= new SqlBookRepository(_context);

    public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(connectionString);

    public IImageRepository ImageRepository => _imageRepository ?? new SqlImageRepository(context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
