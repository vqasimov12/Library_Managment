using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrustructure;

public class SqlImageRepository(AppDbContext context) : BaseSqlRepository, IImageRepository
{
    public async Task AddImageAsync(Image image)
    {
        context.Images.Add(image);
        await context.SaveChangesAsync();
    }

    public async Task DeleteImageAsync(string id)
    {
        var image = await context.Images.FirstOrDefaultAsync(z => z.PhotoId == id);
        context.Images.Remove(image);
    }

    public async Task<Image> GetImageByIdAsync(string id) =>
        await context.Images.FirstOrDefaultAsync(z => z.PhotoId == id)!;
}
