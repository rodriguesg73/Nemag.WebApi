/******************************************************************************************************/
/* CONFIG */
/******************************************************************************************************/
var apiUrl = '';

var webSocketUrl = '';

var gatewayUrl = '';

var loginToken = $.cookie('loginToken');

var loginItem = $.cookie('loginItem');

var formularioAlterado = false;

/******************************************************************************************************/
/* CONFIGURAÇÃO DA API */
/******************************************************************************************************/
function IniciarConfiguracao() {
    var fullHostname = location.hostname;

    fullHostname = fullHostname.replace('webapp', 'webapi');

    apiUrl = location.protocol + '//' + fullHostname + (location.port ? ':' + (parseInt(location.port) + 1000) : '')

    AtivarAjaxSplash();

    jQuery.extend(jQuery.validator.methods, {
        date: function (value, element) {
            var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[\/\-]\d{4}$/g;

            var resultado = regex.test(value);

            return this.optional(element) || resultado;
        }
    });

    $.validator.addMethod('datetime', function (value, element, param) {
        var regex = /^([1-9]|([012][0-9])|(3[01]))\/([0]{0,1}[1-9]|1[012])\/\d\d\d\d\s([0-1]?[0-9]|2?[0-3]):([0-5]\d)$/g;

        var resultado = regex.test(value);

        return this.optional(element) || resultado;
    }, 'Please enter a valid date and time!');

    $.validator.addMethod('decimal', function (value, element, param) {
        var regex = /^\d+(\.\d{1,4})?$/;

        var resultado = regex.test(value);

        return this.optional(element) || resultado;
    }, 'Please enter a decimal number!');

    $.validator.addMethod('integer', function (value, element, param) {
        var regex = /[0-9]$/;

        var resultado = regex.test(value);

        return this.optional(element) || resultado;
    }, 'Please enter a integer number!');

    $.validator.addMethod("greaterOrEqualThan",
        function (value, element, params) {
            var valorOriginal = value;

            var valorComparativo = undefined;

            var inputDestino = element;

            do {
                if ($(inputDestino).parent().find('[name="' + params + '"]').html() !== undefined)
                    inputDestino = $(inputDestino).parent().find('[name="' + params + '"]');
                else
                    inputDestino = $(inputDestino).parent();
            }
            while (inputDestino.attr('name') !== params && $(inputDestino).prop("tagName") !== 'BODY');

            if ($(inputDestino).html() === undefined)
                return false;

            valorComparativo = $(inputDestino).val();

            if (valorOriginal.indexOf('/') < 0)
                valorOriginal = FormatarDataWebApi(valorOriginal);

            if (valorComparativo.indexOf('/') < 0)
                valorComparativo = FormatarDataWebApi(valorComparativo);

            valorOriginal = new Date(valorOriginal.substring(6, 10), valorOriginal.substring(3, 5) - 1, valorOriginal.substring(0, 2));

            valorComparativo = new Date(valorComparativo.substring(6, 10), valorComparativo.substring(3, 5) - 1, valorComparativo.substring(0, 2));

            if (!/Invalid|NaN/.test(valorOriginal))
                return valorOriginal >= valorComparativo;

            return isNaN(value) && isNaN($(params).val()) || (Number(valorOriginal) >= Number(valorComparativo));
        }, 'Must be greater or equal than {0}.');

    window.onerror = function (msg, url, lineNo, columnNo, error) {
        $('body').find('.ajax-request-progress').css('display', 'none');

        if (typeof msg.toLowerCase === 'function') {
            var string = msg.toLowerCase();

            var substring = "script error";

            if (string.indexOf(substring) === -1)
                ExibirMensagem('Script Error', 'Por gentileza, entre em contato com o suporte!<br/><br/><b>Mensagem:</b> ' + msg + '</br><b>Script:</b> ' + url + '</br><b>Linha:</b> ' + lineNo + '</br><b>Coluna:</b> ' + columnNo + '</br><b>Mensagem:</b> ' + error);

            return false;
        }
        else if (typeof msg !== 'object') {
            alert(JSON.stringify(msg));
        } else {
            alert('Error');
        }
    };

    MonitorarFormularioAlteracao();
}

/******************************************************************************************************/
/* MISC */
/******************************************************************************************************/
function Voltar() {
    history.back();
}

function AtualizarLoginAcessoCookie(loginItem, loginAcessoItem) {
    $.removeCookie('loginItem');

    $.cookie('loginItem', encodeURI(JSON.stringify(loginItem)), { path: '/', expires: 7 });

    if (loginAcessoItem !== null && loginAcessoItem !== undefined) {
        $.removeCookie('loginToken');

        $.cookie('loginToken', loginAcessoItem.Token, { path: '/', expires: 7 });

        loginToken = loginAcessoItem.Token;
    }
}

function ObterLoginItemPorCookie() {
    var loginItem = JSON.parse(decodeURI($.cookie('loginItem')));

    return loginItem;
}

function SalvarFiltroItemNoCookie(filtroItem) {
    if (filtroItem !== null && filtroItem !== undefined) {
        $.cookie('filtroItem', encodeURI(JSON.stringify(filtroItem)), { expires: 7 });
    } else
        $.removeCookie('filtroItem');
}

function CarregarFiltroItemPorCookie(formFiltroItem) {
    if ($.cookie('filtroItem') === undefined || $.cookie('filtroItem') === null)
        return undefined;

    var filtroItem = JSON.parse(decodeURI($.cookie('filtroItem')));

    if (filtroItem !== null && filtroItem !== undefined) {
        ExibirObjetoNoFormulario(formFiltroItem, filtroItem);
    }

    return filtroItem;
}

