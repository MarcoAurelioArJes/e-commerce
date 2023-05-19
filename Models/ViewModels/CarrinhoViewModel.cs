using System.Linq;
using System.Collections.Generic;

namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public IList<ItemPedido> ItensPedido { get; }

        public CarrinhoViewModel(IList<ItemPedido> itensPedido)
        {
            ItensPedido = itensPedido;
        }

        public decimal Total => ItensPedido.Sum(c => c.PrecoUnitario * c.Quantidade);
    }
}
