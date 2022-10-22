$(function () {
    InicializarLoginGrupoDataTable();

    CarregarLoginGrupoLista();
});

function CarregarLoginGrupoLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginGrupoLista", JSON.stringify(jsonParametro), ResponseCarregarLoginGrupoLista);
}

function ResponseCarregarLoginGrupoLista(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginGrupoLista = jsonObject['LoginGrupoLista'];

    AdicionarLoginGrupoDataTableLinhaLista(loginGrupoLista);
}

function InicializarLoginGrupoDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableLoginGrupoLista', colunaLista);
}

function AdicionarLoginGrupoDataTableLinhaLista(loginGrupoLista) {
    AdicionarDataTableLinhaLista('#tableLoginGrupoLista', loginGrupoLista);
}

function EditarLoginGrupoItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableLoginGrupoLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Login/Grupo/Item?loginGrupoId=' + linhaSelecionada['Id'];
}

function AdicionarLoginGrupoItem() {
    window.location.href = "/Login/Grupo/Item";
}