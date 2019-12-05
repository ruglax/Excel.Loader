using System;

namespace Labs.Excel.Loader.Model
{
    public partial class c_TipoRelacion
    {
        public string Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
    }
}