/*****************************************************************************************************/
/* AJAX SPLASH */
/*****************************************************************************************************/
function AtivarAjaxSplash() {
    $(document).ajaxStart(function () {
        $('body').find('.ajax-request-progress').css('display', 'block');
    }).ajaxStop(function () {
        $('body').find('.ajax-request-progress').css('display', 'none');
    });
}

function InativarAjaxSplash() {
    $(document).unbind('ajaxStart');
    $(document).unbind('ajaxStop');
}

/******************************************************************************************************/
/* AJAX */
/******************************************************************************************************/
function OnAjaxRequestError(XMLHttpRequest) {
    if (XMLHttpRequest.readyState === 0) {
        $('body').find('.ajax-request-progress').css('display', 'none');

        $('body').find('.ajax-request-disconnect').css('display', 'block');
    } else {
        $('body').find('.ajax-request-progress').css('display', 'block');

        $('body').find('.ajax-request-disconnect').css('display', 'none');
    }
}

function OnAjaxComplete(xhr, callback, objeto) {
    var statusCode = xhr.status;

    if (![0, 200, 400, 401, 405, 500].includes(statusCode)) {
        alert("Erro: " + statusCode + "\nCode: " + xhr.status + " (" + xhr.statusText + ")\nResponse: " + xhr.responseText);

        return;
    }

    if (statusCode === 401) {
        var jsonObject = ProcessarReponseJsonSemValidacao(xhr.responseText);

        var divBloqueioAcesso = $('body').find('.bloqueio-acesso');

        if (divBloqueioAcesso.html() === undefined) {
            ExibirMensagem('Erro', jsonObject['Message']);

            return;
        }

        if (divBloqueioAcesso.css('display') === 'block') {
            divBloqueioAcesso.find('.alert').css('display', 'block');

            divBloqueioAcesso.find('.alert').html(jsonObject['Message']);
        }

        divBloqueioAcesso.css('display', 'block');

        if (jsonObject['LoginItem'] !== undefined && jsonObject['LoginItem'] !== null) {
            $('#formLoginAcessoRenovacaoItem').find('h3').html(jsonObject['LoginItem']['NomeExibicao']);

            $('#formLoginAcessoRenovacaoItem').find('[name="Senha"]').css('display', '');

            $('#formLoginAcessoRenovacaoItem').find('a').first().css('display', '');
        }
    }

    if (callback !== null && callback !== undefined) {
        if (objeto !== undefined)
            callback(JSON.stringify({ "HttpStatusCode": xhr.status, "HttpStatusText": xhr.statusText, "HttpContent": xhr.responseText }), objeto);
        else
            callback(JSON.stringify({ "HttpStatusCode": xhr.status, "HttpStatusText": xhr.statusText, "HttpContent": xhr.responseText }));
    }
}

function OnAjaxProgress() {
    var xhr = new window.XMLHttpRequest();

    xhr.upload.addEventListener("progress",
        function (evt) {
            if (evt.lengthComputable) {
                var progress = Math.round((evt.loaded / evt.total) * 100);

            }
        }, false);

    return xhr;
}

function AjaxRequestPostAsync(url, data, callback) {
    try {
        var contentType = "application/x-www-form-urlencoded";

        $.support.cors = true;

        return new Promise((resolve, reject) => {
            return $.ajax({
                url: url, //"/Api/Cliente/Inserir",
                type: 'POST',
                data: '=' + encodeURIComponent(data),
                crossDomain: true,
                contentType: contentType,
                cache: false,
                async: true,
                success: function (data) {
                    resolve(data);
                },
                error: function (xhr) {
                    OnAjaxRequestError(xhr)

                    reject(xhr.responseText);
                },
                complete: function (xhr, statusText) {
                    OnAjaxComplete(xhr, callback, undefined);
                },
                xhr: OnAjaxProgress
            });
        }).catch(function (message) {
            //alert(message);
        });
        ;
    } catch (e) {
        ExibirMensagem('Erro Local', err.message);
    }
}

function AjaxRequestPostAsyncComObjeto(url, data, callback, objeto) {
    var contentType = "application/x-www-form-urlencoded; charset=utf-8";

    $.support.cors = true;

    return $.ajax({
        url: url, //"/Api/Cliente/Inserir",
        type: 'POST',
        data: '=' + encodeURIComponent(data),
        crossDomain: true,
        contentType: contentType,
        cache: false,
        async: true,
        success: function (data) { },
        error: OnAjaxRequestError,
        complete: function (xhr, statusText) {
            OnAjaxComplete(xhr, callback, objeto);
        }
    });
}

function AjaxRequestGetAsync(url, callback) {
    try {
        var contentType = "application/x-www-form-urlencoded";

        $.support.cors = true;

        return new Promise((resolve, reject) => {
            return $.ajax({
                url: url, //"/Api/Cliente/Inserir",
                type: 'GET',
                crossDomain: true,
                contentType: contentType,
                cache: false,
                async: true,
                success: function (data) {
                    resolve(data);
                },
                error: function (xhr) {
                    OnAjaxRequestError(xhr)

                    reject(xhr.responseText);
                },
                complete: function (xhr, statusText) {
                    OnAjaxComplete(xhr, callback, undefined);
                },
                xhr: OnAjaxProgress
            });
        });
    } catch (e) {
        ExibirMensagem('Erro Local', err.message);
    }
}

