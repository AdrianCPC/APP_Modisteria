using Microsoft.AspNetCore.Mvc;
using APP_Modisteria.Models;
using APP_Modisteria.Data;
using System.Collections.Generic;

namespace APP_Modisteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        // GET: api/Pedido
        [HttpGet]
        public List<Pedido> Get()
        {
            return PedidoData.ListarPedidos();
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public Pedido Get(int id)
        {
            return PedidoData.ObtenerPedido(id);
        }

        // POST: api/Pedido
        [HttpPost]
        public bool Post([FromBody] Pedido oPedido)
        {
            return PedidoData.registrarPedido(oPedido);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Pedido oPedido)
        {
            return PedidoData.actualizarPedido(oPedido);
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return PedidoData.eliminarPedido(id);
        }
    }
}