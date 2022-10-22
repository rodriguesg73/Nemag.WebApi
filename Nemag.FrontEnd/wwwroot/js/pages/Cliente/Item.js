$(function () {
    ExecutarMetodoLista(
        CarregarPessoaContatoTipoLista()
    ).then(metodoResultadoLista => {
        _pessoaContatoTipoLista = ProcessarMetodoResultadoLista(metodoResultadoLista)['PessoaContatoTipoLista'];

        var clienteId = ObterQueryString('clienteId');

        if (clienteId !== '')
            CarregarClienteItem(clienteId);
    });
});

function CarregarClienteItem(clienteId) {
    var jsonParametro = {
        'token': loginToken,
        'clienteId': clienteId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Cliente/CarregarClienteItem", JSON.stringify(jsonParametro), ResponseCarregarClienteItem);
}

function ResponseCarregarClienteItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var clienteItem = jsonObject['ClienteItem'];

    var pessoaContatoLista = jsonObject['PessoaContatoLista'];

    ExibirObjetoNoFormulario($('#formClienteItem'), clienteItem);

    AdicionarPessoaContatoLista(pessoaContatoLista);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formClienteItem');

    return formularioValido;
}

function ObterApiParametro() {
    var clienteItem = $('#formClienteItem').toJsonObject();

    var pessoaContatoLista = $('#formPessoaContatoLista').ConverterParaObjetoLista();

    var jsonParametro = {
        'token': loginToken,
        'clienteItem': clienteItem,
        'pessoaContatoLista': pessoaContatoLista
    };

    return jsonParametro;
}

function SalvarClienteItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarClienteItem, null);
}

function CallbackSalvarClienteItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Cliente/SalvarClienteItem", JSON.stringify(jsonParametro), ResponseSalvarClienteItem);
}

function ResponseSalvarClienteItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}
