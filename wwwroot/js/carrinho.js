class Carrinho {

    adicionarItem(botao) {
        let itemPedido = this.obterItemPedido(botao);
        itemPedido.Quantidade++;
        this.atualizarQuantidade(itemPedido);
    }

    removerItem(botao) {
        let itemPedido = this.obterItemPedido(botao);
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
        let novaQuantidade = $(linhaProduto).find('input').val();

        return {
            Id: itemId,
            Quantidade: novaQuantidade
        };
    }

    atualizarQuantidade(itemPedido) {

        let token = $("[name=__RequestVerificationToken]").val();

        let headers = {}
        headers["RequestVerificationToken"] = token;


        $.ajax({
            url: '/pedido/atualizaquantidade',
            type: 'POST',
            contentType: 'application/json',
            headers: headers,
            data: JSON.stringify(itemPedido)
        }).then(res => {
            let itemPedido = res.itemPedido;
            let carrinhoViewModel = res.carrinhoViewModel;

            let linhaDoItem = $(`[item-id=${itemPedido.id}]`);
            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());

            $('[numero-itens]').html(`Total: ${carrinhoViewModel.itensPedido.length} itens`);

            $('[total]').html(`${(carrinhoViewModel.total).duasCasas()}`);

            if (itemPedido.quantidade === 0)
                linhaDoItem.remove();
        });
    }
}

let carrinho = new Carrinho();

Number.prototype.duasCasas = function() {
    return this.toFixed(2).replace('.', ',');
}