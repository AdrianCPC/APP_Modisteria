using Microsoft.AspNetCore.Mvc;
using APP_Modisteria.Models;
using APP_Modisteria.Data;
using System.Collections.Generic;

namespace APP_Modisteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/Usuario
        [HttpGet]
        public List<Usuario> Get()
        {
            return UsuarioData.ListarUsuarios(); // Llama al método ListarUsuarios
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public Usuario Get(int id) // Cambiado a int para consistencia con el ID de Usuario
        {
            return UsuarioData.ObtenerUsuario(id); // Llama al método ObtenerUsuario
        }

        // POST: api/Usuario
        [HttpPost]
        public bool Post([FromBody] Usuario oUsuario)
        {
            return UsuarioData.registrarUsuario(oUsuario);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")] // Aunque no se use el id en la lógica, se mantiene para consistencia con la ruta
        public bool Put([FromBody] Usuario oUsuario)
        {
            return UsuarioData.actualizarUsuario(oUsuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public bool Delete(int id) // Cambiado a int para consistencia con el ID de Usuario
        {
            return UsuarioData.eliminarUsuario(id);
        }
    }
}