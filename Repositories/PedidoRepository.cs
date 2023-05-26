using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public PedidoRepository(ApplicationContext contexto,
                                IHttpContextAccessor contextAccessor,
                                IItemPedidoRepository itemPedidoRepository) : base(contexto)
        {
            _contextAccessor = contextAccessor;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public Pedido GetPedido()
        {
            int? pedidoId = ObterPedidoId();
            // O include e o ThenInclude fazem o Join, ou seja, eles juntam a consulta
            // retornando os dados que precisamos
            var pedido = _dbSet
                            .Include(p => p.Itens)
                                .ThenInclude(i => i.Produto)
                            .Where(p => p.Id == pedidoId)
                            .SingleOrDefault();

            if (pedido is null)
            {
                pedido = new Pedido();
                _dbSet.Add(pedido);
                _contexto.SaveChanges();
                DefinirPedidoId(pedido.Id);
            }

            return pedido;
        }

        private int? ObterPedidoId()
        {
            return _contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void DefinirPedidoId(int pedidoId)
        {
            _contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

        public void AddItem(string codigo)
        {
            var produto = _contexto.Set<Produto>()
                            .Where(p => p.Codigo == codigo)
                            .SingleOrDefault();

            if (produto is null)
                throw new Exception("Produto não encontrado");

            var pedido = GetPedido();

            var itemPedido = _contexto.Set<ItemPedido>()
                             .Where(i => i.Produto.Codigo == codigo
                                         && i.Pedido.Id == pedido.Id)
                             .SingleOrDefault();

            if (itemPedido is null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _contexto.Set<ItemPedido>()
                    .Add(itemPedido);

                _contexto.SaveChanges();
            }
        }

        public RespostaQuantidadeAtualizada AtualizarQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoDB = _itemPedidoRepository.ObterItemPedido(itemPedido.Id);

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);

                _contexto.SaveChanges();

                var carrinhoViewModel = new CarrinhoViewModel(GetPedido().Itens);

                return new RespostaQuantidadeAtualizada(itemPedidoDB, carrinhoViewModel);
            }

            throw new ArgumentException("ItemPedido não encontrado!!!");
        }
    }
}
