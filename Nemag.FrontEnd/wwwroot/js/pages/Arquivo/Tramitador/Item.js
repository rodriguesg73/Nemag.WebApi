var _arquivoTramitadorAcaoLista = undefined;

$(function () {
    ExecutarMetodoLista(
        CarregarArquivoTramitadorAcaoLista()
    ).then(metodoResultadoLista => {
        _arquivoTramitadorAcaoLista = ProcessarMetodoResultadoLista(metodoResultadoLista)['ArquivoTramitadorAcaoLista'];

        var arquivoTramitadorId = ObterQueryString('arquivoTramitadorId');

        if (arquivoTramitadorId !== '')
            CarregarArquivoTramitadorItem(arquivoTramitadorId);
    });

});

function CarregarArquivoTramitadorAcaoLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/CarregarArquivoTramitadorAcaoLista", JSON.stringify(jsonParametro), null);
}

function CarregarArquivoTramitadorItem(arquivoTramitadorId) {
    var jsonParametro = {
        'token': loginToken,
        'arquivoTramitadorId': arquivoTramitadorId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/CarregarArquivoTramitadorItem", JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var arquivoTramitadorItem = jsonObject['ArquivoTramitadorItem'];

        var arquivoTramitadorEmailLista = jsonObject['ArquivoTramitadorEmailLista'];

        var arquivoTramitadorFtpLista = jsonObject['ArquivoTramitadorFtpLista'];

        var arquivoTramitadorDiretorioLista = jsonObject['ArquivoTramitadorDiretorioLista'];

        ExibirObjetoNoFormulario($('#formArquivoTramitadorItem'), arquivoTramitadorItem);

        AdicionarArquivoTramitadorEmailLista(arquivoTramitadorEmailLista);

        AdicionarArquivoTramitadorFtpLista(arquivoTramitadorFtpLista);

        AdicionarArquivoTramitadorDiretorioLista(arquivoTramitadorDiretorioLista);
    });
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formArquivoTramitadorItem');

    formularioValido = ValidarFormulario('#formArquivoTramitadorEmailLista') && formularioValido;

    formularioValido = ValidarFormulario('#formArquivoTramitadorFtpLista') && formularioValido;

    formularioValido = ValidarFormulario('#formArquivoTramitadorDiretorioLista') && formularioValido;

    return formularioValido;
}

function ObterApiParametro() {
    var arquivoTramitadorItem = $('#formArquivoTramitadorItem').ConverterParaObjetoItem();

    var arquivoTramitadorEmailLista = $('#formArquivoTramitadorEmailLista').ConverterParaObjetoLista();

    var arquivoTramitadorFtpLista = $('#formArquivoTramitadorFtpLista').ConverterParaObjetoLista();

    var arquivoTramitadorDiretorioLista = $('#formArquivoTramitadorDiretorioLista').ConverterParaObjetoLista();

    var jsonParametro = {
        'token': loginToken,
        'arquivoTramitadorItem': arquivoTramitadorItem,
        'arquivoTramitadorEmailLista': arquivoTramitadorEmailLista,
        'arquivoTramitadorFtpLista': arquivoTramitadorFtpLista,
        'arquivoTramitadorDiretorioLista': arquivoTramitadorDiretorioLista
    };

    return jsonParametro;
}

function SalvarArquivoTramitadorItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarArquivoTramitadorItem, null);
}

function CallbackSalvarArquivoTramitadorItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/SalvarArquivoTramitadorItem", JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
    });
}

function HabilitarElementoArrastavel(formItem) {
    $(formItem)
        .sortable({
            items: "> .item",
            handle: '.handler'
        })
        .disableSelection();
}

function ExecutarArquivoTramitadorItem() {
    var arquivoTramitadorId = ObterQueryString('arquivoTramitadorId');

    var jsonParametro = {
        'token': loginToken,
        'arquivoTramitadorId': arquivoTramitadorId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/ExecutarArquivoTramitadorItem", JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;


    });
}

/* Email */

function AdicionarArquivoTramitadorEmailLista(arquivoTramitadorEmailLista) {
    if (arquivoTramitadorEmailLista !== undefined && arquivoTramitadorEmailLista !== null && arquivoTramitadorEmailLista.length > 0)
        $.each(arquivoTramitadorEmailLista, function (arquivoTramitadorEmailIndex, arquivoTramitadorEmailItem) {
            AdicionarArquivoTramitadorEmailItem(arquivoTramitadorEmailItem);
        });
    else
        ValidarArquivoTramitadorEmailListaCabecalho();
}

