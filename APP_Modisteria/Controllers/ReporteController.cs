using Microsoft.AspNetCore.Mvc;
using APP_Modisteria.Models;
using APP_Modisteria.Data;
using System.Collections.Generic;

namespace APP_Modisteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        // GET: api/Reporte
        [HttpGet]
        public List<Reporte> Get()
        {
            return ReporteData.ListarReportes();
        }

        // GET: api/Reporte/5
        [HttpGet("{id}")]
        public Reporte Get(int id)
        {
            return ReporteData.ObtenerReporte(id);
        }

        // POST: api/Reporte
        [HttpPost]
        public bool Post([FromBody] Reporte oReporte)
        {
            return ReporteData.registrarReporte(oReporte);
        }

        // PUT: api/Reporte/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Reporte oReporte)
        {
            return ReporteData.actualizarReporte(oReporte);
        }

        // DELETE: api/Reporte/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return ReporteData.eliminarReporte(id);
        }
    }
}