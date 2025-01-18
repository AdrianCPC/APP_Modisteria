using System;
using System.Collections.Generic;

namespace APP_Modisteria.Models
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public int idCliente { get; set; } 
        public Cliente Cliente { get; set; } 
        public List<DetallePedido> DetallesPedido { get; set; } 
    }

    public class DetallePedido
    {
        public int idDetallePedido { get; set; }
        public int idPedido { get; set; }
        public int idMaterial { get; set; }
        public int cantidad { get; set; }
        public Pedido Pedido { get; set; } 
        public Material Material { get; set; } 
    }
}