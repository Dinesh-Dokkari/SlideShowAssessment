using AutoMapper;

namespace SlidesShow.Models
{
    public class mapConfig:Profile
    {
        public mapConfig()
        {
            CreateMap<CompanySlide, createslide>().ReverseMap();
            CreateMap<CompanySlide, Preview>().ReverseMap();
            CreateMap<CompanySlide, EditSlide>().ReverseMap();
        }
    }
}
