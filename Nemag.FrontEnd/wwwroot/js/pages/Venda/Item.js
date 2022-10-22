$(function () {

    var vendaId = ObterQueryString('vendaId');

    if (vendaId !== '')
        CarregarVendaItem(vendaId);
});

function CarregarVendaItem(vendaId) {
    var jsonParametro = {
        'token': loginToken,
        'vendaId': vendaId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Venda/CarregarVendaItem", JSON.stringify(jsonParametro), ResponseCarregarVendaItem);
}

function ResponseCarregarVendaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var vendaItem = jsonObject['VendaItem'];

    var vendaProdutoLista = jsonObject['VendaProdutoLista'];

    AdicionarProdutoVendaLista(vendaProdutoLista);

    ValidarProdutoVendaListaInterface();

    ExibirObjetoNoFormulario($('#formVendaItem'), vendaItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formVendaItem');

    return formularioValido;
}

function ObterApiParametro() {
    var vendaItem = $('#formVendaItem').toJsonObject();
    var produtoVendaLista = $('#formProdutoVendaLista').toJsonList();

    var jsonParametro = {
        'token': loginToken,
        'vendaItem': vendaItem,
        'produtoVendaLista': produtoVendaLista
    };

    return jsonParametro;
}

function SalvarVendaItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarVendaItem, null);
}

function CallbackSalvarVendaItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Venda/SalvarVendaItem", JSON.stringify(jsonParametro), ResponseSalvarVendaItem);
}

function ResponseSalvarVendaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}

function EfetuarPesquisaClienteItem(htmlButton) {
    EfetuarPesquisaItemGenerico(apiUrl + '/Api/Cliente/CarregarClienteLista', htmlButton);
}