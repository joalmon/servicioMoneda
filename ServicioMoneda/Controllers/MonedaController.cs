using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicioMoneda.Logica;
using ServicioMoneda.Models;

namespace ServicioMoneda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        // GET api/moneda/dolar
        [HttpGet("{moneda}")]
        public async Task<string> Get(string moneda)
        {
            ControladorLogico controladorLogico = new ControladorLogico();
            try
            {
                string url = controladorLogico.URLServicio(moneda);
                decimal valor = await controladorLogico.ValorMoneda(url);
                decimal resultado = controladorLogico.TransformarValorMoneda(valor,moneda);
                return "El valor el día de hoy es de: "+resultado;
            }
            catch (Exception e)
            {
                controladorLogico.RegistrarMensajeLog(e.Message);
                return "Ha ocurrido un error inesperado";
            }
        }

    }
}
