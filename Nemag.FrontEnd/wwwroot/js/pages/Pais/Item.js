$(function () {

        var paisId = ObterQueryString('paisId');

        if (paisId !== '')
            CarregarPaisItem(paisId);
});

function CarregarPaisItem(paisId) {
    var jsonParametro = {
        'token': loginToken,
        'paisId': paisId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Pais/CarregarPaisItem", JSON.stringify(jsonParametro), ResponseCarregarPaisItem);
}

function ResponseCarregarPaisItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var paisItem = jsonObject['PaisItem'];

    ExibirObjetoNoFormulario($('#formPaisItem'), paisItem);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formPaisItem');

    return formularioValido;
}

function ObterApiParametro() {
    var paisItem = $('#formPaisItem').toJsonObject();

    var jsonParametro = {
        'token': loginToken,
        'paisItem': paisItem
    };

    return jsonParametro;
}

function SalvarPaisItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarPaisItem, null);
}

function CallbackSalvarPaisItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Pais/SalvarPaisItem", JSON.stringify(jsonParametro), ResponseSalvarPaisItem);
}

function ResponseSalvarPaisItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}
