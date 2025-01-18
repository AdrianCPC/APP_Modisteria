using System;

namespace APP_Modisteria.Models
{
    public class Reporte
    {
        public int idReporte { get; set; }
        public string tipo { get; set; }
        public DateTime fechaGeneracion { get; set; }
        public string contenido { get; set; }
        public int idUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}