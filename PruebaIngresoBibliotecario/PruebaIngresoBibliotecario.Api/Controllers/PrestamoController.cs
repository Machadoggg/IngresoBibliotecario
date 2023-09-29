using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Api.Models;
using PruebaIngresoBibliotecario.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly PersistenceContext _context;
        public PrestamoController(PersistenceContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult> PostPrestamo(PrestamoLibro prestamo)
        {
            var weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var fechaDevolucion = DateTime.Now;
            int diasPrestamo = 0;
            if (prestamo.TipoUsuario == 1)
                diasPrestamo = 10;
            if (prestamo.TipoUsuario == 2)
                diasPrestamo = 8;
            if (prestamo.TipoUsuario == 3)
                diasPrestamo = 7;
            for (int i = 0; i < diasPrestamo;)
            {
                fechaDevolucion = fechaDevolucion.AddDays(1);
                i = (!weekend.Contains(fechaDevolucion.DayOfWeek)) ? ++i : i;
            }
            prestamo.Id = Guid.NewGuid();
            _context.PrestamoLibros.Add(prestamo);
            await _context.SaveChangesAsync();
            return Ok(new {Id = prestamo.Id, FechaMaximaDevolucion = fechaDevolucion.ToShortDateString() });

        }


        [HttpGet]
        public async Task<ActionResult> GetListPrestamoLibros()
        {
            var prestamoLibros = await _context.PrestamoLibros.ToListAsync();
            return Ok(prestamoLibros);
        }


        [HttpGet("{idPrestamo}")]
        public async Task<ActionResult> GetPrestamoLibroById(Guid idPrestamo)
        {
            var prestamo = await _context.PrestamoLibros.FirstOrDefaultAsync(x => x.Id.ToString() == idPrestamo.ToString());
            if (prestamo == null)
            {
                return NotFound(new { menssaje = "El prestamo con id " + idPrestamo + " no existe" });
            }
            var weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var fechaDevolucion = DateTime.Now;
            int diasPrestamo = 0;
            if (prestamo.TipoUsuario == 1)
                diasPrestamo = 10;
            if (prestamo.TipoUsuario == 2)
                diasPrestamo = 8;
            if (prestamo.TipoUsuario == 3)
                diasPrestamo = 7;
            for (int i = 0; i < diasPrestamo;)
            {
                fechaDevolucion = fechaDevolucion.AddDays(1);
                i = (!weekend.Contains(fechaDevolucion.DayOfWeek)) ? ++i : i;
            }
            return Ok(new { prestamo.Id, prestamo.Isbn, prestamo.IdentificacionUsuario, FechaMaximaDevolucion = fechaDevolucion.ToShortDateString()});
        }

    }
}
