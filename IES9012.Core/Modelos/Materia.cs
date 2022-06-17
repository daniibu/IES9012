using System.ComponentModel.DataAnnotations;
namespace IES9012.Core.Modelos
{
    public class Materia
    {
        public int MateriaId { get; set; }
        [Required]
        [StringLength(30)]
        public string? Nombre { get; set; }

        public int Creditos { get; set; }
        //los creditos son las notas que le faltan al alumno para saber si el alumno apruba o no

        //La propiedad Inscripcion es una propiedad de navegacion
        //
        public ICollection<Inscripcion>? Inscripciones{ get; set; }

    }
}