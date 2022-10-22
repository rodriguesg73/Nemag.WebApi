$(function () {
    ExecutarMetodoLista(
        CarregarLoginSituacaoLista(),
        CarregarPessoaContatoTipoLista()
    ).then(metodoResultadoLista => {
        var formLoginItem = $('#formLoginItem');

        _pessoaContatoTipoLista = ProcessarMetodoResultadoLista(metodoResultadoLista)['PessoaContatoTipoLista'];

        ExibirDropdownListaNoObjeto(formLoginItem.find('[name="LoginSituacaoId"]'), ProcessarMetodoResultadoLista(metodoResultadoLista)['LoginSituacaoLista']);

        var loginId = ObterQueryString('loginId');

        if (loginId !== '')
            CarregarLoginItem(loginId);
    });
});

function CarregarLoginSituacaoLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginSituacaoLista", JSON.stringify(jsonParametro), null);
}

function CarregarLoginItem(loginId) {
    var jsonParametro = {
        'token': loginToken,
        'loginId': loginId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/CarregarLoginItem", JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var loginItem = jsonObject['LoginItem'];

        var loginAtribuicaoLista = jsonObject['LoginAtribuicaoLista'];

        var pessoaContatoLista = jsonObject['PessoaContatoLista'];

        ExibirObjetoNoFormulario($('#formLoginItem'), loginItem);

        AdicionarLoginAtribuicaoLista(loginAtribuicaoLista);

        AdicionarPessoaContatoLista(pessoaContatoLista);
    });
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formLoginItem');

    formularioValido = ValidarFormulario('#formLoginAtribuicaoLista') && formularioValido;

    return formularioValido;
}

function ObterApiParametro() {
    var loginItem = $('#formLoginItem').ConverterParaObjetoItem();

    var loginAtribuicaoLista = $('#formLoginAtribuicaoLista').ConverterParaObjetoLista();

    var pessoaContatoLista = $('#formPessoaContatoLista').ConverterParaObjetoLista();

    var jsonParametro = {
        'token': loginToken,
        'loginItem': loginItem,
        'loginAtribuicaoLista': loginAtribuicaoLista,
        'pessoaContatoLista': pessoaContatoLista
    };

    return jsonParametro;
}

function SalvarLoginItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarLoginItem, null);
}

function CallbackSalvarLoginItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Login/SalvarLoginItem", JSON.stringify(jsonParametro), ResponseSalvarLoginItem);
}

function ResponseSalvarLoginItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}

/*******************************************************************************/
/* ATRIBUIÇÃO */
/*******************************************************************************/
function AdicionarLoginAtribuicaoLista(loginAtribuicaoLista) {
    if (loginAtribuicaoLista !== undefined && loginAtribuicaoLista !== null && loginAtribuicaoLista.length > 0)
        for (var i = 0; i < loginAtribuicaoLista.length; i++) {
            var loginAtribuicaoItem = loginAtribuicaoLista[i];

            AdicionarLoginAtribuicaoItem(loginAtribuicaoItem);
        }
    else
        ProcessarFormularioLoginAtribuicaoLista();
}

function AdicionarLoginAtribuicaoItem(loginAtribuicaoItem) {
    var formLoginAtribuicaoLista = $('#formLoginAtribuicaoLista');

    var html = MontarLoginAtribuicaoItemHtml(loginAtribuicaoItem);

    formLoginAtribuicaoLista.append(html);

    ProcessarFormularioLoginAtribuicaoLista();
}

