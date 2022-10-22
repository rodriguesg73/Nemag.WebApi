$(function () {
    InicializarProdutoDataTable();

    CarregarProdutoLista();
});

function CarregarProdutoLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/CarregarProdutoLista", JSON.stringify(jsonParametro), ResponseCarregarProdutoItem);
}

function ResponseCarregarProdutoItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var produtoLista = jsonObject['ProdutoLista'];

    AdicionarProdutoDataTableLinhaLista(produtoLista);
}

function InicializarProdutoDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Categoria', 'data': 'ProdutoCategoriaNome'},
        { 'title': 'Valor', 'data': 'Valor', 'mRender': RenderizarDataTableMoeda},
        { 'title': 'Codigo do Produto', 'data': 'Codigo'},
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora}
    ];

    InicializarDataTable('#tableProdutoLista', colunaLista);
}

function AdicionarProdutoDataTableLinhaLista(produtoLista) {
    AdicionarDataTableLinhaLista('#tableProdutoLista', produtoLista);
}

function EditarProdutoItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableProdutoLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Produto/Item?produtoId=' + linhaSelecionada['Id'];
}

function AdicionarProdutoItem() {
    window.location.href = '/Produto/Item';
}