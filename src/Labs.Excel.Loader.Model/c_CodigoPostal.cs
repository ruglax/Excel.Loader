using System;

namespace Labs.Excel.Loader.Model
{
    public class c_CodigoPostal
    {
        public string Clave { get; set; }

        public string Estado { get; set; }

        public string Municipio { get; set; }

        public string Localidad { get; set; }

        public short EstimuloFranjaFronteriza { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }


    }
}
