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
        private readonly IES9012.UI.Data.IES9012Context _context;

        public DetailsModel(IES9012.UI.Data.IES9012Context context)
        {
            _context = context;
        }

      public Estudiante Estudiantes { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes.FirstOrDefaultAsync(m => m.EstudianteId == id);
            if (estudiantes == null)
            {
                return NotFound();
            }
            else 
            {
                Estudiantes = estudiantes;
            }
            return Page();
        }
    }
}
