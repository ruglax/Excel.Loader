using System;

namespace Labs.Excel.Loader.Model
{
    public partial class c_Pais
    {
        public string Id { get; set; }

        public string Descripcion { get; set; }

        public string FormatoCodigoPostal { get; set; }

        public string FormatoRegistroIdentidad { get; set; }

        public string ValidacionRegistroIdentidad { get; set; }

        public string Agrupaciones { get; set; }

        public DateTime FechaInicio { get; set; } = new DateTime(2017, 01, 01);

        public DateTime? FechaFin { get; set; }
    }
}
