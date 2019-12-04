using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Primitives;

namespace Labs.Excel.Loader.Database
{
    public class DbCatalogosContext : DbContext, IDesignTimeDbContextFactory<DbCatalogosContext>

    {
        private readonly string _connectionString =
            @"Data Source=.\STAMPING;Initial Catalog=DBCATALOGOSv4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbCatalogosContext()
        {

        }

        public DbCatalogosContext(DbContextOptions<DbCatalogosContext> options) :
            base(options)
        {
        }

        public DbCatalogosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbCatalogosContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            return new DbCatalogosContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Extract logic to specific classes to map every column with max length

            modelBuilder.Entity<c_Aduana>().ToTable("c_Aduana").HasKey(p => p.Clave);
            modelBuilder.Entity<c_ClaveProdServ>().ToTable("c_ClaveProdServ").HasKey(p => p.Clave);
            modelBuilder.Entity<c_ClaveUnidad>().ToTable("c_ClaveUnidad").HasKey(p => p.Clave);
            modelBuilder.Entity<c_CodigoPostal>().ToTable("c_CodigoPostal").HasKey(p => p.Clave);
            modelBuilder.Entity<c_FormaPago>()
                .ToTable("c_FormaPago").HasKey(p => p.Clave);
            modelBuilder.Entity<c_Impuesto>().ToTable("c_Impuesto").HasKey(p => p.Clave);
            modelBuilder.Entity<c_MetodoPago>().ToTable("c_MetodoPago").HasKey(p => p.Clave);
            modelBuilder.Entity<c_Moneda>().ToTable("c_Moneda").HasKey(p => p.Clave);
            modelBuilder.Entity<c_NumPedimentoAduana>()
                .ToTable("c_NumPedimentoAduana").Property(t => t.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<c_Pais>().ToTable("c_Pais").HasKey(p => p.Clave);
            modelBuilder.Entity<c_PatenteAduanal>().ToTable("c_PatenteAduanal").HasKey(p => p.Clave);
            modelBuilder.Entity<c_RegimenFiscal>().ToTable("c_RegimenFiscal").HasKey(p => p.Clave);
            modelBuilder.Entity<c_TasaOCuota>()
                .ToTable("c_TasaOCuota").Property(t => t.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<c_TipoDeComprobante>().ToTable("c_TipoDeComprobante").HasKey(p => p.Clave);
            modelBuilder.Entity<c_TipoFactor>().ToTable("c_TipoFactor").HasKey(p => p.Clave);
            modelBuilder.Entity<c_TipoRelacion>().ToTable("c_TipoRelacion").HasKey(p => p.Clave);
            modelBuilder.Entity<c_UsoCFDI>().ToTable("c_UsoCFDI").HasKey(p => p.Clave);
        }
    }
}
