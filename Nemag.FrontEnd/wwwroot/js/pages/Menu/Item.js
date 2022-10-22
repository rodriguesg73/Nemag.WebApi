$(function () {
    ExecutarMetodoLista(
        CarregarMenuLista()
    ).then(metodoResultadoLista => {
        ExibirMenuSuperiorLista(ProcessarMetodoResultadoLista(metodoResultadoLista)['MenuLista']);

        var menuId = ObterQueryString('menuId');

        if (menuId !== '')
            CarregarMenuItem(menuId);
    });
});

function CarregarMenuLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Menu/CarregarMenuLista", JSON.stringify(jsonParametro), null);
}

function CarregarMenuItem(menuId) {
    var jsonParametro = {
        'token': loginToken,
        'menuId': menuId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Menu/CarregarMenuItem", JSON.stringify(jsonParametro), ResponseCarregarMenuLista);
}

function ResponseCarregarMenuLista(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var menuItem = jsonObject['MenuItem'];

    var menuPermissaoAtribuicaoLista = jsonObject['MenuPermissaoAtribuicaoLista'];

    ExibirObjetoNoFormulario($('#formMenuItem'), menuItem);

    ExibirFormularioMenuPermissaoAtribuicaoLista(menuPermissaoAtribuicaoLista);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formMenuItem');

    formularioValido = ValidarFormulario('#formMenuPermissaoAtribuicaoLista') && formularioValido;

    return formularioValido;
}

function ObterApiParametro() {
    var menuItem = $('#formMenuItem').toJsonObject();

    var menuPermissaoAtribuicaoLista = $('#formMenuPermissaoAtribuicaoLista').toJsonList();

    var jsonParametro = {
        'token': loginToken,
        'menuItem': menuItem,
        'menuPermissaoAtribuicaoLista': menuPermissaoAtribuicaoLista
    };

    return jsonParametro;
}

function SalvarMenuItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarMenuItem, null);
}

function CallbackSalvarMenuItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Menu/SalvarMenuItem", JSON.stringify(jsonParametro), ResponseSalvarMenuItem);
}

function ResponseSalvarMenuItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}

function ExcluirMenuItem() {
    var menuItem = $('#formMenuItem').toJsonObject();

    if (menuItem['Id'] === '0') {
        ExibirMensagem('Erro', 'O item é novo, como você pretende excluir algo que não existe? Não vai ser possível..');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente excluir?', CallbackExcluirMenuItem, null);
}

function CallbackExcluirMenuItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Menu/ExcluirMenuItem", JSON.stringify(jsonParametro), ResponseExcluirMenuItem);
}

function ResponseExcluirMenuItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}


function ExibirMenuSuperiorLista(menuLista) {
    menuLista.splice(0, 0, { 'Id': 0, 'MenuSuperiorId': 0, 'Titulo': 'Selecione...' });

    ExibirDropdownListaNoObjeto($('#formMenuItem').find('[name="MenuSuperiorId"]'), menuLista);
}

/*******************************************************************************/
/* Atribuição */
/*******************************************************************************/
function ExibirFormularioMenuPermissaoAtribuicaoLista(menuPermissaoAtribuicaoLista) {
    if (menuPermissaoAtribuicaoLista !== undefined && menuPermissaoAtribuicaoLista !== null && menuPermissaoAtribuicaoLista.length > 0)
        for (var i = 0; i < menuPermissaoAtribuicaoLista.length; i++) {
            var menuPermissaoAtribuicaoItem = menuPermissaoAtribuicaoLista[i];

            AdicionarMenuPermissaoAtribuicaoItem(menuPermissaoAtribuicaoItem);
        }
    else
        ProcessarFormularioMenuPermissaoAtribuicaoLista();
}

function MontarMenuPermissaoAtribuicaoItemHtml(menuPermissaoAtribuicaoItem) {
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
    html += '        <button type="button" class="btn btn-primary btn-block" onclick="RemoverMenuPermissaoAtribuicaoItem($(this).parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    if (menuPermissaoAtribuicaoItem !== null && menuPermissaoAtribuicaoItem !== undefined && menuPermissaoAtribuicaoItem !== {}) {
        $(htmlObject).find('[name="Id"]').val(menuPermissaoAtribuicaoItem['Id']);

        $(htmlObject).find('[name="LoginGrupoId"]').val(menuPermissaoAtribuicaoItem['LoginGrupoId']);

        $(htmlObject).find('[name="LoginGrupoNome"]').val(menuPermissaoAtribuicaoItem['LoginGrupoNome']);

        $(htmlObject).find('[name="LoginPerfilId"]').val(menuPermissaoAtribuicaoItem['LoginPerfilId']);

        $(htmlObject).find('[name="LoginPerfilNome"]').val(menuPermissaoAtribuicaoItem['LoginPerfilNome']);
    }

    html = htmlObject;

    return html;
}

function AdicionarMenuPermissaoAtribuicaoItem(menuPermissaoAtribuicaoItem) {
    var formMenuPermissaoAtribuicaoLista = $('#formMenuPermissaoAtribuicaoLista');

    var html = MontarMenuPermissaoAtribuicaoItemHtml(menuPermissaoAtribuicaoItem);

    formMenuPermissaoAtribuicaoLista.append(html);

    ProcessarFormularioMenuPermissaoAtribuicaoLista();
}

function RemoverMenuPermissaoAtribuicaoItem(menuPermissaoAtribuicaoHtml) {
    $(menuPermissaoAtribuicaoHtml).remove();

    ProcessarFormularioMenuPermissaoAtribuicaoLista();
}

function ProcessarFormularioMenuPermissaoAtribuicaoLista() {
    var formMenuPermissaoAtribuicaoLista = $('#formMenuPermissaoAtribuicaoLista');

    var menuPermissaoAtribuicaoLista = $('#formMenuPermissaoAtribuicaoLista').find('.item');

    var formTituloItem = '';

    if (menuPermissaoAtribuicaoLista === null || menuPermissaoAtribuicaoLista === undefined || menuPermissaoAtribuicaoLista.length === 0) {
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

    formMenuPermissaoAtribuicaoLista.find('.titulo').remove();

    formMenuPermissaoAtribuicaoLista.prepend(formTituloItem);
}

function EfetuarPesquisaLoginGrupoItem(menuPermissaoAtribuicaoHtml) {
    EfetuarPesquisa('Selecione um grupo', apiUrl + '/Api/Login/CarregarLoginGrupoLista', null, EfetuarPesquisaLoginGrupoItemCallback, null, menuPermissaoAtribuicaoHtml);
}

function EfetuarPesquisaLoginGrupoItemCallback(data, container) {
    $(container).find('[name="LoginGrupoId"]').val(data['Id']);

    $(container).find('[name="LoginGrupoNome"]').val(data['Nome']);
}

function EfetuarPesquisaLoginPerfilItem(menuPermissaoAtribuicaoHtml) {
    EfetuarPesquisa('Selecione um perfil', apiUrl + '/Api/Login/CarregarLoginPerfilLista', null, EfetuarPesquisaLoginPerfilItemCallback, null, menuPermissaoAtribuicaoHtml);
}

function EfetuarPesquisaLoginPerfilItemCallback(data, container) {
    $(container).find('[name="LoginPerfilId"]').val(data['Id']);

    $(container).find('[name="LoginPerfilNome"]').val(data['Nome']);
}
