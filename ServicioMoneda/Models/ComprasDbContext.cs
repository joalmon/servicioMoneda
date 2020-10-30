using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ServicioMoneda.Models
{
    class ComprasDbContext : DbContext
    {
            public ComprasDbContext()
            {
            }
            public ComprasDbContext(DbContextOptions options)
            : base(options)
            {
            }

           
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var configurationBuilder = new ConfigurationBuilder();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);

            if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(configurationBuilder.Build().GetSection("ConnectionStrings:bd").Value);
                }
            }

            //Tablas de datos
            public virtual DbSet<Compras> Compras { get; set; }
            public virtual DbSet<Log> Log { get; set; }
            public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
