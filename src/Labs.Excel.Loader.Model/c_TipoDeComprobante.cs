using System;

namespace Labs.Excel.Loader.Model
{
    public partial class c_TipoFactor
    {
        public string Id { get; set; }

        public DateTime FechaInicio { get; set; } = new DateTime(2017, 1, 1);

        public DateTime? FechaFin { get; set; }
    }
}
