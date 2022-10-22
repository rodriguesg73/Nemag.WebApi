$(function () {
    InicializarLoginDataTable();

    CarregarFiltroItemPorCookie($('#formFiltroItem'));

    EfetuarPesquisaLoginLista();
});

function EfetuarPesquisaLoginLista() {
    var filtroItem = $('#formFiltroItem').toJsonObject();

    SalvarFiltroItemNoCookie(filtroItem);

    var jsonParametro = {
        'token': loginToken,
        'loginFiltroItem': filtroItem
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginListaPorFiltroItem", JSON.stringify(jsonParametro), ResponseCarregarLoginItem);
}

function ResponseCarregarLoginItem(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginLista = jsonObject['LoginLista'];

    AdicionarLoginTarefaDataTableLinhaLista(loginLista);
}

function InicializarLoginDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Usuário', 'data': 'Usuario' },
        { 'title': 'Nome completo', 'data': 'Nome' },
        { 'title': 'Nome de exibição', 'data': 'NomeExibicao' },
        { 'title': 'Situacao', 'data': 'LoginSituacaoNome' },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': FormatarDataHoraWebApi }
    ];

    InicializarDataTable('#tableLoginLista', colunaLista);
}

function AdicionarLoginTarefaDataTableLinhaLista(loginLista) {
    AdicionarDataTableLinhaLista('#tableLoginLista', loginLista);
}

function EditarLoginItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableLoginLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Login/Item?loginId=' + linhaSelecionada['Id'];
}

function AdicionarLoginItem() {
    window.location.href = '/Login/Item';
}