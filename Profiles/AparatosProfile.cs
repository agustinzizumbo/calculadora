using AutoMapper;
using CalculadoraKW.Api.Models;
using CalculadoraKW.Api.DTOs;

namespace CalculadoraKW.Api.Profiles;

public class AparatosProfile : Profile
{
    public AparatosProfile()
    {
        CreateMap<Aparatos, AparatoDto>();
        CreateMap<CrearAparatoDto, Aparatos>();
        CreateMap<ActualizarAparatoDto, Aparatos>();
    }
}
