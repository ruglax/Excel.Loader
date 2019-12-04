using System;

namespace Labs.Excel.Loader.Model
{
    public partial class c_TipoRelacion
    {
        public string Clave { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
    }
}
