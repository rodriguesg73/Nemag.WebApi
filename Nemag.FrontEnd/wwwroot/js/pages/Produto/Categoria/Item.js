$(function () {

        var produtoCategoriaId = ObterQueryString('produtoCategoriaId');

        if (produtoCategoriaId !== '')
            CarregarProdutoCategoriaItem(produtoCategoriaId);
});

function CarregarProdutoCategoriaItem(produtoCategoriaId) {
    var jsonParametro = {
        'token': loginToken,
        'produtoCategoriaId': produtoCategoriaId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/CarregarProdutoCategoriaItem", JSON.stringify(jsonParametro), ResponseCarregarProdutoCategoriaItem);
}

function ResponseCarregarProdutoCategoriaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var produtoCategoriaItem = jsonObject['ProdutoCategoriaItem'];

    ExibirObjetoNoFormulario($('#formProdutoCategoriaItem'), produtoCategoriaItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formProdutoCategoriaItem');

    return formularioValido;
}

function ObterApiParametro() {
    var produtoCategoriaItem = $('#formProdutoCategoriaItem').toJsonObject();

    var jsonParametro = {
        'token': loginToken,
        'produtoCategoriaItem': produtoCategoriaItem
    };

    return jsonParametro;
}

function SalvarProdutoCategoriaItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarProdutoCategoriaItem, null);
}

function CallbackSalvarProdutoCategoriaItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/SalvarProdutoCategoriaItem", JSON.stringify(jsonParametro), ResponseSalvarProdutoCategoriaItem);
}

function ResponseSalvarProdutoCategoriaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}
