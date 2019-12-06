using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Excel.Loader.Model
{
    public partial class c_NumPedimentoAduana
    {
        public int Id { get; set; }

        public string Aduana { get; set; }

        public string Patente { get; set; }

        public string Ejercicio { get; set; }

        public string Cantidad { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
    }
}
