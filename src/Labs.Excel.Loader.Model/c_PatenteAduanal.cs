using System;

namespace Labs.Excel.Loader.Model
{
    public partial class c_PatenteAduanal
    {
        public string Clave { get; set; }

        public DateTime FechaInicio { get; set; } = new DateTime(2017, 01, 01);

        public DateTime? FechaFin { get; set; }
    }
}
