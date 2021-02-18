using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Email { get; set; }
        public String Pass { get; set; }
        public String Foto { get; set; }
    }
}