function ExecutarMetodoLista() {
    var metodoLista = Array.from(arguments);

    var promisseLista = [];

    $.each(metodoLista, function (metodoIndex, metodoItem) {
        var promisseItem = new Promise((resolve, reject) => {
            var result = undefined;

            if (metodoItem === undefined)
                result = metodoItem
            else
                result = metodoItem.catch(erro => { return erro });

            resolve(result);
        });

        promisseLista.push(promisseItem);
    });

    return Promise.all(
        promisseLista
    );
}

/*****************************************************************************************************/
/* PROCESSAMENTO DE REQUISIÇÃO */
/*****************************************************************************************************/
function ProcessarResponseJson(data) {
    var httpResponseMessage = JSON.parse(data);

    if (httpResponseMessage === null || httpResponseMessage === undefined)
        return null;

    var jsonObject = null;

    try {
        jsonObject = JSON.parse(httpResponseMessage.HttpContent);
    }
    catch (e) { }

    var mensagem = '';

    if (httpResponseMessage.HttpStatusCode === 400) {
        mensagem = 'Por gentileza, entre em contato com o suporte!<br/><br/>';

        mensagem += '<b>Mensagem:</b> ' + jsonObject['Message'] + '<br/>';

        mensagem += '<b>Classe/Módulo:</b> ' + jsonObject['TargetSite'] + '<br/>';

        mensagem += '<b>Linha:</b> ' + jsonObject['LineNumber'];

        ExibirMensagem('Erro de Requisição', mensagem);

        return null;
    } else if (httpResponseMessage.HttpStatusCode === 401) {
        return null;
    } else if (httpResponseMessage.HttpStatusCode === 500) {
        if (jsonObject === null) {
            mensagem = 'Por gentileza, entre em contato com o suporte e peça para o técnico verificar o console.';

            console.error(httpResponseMessage.HttpContent);
        }
        else {
            mensagem = jsonObject['Message'];
        }

        ExibirMensagem('Error', mensagem);

        return null;
    }

    return jsonObject;
}

function ProcessarReponseJsonSemValidacao(data) {
    try {
        var jsonObject = JSON.parse(data);

        return jsonObject;
    } catch (e) {

        return null;
    }
}

function ProcessarMetodoResultadoLista(metodoResultadoLista) {
    var metodoResultadoObjeto = {};

    if (Object.prototype.toString.call(metodoResultadoLista) == '[object String]')
        metodoResultadoObjeto = JSON.parse(metodoResultadoLista);

    if (Array.isArray(metodoResultadoLista)) {
        $(metodoResultadoLista)
            .each(function (index, item) {
                $.extend(metodoResultadoObjeto, item);
            });
    }

    return metodoResultadoObjeto;
}

