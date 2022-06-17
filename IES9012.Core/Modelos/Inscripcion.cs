using IES9012.Core.Enumeraciones;
using System.ComponentModel.DataAnnotations;
namespace IES9012.Core.Modelos
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }
        //la propiedad MateriaID es una clave externa y la propiedad de navegacion
        //correspondiente es Materia
        //una entidad inscriocion esta asociadad con una entidad Materia
        public int MateriaId { get; set; }
        //lA propiedad EstudianteId es una clave externa y la propiedad de navegacion
        //correspondiente es Estudiante.Una entidad Inscripcion esta asociada con una entidad Estudiante,
        //por lo que la propiedad contiene una unica entidad Estudiante
        public int EstudianteId { get; set; }

        [DisplayFormat(NullDisplayText ="Sin Calificación")]//<-- ese es el valor por defecto
        //con esto le digo que la siguiente propiedad no llega a tener ningun texto escrito, por defecto
        //no tiene nota, le pone valor por defecto
        //es una tabla puente
        public Calificacion? Calificacion{ get; set; }
        public Materia? Materia { get; set; }
        public Estudiante? Estudiante { get; set; }
    }
}