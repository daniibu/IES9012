using IES9012.Core.Modelos;
using IES9012.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IES9012.UI.Pages.Estudiantes
{
    public class EditModel : PageModel
    {
        private readonly IES9012Context _context;

        public EditModel(IES9012Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Estudiante? Estudiante { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Estudiante = await _context.Estudiantes.FindAsync(id);
            if (Estudiante == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var estudianteToUpdate = await _context.Estudiantes.FindAsync(id);
            if (estudianteToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Estudiante>(
            estudianteToUpdate,
                "estudiante",
                s => s.Nombre, s => s.Apellido, s => s.FechaInscripcion))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }

    }
}