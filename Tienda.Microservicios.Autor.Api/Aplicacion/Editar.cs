using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Modelo;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class Editar
    {
        public class Ejecuta : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime FechaNacimiento { get; set; }
            // agrega otros campos que quieras editar
        }

        public class Manejador : IRequestHandler<Ejecuta, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibros.FirstOrDefaultAsync(a => a.AutorLibroGuid == request.AutorGuid);
                if (autor == null)
                    throw new Exception("El autor no existe");

                autor.Nombre = request.Nombre;
                autor.Apellido = request.Apellido;
                autor.FechaNacimiento = request.FechaNacimiento;
                // actualiza otros campos si es necesario

                var resultado = await _contexto.SaveChangesAsync();

                if (resultado > 0)
                {
                    var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                    return autorDto;
                }

                throw new Exception("No se pudo actualizar el autor");
            }
        }
    }
}
