using AutoMapper;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Models.ViewModels;

namespace MagicVilla_Web.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumberCreateVM>().ReverseMap();
            CreateMap<VillaNumberUpdateDTO, VillaNumberCreateVM>().ReverseMap();
        }
    }
}