function MontarLoginAtribuicaoItemHtml(loginAtribuicaoItem) {
    var html = '';

    html += '<div class="row item"> \n';
    html += '    <input type="hidden" name="Id" value="0" /> \n';
    html += '    <input type="hidden" name="LoginGrupoId" value="0" /> \n';
    html += '    <input type="hidden" name="LoginPerfilId" value="0" /> \n';
    html += '    <div class="col-md-5"> \n';
    html += '        <div class="input-group"> \n';
    html += '            <input type="text" name="LoginGrupoNome" class="form-control" required disabled /> \n';
    html += '            <span class="input-group-append"> \n';
    html += '                <button type="button" class="btn btn-primary" onclick="EfetuarPesquisaLoginGrupoItem($(this).parent().parent().parent().parent());"> \n';
    html += '                    <i class="fa fa-search"></i> \n';
    html += '                </button> \n';
    html += '            </span> \n';
    html += '        </div> \n';
    html += '    </div> \n';
    html += '    <div class="col-md-5"> \n';
    html += '        <div class="input-group"> \n';
    html += '            <input type="text" name="LoginPerfilNome" class="form-control" required disabled /> \n';
    html += '            <span class="input-group-append"> \n';
    html += '                <button type="button" class="btn btn-primary" onclick="EfetuarPesquisaLoginPerfilItem($(this).parent().parent().parent().parent());"> \n';
    html += '                    <i class="fa fa-search"></i> \n';
    html += '                </button> \n';
    html += '            </span> \n';
    html += '        </div> \n';
    html += '    </div> \n';
    html += '    <div class="col-md-1">&nbsp;</div> \n';
    html += '    <div class="col-md-1"> \n';
    html += '        <button type="button" class="btn btn-primary btn-block" onclick="RemoverLoginAtribuicaoItem($(this).parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    if (loginAtribuicaoItem !== null && loginAtribuicaoItem !== undefined && loginAtribuicaoItem !== {}) {
        $(htmlObject).find('[name="Id"]').val(loginAtribuicaoItem['Id']);

        $(htmlObject).find('[name="LoginGrupoId"]').val(loginAtribuicaoItem['LoginGrupoId']);

        $(htmlObject).find('[name="LoginGrupoNome"]').val(loginAtribuicaoItem['LoginGrupoNome']);

        $(htmlObject).find('[name="LoginPerfilId"]').val(loginAtribuicaoItem['LoginPerfilId']);

        $(htmlObject).find('[name="LoginPerfilNome"]').val(loginAtribuicaoItem['LoginPerfilNome']);
    }

    html = htmlObject;

    return html;
}

function RemoverLoginAtribuicaoItem(loginAtribuicaoHtml) {
    $(loginAtribuicaoHtml).remove();

    ProcessarFormularioLoginAtribuicaoLista();
}

function ProcessarFormularioLoginAtribuicaoLista() {
    var formLoginAtribuicaoLista = $('#formLoginAtribuicaoLista');

    var loginAtribuicaoLista = $('#formLoginAtribuicaoLista').find('.item');

    var formTituloItem = '';

    if (loginAtribuicaoLista === null || loginAtribuicaoLista === undefined || loginAtribuicaoLista.length === 0) {
        formTituloItem += '<div class="row titulo text-center"> \n';
        formTituloItem += '    <div class="col-md-12"><p>Não há registros a serem exibidos</p></div>';
        formTituloItem += '</div> \n';
    } else {
        formTituloItem += '<div class="row titulo"> \n';
        formTituloItem += '    <div class="col-md-5"><label>Grupo:</label></div> \n';
        formTituloItem += '    <div class="col-md-5"><label>Perfil:</label></div> \n';
        formTituloItem += '    <div class="col-md-2">&nbsp;</div> \n';
        formTituloItem += '</div> \n';
    }

    formLoginAtribuicaoLista.find('.titulo').remove();

    formLoginAtribuicaoLista.prepend(formTituloItem);
}

function EfetuarPesquisaLoginGrupoItem(loginAtribuicaoHtml) {
    EfetuarPesquisa('Selecione um grupo', apiUrl + '/Api/Login/CarregarLoginGrupoLista', null, EfetuarPesquisaLoginGrupoItemCallback, null, loginAtribuicaoHtml);
}

function EfetuarPesquisaLoginGrupoItemCallback(data, container) {
    $(container).find('[name="LoginGrupoId"]').val(data['Id']);

    $(container).find('[name="LoginGrupoNome"]').val(data['Nome']);
}

function EfetuarPesquisaLoginPerfilItem(loginAtribuicaoHtml) {
    EfetuarPesquisa('Selecione um perfil', apiUrl + '/Api/Login/CarregarLoginPerfilLista', null, EfetuarPesquisaLoginPerfilItemCallback, null, loginAtribuicaoHtml);
}

function EfetuarPesquisaLoginPerfilItemCallback(data, container) {
    $(container).find('[name="LoginPerfilId"]').val(data['Id']);

    $(container).find('[name="LoginPerfilNome"]').val(data['Nome']);
}

