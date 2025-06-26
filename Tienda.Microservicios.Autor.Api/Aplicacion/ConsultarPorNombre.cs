using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class ConsultarPorNombre
    {
        public class AutorPorNombre : IRequest<AutorDto?>
        {
            public string NombreCompleto { get; set; }
        }

        public class Manejador : IRequestHandler<AutorPorNombre, AutorDto?>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDto?> Handle(AutorPorNombre request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.NombreCompleto))
                    return null; // ❌ no procesar si está vacío

                var nombreBuscado = request.NombreCompleto.Trim().ToLower();

                var autor = await _contexto.AutorLibros
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p =>
                        (p.Nombre + " " + p.Apellido).Trim().ToLower() == nombreBuscado,
                        cancellationToken);

                // ✅ Simplemente retorna null si no encuentra
                return autor != null ? _mapper.Map<AutorDto>(autor) : null;
            }
        }
    }
}
