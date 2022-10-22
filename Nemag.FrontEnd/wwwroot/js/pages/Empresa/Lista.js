$(function () {
    InicializarEmpresaDataTable();

    CarregarEmpresaLista();
});

function CarregarEmpresaLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Empresa/CarregarEmpresaLista", JSON.stringify(jsonParametro), ResponseCarregarEmpresaItem);
}

function ResponseCarregarEmpresaItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var empresaLista = jsonObject['EmpresaLista'];

    AdicionarEmpresaDataTableLinhaLista(empresaLista);
}

function InicializarEmpresaDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableEmpresaLista', colunaLista);
}

function AdicionarEmpresaDataTableLinhaLista(empresaLista) {
    AdicionarDataTableLinhaLista('#tableEmpresaLista', empresaLista);
}

function EditarEmpresaItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableEmpresaLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Empresa/Item?empresaId=' + linhaSelecionada['Id'];
}

function AdicionarEmpresaItem() {
    window.location.href = '/Empresa/Item';
}