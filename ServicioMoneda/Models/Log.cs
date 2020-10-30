using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ServicioMoneda.Models
{
    public class Log
    {
        [Key]
        public string ID_LOG { get; set; }
        public string Mensaje { get; set; }
        public string Fecha { get; set; }

    }
}
