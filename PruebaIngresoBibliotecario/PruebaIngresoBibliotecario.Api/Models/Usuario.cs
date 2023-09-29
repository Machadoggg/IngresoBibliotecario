using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string IdentificacionUsuario { get; set; }

        public int TipoUsuario { get; set; }

        //public virtual ICollection<PrestamoLibro> PrestamoLibros { get; set; }
        //public virtual PrestamoLibro PrestamoLibros { get; set; }
    }
}
