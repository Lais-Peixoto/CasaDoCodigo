


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

    getDados(elementoHTML) {
        var linhaDoItem = $(elementoHTML).parents("[item-id]");
        var itemId = $(linhaDoItem).attr("item-id");
        var novaQtde = $(linhaDoItem).find("input").val();

        return dados = {
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
        });
    }
}

var carrinho = new Carrinho();