function AdicionarArquivoTramitadorEmailItem(arquivoTramitadorEmailItem) {
    var formArquivoTramitadorEmailLista = $('#formArquivoTramitadorEmailLista');

    var html = MontarArquivoTramitadorEmailItemHtml(arquivoTramitadorEmailItem);

    formArquivoTramitadorEmailLista.append(html);

    ValidarArquivoTramitadorEmailListaCabecalho();

    HabilitarElementoArrastavel(formArquivoTramitadorEmailLista);
}

function MontarArquivoTramitadorEmailItemHtml(arquivoTramitadorEmailItem) {
    var html = '';
    
    html += '<div class="row item"> \n';
    html += '    <input type="hidden" name="Id" value="0" /> \n';

    html += '    <div class="col-custom-6">\n';
    html += '        <select name="ArquivoTramitadorAcaoId" class="form-control" required></select>\n';
    html += '    </div>\n';

    html += '    <div class="col-custom-9"> \n';
    html += '        <input type="text" name="Servidor" class="form-control" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-4"> \n';
    html += '        <input type="text" name="Porta" class="form-control" data-type="integer" value="0" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-7"> \n';
    html += '        <input type="text" name="Usuario" class="form-control" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-7"> \n';
    html += '        <input type="text" name="Senha" class="form-control" /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-3"> \n';
    html += '        <div class="btn-group btn-block">\n';
    html += '            <a type="button" class="btn btn-primary handler" style="color: rgb(255,255,255);"><i class="fa fa-sort"></i></a> \n';
    html += '            <button type="button" class="btn btn-primary" onclick="RemoverArquivoTramitadorEmailItem($(this).parent().parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '        </div>\n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    ExibirDropdownListaNoObjeto($(htmlObject).find('[name="ArquivoTramitadorAcaoId"]'), _arquivoTramitadorAcaoLista);

    if (arquivoTramitadorEmailItem !== null && arquivoTramitadorEmailItem !== undefined && arquivoTramitadorEmailItem !== {})
        ExibirObjetoNoFormulario(htmlObject, arquivoTramitadorEmailItem);

    html = htmlObject;

    return html;
}

function RemoverArquivoTramitadorEmailItem(arquivoTramitadorEmailHtml) {
    $(arquivoTramitadorEmailHtml).remove();

    ValidarArquivoTramitadorEmailListaCabecalho();
}

function ValidarArquivoTramitadorEmailListaCabecalho() {
    var formArquivoTramitadorEmailLista = $('#formArquivoTramitadorEmailLista');

    var arquivoTramitadorEmailLista = $('#formArquivoTramitadorEmailLista').find('.item');

    var formTituloItem = '';

    if (arquivoTramitadorEmailLista === null || arquivoTramitadorEmailLista === undefined || arquivoTramitadorEmailLista.length === 0) {
        formTituloItem += '<div class="row titulo text-center"> \n';
        formTituloItem += '    <div class="col-md-12"><p>Não há registros a serem exibidos</p></div>';
        formTituloItem += '</div> \n';
    } else {
        formTituloItem += '<div class="row titulo"> \n';
        formTituloItem += '    <div class="col-custom-6"><label>Ação:</label></div> \n';
        formTituloItem += '    <div class="col-custom-9"><label>Servidor:</label></div> \n';
        formTituloItem += '    <div class="col-custom-4"><label>Porta:</label></div> \n';
        formTituloItem += '    <div class="col-custom-7"><label>Usuário:</label></div> \n';
        formTituloItem += '    <div class="col-custom-7"><label>Senha</label></div> \n';
        formTituloItem += '    <div class="col-custom-3"><label></label></div> \n';
        formTituloItem += '</div> \n';
    }

    formArquivoTramitadorEmailLista.find('.titulo').remove();

    formArquivoTramitadorEmailLista.prepend(formTituloItem);
}

/* Ftp */

