using CasaDoCodigo.Models;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public ItemPedido ObterItemPedido(int id)
        {
            return _dbSet.Where(i => i.Id == id).SingleOrDefault() ?? throw new Exception($"Não existe Item de Pedido com o ID {id}");
        }

        public void RemoverItemPedido(int id)
        {
            _dbSet.Remove(ObterItemPedido(id));
        }
    }
}
