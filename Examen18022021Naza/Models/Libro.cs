using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Models
{
    [Table("LIBROS")]
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }
        public String Titulo { get; set; }
        public String Autor { get; set; }
        public String Editorial { get; set; }
        public String Portada { get; set; }
        public String Resumen { get; set; }
        public int Precio { get; set; }
        public int IdGenero { get; set; }
    }
}
