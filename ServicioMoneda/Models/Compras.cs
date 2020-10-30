using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ServicioMoneda.Models
{
    public class Compras
    {
        [Key]
        public string ID_COMPRA { get; set; }
        public int ID_USER { get; set; }
        public decimal VALOR { get; set; }
        public string MONEDA { get; set; }
        public string FECHA { get; set; }
        public string DIA { get; set; }
        public string MES { get; set; }
        public string ANO { get; set; }


    }
}