/******************************************************************************************************/
/* Pegar QueryString via Ajax/Javascript */
/******************************************************************************************************/
function ObterQueryString(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

/*****************************************************************************************************/
/* Função de formatação de DATA para a API */
/*****************************************************************************************************/
function FormatarDataWebApi(data) {
    if (data === undefined || data === null || data === "" || data === '0001-01-01T00:00:00')
        return '';

    if (data.indexOf('.') >= 0)
        data = data.substr(0, data.indexOf('.'))

    if (data.indexOf('/') >= 0)
        data = data.substring(6, 10) + '-' + data.substring(3, 5) + '-' + data.substring(0, 2) + 'T00:00:00';

    if (data.substr(data.indexOf('T')).indexOf('-') <= 0)
        data = data + '-03:00'; //MALDITO UTC

    data = JSON.parse('"' + data + '"');

    var dataTeste = new Date(data);

    var dataTexto = ('0' + dataTeste.getDate()).slice(-2) + '/'
        + ('0' + (dataTeste.getMonth() + 1)).slice(-2) + '/'
        + dataTeste.getFullYear();

    if (dataTexto.length !== 10 || dataTexto === null || isNaN(dataTeste) || dataTexto === '01/01/1' || dataTexto === '0001-01-01T00:00:00')
        return '';

    return dataTexto;
}

function FormatarHoraWebApi(data) {
    if (data === null || data === "")
        return '';

    var dataObjeto = new Date(data).toLocaleDateString();

    if (dataObjeto === '02/01/1' || dataObjeto === null || dataObjeto === 'Invalid Date')
        return '';

    dataObjeto = new Date(data).toLocaleTimeString(navigator.language, {
        hour: '2-digit',
        minute: '2-digit'
    });

    //if (dataObjeto === '00:00')
    //    dataObjeto = '';

    return dataObjeto;
}

function FormatarDataHoraWebApi(data) {
    var dataWebApi = FormatarDataWebApi(data);

    var horaWebApi = FormatarHoraWebApi(data);

    var dataHoraWebApi = jQuery.trim(dataWebApi + ' ' + horaWebApi);

    if (dataWebApi === '')
        dataHoraWebApi = '';

    return dataHoraWebApi;
}

function ObterDataAtualWebApi(htmlButton) {
    var dataTeste = new Date();

    var dataAtual = dataTeste.getFullYear() + '-'
        + ('0' + (dataTeste.getMonth() + 1)).slice(-2) + '-'
        + ('0' + dataTeste.getDate()).slice(-2);

    dataAtual += "T00:00:00.000";

    if (htmlButton !== undefined && htmlButton !== null)
        $(htmlButton).parent().parent().find('input').val(dataAtual);

    return dataAtual;
}

function ObterHoraAtualWebApi(htmlButton) {
    var horaAtual = new Date().toLocaleTimeString(navigator.language, {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
    });

    horaAtual = '0001-01-01T' + horaAtual + '.000';

    if (htmlButton !== undefined && htmlButton !== null)
        $(htmlButton).parent().parent().find('input').val(horaAtual);

    return horaAtual;
}

function ObterDataHoraAtualWebApi(htmlButton) {
    var dataObjeto = new Date();

    var dataAtualWebApi = dataObjeto.getFullYear() + '-'
        + ('0' + (dataObjeto.getMonth() + 1)).slice(-2) + '-'
        + ('0' + dataObjeto.getDate()).slice(-2);

    var horaAtualWebApi = dataObjeto.toLocaleTimeString(navigator.language, {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
    });

    var dataHoraAtualApi = dataAtualWebApi + 'T' + horaAtualWebApi + '.000';

    if (htmlButton !== undefined && htmlButton !== null)
        $(htmlButton).parent().parent().find('input').val(dataHoraAtualApi);

    return dataHoraAtualApi;
}

function ObterDataAtual(htmlButton) {
    var dataAtualWebApi = ObterDataAtualWebApi(htmlButton);

    var dataAtual = FormatarDataWebApi(dataAtualWebApi);

    if (htmlButton !== undefined && htmlButton !== null)
        $(htmlButton).parent().parent().find('input').val(dataAtual);

    return dataAtual;
}

function ObterHoraAtual(htmlButton) {
    var horaAtualWebApi = ObterHoraAtualWebApi(htmlButton);

    var horaAtual = FormatarHoraWebApi(horaAtualWebApi);

    if (htmlButton !== undefined && htmlButton !== null)
        $(htmlButton).parent().parent().find('input').val(horaAtual);

    return horaAtual;
}

function ObterDataHoraAtual(htmlButton) {
    var dataAtual = ObterDataAtual(htmlButton);

    var horaAtual = ObterHoraAtual(htmlButton);

    var dataHoraAtual = dataAtual + ' ' + horaAtual;

    if (htmlButton !== undefined && htmlButton !== null)
        $(htmlButton).parent().parent().find('input').val(dataHoraAtual);

    return dataHoraAtual;
}

/*****************************************************************************************************/
/* VALIDAÇÃO DE FORMULÁRIO */
/*****************************************************************************************************/
function DefinirFormularioValidacaoRegraLista(formularioItem) {
    var inputLista = $(formularioItem).find('input');

    inputLista.push($(formularioItem).find('textarea'));

    var regraLista = {};

    for (var i = 0; i < inputLista.length; i++) {
        var inputItem = $(inputLista[i]);

        var equalTo = inputItem.attr('equal');

        if (equalTo !== undefined)
            regraLista[inputItem.attr('name')] = { 'equalTo': '#' + equalTo };

        if (inputItem.attr('data-type') === 'decimal')
            regraLista[inputItem.attr('name')] = { 'decimal': true };

        if (inputItem.attr('data-type') === 'datetime')
            regraLista[inputItem.attr('name')] = { 'datetime': true }

        if (inputItem.attr('data-type') === 'date')
            regraLista[inputItem.attr('name')] = { 'date': true };

        if (inputItem.attr('data-type') === 'integer')
            regraLista[inputItem.attr('name')] = { 'integer': true };

        if (inputItem.attr('data-type') === 'decimal')
            regraLista[inputItem.attr('name')] = { 'decimal': true };
    }

    return $(formularioItem).validate({
        rules: regraLista,
        ignore: '',
        errorPlacement: function (error, element) {
            var container = $(element).parent();

            if (container.hasClass('input-group'))
                container = container.parent();

            container.append(error);
        }
    });

}

function ValidarFormulario(formularioItem) {
    DispararEventoOnBlurNoFormulario(formularioItem)

    DefinirFormularioValidacaoRegraLista(formularioItem);

    var formularioValido = $(formularioItem).valid();

    var tabPane = $(formularioItem).closest('.tab-pane');

    var tabId = tabPane.attr('id');

    var indicador = $(tabPane).parent().parent().find('[href="#' + tabId + '"] span');

    if (!formularioValido && ($(indicador).html() === null || $(indicador).html() === undefined))
        $(tabPane).parent().parent().find('[href="#' + tabId + '"]').append('<span class="badge badge-danger" style="margin-left: 5px;"><i style="margin: 0;" class="fa fa-exclamation-triangle"></i></span>');
    else if (formularioValido && $(indicador).html() !== null && $(indicador).html() !== undefined)
        $(indicador).remove();

    return formularioValido;
}

function LimparFormularioValidacao(formularioItem) {
    DefinirFormularioValidacaoRegraLista(formularioItem).resetForm();
}

function AoPressionarEnter(sender, event, funcao, parametro) {
    if (event.keyCode === 13) {
        if (parametro !== undefined && parametro !== null)
            funcao(parametro);
        else
            funcao();

        event.preventDefault();

        return false;
    }
}

function AoPressionarEsc(sender, event, funcao, parametro) {
    if (event.keyCode === 27) {
        if (parametro !== undefined && parametro !== null)
            funcao(parametro);
        else
            funcao();

        event.preventDefault();

        return false;
    }
}

function AoPressionarHashtag(sender, event, funcao, parametro) {
    if (event.keyCode === 35) {
        if (parametro !== undefined && parametro !== null)
            funcao(parametro);
        else
            funcao();

        //event.preventDefault();

        return true;
    }
}

/*****************************************************************************************************/
/* Validação de Dígito numérico */
/*****************************************************************************************************/
function OnBlurValidarCampoInteiro(input) {
    if (input.value === '')
        input.value = 0;

    input.value = input.value.toString().replace(',', '.');
}

function OnBlurValidarCampoDecimal(input, casasDecimais) {
    var item = $(input);

    if (item.val() === '')
        item.val(0);

    if (casasDecimais === null || casasDecimais === undefined)
        casasDecimais = 2;

    item.val((item.val().toString().replace(',', '.') / 1).toFixed(casasDecimais));
}

function OnBlurDateTime(input) {
    // se houver só data, adicionar a hora atual (24/05/2021)
    input.value = input.value;
}

/*****************************************************************************************************/
/* EXIBIÇÃO DE OBJETO NO FORMULÁRIO */
/*****************************************************************************************************/
function ExibirObjetoNoFormulario(formularioItem, objetoItem) {
    if (formularioItem === undefined || formularioItem === null)
        return;

    if (objetoItem === undefined || objetoItem === null)
        return;

    formularioItem = $(formularioItem);

    $.each(objetoItem, function (key, value) {
        var inputItem = formularioItem.find('[name="' + key + '"]');

        if (inputItem.attr('data-type') === 'date') {
            inputItem.val(FormatarDataWebApi(value).trim());
        } else if (inputItem.attr('data-type') === 'time') {
            inputItem.val(FormatarHoraWebApi(value).trim());
        } else if (inputItem.attr('data-type') === 'datetime') {
            inputItem.val(FormatarDataHoraWebApi(value).trim());
        } else {
            formularioItem.find('[name="' + key + '"]').val(value);
        }
    });

    DispararEventoOnBlurNoFormulario(formularioItem);
}

/*****************************************************************************************************/
/* Função de dropdown */
/*****************************************************************************************************/
function ExibirDropdownLista(dropdownId, jsonObject) {
    var container = $('select#' + dropdownId);

    var html = '';

    $.each(jsonObject, function (index, item) {
        var nome = item['Nome'] !== undefined ? item['Nome'] : item['Titulo'];

        html += '<option value="' + item['Id'] + '">' + nome + '</option>';
    });

    container.html(html);

    container.prop('selectedIndex', 0);
}

function ExibirDropdownListaNoObjeto(container, jsonObject) {
    var html = '';

    $.each(jsonObject, function (index, item) {
        var nome = item['Nome'] !== undefined ? item['Nome'] : item['Titulo'];

        html += '<option value="' + item['Id'] + '">' + nome + '</option>';
    });

    $(container).html(html);

    $(container).prop('selectedIndex', 0);
}

function ExibirDropdownListaNoObjetoComSelecione(container, jsonObject) {
    var html = '<option value="">SELECIONE...</option>';

    $.each(jsonObject, function (index, item) {
        var nome = item['Nome'] !== undefined ? item['Nome'] : item['Titulo'];

        html += '<option value="' + item['Id'] + '">' + nome + '</option>';
    });

    $(container).html(html);

    $(container).prop('selectedIndex', 0);
}

/*****************************************************************************************************/
/* MENSAGENS */
/*****************************************************************************************************/
function ExibirMensagem(titulo, conteudo, callback) {
    var itemHtml = $(document).find('div.bootbox-mensagem');

    if (itemHtml.html() !== undefined && itemHtml.html() !== null) // impede abrir 300 notificações
        return;

    return bootbox.dialog({
        className: 'bootbox-mensagem',
        animate: true,
        message: conteudo,
        title: titulo,
        buttons: {
            main: {
                label: "OK!",
                className: "btn-success",
                callback: function () {
                    if (callback !== null && callback !== undefined && typeof callback === 'function')
                        callback();
                }
            }
        }
    });
}

function ExibirConfirmacao(titulo, conteudo, confirmacaoCallback, cancelamentoCallback, finalizacaoCallback) {
    return bootbox.dialog({
        className: 'bootbox-confirmacao',
        animate: false,
        title: titulo,
        message: conteudo,
        buttons: {
            cancel: {
                label: "Cancelar",
                className: "btn-danger",
                callback: function () {
                    if (cancelamentoCallback !== null && cancelamentoCallback !== undefined)
                        cancelamentoCallback();

                    if (finalizacaoCallback !== null && finalizacaoCallback !== undefined)
                        finalizacaoCallback();
                }
            },
            main: {
                label: "OK!",
                className: "btn-success",
                callback: function () {
                    if (confirmacaoCallback !== null && confirmacaoCallback !== undefined)
                        confirmacaoCallback();

                    if (finalizacaoCallback !== null && finalizacaoCallback !== undefined)
                        finalizacaoCallback();
                }
            }
        }
    });
}

/*****************************************************************************************************/
/* FUNÇÕES DE CONVERSÃO */
/*****************************************************************************************************/
function DispararEventoOnBlurNoFormulario(container) {
    container = $(container);

    var htmlLista = [];

    var inputLista = container.find('input');

    $.each(inputLista, function (inputIndex, inputItem) {
        htmlLista.push(inputItem);
    });

    var selectLista = container.find('select');

    $.each(selectLista, function (selectIndex, selectItem) {
        htmlLista.push(selectItem);
    });

    var textareaLista = container.find('textarea');

    $.each(textareaLista, function (textareaIndex, textareaItem) {
        htmlLista.push(textareaItem);
    });

    $.each(htmlLista, function (htmlIndex, htmlItem) {
        htmlItem = $(htmlItem);

        htmlItem.blur();
    });
}

function DispararEventoOnBlurNoInput(input) {
    $(input).blur();
}

(function ($) {
    $.fn.toJsonObject = function () {
        DispararEventoOnBlurNoFormulario(this);

        var result = {};

        var inputDisabled = this.find(':input:disabled').removeAttr('disabled');

        var inputDefault = [];

        var inputLista = this.find('input');

        for (var i = 0; i < inputLista.length; i++) {
            if ($(inputLista[i]).val() === '' && $(inputLista[i]).attr('default') !== '' && $(inputLista[i]).attr('default') !== undefined && $(inputLista[i]).attr('default') !== null) {
                $(inputLista[i]).val($(inputLista[i]).attr('default'));

                inputDefault.push(inputLista[i]);
            }
        }

        var a = this.serializeArray();

        for (var i = 0; i < inputDefault.length; i++)
            $(inputDefault[i]).val('');

        inputDisabled.attr('disabled', 'disabled');

        $.each(a, function () {
            if (result[this.name] !== undefined) {
                if (!result[this.name].push) {
                    result[this.name] = [result[this.name]];
                }
                result[this.name].push(this.value || '');
            } else {
                result[this.name] = this.value || '';
            }
        });

        return result;
    };

    $.fn.toJsonList = function () {
        var result = [];

        var a = this.toJsonObject();

        $.each(a, function (key, value) {
            if (Array.isArray(value))
                $.each(value, function (keyIndex, keyItem) {
                    var item = result[keyIndex];

                    if (item !== undefined) { // existe já
                        if (item[key] === undefined) {// não existe o nome da propriedade
                            item[key] = keyItem;
                        }
                    } else {
                        item = {};

                        item[key] = keyItem;

                        result.push(item);
                    }
                });
        });

        if (result.length === 0 && !jQuery.isEmptyObject(a))
            result.push(a);

        return result;
    };

    $.fn.ConverterParaObjetoItem = function () {
        DispararEventoOnBlurNoFormulario(this);

        var htmlLista = [];

        var inputLista = this.find('input');

        $.each(inputLista, function (inputIndex, inputItem) {
            htmlLista.push(inputItem);
        });

        var selectLista = this.find('select');

        $.each(selectLista, function (selectIndex, selectItem) {
            htmlLista.push(selectItem);
        });

        var textareaLista = this.find('textarea');

        $.each(textareaLista, function (textareaIndex, textareaItem) {
            htmlLista.push(textareaItem);
        });

        var objetoItem = {};

        $.each(htmlLista, function (htmlIndex, htmlItem) {
            htmlItem = $(htmlItem);

            var valor = undefined;

            if (htmlItem.val() === '' && htmlItem.attr('default') !== '' && htmlItem.attr('default') !== undefined && htmlItem.attr('default') !== null) {
                valor = htmlItem.attr('default');
            }
            else {
                valor = htmlItem.val();
            }

            if (valor !== '' && valor !== undefined && valor !== null && htmlItem.attr('data-type') === 'datetime') {
                var ano = valor.substring(6, 10);

                var mes = valor.substring(3, 5);

                var dia = valor.substring(0, 2);

                var hora = valor.substring(11, 13);

                var minuto = valor.substring(14, 16);

                valor = ano + '-' + mes + '-' + dia + 'T' + hora + ':' + minuto + ':00.000';
            }

            if (objetoItem[htmlItem.attr('name')] === undefined || objetoItem[htmlItem.attr('name')] === null)
                objetoItem[htmlItem.attr('name')] = valor;
            else {
                if (!Array.isArray(objetoItem[htmlItem.attr('name')]))
                    objetoItem[htmlItem.attr('name')] = [objetoItem[htmlItem.attr('name')]];

                objetoItem[htmlItem.attr('name')].push(valor);
            }
        });

        return objetoItem;
    };

    $.fn.ConverterParaObjetoLista = function () {
        var objetoLista = [];

        var inputTagName = this[0].tagName.toLowerCase();

        if (inputTagName !== 'table') {
            var objetoItem = $(this).ConverterParaObjetoItem();

            $.each(objetoItem, function (objetoKey, objetoValue) {
                if (Array.isArray(objetoValue))
                    $.each(objetoValue, function (keyIndex, keyValue) {
                        var item = objetoLista[keyIndex];

                        if (item !== undefined) {
                            if (item[objetoKey] === undefined) {
                                item[objetoKey] = keyValue;
                            }
                        } else {
                            item = {};

                            item[objetoKey] = keyValue;

                            objetoLista.push(item);
                        }
                    });
            });

            if (objetoLista.length === 0 && !jQuery.isEmptyObject(objetoItem))
                objetoLista.push(objetoItem);
        } else if (inputTagName === 'table') {
            var dataTableDataLista = $(this).DataTable().rows().data();

            $.each(dataTableDataLista, function (dataTableDataIndex, dataTableDataItem) {
                objetoLista.push(dataTableDataItem);
            });
        }

        return objetoLista;
    };
})(jQuery);

/*****************************************************************************************************/
/* GUID */
/*****************************************************************************************************/
function ObterGuid() {
    var d = new Date().getTime();

    if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
        d += performance.now(); //use high-precision timer if available
    }

    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;

        d = Math.floor(d / 16);

        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });

    //function s4() {
    //    return Math.floor((1 + Math.random()) * 0x10000)
    //        .toString(16)
    //        .substring(1);
    //}

    //return s4() + s4() + s4() + s4() + s4() + s4() + s4() + s4();
}

