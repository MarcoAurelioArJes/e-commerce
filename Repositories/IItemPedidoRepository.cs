﻿using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido ObterItemPedido(int id);
        void RemoverItemPedido(int id);
    }
}