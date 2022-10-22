$(function () {
    InicializarLoginPerfilDataTable();

    CarregarLoginPerfilLista();
});

function CarregarLoginPerfilLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginPerfilLista", JSON.stringify(jsonParametro), ResponseCarregarLoginPerfilLista);
}

function ResponseCarregarLoginPerfilLista(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginPerfilLista = jsonObject['LoginPerfilLista'];

    AdicionarLoginPerfilDataTableLinhaLista(loginPerfilLista);
}

function InicializarLoginPerfilDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableLoginPerfilLista', colunaLista);
}

function AdicionarLoginPerfilDataTableLinhaLista(loginPerfilLista) {
    AdicionarDataTableLinhaLista('#tableLoginPerfilLista', loginPerfilLista);
}

function EditarLoginPerfilItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableLoginPerfilLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Login/Perfil/Item?loginPerfilId=' + linhaSelecionada['Id'];
}

function AdicionarLoginPerfilItem() {
    window.location.href = "/Login/Perfil/Item";
}