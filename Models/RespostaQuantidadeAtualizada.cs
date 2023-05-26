using CasaDoCodigo.Models.ViewModels;

namespace CasaDoCodigo.Models
{
    public class RespostaQuantidadeAtualizada
    {
        public RespostaQuantidadeAtualizada(ItemPedido itemPedido, CarrinhoViewModel carrinhoViewModel)
        {
            ItemPedido = itemPedido;
            CarrinhoViewModel = carrinhoViewModel;
        }

        public ItemPedido ItemPedido { get; }
        public CarrinhoViewModel CarrinhoViewModel { get; set; }
    }
}
