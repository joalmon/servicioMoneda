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
    public class ComprasController : ControllerBase
    {
        // GET api/compras/1/2000/real
        [HttpGet("{id}/{monto}/{moneda}")]
        public async Task<string> Get(string id, string monto, string moneda)
        {
            ControladorLogico controladorLogico = new ControladorLogico();
            try
            {
                return await controladorLogico.RegistrarCompra(id, Convert.ToDecimal(monto), moneda);
            }
            catch(Exception e)
            {
                controladorLogico.RegistrarMensajeLog(e.Message);
                return "Ha ocurrido un error inesperado";
            }

        }

    }
}
