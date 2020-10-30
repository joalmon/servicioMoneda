using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ServicioMoneda.Models
{
    public class Usuarios
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }

    }
}