function ImprimirConteudoHtml(content) {
    var popup = window.open('', 'Print-Window');

    var url = location.protocol + '//' + location.hostname + (location.port ? ':' + parseInt(location.port) : '');

    var linkLista = $('link');

    var scriptLista = $('script');

    var html = '';

    html += '<html>\n';
    html += '    <head>\n';

    $.each(linkLista, function (linkIndex, linkItem) {
        var linkHref = $(linkItem).attr('href');

        if (linkHref !== undefined && linkHref !== null && linkHref !== '')
            html += '        <link href="' + url + linkHref + '" rel="stylesheet" />\n';
    });

    $.each(scriptLista, function (scriptIndex, scriptItem) {
        var scriptSrc = $(scriptItem).attr('src');

        if (scriptSrc !== undefined && scriptSrc !== null && scriptSrc !== '')
            html += '        <script src="' + url + scriptSrc + '"></script>\n';
    });

    html += '    </head>\n';
    html += '    <body class="gray-bg" onload="window.print()">\n';
    html += '        <div class="panel-body animated fadeIn">\n';
    html += content + '\n';
    html += '        </div>\n';
    html += '    </body>\n';
    html += '</html>\n';

    popup.document.open();

    popup.document.write(html);

    popup.document.close();

    setTimeout(function () {
        popup.close();
    }, 10);
}

