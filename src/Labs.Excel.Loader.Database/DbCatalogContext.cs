using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Labs.Excel.Loader.Database
{
    public class DbCatalogContext : DbContext
    {
        private readonly string _connectionString = "server=.\\STAMPING;database=DBCATALOGOSv4;trusted_connection=true;User Id=sa;Password=123;";

        public DbCatalogContext()
        {
                
        }

        public DbCatalogContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<c_Aduana>().ToTable("c_Aduana").HasKey(p => p.Clave);
            modelBuilder.Entity<c_ClaveProdServ>().ToTable("c_ClaveProdServ").HasKey(p => p.Clave);
            modelBuilder.Entity<c_ClaveUnidad>().ToTable("c_ClaveUnidad").HasKey(p => p.Clave);
            modelBuilder.Entity<c_CodigoPostal>().ToTable("c_CodigoPostal").HasKey(p => p.Clave);
        }
    }
}
