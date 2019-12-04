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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Extract logic to specific classes to map every column with max length
            modelBuilder.Entity<c_Aduana>().ToTable("c_Aduana");
            modelBuilder.Entity<c_Aduana>().Property(p => p.Id).HasMaxLength(2);
            modelBuilder.Entity<c_Aduana>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_Aduana>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_Aduana>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_ClaveUnidad>().ToTable("c_ClaveUnidad");
            modelBuilder.Entity<c_ClaveUnidad>().Property(p => p.Id).HasMaxLength(3);
            modelBuilder.Entity<c_ClaveUnidad>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_ClaveUnidad>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_ClaveUnidad>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_ClaveProdServ>().ToTable("c_ClaveProdServ");
            modelBuilder.Entity<c_ClaveProdServ>().Property(p => p.Id).HasMaxLength(8);
            modelBuilder.Entity<c_ClaveProdServ>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_ClaveProdServ>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_ClaveProdServ>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_CodigoPostal>().ToTable("c_CodigoPostal");
            modelBuilder.Entity<c_CodigoPostal>().Property(p => p.Id).HasMaxLength(5);
            modelBuilder.Entity<c_CodigoPostal>().Property(p => p.Estado).HasMaxLength(3);
            modelBuilder.Entity<c_CodigoPostal>().Property(p => p.Municipio).HasMaxLength(3);
            modelBuilder.Entity<c_CodigoPostal>().Property(p => p.Localidad).HasMaxLength(3);
            modelBuilder.Entity<c_CodigoPostal>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_CodigoPostal>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_FormaPago>().ToTable("c_FormaPago");
            modelBuilder.Entity<c_FormaPago>().Property(p => p.Id).HasMaxLength(2);
            modelBuilder.Entity<c_FormaPago>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_FormaPago>().Property(p => p.PatronCuentaOrdenante).HasMaxLength(500);
            modelBuilder.Entity<c_FormaPago>().Property(p => p.PatronCuentaBeneficiaria).HasMaxLength(500);
            modelBuilder.Entity<c_FormaPago>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_FormaPago>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_Impuesto>().ToTable("c_Impuesto");
            modelBuilder.Entity<c_Impuesto>().Property(p => p.Id).HasMaxLength(3);
            modelBuilder.Entity<c_Impuesto>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_Impuesto>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_Impuesto>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_MetodoPago>().ToTable("c_MetodoPago");
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.Id).HasMaxLength(3);
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_Moneda>().ToTable("c_Moneda").HasKey(p => p.Id);
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<c_MetodoPago>().Property(p => p.FechaFin).HasColumnType("date");

            modelBuilder.Entity<c_NumPedimentoAduana>().ToTable("c_NumPedimentoAduana");
            modelBuilder.Entity<c_NumPedimentoAduana>().Property(t => t.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<c_NumPedimentoAduana>().Property(p => p.Patente).HasMaxLength(4);
            modelBuilder.Entity<c_NumPedimentoAduana>().Property(p => p.Ejercicio).HasMaxLength(4);
            modelBuilder.Entity<c_NumPedimentoAduana>().Property(p => p.Cantidad).HasMaxLength(6);

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
