$(function () {
    IniciarConfiguracao();
});

function EfetuarLoginApi() {
    var loginUsuario = $('#inputLoginUsuario').val();

    var loginSenha = $('#inputLoginSenha').val();

    return AjaxRequestPostAsync(apiUrl + '/Api/Login/EfetuarLogin', JSON.stringify({ 'usuario': loginUsuario, 'senha': loginSenha }), ResponseEfetuarLoginApi);
}

function ResponseEfetuarLoginApi(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var queryString = ObterQueryString('ReturnUrl');

    if (queryString === '')
        queryString = '/Home/Index';

    var loginItem = jsonObject['LoginItem'];

    var loginAcessoItem = jsonObject['LoginAcessoItem'];

    AtualizarLoginAcessoCookie(loginItem, loginAcessoItem);

    document.location.reload(true);
}

function RenovarLoginAcessoItem() {
    var senha = $('#formLoginAcessoRenovacaoItem').find('[name="Senha"]').val();

    var jsonParametro = {
        'token': loginToken,
        'senha': senha
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Login/RenovarLoginAcessoItem', JSON.stringify(jsonParametro), ResponseRenovarLoginAcessoItem);
}

function ResponseRenovarLoginAcessoItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginItem = jsonObject['LoginItem'];

    var loginAcessoItem = jsonObject['LoginAcessoItem'];

    AtualizarLoginAcessoCookie(loginItem, loginAcessoItem);

    $('body').find('.bloqueio-acesso').css('display', 'none');

    document.location.reload(true);
}