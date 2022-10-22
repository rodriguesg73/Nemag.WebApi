$(function () {
    IniciarConfiguracao();
});

function EfetuarPesquisaPessoaItem(htmlButton) {
    EfetuarPesquisaItemGenerico(apiUrl + '/Api/Pessoa/CarregarPessoaLista', htmlButton);
}

function EfetuarPesquisaItemGenericoCallback(apiUrl, htmlButton, callback, propriedadeExibicaoLista) {
    var container = $(htmlButton).closest('.input-group');

    var jsonParametro = {
        'token': loginToken
    }

    EfetuarPesquisa('Pesquisa', apiUrl, jsonParametro, function (data) {
        if (data === undefined || data === null)
            return;

        var nomeTitulo = undefined;

        if (data['Nome'] !== null && data['Nome'] !== undefined)
            nomeTitulo = data['Nome'];
        else if (data['Titulo'] !== null && data['Titulo'] !== undefined)
            nomeTitulo = data['Titulo'];
        else if (data['Codigo'] !== null && data['Codigo'] !== undefined)
            nomeTitulo = data['Codigo'];


        $(container).find('[type="hidden"]').val(data['Id']);

        $(container).find('[type="text"]').val(nomeTitulo);

        ExibirObjetoNoFormulario(container, data);

        if (callback !== undefined && callback !== null)
            callback(data);
    }, null, null, null, propriedadeExibicaoLista);
}

function EfetuarPesquisaItemGenerico(apiUrl, htmlButton, propriedadeExibicaoLista) {
    EfetuarPesquisaItemGenericoCallback(apiUrl, htmlButton, undefined, propriedadeExibicaoLista);
}

function LimparPesquisaItemGenerica(htmlButton) {
    var container = $(htmlButton).parent().parent();

    $(container).find('[type="hidden"]').val(0);

    $(container).find('[type="text"]').val('');
}
