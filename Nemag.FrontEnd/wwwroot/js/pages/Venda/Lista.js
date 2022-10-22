$(function () {
    InicializarVendaDataTable();

    CarregarVendaLista();
});

function CarregarVendaLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Venda/CarregarVendaLista", JSON.stringify(jsonParametro), ResponseCarregarVendaItem);
}

function ResponseCarregarVendaItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var vendaLista = jsonObject['VendaLista'];

    AdicionarVendaDataTableLinhaLista(vendaLista);
}

function InicializarVendaDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Cliente', 'data': 'ClienteNome' },
        { 'title': 'Valor', 'data': 'Valor', 'mRender': RenderizarDataTableMoeda },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
    ];

    InicializarDataTable('#tableVendaLista', colunaLista);
}

function AdicionarVendaDataTableLinhaLista(vendaLista) {
    AdicionarDataTableLinhaLista('#tableVendaLista', vendaLista);
}

function EditarVendaItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableVendaLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Venda/Item?vendaId=' + linhaSelecionada['Id'];
}

function AdicionarVendaItem() {
    window.location.href = '/Venda/Item';
}