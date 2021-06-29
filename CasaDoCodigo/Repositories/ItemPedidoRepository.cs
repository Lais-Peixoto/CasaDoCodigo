using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public void UpdateQuantidade(ItemPedido itemPedido)
        {
           var itemPedidoDB = _dbSet.Where(i => i.Id == itemPedido.Id).SingleOrDefault();

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade); // foi necessario criar esse método para modificar o setter privado
                _contexto.SaveChanges();
            }
        }
    }
}
