using Microsoft.AspNetCore.Mvc;
using APP_Modisteria.Models;
using APP_Modisteria.Data;
using System.Collections.Generic;

namespace APP_Modisteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        // GET: api/Material
        [HttpGet]
        public List<Material> Get()
        {
            return MaterialData.ListarMateriales();
        }

        // GET: api/Material/5
        [HttpGet("{id}")]
        public Material Get(int id)
        {
            return MaterialData.ObtenerMaterial(id);
        }

        // POST: api/Material
        [HttpPost]
        public bool Post([FromBody] Material oMaterial)
        {
            return MaterialData.registrarMaterial(oMaterial);
        }

        // PUT: api/Material/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Material oMaterial)
        {
            return MaterialData.actualizarMaterial(oMaterial);
        }

        // DELETE: api/Material/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return MaterialData.eliminarMaterial(id);
        }
    }
}