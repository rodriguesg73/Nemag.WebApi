$(function () {
    InicializarPaisDataTable();

    CarregarPaisLista();
});

function CarregarPaisLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Pais/CarregarPaisLista", JSON.stringify(jsonParametro), ResponseCarregarPaisItem);
}

function ResponseCarregarPaisItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var paisLista = jsonObject['PaisLista'];

    AdicionarPaisDataTableLinhaLista(paisLista);
}

function InicializarPaisDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tablePaisLista', colunaLista);
}

function AdicionarPaisDataTableLinhaLista(paisLista) {
    AdicionarDataTableLinhaLista('#tablePaisLista', paisLista);
}

function EditarPaisItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tablePaisLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Pais/Item?paisId=' + linhaSelecionada['Id'];
}

function AdicionarPaisItem() {
    window.location.href = '/Pais/Item';
}