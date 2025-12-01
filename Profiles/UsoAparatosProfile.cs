using AutoMapper;
using CalculadoraKW.Api.Models;
using CalculadoraKW.Api.DTOs;

namespace CalculadoraKW.Api.Profiles;

public class UsoAparatosProfile : Profile
{
    public UsoAparatosProfile()
    {
        CreateMap<UsoAparatos, UsoAparatoDto>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.IdAparatoNavigation.Tipo))
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.IdAparatoNavigation.Marca))
            .ForMember(dest => dest.ConsumoKWh, opt => opt.MapFrom(src => src.IdAparatoNavigation.ConsumoKWh));

        CreateMap<CrearUsoAparatoDto, UsoAparatos>();
        CreateMap<ActualizarUsoAparatoDto, UsoAparatos>();
    }
}
