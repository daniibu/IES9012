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

        public IndexModel(IES9012.UI.Data.IES9012Context context)
        {
            _context = context;
        }

        public IList<Estudiante> Estudiantes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Estudiantes != null)
            {
                Estudiantes = await _context.Estudiantes.ToListAsync();
            }
        }
    }
}