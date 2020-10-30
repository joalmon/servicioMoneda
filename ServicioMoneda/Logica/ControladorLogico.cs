using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ServicioMoneda.Models;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace ServicioMoneda.Logica
{
    public class ControladorLogico
    {

        public async Task<decimal> ValorMoneda(string urlfuente)
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetStringAsync(urlfuente);
                var parser = JsonConvert.DeserializeObject<List<string>>(response);
                return Convert.ToDecimal(parser[0].Replace('.', ','));
            }
        }
        public string URLServicio(string moneda)
        {
            string url = "";

            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            switch (moneda)
            {
                case "dolar":
                    url = configurationBuilder.Build().GetSection("ServiciosMonedas:dolar").Value;
                    break;

                case "real":
                    url = configurationBuilder.Build().GetSection("ServiciosMonedas:real").Value;
                    break;
            }

            return url;
        }
        public decimal TransformarValorMoneda(decimal valor, string moneda)
        {
            decimal resultado=Convert.ToDecimal("0.0");

            switch(moneda)
            {
                case "dolar":
                    resultado = valor;
                break;

                case "real":
                    resultado = valor / 4;
                break;

            }

            return resultado;
        }

        public decimal RetornarLimite(string moneda)
        {
            decimal limite = Convert.ToDecimal("0.0");

            switch (moneda)
            {
                case "dolar":
                    limite = 200;
                break;

                case "real":
                    limite = 300;
                break;
            }

            return limite;
        }
        public void RegistrarMensajeLog(string mensaje)
        {
                using (ComprasDbContext contextoLog = new ComprasDbContext())
                {
                    var log = new Log();
                    log.ID_LOG = Guid.NewGuid().ToString();
                    log.Mensaje = mensaje;
                    log.Fecha = DateTime.Now.ToString();

                    contextoLog.Add(log);
                    contextoLog.SaveChanges();
                }
            
        }
        public async Task<string> RegistrarCompra(string id, decimal monto, string moneda)
        {
            if (!(moneda == "dolar" || moneda == "real"))
            {
                return "No está permitido el uso de la moneda: "+ moneda;
            }

            string url = URLServicio(moneda);
            decimal valorMoneda = await ValorMoneda(url);
            decimal factor = TransformarValorMoneda(valorMoneda, moneda);
            decimal montoCalculado = monto / factor;
            decimal limite = RetornarLimite(moneda); 

            using (ComprasDbContext context = new ComprasDbContext())
            {
                // Se valida cuánto ha comprado el cliente en el mes para saber si no se rebasa el límite
                // Dolar: 200 , Real: 300
                string mesActual = DateTime.Now.Date.Month.ToString();
                string anoActual = DateTime.Now.Date.Year.ToString();

                decimal montoAcumulado = context.Compras.Where(c => c.ID_USER == Convert.ToInt32(id) && c.MONEDA == moneda && c.MES == mesActual && c.ANO == anoActual).Sum(c => c.VALOR);

                if (!( montoAcumulado + montoCalculado < limite))
                {
                    return "Se excede el tope mensual permitido";
                }
                
                // Se hace el registro de compra
                var compra = new Compras();
                compra.ID_COMPRA = Guid.NewGuid().ToString();
                compra.ID_USER = Convert.ToInt32(id);
                compra.VALOR = Convert.ToDecimal(montoCalculado);
                compra.MONEDA = moneda;
                compra.FECHA = DateTime.Now.Date.ToShortDateString();
                compra.DIA = DateTime.Now.Date.Day.ToString();
                compra.MES = DateTime.Now.Date.Month.ToString();
                compra.ANO = DateTime.Now.Date.Year.ToString();

                context.Add(compra);
                context.SaveChanges();
                return "Se ha realizado la compra";
            }
        }

    }
}
