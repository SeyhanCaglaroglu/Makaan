using Makaan.DtoLayer.CatalogDtos.IntroSliderAreaDtos;
using Makaan.DtoLayer.CatalogDtos.IntroTextAreaDtos;

namespace Makaan.WebUI.Models
{
    public class HeaderViewModel
    {
        public List<ResultIntroTextAreaDto> resultIntroTextAreaDto { get; set; }
        public List<ResultIntroSliderAreaDto> resultSliderTextAreaDto { get; set; }
    }
}
