function EfetuarLoginApi() {
    $("#formLoginIndex").ajaxSubmit({
        type: 'post',
        success: function () {
            var queryString = ObterQueryString('ReturnUrl');

            if (queryString === '')
                queryString = '/Home/Index';

            window.location.href = queryString;
        },
        error: function (request, errordata, errorObject) {
            var mensagemItem = JSON.parse(request.responseText);

            ExibirMensagem('Erro', mensagemItem.value);
        }
    })
}