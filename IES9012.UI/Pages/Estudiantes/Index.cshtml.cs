using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IES9012.Core.Modelos;
using IES9012.UI.Data;

namespace IES9012.UI.Pages.Estudiantes
{
    public class IndexModel : PageModel
    {
        private readonly IES9012.UI.Data.IES9012Context _context;
        private readonly IConfiguration Configuration;

        public IndexModel(IES9012.UI.Data.IES9012Context context,IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        //vamos a crear 4 propiedades para establcer nombres o fechas, si quiero ordenar por
        //nombre o fechalo establzco 
        //el orden puede ser ascendente o desendiente
        //tengo que preveer a futuro
        //la paginacion: es mostrar una cantidad de datos y un boton siguiente y me muestra los datos siguentes
        //por ejemplo buscamos en google y no sale todo, pero al fondo de la pagina va a ver una segunda, tercera
        //cuarta y así de partes con mucha informacion
        //para la paginacion debemos de tener guardado los datos
        //orden actual me ordena el metodo
        //cada vez que se actualiza una pagina se destruye la anterior
        //tiene una vida util, es el tiempo que se está visualizando y esos datos se pierden
        //para no perder datos, pasamos de una pagina a otra el ordenamiento
        //y si tenemos un filtro, tambien pasamos ese filtro

        //----------------------------------------------
        public string? OrdenNombre { get; set; }
        public string? OrdenFecha { get; set; }
        public string? OrdenActual { get; set; }
        public string? FiltroActual { get; set; }
        //van a permitir valores nulos
        //----------------------------------------------
        
        public ListaPaginada<Estudiante> Estudiantes { get; set; }
        //luego en OnGetAsync(me muestra la pagina cuando se carga)
        //vamos a estab lecer el valor al ordenNombre
        //para esto agregamos al OnGetAsync un parametro de tipo string que me va a traer de la pagina
        //a mi el orden que voy a establecer al formulario
        //por ello le aviso a la pagina el orden que quiero
        //lo que esta dentro del parametro clasificarOrden
        // el ? operador ternario es como un if
        //si la cadena que viene en esta variable no es nula o vacia
        //entonces asignale este valor
        //si es nula o vacia asignale este valor
        
        public async Task OnGetAsync(string clasificarOrden, string cadenaFiltro,string filtroActual,int? indicePagina)
        {
            OrdenNombre = String.IsNullOrEmpty(clasificarOrden) ? "Apellido_desc" : "";
            //Se le asigna a OrdenFecha un valor dependiendo del valor de clasificaOrden.
            //Si clasificaOrden contiene FechaInscripcion, a OrdenFecha le asigna FechaInscripcion_desc
            //si clasificaOrden no contiene FechaInscripcion, a OrdenFecha le asigna FechaInscripcion
            OrdenFecha = clasificarOrden == "FechaInscripcion" ? "FechaInscripcion_desc" : "FechaInscripcion";
            OrdenActual = clasificarOrden;
            //esto sería lo mismo que...
            //If (clasificaOrden=="FechaInscipcion")
            //  OrdenFecha="FechaInscripcion_desc"
            //else
            //  OrdenFecha="FechaInscripcion"

            //------------------------------------------------------------------------------
            //IQueryable consulta almacenes de datos sin usar la memoria del cliente, mientras que
            //IEnumerable consulta datos en memoria.
            //Para el enfoque IQueryable (from s in_context.Estudiantes select s;), elSQL traducido es:
            //SELECT * FROM Estudiantes AS s;
            //IQueryable<Estudiante> estudianteIQ = from s in _context.Estudiantes select s;
            if (_context.Estudiantes != null)
            {
                IQueryable<Estudiante> estudianteIQ = from s in _context.Estudiantes select s;
                if (cadenaFiltro != null)
                {
                    indicePagina = 1;
                }
                else
                {
                    cadenaFiltro = filtroActual;
                }
                FiltroActual = cadenaFiltro;
                if (!string.IsNullOrEmpty(cadenaFiltro))
                {
                    estudianteIQ = estudianteIQ.Where(s =>
                    s.Apellido.Contains(cadenaFiltro) || s.Nombre.Contains(cadenaFiltro));
                }
                if (String.IsNullOrEmpty(clasificarOrden))
                {
                    clasificarOrden = "Apellido";
                }
                bool descending = false;
                if (clasificarOrden.EndsWith("_desc"))
                {
                    clasificarOrden = clasificarOrden.Substring(0,
                    clasificarOrden.Length - 5);
                    descending = true;
                }
                if (descending)
                {
                    estudianteIQ = estudianteIQ.OrderByDescending(e =>
                    EF.Property<object>(e, clasificarOrden));
                }
                else
                {
                    estudianteIQ = estudianteIQ.OrderBy(e =>
                    EF.Property<object>(e, clasificarOrden));
                }
                var pageSize = Configuration.GetValue("PageSize", 4);
                Estudiantes = await
                ListaPaginada<Estudiante>.CreateAsync(estudianteIQ.AsNoTracking(),
                indicePagina ?? 1, pageSize);
            }
        }
    }
}
            