function AplicarMascaraNoForm(form) {
    LimparMascaraNoForm(form);

    var inputLista = $(form).find('input');

    for (var i = 0; i < inputLista.length; i++) {
        AplicarMascaraNoInput(inputLista[i]);
    }
}

function LimparMascaraNoForm(form) {
    var inputLista = $(form).find('input');

    for (var i = 0; i < inputLista.length; i++)
        LimparMascaraNoInput($(inputLista[i]));
}

function AplicarMascaraNoInput(inputItem) {
    inputItem = $(inputItem);

    var mascaraDate = '00/00/0000';

    var mascaraMesAno = '00/0000';

    var mascaraDateTime = '00/00/0000 00:00';

    var mascaraCep = '00000-000';

    var mascaraCelular = '(00) 00000-0000';

    LimparMascaraNoInput(inputItem);

    if (inputItem.attr('data-type') === 'date')
        inputItem.mask(mascaraDate, { clearIfNotMatch: true });

    if (inputItem.attr('data-type') === 'datetime')
        inputItem.mask(mascaraDateTime, { clearIfNotMatch: true });

    if (inputItem.attr('data-type') === 'mes-ano')
        inputItem.mask(mascaraMesAno, { clearIfNotMatch: true });

    if (inputItem.attr('data-type') === 'cep')
        inputItem.mask(mascaraCep, { clearIfNotMatch: true });

    if (inputItem.attr('data-type') === 'celular')
        inputItem.mask(mascaraCelular, { clearIfNotMatch: true });
}

