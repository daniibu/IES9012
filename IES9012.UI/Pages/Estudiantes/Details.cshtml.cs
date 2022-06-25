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
    public class DetailsModel : PageModel
    {
        private readonly IES9012Context _context;

        public DetailsModel(IES9012Context context)
        {
            _context = context;
        }

        public Estudiante? Estudiante { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Estudiante = await _context.Estudiantes
                        .Include(s => s.Inscripciones)
                        .ThenInclude(e => e.Materia)
                       //Los métodos Include y ThenInclude hacen que el contexto cargue la propiedad de
                        //navegación Estudiante.Inscripciones y, dentro de cada inscripción, la propiedad de
                        //navegación Inscripcion.Materia.
                        .AsNoTracking()
                        //El método AsNoTracking mejora el rendimiento en casos en los que las entidades
                        //devueltas no se actualizan en el contexto actual.
                        .FirstOrDefaultAsync(m => m.EstudianteId == id);
            if (Estudiante == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}