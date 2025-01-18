using System.Collections.Generic; 

namespace APP_Modisteria.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public List<Pedido> Pedidos { get; set; } 
    }
}