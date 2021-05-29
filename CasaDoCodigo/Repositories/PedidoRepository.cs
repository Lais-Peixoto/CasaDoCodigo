using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public PedidoRepository(ApplicationContext contexto, IHttpContextAccessor contextAccessor) : base(contexto)
        {
            _contextAccessor = contextAccessor;
        }

        // método para obter o id do pedido que será armazenado na sessão
        private int? GetPedidoId()
        {
           return _contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        // método para gravar o pedidoId na sessão
        private void SetPedidoId(int pedidoId)
        {
            _contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }
    }
}
