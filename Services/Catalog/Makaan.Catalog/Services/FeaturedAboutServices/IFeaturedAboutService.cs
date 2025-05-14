using Makaan.Catalog.Dtos.FeaturedAboutDtos;

namespace Makaan.Catalog.Services.FeaturedAboutServices
{
    public interface IFeaturedAboutService
    {
        Task<List<ResultFeaturedAboutDto>> GetAllResultFeaturedAboutAsync();
        Task CreateFeaturedAboutAsync(CreateFeaturedAboutDto createFeaturedAboutDto);
        Task UpdateFeaturedAboutAsync(UpdateFeaturedAboutDto updateFeaturedAboutDto);
        Task DeleteFeaturedAboutAsync(string id);
        Task<GetByIdFeaturedAboutDto> GetByIdFeaturedAboutAsync(string id);
    }
}
