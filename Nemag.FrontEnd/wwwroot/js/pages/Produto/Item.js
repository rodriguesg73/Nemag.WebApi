$(function () {
    ExecutarMetodoLista(
        CarregarProdutoCategoriaLista()
    ).then(metodoResultadoLista => {
        var formProdutoItem = $('#formProdutoItem');

        ExibirDropdownListaNoObjeto(formProdutoItem.find('[name="ProdutoCategoriaId"]'), ProcessarMetodoResultadoLista(metodoResultadoLista)['ProdutoCategoriaLista']);

        var produtoId = ObterQueryString('produtoId');

        if (produtoId !== '')
            CarregarProdutoItem(produtoId);
    });

});

function CarregarProdutoCategoriaLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/CarregarProdutoCategoriaLista", JSON.stringify(jsonParametro), null);
}

function CarregarProdutoItem(produtoId) {
    var jsonParametro = {
        'token': loginToken,
        'produtoId': produtoId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/CarregarProdutoItem", JSON.stringify(jsonParametro), ResponseCarregarProdutoItem);
}

function ResponseCarregarProdutoItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var produtoItem = jsonObject['ProdutoItem'];

    ExibirObjetoNoFormulario($('#formProdutoItem'), produtoItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formProdutoItem');

    return formularioValido;
}

function ObterApiParametro() {
    var produtoItem = $('#formProdutoItem').toJsonObject();

    var jsonParametro = {
        'token': loginToken,
        'produtoItem': produtoItem
    };

    return jsonParametro;
}

function SalvarProdutoItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarProdutoItem, null);
}

function CallbackSalvarProdutoItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/SalvarProdutoItem", JSON.stringify(jsonParametro), ResponseSalvarProdutoItem);
}

function ResponseSalvarProdutoItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}

function GerarCodigoProdutoItem() {
    var add = 1, max = 12 - add; 

    max = Math.pow(10, 11 + add);
    var min = max / 10; 
    var number = Math.floor(Math.random() * (max - min + 1)) + min;

    var finalCode = ("" + number).substring(add)

    $('#formProdutoItem').find('[name="Codigo"]').val(finalCode);
}