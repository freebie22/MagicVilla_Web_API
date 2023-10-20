using AutoMapper;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;

namespace Magic_Villa_VillaApi.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
