using System.Collections.Generic;

namespace APP_Modisteria.Models
{
    public class Material
    {
        public int idMaterial { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int cantidadDisponible { get; set; }
        public decimal precioUnitario { get; set; }
        public List<DetallePedido> DetallesPedido { get; set; }
        public Inventario Inventario { get; set; } //Relación con Inventario
    }
}