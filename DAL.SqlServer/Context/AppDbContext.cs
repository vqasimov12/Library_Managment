using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User>Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Image> Images { get; set; }

}
