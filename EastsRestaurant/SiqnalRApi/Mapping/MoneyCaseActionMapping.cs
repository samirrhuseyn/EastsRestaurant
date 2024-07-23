using AutoMapper;
using DtoLayer.MoneyCaseActionDto;
using EntityLayer.Entities;

namespace WebApi.Mapping
{
    public class MoneyCaseActionMapping : Profile
    {
        public MoneyCaseActionMapping() 
        {
            CreateMap<MoneyCaseAction, ResultMoneyCaseActionDto>().ReverseMap();
        }
    }
}
