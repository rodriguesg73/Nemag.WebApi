$(function () {
    InicializarMenuDataTable();

    CarregarMenuLista();
});

function CarregarMenuLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Menu/CarregarMenuLista", JSON.stringify(jsonParametro), ResponseCarregarMenuLista);
}

function ResponseCarregarMenuLista(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var menuLista = jsonObject['MenuLista'];

    AdicionarMenuDataTableLinhaLista(menuLista);
}

function InicializarMenuDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Titulo', 'data': 'Titulo' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableMenuLista', colunaLista);
}

function AdicionarMenuDataTableLinhaLista(menuLista) {
    AdicionarDataTableLinhaLista('#tableMenuLista', menuLista);
}

function EditarMenuItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableMenuLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Menu/Item?menuId=' + linhaSelecionada['Id'];
}

function AdicionarMenuItem() {
    window.location.href = "/Menu/Item";
}