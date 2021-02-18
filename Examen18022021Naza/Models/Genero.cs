using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Models
{
    [Table("GENEROS")]
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }
        public String Nombre { get; set; }
    }
}
