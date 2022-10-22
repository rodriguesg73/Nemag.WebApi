$(function () {
    InicializarDatabaseDataTable();

    CarregarDatabaseLista();
});

function CarregarDatabaseLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Database/CarregarDatabaseLista", JSON.stringify(jsonParametro), ResponseCarregarDatabaseItem);
}

function ResponseCarregarDatabaseItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var databaseLista = jsonObject['DatabaseLista'];

    AdicionarDatabaseDataTableLinhaLista(databaseLista);
}

function InicializarDatabaseDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Titulo' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableDatabaseLista', colunaLista);
}

function AdicionarDatabaseDataTableLinhaLista(databaseLista) {
    AdicionarDataTableLinhaLista('#tableDatabaseLista', databaseLista);
}

function EditarDatabaseItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableDatabaseLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Database/Item?databaseId=' + linhaSelecionada['Id'];
}

function AdicionarDatabaseItem() {
    window.location.href = '/Database/Item';
}