function AdicionarArquivoTramitadorFtpLista(arquivoTramitadorFtpLista) {
    if (arquivoTramitadorFtpLista !== undefined && arquivoTramitadorFtpLista !== null && arquivoTramitadorFtpLista.length > 0)
        $.each(arquivoTramitadorFtpLista, function (arquivoTramitadorFtpIndex, arquivoTramitadorFtpItem) {
            AdicionarArquivoTramitadorFtpItem(arquivoTramitadorFtpItem);
        });
    else
        ValidarArquivoTramitadorFtpListaCabecalho();
}

function AdicionarArquivoTramitadorFtpItem(arquivoTramitadorFtpItem) {
    var formArquivoTramitadorFtpLista = $('#formArquivoTramitadorFtpLista');

    var html = MontarArquivoTramitadorFtpItemHtml(arquivoTramitadorFtpItem);

    AplicarMascaraNoForm(html);

    formArquivoTramitadorFtpLista.append(html);

    ValidarArquivoTramitadorFtpListaCabecalho();

    HabilitarElementoArrastavel(formArquivoTramitadorFtpLista);
}

function MontarArquivoTramitadorFtpItemHtml(arquivoTramitadorFtpItem) {
    var html = '';

    html += '<div class="row item"> \n';
    html += '    <input type="hidden" name="Id" value="0" /> \n';

    html += '    <div class="col-custom-6">\n';
    html += '        <select name="ArquivoTramitadorAcaoId" class="form-control" required></select>\n';
    html += '    </div>\n';

    html += '    <div class="col-custom-7"> \n';
    html += '        <input type="text" name="Servidor" class="form-control" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-3"> \n';
    html += '        <input type="text" name="Porta" class="form-control" data-type="integer" value="0" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-6"> \n';
    html += '        <input type="text" name="Usuario" class="form-control" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-6"> \n';
    html += '        <input type="text" name="Senha" class="form-control" /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-5"> \n';
    html += '        <input type="text" name="DiretorioUrl" class="form-control" /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-3"> \n';
    html += '        <div class="btn-group btn-block">\n';
    html += '            <a type="button" class="btn btn-primary handler" style="color: rgb(255,255,255);"><i class="fa fa-sort"></i></a> \n';
    html += '            <button type="button" class="btn btn-primary" onclick="RemoverArquivoTramitadorFtpItem($(this).parent().parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '        </div>\n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    ExibirDropdownListaNoObjeto($(htmlObject).find('[name="ArquivoTramitadorAcaoId"]'), _arquivoTramitadorAcaoLista);

    if (arquivoTramitadorFtpItem !== null && arquivoTramitadorFtpItem !== undefined && arquivoTramitadorFtpItem !== {})
        ExibirObjetoNoFormulario(htmlObject, arquivoTramitadorFtpItem);

    html = htmlObject;

    return html;
}

function RemoverArquivoTramitadorFtpItem(arquivoTramitadorFtpHtml) {
    $(arquivoTramitadorFtpHtml).remove();

    ValidarArquivoTramitadorFtpListaCabecalho();
}

function ValidarArquivoTramitadorFtpListaCabecalho() {
    var formArquivoTramitadorFtpLista = $('#formArquivoTramitadorFtpLista');

    var arquivoTramitadorFtpLista = $('#formArquivoTramitadorFtpLista').find('.item');

    var formTituloItem = '';

    if (arquivoTramitadorFtpLista === null || arquivoTramitadorFtpLista === undefined || arquivoTramitadorFtpLista.length === 0) {
        formTituloItem += '<div class="row titulo text-center"> \n';
        formTituloItem += '    <div class="col-md-12"><p>Não há registros a serem exibidos</p></div>';
        formTituloItem += '</div> \n';
    } else {
        formTituloItem += '<div class="row titulo"> \n';
        formTituloItem += '    <div class="col-custom-6"><label>Ação:</label></div> \n';
        formTituloItem += '    <div class="col-custom-7"><label>Servidor:</label></div> \n';
        formTituloItem += '    <div class="col-custom-3"><label>Porta:</label></div> \n';
        formTituloItem += '    <div class="col-custom-6"><label>Usuário:</label></div> \n';
        formTituloItem += '    <div class="col-custom-6"><label>Senha:</label></div> \n';
        formTituloItem += '    <div class="col-custom-5"><label>Diretório:</label></div> \n';
        formTituloItem += '    <div class="col-custom-3"><label></label></div> \n';
        formTituloItem += '</div> \n';
    }

    formArquivoTramitadorFtpLista.find('.titulo').remove();

    formArquivoTramitadorFtpLista.prepend(formTituloItem);
}

