$(function () {
    var loginGrupoId = ObterQueryString('loginGrupoId');

    if (loginGrupoId !== '')
        CarregarLoginGrupoItem(loginGrupoId);
});

function CarregarLoginGrupoItem(loginGrupoId) {
    var jsonParametro = {
        'token': loginToken,
        'loginGrupoId': loginGrupoId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginGrupoItem", JSON.stringify(jsonParametro), ResponseCarregarLoginGrupoLista);
}

function ResponseCarregarLoginGrupoLista(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginGrupoItem = jsonObject['LoginGrupoItem'];

    ExibirObjetoNoFormulario($('#formLoginGrupoItem'), loginGrupoItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formLoginGrupoItem');

    return formularioValido;
}

function ObterApiParametro() {
    var loginGrupoItem = $('#formLoginGrupoItem').toJsonObject();

    var jsonParametro = {
        'token': loginToken,
        'loginGrupoItem': loginGrupoItem
    };

    return jsonParametro;
}

function SalvarLoginGrupoItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarLoginGrupoItem, null);
}

function CallbackSalvarLoginGrupoItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/SalvarLoginGrupoItem", JSON.stringify(jsonParametro), ResponseSalvarLoginGrupoItem);
}

function ResponseSalvarLoginGrupoItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}