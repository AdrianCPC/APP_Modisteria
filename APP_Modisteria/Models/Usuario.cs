using System;
namespace APP_Modisteria.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public char tipoUsuario { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Empleado Empleado { get; set; }
        public Administrador Administrador { get; set; }
        public List<Reporte> Reportes { get; set; }
    }

    public class Empleado 
    {
        public int idEmpleado { get; set; }
        public int idUsuario { get; set; }
        public string departamento { get; set; }
        public Usuario Usuario { get; set; }
    }

    public class Administrador 
    {
        public int idAdministrador { get; set; }
        public int idUsuario { get; set; }
        public int nivelAcceso {  get; set; }
        public Usuario Usuario { get; set; }
    }
}
