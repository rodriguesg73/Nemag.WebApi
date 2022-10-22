$(function () {
    InicializarProdutoCategoriaDataTable();

    CarregarProdutoCategoriaLista();
});

function CarregarProdutoCategoriaLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Produto/CarregarProdutoCategoriaLista", JSON.stringify(jsonParametro), ResponseCarregarProdutoCategoriaItem);
}

function ResponseCarregarProdutoCategoriaItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var produtoCategoriaLista = jsonObject['ProdutoCategoriaLista'];

    AdicionarProdutoCategoriaDataTableLinhaLista(produtoCategoriaLista);
}

function InicializarProdutoCategoriaDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableProdutoCategoriaLista', colunaLista);
}

function AdicionarProdutoCategoriaDataTableLinhaLista(produtoCategoriaLista) {
    AdicionarDataTableLinhaLista('#tableProdutoCategoriaLista', produtoCategoriaLista);
}

function EditarProdutoCategoriaItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableProdutoCategoriaLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Produto/Categoria/Item?produtoCategoriaId=' + linhaSelecionada['Id'];
}

function AdicionarProdutoCategoriaItem() {
    window.location.href = '/Produto/Categoria/Item';
}