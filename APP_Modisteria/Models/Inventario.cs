using System;

namespace APP_Modisteria.Models
{
    public class Inventario
    {
        public int idInventario { get; set; }
        public int idMaterial { get; set; }
        public int cantidad { get; set; }
        public Material Material { get; set; }
    }
}
