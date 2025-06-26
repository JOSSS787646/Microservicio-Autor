using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda.Microservicios.Autor.Api.Aplicacion;
using Tienda.Microservicios.Autor.Api.Modelo;

namespace Tienda.Microservicios.Autor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroAutorController : Controller
    {
        private readonly IMediator _mediator;

        public LibroAutorController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetAutor()
        {
            var autores = await _mediator.Send(new Consulta.ListaAutor());
            return Ok(new
            {
                Libros = autores
            });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AutorDto>> GetAutorUnico(string id)
        {
            return await _mediator.Send(new ConsultarFiltro.AutorUnico
            {
                AutorGuid = id
            });
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> EliminarAutor(string id)
        {
            await _mediator.Send(new Eliminar.Ejecuta { AutorGuid = id });
            return NoContent(); // 204
        }

        /*
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AutorDto>> EditarAutor(string id, [FromBody] Editar.Ejecuta data)
        {
            if (id != data.AutorGuid)
            {
                return BadRequest("El ID del autor no coincide");
            }

            var autorEditado = await _mediator.Send(data);
            return Ok(autorEditado);
        }
        */

        // 🔍 NUEVO: Buscar autor por nombre completo (GET con query param)
        // Ejemplo: GET /api/LibroAutor/nombre?nombreCompleto=Jose%20Martinez
        [HttpGet("nombre")]
        [AllowAnonymous]
        public async Task<ActionResult<AutorDto>> ObtenerAutorPorNombre([FromQuery] string nombreCompleto)
        {
            try
            {
                var autor = await _mediator.Send(new ConsultarPorNombre.AutorPorNombre
                {
                    NombreCompleto = nombreCompleto
                });

                return Ok(autor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}
