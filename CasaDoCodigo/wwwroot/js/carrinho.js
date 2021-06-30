


class Carrinho {

    cliqueIncremento(btn) {
        let dado = this.getDados(btn);
        dado.Quantidade++;
        this.sendQuantidade(dado);
    }

    cliqueDecremento(btn) {
        let dado = this.getDados(btn);
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
        var novaQtde = $(linhaDoItem).find("input").val();

        return {
            Id: itemId,
            Quantidade: novaQtde
        };
    }

    sendQuantidade(dados) {
        $.ajax({
            url: "/pedido/updatequantidade",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(dados),
        }).done(
            function (response) {
                let itemPedido = response.itemPedido;
                let linhaDoItem = $('[item-id=' + itemPedido.id + ']');
                linhaDoItem.find('input').val(itemPedido.quantidade);
                linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());
            }
        );
    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}




