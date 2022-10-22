$(function () {
    InicializarClienteDataTable();

    CarregarClienteLista();
});

function CarregarClienteLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Cliente/CarregarClienteLista", JSON.stringify(jsonParametro), ResponseCarregarClienteItem);
}

function ResponseCarregarClienteItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var clienteLista = jsonObject['ClienteLista'];

    AdicionarClienteDataTableLinhaLista(clienteLista);
}

function InicializarClienteDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableClienteLista', colunaLista);
}

function AdicionarClienteDataTableLinhaLista(clienteLista) {
    AdicionarDataTableLinhaLista('#tableClienteLista', clienteLista);
}

function EditarClienteItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableClienteLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Cliente/Item?clienteId=' + linhaSelecionada['Id'];
}

function AdicionarClienteItem() {
    window.location.href = '/Cliente/Item';
}