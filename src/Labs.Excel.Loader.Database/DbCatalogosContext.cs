using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public DbSet<c_Aduana> Aduana { get; set; }

        public DbSet<c_ClaveProdServ> ClavesProdServ { get; set; }

        public DbSet<c_ClaveUnidad> ClaveUnidad { get; set; }

        public DbSet<c_CodigoPostal> CodigoPostal { get; set; }

        public DbSet<c_FormaPago> FormaPago { get; set; }

        public DbSet<c_Impuesto> Impuesto { get; set; }

        public DbSet<c_MetodoPago> MetodoPago { get; set; }

        public DbSet<c_Moneda> Moneda { get; set; }

        public DbSet<c_NumPedimentoAduana> NumPedimentoAduana { get; set; }

        public DbSet<c_Pais> Pais { get; set; }

        public DbSet<c_PatenteAduanal> PatenteAduanal { get; set; }

        public DbSet<c_RegimenFiscal> RegimenFiscal { get; set; }

        public DbSet<c_TasaOCuota> TasaOCuota { get; set; }

        public DbSet<c_TipoDeComprobante> TipoDeComprobante { get; set; }

        public DbSet<c_TipoFactor> TipoFactor { get; set; }

        public DbSet<c_TipoRelacion> TipoRelacion { get; set; }

        public DbSet<c_UsoCFDI> UsoCFDI { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DbCatalogosContext).Assembly);
        }
    }
}
