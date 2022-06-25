using IES9012.Core.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace IES9012.UI.Pages.Estudiantes
{
    public class DeleteModel : PageModel
    {
        private readonly IES9012.UI.Data.IES9012Context _context;
        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(IES9012.UI.Data.IES9012Context context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        [BindProperty]
        public Estudiante Estudiante { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)

        {
            if (id == null)
            {
                return NotFound();
            }
            Estudiante = await _context.Estudiantes
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.EstudianteId == id);
            if (Estudiante == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            try
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}