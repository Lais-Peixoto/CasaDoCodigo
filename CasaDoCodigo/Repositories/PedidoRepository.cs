using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = _dbSet
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();

            if (pedido == null)
            {
                pedido = new Pedido();
                _dbSet.Add(pedido);
                _contexto.SaveChanges();
                SetPedidoId(pedido.Id);
            }
            return pedido;
        }

        public void AddItem(string codigo)
        {
            var produto = _contexto.Set<Produto>()
                .Where(p => p.Codigo == codigo)
                .SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            var pedido = GetPedido();

            var itemPedido = _contexto.Set<ItemPedido>()
                .Where(i => i.Produto.Codigo == codigo
                && i.Pedido.Id == pedido.Id)
                .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _contexto.Set<ItemPedido>().Add(itemPedido);
                _contexto.SaveChanges();
            }
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
