$(function () {
    var loginPerfilId = ObterQueryString('loginPerfilId');

    if (loginPerfilId !== '')
        CarregarLoginPerfilItem(loginPerfilId);
});

function CarregarLoginPerfilItem(loginPerfilId) {
    var jsonParametro = {
        'token': loginToken,
        'loginPerfilId': loginPerfilId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginPerfilItem", JSON.stringify(jsonParametro), ResponseCarregarLoginPerfilLista);
}

function ResponseCarregarLoginPerfilLista(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginPerfilItem = jsonObject['LoginPerfilItem'];

    ExibirObjetoNoFormulario($('#formLoginPerfilItem'), loginPerfilItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formLoginPerfilItem');

    return formularioValido;
}

function ObterApiParametro() {
    var loginPerfilItem = $('#formLoginPerfilItem').toJsonObject();

    var jsonParametro = {
        'token': loginToken,
        'loginPerfilItem': loginPerfilItem
    };

    return jsonParametro;
}

function SalvarLoginPerfilItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarLoginPerfilItem, null);
}

function CallbackSalvarLoginPerfilItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/SalvarLoginPerfilItem", JSON.stringify(jsonParametro), ResponseSalvarLoginPerfilItem);
}

function ResponseSalvarLoginPerfilItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}