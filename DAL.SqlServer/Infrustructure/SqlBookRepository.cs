using DAL.SqlServer.Context;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrustructure;

public class SqlBookRepository(AppDbContext context) : BaseSqlRepository, IBookRepository
{
    public async Task AddBookAsync(Book book) =>  context.Books.Add(book);

    public async Task<bool> DeleteBookAsync(int id, int deletedBy)
    {
        var book = await context.Books.FirstOrDefaultAsync(z => z.Id == id && !z.IsDeleted)!;
        if (book is null) return false;
        book.IsDeleted = true;
        book.DeletedBy = deletedBy;
        book.DeletedDate = DateTime.Now;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IQueryable<Book>> GetAll() =>  
        context.Books.Where(z => !z.IsDeleted).OrderByDescending(z => z.CreatedDate);

    public async Task<Book> GetBookByIdAsync(int id) =>
        await context.Books.FirstOrDefaultAsync(z => z.Id == id && !z.IsDeleted)!;

    public async Task<Book> GetBookByNameAsync(string name) =>
        await context.Books.FirstOrDefaultAsync(z => z.Name == name && !z.IsDeleted)!;

    public async Task<IEnumerable<Book>> GetByAuthorAsync(string author) =>
        await context.Books.Where(z => z.Author == author && !z.IsDeleted).ToListAsync();

    public async Task<IEnumerable<Book>> GetByLanguagesync(Language language) =>
        await context.Books.Where(z => z.Language == language && !z.IsDeleted).ToListAsync();

    public async Task<IEnumerable<Book>> GetByUserIdAsync(int id) =>
        await context.Books.Where(z => z.UserId == id && !z.IsDeleted).ToListAsync();

    public async Task UpdateBookAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }
}