/* Diretorio */

function AdicionarArquivoTramitadorDiretorioLista(arquivoTramitadorDiretorioLista) {
    if (arquivoTramitadorDiretorioLista !== undefined && arquivoTramitadorDiretorioLista !== null && arquivoTramitadorDiretorioLista.length > 0)
        $.each(arquivoTramitadorDiretorioLista, function (arquivoTramitadorDiretorioIndex, arquivoTramitadorDiretorioItem) {
            AdicionarArquivoTramitadorDiretorioItem(arquivoTramitadorDiretorioItem);
        });
    else
        ValidarArquivoTramitadorDiretorioListaCabecalho();
}

function AdicionarArquivoTramitadorDiretorioItem(arquivoTramitadorDiretorioItem) {
    var formArquivoTramitadorDiretorioLista = $('#formArquivoTramitadorDiretorioLista');

    var html = MontarArquivoTramitadorDiretorioItemHtml(arquivoTramitadorDiretorioItem);

    formArquivoTramitadorDiretorioLista.append(html);

    ValidarArquivoTramitadorDiretorioListaCabecalho();

    HabilitarElementoArrastavel(formArquivoTramitadorDiretorioLista);
}

function MontarArquivoTramitadorDiretorioItemHtml(arquivoTramitadorDiretorioItem) {
    var html = '';

    html += '<div class="row item"> \n';
    html += '    <input type="hidden" name="Id" value="0" /> \n';
    
    html += '    <div class="col-custom-6">\n';
    html += '        <select name="ArquivoTramitadorAcaoId" class="form-control" required></select>\n';
    html += '    </div>\n';

    html += '    <div class="col-custom-27"> \n';
    html += '        <input type="text" name="DiretorioUrl" class="form-control" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-3"> \n';
    html += '        <div class="btn-group btn-block">\n';
    html += '            <a type="button" class="btn btn-primary handler" style="color: rgb(255,255,255);"><i class="fa fa-sort"></i></a> \n';
    html += '            <button type="button" class="btn btn-primary" onclick="RemoverArquivoTramitadorDiretorioItem($(this).parent().parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '        </div>\n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    ExibirDropdownListaNoObjeto($(htmlObject).find('[name="ArquivoTramitadorAcaoId"]'), _arquivoTramitadorAcaoLista);

    if (arquivoTramitadorDiretorioItem !== null && arquivoTramitadorDiretorioItem !== undefined && arquivoTramitadorDiretorioItem !== {})
        ExibirObjetoNoFormulario(htmlObject, arquivoTramitadorDiretorioItem);

    html = htmlObject;

    return html;
}

function RemoverArquivoTramitadorDiretorioItem(arquivoTramitadorDiretorioHtml) {
    $(arquivoTramitadorDiretorioHtml).remove();

    ValidarArquivoTramitadorDiretorioListaCabecalho();
}

function ValidarArquivoTramitadorDiretorioListaCabecalho() {
    var formArquivoTramitadorDiretorioLista = $('#formArquivoTramitadorDiretorioLista');

    var arquivoTramitadorDiretorioLista = $('#formArquivoTramitadorDiretorioLista').find('.item');

    var formTituloItem = '';

    if (arquivoTramitadorDiretorioLista === null || arquivoTramitadorDiretorioLista === undefined || arquivoTramitadorDiretorioLista.length === 0) {
        formTituloItem += '<div class="row titulo text-center"> \n';
        formTituloItem += '    <div class="col-md-12"><p>Não há registros a serem exibidos</p></div>';
        formTituloItem += '</div> \n';
    } else {
        formTituloItem += '<div class="row titulo"> \n';
        formTituloItem += '    <div class="col-custom-6"><label>Ação:</label></div> \n';
        formTituloItem += '    <div class="col-custom-27"><label>Diretório:</label></div> \n';
        formTituloItem += '    <div class="col-custom-3"><label></label></div> \n';
        formTituloItem += '</div> \n';
    }

    formArquivoTramitadorDiretorioLista.find('.titulo').remove();

    formArquivoTramitadorDiretorioLista.prepend(formTituloItem);
}