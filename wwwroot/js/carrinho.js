class Carrinho {

    adicionarItem(btn) {
        let itemPedido = this.obterItemPedido(btn);
        itemPedido.Quantidade++;
        this.atualizarQuantidade(itemPedido);
    }

    removerItem(btn) {
        let itemPedido = this.obterItemPedido(btn);
        itemPedido.Quantidade--;
        this.atualizarQuantidade(itemPedido);
    }

    alterarQuantidadeItem(input) {
        let itemPedido = this.obterItemPedido(input);
        this.atualizarQuantidade(itemPedido);
    }

    obterItemPedido(elemento) {
        let linhaProduto = $(elemento).parents('[item-id]');
        let itemId = $(linhaProduto).attr("item-id");
        let novaQtde = $(linhaProduto).find('input').val();

        return {
            Id: itemId,
            Quantidade: novaQtde
        };
    }

    atualizarQuantidade(itemPedido) {
        $.ajax({
            url: '/pedido/atualizaquantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(itemPedido)
        }).then(() => window.location.reload());
    }
}

let carrinho = new Carrinho();