function LimparMascaraNoInput(input) {
    $(input).unmask();
}

/*****************************************************************************************************/
/* Função de bloqueio e desbloqueio dos itens de um formulário */
/*****************************************************************************************************/
function DesbloquearFormulario(formularioId) {
    DesbloquearFormularioInput(formularioId);

    DesbloquearFormularioSelect(formularioId);

    DesbloquearFormularioButton(formularioId);

    DesbloquearFormularioLink(formularioId);

    DesbloquearFormularioTextArea(formularioId);

    DesbloquearFormularioSpan(formularioId);
}

function BloquearFormulario(formularioId) {
    BloquearFormularioInput(formularioId);

    BloquearFormularioSelect(formularioId);

    BloquearFormularioButton(formularioId);

    BloquearFormularioLink(formularioId);

    BloquearFormularioTextArea(formularioId);

    BloquearFormularioSpan(formularioId);
}

function DesbloquearFormularioInput(formularioId) {
    $.each($('#' + formularioId + ' input'), function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('readonly');
    });
}

function DesbloquearFormularioSelect(formularioId) {
    $.each($('#' + formularioId + ' select'), function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function DesbloquearFormularioButton(formularioId) {
    $.each($('#' + formularioId + ' button'), function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function DesbloquearFormularioLink(formularioId) {
    $.each($('#' + formularioId + ' a'), function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function DesbloquearFormularioTextArea(formularioId) {
    $.each($('#' + formularioId + ' textarea'), function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('readonly');
    });
}

function DesbloquearFormularioSpan(formularioId) {
    $.each($('#' + formularioId + ' span'), function (index, item) {
        if ($(item).hasClass('input-group-addon') && $(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function BloquearFormularioInput(formularioId) {
    $.each($('#' + formularioId + ' input'), function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('readonly', 'readonly');
    });
}

function BloquearFormularioSelect(formularioId) {
    $.each($('#' + formularioId + ' select'), function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearFormularioButton(formularioId) {
    $.each($('#' + formularioId + ' button'), function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearFormularioLink(formularioId) {
    $.each($('#' + formularioId + ' a'), function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearFormularioTextArea(formularioId) {
    $.each($('#' + formularioId + ' textarea'), function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearFormularioSpan(formularioId) {
    $.each($('#' + formularioId + ' span'), function (index, item) {
        if ($(item).hasClass('input-group-addon') && $(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

/*****************************************************************************************************/
/* Função de bloqueio e desbloqueio dos itens de um elemento */
/*****************************************************************************************************/
function DesbloquearElemento(elementoHtml) {
    DesbloquearElementoInput(elementoHtml);

    DesbloquearElementoSelect(elementoHtml);

    DesbloquearElementoButton(elementoHtml);

    DesbloquearElementoLink(elementoHtml);

    DesbloquearElementoTextArea(elementoHtml);

    DesbloquearElementoSpan(elementoHtml);
}

function BloquearElemento(elementoHtml) {
    BloquearElementoInput(elementoHtml);

    BloquearElementoSelect(elementoHtml);

    BloquearElementoButton(elementoHtml);

    BloquearElementoLink(elementoHtml);

    BloquearElementoTextArea(elementoHtml);

    BloquearElementoSpan(elementoHtml);
}

function DesbloquearElementoInput(elementoHtml) {
    var elementoLista = $(elementoHtml).find('input');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('readonly');
    });
}

function DesbloquearElementoSelect(elementoHtml) {
    var elementoLista = $(elementoHtml).find('select');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function DesbloquearElementoButton(elementoHtml) {
    var elementoLista = $(elementoHtml).find('button');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function DesbloquearElementoLink(elementoHtml) {
    var elementoLista = $(elementoHtml).find('a');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function DesbloquearElementoTextArea(elementoHtml) {
    var elementoLista = $(elementoHtml).find('textarea');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nounlock') === undefined)
            $(item).removeAttr('readonly');
    });
}

function DesbloquearElementoSpan(elementoHtml) {
    var elementoLista = $(elementoHtml).find('span');

    $.each(elementoLista, function (index, item) {
        if ($(item).hasClass('input-group-addon') && $(item).attr('nounlock') === undefined)
            $(item).removeAttr('disabled');
    });
}

function BloquearElementoInput(elementoHtml) {
    var elementoLista = $(elementoHtml).find('input');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('readonly', 'readonly');
    });
}

function BloquearElementoSelect(elementoHtml) {
    var elementoLista = $(elementoHtml).find('select');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearElementoButton(elementoHtml) {
    var elementoLista = $(elementoHtml).find('button');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearElementoLink(elementoHtml) {
    var elementoLista = $(elementoHtml).find('a');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearElementoTextArea(elementoHtml) {
    var elementoLista = $(elementoHtml).find('textarea');

    $.each(elementoLista, function (index, item) {
        if ($(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

function BloquearElementoSpan(elementoHtml) {
    var elementoLista = $(elementoHtml).find('span');

    $.each(elementoLista, function (index, item) {
        if ($(item).hasClass('input-group-addon') && $(item).attr('nolock') === undefined)
            $(item).attr('disabled', 'disabled');
    });
}

/*****************************************************************************************************/
/* Função de limpeza dos itens de um formulário */
/*****************************************************************************************************/
function LimparFormularioItem(formularioItem) {
    LimparFormularioInput(formularioItem);

    LimparFormularioSelect(formularioItem);

    LimparFormularioTextArea(formularioItem);

    LimparFormularioValidacao(formularioItem);
}

function LimparFormularioInput(formularioItem) {
    var inputLista = $(formularioItem).find('input');

    $.each(inputLista, function (index, item) {
        if ($(item).attr('min') !== undefined)
            $(item).val($(item).attr('min'));
        else if ($(item).attr('data-type') === 'number' || $(item).attr('name')?.substr($(item).attr('name').length - 2) === 'Id')
            $(item).val(0);
        else
            $(item).val('');
    });
}

function LimparFormularioSelect(formularioItem) {
    var inputLista = $(formularioItem).find('select');

    $.each(inputLista, function (index, item) {
        $(item).prop('selectedIndex', 0);
    });
}

function LimparFormularioTextArea(formularioItem) {
    var inputLista = $(formularioItem).find('textarea');

    $.each(inputLista, function (index, item) {
        $(item).val('');
    });
}

/*****************************************************************************************************/
/* Monitor de alterações de formulários */
/*****************************************************************************************************/
function MonitorarFormularioAlteracao() {
    $('.footer').find('i').css('color', '');

    function AlterarAlteracaoIndicador() {
        formularioAlterado = true;

        $('.footer').find('i').css('color', 'rgb(255,0,0)');
    }

    var formLista = $(document).find('form');

    MutationObserver = window.MutationObserver || window.WebKitMutationObserver;

    var config = {
        characterData: false,
        attributes: false,
        childList: true,
        subtree: false
    };

    var mutationObserver = new MutationObserver(function (mutations, observer) {
        AlterarAlteracaoIndicador();
    });

    $.each(formLista, function (formIndex, formItem) {
        if ($(formItem).attr('monitorar') === undefined)
            return;

        mutationObserver.observe(formItem, config);

        var inputLista = $(formItem).find('input');

        $.each(inputLista, function (inputIndex, inputItem) {
            $(inputItem).keydown(function () {
                AlterarAlteracaoIndicador(true);

                console.log('Input');
            });
        });

        var textareaLista = $(formItem).find('textarea');

        $.each(textareaLista, function (textareaIndex, textareaItem) {
            $(textareaItem).keydown(function () {
                AlterarAlteracaoIndicador(true);

                console.log('Textarea');
            });
        });

        var selectLista = $(formItem).find('select');

        $.each(selectLista, function (selectIndex, selectItem) {
            $(selectItem).change(function () {
                AlterarAlteracaoIndicador(true);

                console.log('Select');
            });
        });
    });
}