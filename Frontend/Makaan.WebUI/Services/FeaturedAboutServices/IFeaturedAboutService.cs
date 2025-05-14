using Makaan.DtoLayer.CatalogDtos.FeaturedAboutDtos;

namespace Makaan.WebUI.Services.FeaturedAboutServices
{
    public interface IFeaturedAboutService
    {
        Task<List<ResultFeaturedAboutDto>> GetAllResultFeaturedAboutAsync();
        Task CreateFeaturedAboutAsync(CreateFeaturedAboutDto createFeaturedAboutDto);
        Task UpdateFeaturedAboutAsync(GetByIdFeaturedAboutDto getByIdFeaturedAboutDto);
        Task DeleteFeaturedAboutAsync(string id);
        Task<GetByIdFeaturedAboutDto> GetByIdFeaturedAboutAsync(string id);
    }
}
