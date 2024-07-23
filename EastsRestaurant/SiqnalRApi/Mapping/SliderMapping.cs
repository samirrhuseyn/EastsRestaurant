using AutoMapper;
using DtoLayer.SlideDto;
using EntityLayer.Entities;

namespace WebApi.Mapping
{
    public class SliderMapping : Profile
    {
        public SliderMapping() 
        {
            CreateMap<Slider, ResultSlideDto>().ReverseMap();
            CreateMap<Slider, CreateSlideDto>().ReverseMap();
            CreateMap<Slider, GetSlideDto>().ReverseMap();
            CreateMap<Slider, UpdateSlideDto>().ReverseMap();
        }
    }
}
