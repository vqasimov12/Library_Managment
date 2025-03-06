using Domain.Entities;

namespace Repository.Repositories;


public interface IImageRepository
{
    Task AddImageAsync(Image image);
    Task DeleteImageAsync(string id);
    Task<Image> GetImageByIdAsync(string id);
    Task<IQueryable<Image>> GetAllImagesAsync();
}