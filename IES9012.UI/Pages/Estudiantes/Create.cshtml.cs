using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IES9012.Core.Modelos;
using IES9012.UI.Data;

namespace IES9012.UI.Pages.Estudiantes
{
    public class CreateModel : PageModel
    {
        private readonly IES9012.UI.Data.IES9012Context _context;

        public CreateModel(IES9012.UI.Data.IES9012Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Estudiante? Estudiante { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyStudient = new Estudiante();
            if (await TryUpdateModelAsync<Estudiante>(
            emptyStudient,
            "estudiante", //Prefijo para el valor del formulario.
            s => s.Nombre, s => s.Apellido, s => s.FechaInscripcion))
            {
                _context.Estudiantes.Add(emptyStudient);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}