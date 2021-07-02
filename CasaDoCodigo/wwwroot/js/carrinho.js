


class Carrinho {

    cliqueIncremento(botao) {
        let dado = this.getDados(botao);
        dado.Quantidade++;
        this.sendQuantidade(dado);
    }

    cliqueDecremento(botao) {
        let dado = this.getDados(botao);
        dado.Quantidade--;
        this.sendQuantidade(dado);
    }

    updateQuantidade(input) {
        let dado = this.getDados(input);
        this.sendQuantidade(dado);
    }

    getDados(elementoHTML) {
        var linhaDoItem = $(elementoHTML).parents("[item-id]");
        var itemId = $(linhaDoItem).attr("item-id");
        var novaQuantidade = $(linhaDoItem).find("input").val();

        return {
            Id: itemId,
            Quantidade: novaQuantidade
        };
    }

    sendQuantidade(dados) {

        /*o token deve ser fornecido como um valor do cabeçalho (headers)*/

        let token = $('[name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: "/pedido/updatequantidade",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(dados),
            headers: headers
        }).done(
            function (response) {
                let itemPedido = response.itemPedido;
                let linhaDoItem = $('[item-id=' + itemPedido.id + ']');
                linhaDoItem.find('input').val(itemPedido.quantidade);
                linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());

                let carrinhoViewModel = response.carrinhoViewModel
                $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + ' itens')

                if (itemPedido.quantidade == 0) {
                    linhaDoItem.remove();
                }

                $('[total]').html((carrinhoViewModel.total).duasCasas());
            }
        );
    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}




