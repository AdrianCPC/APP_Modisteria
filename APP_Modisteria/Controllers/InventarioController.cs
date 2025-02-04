using Microsoft.AspNetCore.Mvc;
using APP_Modisteria.Models;
using APP_Modisteria.Data;
using System.Collections.Generic;

namespace APP_Modisteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        // GET: api/Inventario
        [HttpGet]
        public List<Inventario> Get()
        {
            return InventarioData.ListarInventarios();
        }

        // GET: api/Inventario/5
        [HttpGet("{id}")]
        public Inventario Get(int id)
        {
            return InventarioData.ObtenerInventario(id);
        }

        // POST: api/Inventario
        [HttpPost]
        public bool Post([FromBody] Inventario oInventario)
        {
            return InventarioData.registrarInventario(oInventario);
        }

        // PUT: api/Inventario/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Inventario oInventario)
        {
            return InventarioData.actualizarInventario(oInventario);
        }

        // DELETE: api/Inventario/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return InventarioData.eliminarInventario(id);
        }
    }
}