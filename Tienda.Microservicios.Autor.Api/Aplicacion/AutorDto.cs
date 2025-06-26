using Tienda.Microservicios.Autor.Api.Modelo;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class AutorDto
    {

        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public ICollection<GradoAcademico> GradosAcademicos { get; set; }

        //Llave que conecta con el microservicio de libros67
        public string AutorLibroGuid { get; set; }
    }
}
