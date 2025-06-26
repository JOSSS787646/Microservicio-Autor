using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibros.FirstOrDefaultAsync(a => a.AutorLibroGuid == request.AutorGuid);
                if (autor == null)
                    throw new Exception("El autor no existe");

                _contexto.AutorLibros.Remove(autor);

                var result = await _contexto.SaveChangesAsync();

                if (result > 0)
                    return Unit.Value;

                throw new Exception("No se pudo eliminar el autor");
            }
        }
    }
}
