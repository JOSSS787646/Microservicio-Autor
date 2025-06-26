using AutoMapper;
using Tienda.Microservicios.Autor.Api.Modelo;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();  
        }
    }
}

