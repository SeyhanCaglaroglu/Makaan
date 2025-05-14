using AutoMapper;
using Makaan.Catalog.Dtos.ContactDtos;
using Makaan.Catalog.Dtos.ContactIntroPosterDtos;
using Makaan.Catalog.Dtos.EstateAgentApplicationDtos;
using Makaan.Catalog.Dtos.FeaturedAboutDtos;
using Makaan.Catalog.Dtos.IntroSliderAreaDtos;
using Makaan.Catalog.Dtos.IntroTextAreaDtos;
using Makaan.Catalog.Dtos.PropertyAgentDtos;
using Makaan.Catalog.Dtos.PropertyDetailDtos;
using Makaan.Catalog.Dtos.PropertyDtos;
using Makaan.Catalog.Dtos.PropertyImageDtos;
using Makaan.Catalog.Dtos.PropertyIntroPosterDtos;
using Makaan.Catalog.Dtos.PropertyTypeDtos;
using Makaan.Catalog.Entites;

namespace Makaan.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //IntroTextArea
            CreateMap<IntroTextArea,ResultIntroTextAreaDto>().ReverseMap();
            CreateMap<IntroTextArea,CreateIntroTextAreaDto>().ReverseMap();
            CreateMap<IntroTextArea,UpdateIntroTextAreaDto>().ReverseMap();
            CreateMap<IntroTextArea,GetByIdIntroTextAreaDto>().ReverseMap();

            //IntroSliderArea
            CreateMap<IntroSliderArea,ResultIntroSliderAreaDto>().ReverseMap();
            CreateMap<IntroSliderArea,CreateIntroSliderAreaDto>().ReverseMap();
            CreateMap<IntroSliderArea,UpdateIntroSliderAreaDto>().ReverseMap();
            CreateMap<IntroSliderArea,GetByIdIntroSliderAreaDto>().ReverseMap();

            //PropertyType
            CreateMap<PropertyType,ResultPropertyTypeDto>().ReverseMap();
            CreateMap<PropertyType,CreatePropertyTypeDto>().ReverseMap();
            CreateMap<PropertyType,UpdatePropertyTypeDto>().ReverseMap();
            CreateMap<PropertyType,GetByIdPropertyTypeDto>().ReverseMap();

            //FeaturedAbout
            CreateMap<FeaturedAbout, ResultFeaturedAboutDto>().ReverseMap();
            CreateMap<FeaturedAbout, CreateFeaturedAboutDto>().ReverseMap();
            CreateMap<FeaturedAbout, UpdateFeaturedAboutDto>().ReverseMap();
            CreateMap<FeaturedAbout, GetByIdFeaturedAboutDto>().ReverseMap();

            //Property
            CreateMap<Property, ResultPropertyDto>().ReverseMap();
            CreateMap<Property, CreatePropertyDto>().ReverseMap();
            CreateMap<Property, UpdatePropertyDto>().ReverseMap();
            CreateMap<Property, GetByIdPropertyDto>().ReverseMap();

            //PropertyAgent
            CreateMap<PropertyAgent, ResultPropertyAgentDto>().ReverseMap();
            CreateMap<PropertyAgent, CreatePropertyAgentDto>().ReverseMap();
            CreateMap<PropertyAgent, UpdatePropertyAgentDto>().ReverseMap();
            CreateMap<PropertyAgent, GetByIdPropertyAgentDto>().ReverseMap();

            //Contact
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, GetByIdContactDto>().ReverseMap();

            //PropertyIntroPoster
            CreateMap<PropertyIntroPoster,ResultPropertyIntroPosterDto>().ReverseMap();
            CreateMap<PropertyIntroPoster,CreatePropertyIntroPosterDto>().ReverseMap();
            CreateMap<PropertyIntroPoster,UpdatePropertyIntroPosterDto>().ReverseMap();
            CreateMap<PropertyIntroPoster,GetByIdPropertyIntroPosterDto>().ReverseMap();

            //ContactIntroPoster
            CreateMap<ContactIntroPoster, ResultContactIntroPosterDto>().ReverseMap();
            CreateMap<ContactIntroPoster, CreateContactIntroPosterDto>().ReverseMap();
            CreateMap<ContactIntroPoster, UpdateContactIntroPosterDto>().ReverseMap();
            CreateMap<ContactIntroPoster, GetByIdContactIntroPosterDto>().ReverseMap();

            //PropertyDetail
            CreateMap<PropertyDetail, ResultPropertyDetailDto>().ReverseMap();
            CreateMap<PropertyDetail, CreatePropertyDetailDto>().ReverseMap();
            CreateMap<PropertyDetail, UpdatePropertyDetailDto>().ReverseMap();
            CreateMap<PropertyDetail, GetByIdPropertyDetailDto>().ReverseMap();

            //PropertyImage
            CreateMap<PropertyImage, ResultPropertyImageDto>().ReverseMap();
            CreateMap<PropertyImage, CreatePropertyImageDto>().ReverseMap();
            CreateMap<PropertyImage, UpdatePropertyImageDto>().ReverseMap();
            CreateMap<PropertyImage, GetByIdPropertyImageDto>().ReverseMap();

            //EstateAgentApplication
            CreateMap<EstateAgentApplication, ResultEstateAgentApplicationDto>().ReverseMap();
            CreateMap<EstateAgentApplication, CreateEstateAgentApplicationDto>().ReverseMap();
            CreateMap<EstateAgentApplication, GetByIdEstateAgentApplicationDto>().ReverseMap();
        }
    }
}
