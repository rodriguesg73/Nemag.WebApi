function EfetuarRequisicao() {
    var requisicaoItem = $('#formRequisicaoItem').ConverterParaObjetoItem();

    var requisicaoConteudo = requisicaoItem['Conteudo'];

    return AjaxRequestPostAsync(requisicaoItem['Url'], requisicaoConteudo, ResponseEfetuarRequisicao);
}

function ResponseEfetuarRequisicao(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    $('#formRequisicaoItem').find('[name="Retorno"]').val(data);
}