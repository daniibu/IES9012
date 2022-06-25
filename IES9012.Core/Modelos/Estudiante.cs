using System.ComponentModel.DataAnnotations;

namespace IES9012.Core.Modelos
{
    public class Estudiante
        //la propiedad ID se conveirte en la columna de clave principal de la tabla de base de datos
    {
        //vamos a utilizar un sistema que desde los MODELOS va a ejecutar una base de datos
        //para que el campo sea identificado, tiene que tener el nombre de la identidad "estudiantes"y  luego el id
        [Key]
        public int EstudianteId { get; set; }
        [Required]//hace que el campo no sea nulo, no acepta valores nulos
        [StringLength(50)]
        public string? Nombre{ get; set; }//puede ser del tipo NULL
        //public string Nombre{get;set;} =null!; tambien esta forma es valida
        [Required]
        [StringLength(35)]
        public string? Apellido { get; set; }
        
        [DataType(DataType.Date)]//solo fecha para que no ponga la hora
        //cuando abra el navegador ademas de mostrar el fecha, me muestra el calendario

        
        //
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name ="Fecha de Inscripción")]//decorador: cambia la propiedad con la que voy a visualizar el campo
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;//para la fecha actual, la fecha del sistema

        //agregamos la propiedad inscripciones, que se va a relacionar con la tabla inscripciones
        //va a ser una tabla puente entre materia y alumno,//esto es una clave foranea
        //La propiedad inscripcion es una propiedad de navegacion
        //Las propiedaddes de navegacion contienen otras entidades relacionadas con esta entidad
        //la propiedad inscipciones se define con ICollection<Inscriocion>
        //porque puede haber varias entidades Inscripcion relacionadas con Estudiante.
        public ICollection<Inscripcion>? Inscripciones { get; set; }//icollection es un tipo de dato que me arma una coleccion de objetos
        //I es una interfaz
        //una conexion es tu tipo de dato que va a tener en su interrior otro tipo de dato
        //voy a tener una lista de un par de datos IDAlumnos,IDMateria y así sigue
        //lo pongo null por las dudas que hay un alumno que no este cursando ninguna materia
        

    }
}
