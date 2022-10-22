$(function () {
    var databaseId = ObterQueryString('databaseId');

    if (databaseId !== '')
        CarregarDatabaseItem(databaseId);
});

function CarregarDatabaseItem(databaseId) {
    var jsonParametro = {
        'token': loginToken,
        'databaseId': databaseId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Database/CarregarDatabaseItem", JSON.stringify(jsonParametro), ResponseCarregarDatabaseItem);
}

function ResponseCarregarDatabaseItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var databaseItem = jsonObject['DatabaseItem'];

    ExibirObjetoNoFormulario($('#formDatabaseItem'), databaseItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formDatabaseItem');

    return formularioValido;
}

function ObterApiParametro() {
    var databaseItem = $('#formDatabaseItem').toJsonObject();

    var jsonParametro = {
        'token': loginToken,
        'databaseItem': databaseItem
    };

    return jsonParametro;
}

function SalvarDatabaseItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarDatabaseItem, null);
}

function CallbackSalvarDatabaseItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Database/SalvarDatabaseItem", JSON.stringify(jsonParametro), ResponseSalvarDatabaseItem);
}

function ResponseSalvarDatabaseItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}
