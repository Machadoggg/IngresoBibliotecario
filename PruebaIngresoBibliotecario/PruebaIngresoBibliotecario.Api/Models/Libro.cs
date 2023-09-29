using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class Libro
    {
        [Key]
        public Guid Isbn2 { get; set; }
        public string NombreLibro { get; set; }

        //public virtual PrestamoLibro PrestamoLibros { get; set; }
    }
}
