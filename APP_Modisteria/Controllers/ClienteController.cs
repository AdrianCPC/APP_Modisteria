using Microsoft.AspNetCore.Mvc;
using APP_Modisteria.Models;
using APP_Modisteria.Data;
using System.Collections.Generic;

namespace APP_Modisteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // GET: api/Cliente
        [HttpGet]
        public List<Cliente> Get()
        {
            return ClienteData.ListarClientes();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public Cliente Get(int id)
        {
            return ClienteData.ObtenerCliente(id);
        }

        // POST: api/Cliente
        [HttpPost]
        public bool Post([FromBody] Cliente oCliente)
        {
            return ClienteData.registrarCliente(oCliente);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Cliente oCliente)
        {
            return ClienteData.actualizarCliente(oCliente);
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return ClienteData.eliminarCliente(id);
        }
    }
}