using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        void AtualizarQuantidade(ItemPedido itemPedido);
    }
}