$(function () {
    if (loginToken === undefined || loginToken === '')
        window.location.href = '/Login/Index';

    IniciarConfiguracao();

    CarregarLoginItemPorToken();

    RenderizarMenuLista();

    AtivarPluginLista();
});

function CarregarLoginItemPorToken() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Login/CarregarLoginItemPorToken', JSON.stringify(jsonParametro), ResponseCarregarLoginItemPorToken);
}

function ResponseCarregarLoginItemPorToken(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var loginItem = jsonObject['LoginItem'];

    var loginNotificacaoLista = jsonObject['LoginNotificacaoLista'];

    $('.login-nome').html(loginItem['NomeExibicao']);

    RenderizarLoginNotificacaoDropdownLista(loginNotificacaoLista);

    AtualizarLoginAcessoCookie(loginItem, null);
}

function RenderizarMenuLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Menu/CarregarMenuListaPorLoginToken', JSON.stringify(jsonParametro), ResponseRenderizarMenuLista);
}

function ResponseRenderizarMenuLista(data) {

    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var menuLista = jsonObject['MenuLista'];

    var menuContainer = $('#side-menu');

    for (var i = 0; i < menuLista.length; i++) {
        var menuItem = menuLista[i];

        if (menuItem['MenuSuperiorId'] === 0) {
            menuContainer.append('<li id="liMenuItem' + menuItem['Id'] + '"><a href="' + menuItem['WebUrl'] + '"><i class="fa ' + menuItem['IconeCss'] + '"></i><span class="nav-label">' + menuItem['Titulo'] + '</a></li>');
        } else {
            var menuSuperiorItem = $('#liMenuItem' + menuItem['MenuSuperiorId']);

            var menuNivel = IdentificarMenuItemNivel(menuLista, menuItem);

            if (menuSuperiorItem.find('a').find('.arrow').html() === undefined)
                menuSuperiorItem.find('a').append('<span class="fa arrow"></span>');

            if (menuSuperiorItem.find('ul').html() === undefined)
                if (menuNivel === 1)
                    menuSuperiorItem.append('<ul class="nav nav-second-level collapse"></ul>');
                else if (menuNivel === 2)
                    menuSuperiorItem.append('<ul class="nav nav-third-level collapse"></ul>');

            menuSuperiorItem.find('ul').append('<li id="liMenuItem' + menuItem['Id'] + '"><a href="' + menuItem['WebUrl'] + '">' + menuItem['Titulo'] + '</a></li>');
        }
    }

    IdentificarMenuAtivo();

    IdentificarBreadcumb();

    $('#side-menu').metisMenu();
}

function IdentificarMenuItemNivel(menuLista, menuItemAtual) {
    var nivel = 0;

    for (var i = 0; i < menuLista.length; i++) {
        var menuItem = menuLista[i];

        if (menuItemAtual['Id'] === menuItem['Id'])
            continue;

        if (menuItemAtual['MenuSuperiorId'] === menuItem['Id']) {
            nivel += 1;

            nivel += IdentificarMenuItemNivel(menuLista, menuItem);

            return nivel;
        }
    }

    return 0;
}

function IdentificarMenuAtivo(menuItemAtual) {
    var urlAtual = window.location.pathname;

    var menuContainer = $('#side-menu');

    if (window.location.pathname === '/' && menuItemAtual === undefined)
        menuItemAtual = menuContainer.children().eq(1).children();

    if (menuItemAtual === undefined || menuItemAtual.html() === undefined)
        menuItemAtual = menuContainer.find('a[href="' + urlAtual + '"]');

    if (ObterQueryString('ReturnUrl') !== null && ObterQueryString('ReturnUrl') !== undefined && ObterQueryString('ReturnUrl') !== '')
        urlAtual = ObterQueryString('ReturnUrl').replace(window.location.protocol + "//" + window.location.host, '');

    if (menuItemAtual === undefined || menuItemAtual.html() === undefined)
        menuItemAtual = menuContainer.find('a[href="' + urlAtual + '"]');

    if (menuItemAtual === undefined || menuItemAtual.html() === undefined)
        return undefined;

    menuItemAtual.parent().addClass('active');

    if (menuItemAtual.parent().parent().attr('id') !== 'side-menu')
        IdentificarMenuAtivo(menuItemAtual.parent().parent().parent().children());
}

function IdentificarBreadcumb(menuItemAtual) {
    var urlAtual = window.location.pathname;

    var menuContainer = $('#side-menu');

    var regex = /(<([^>]+)>)/ig;

    if (window.location.pathname === '/' && menuItemAtual === undefined)
        menuItemAtual = menuContainer.children().eq(1).children();

    if (menuItemAtual === undefined)
        menuItemAtual = menuContainer.find('a[href="' + urlAtual + '"]');

    if (ObterQueryString('ReturnUrl') !== null && ObterQueryString('ReturnUrl') !== undefined && ObterQueryString('ReturnUrl') !== '')
        urlAtual = ObterQueryString('ReturnUrl').replace(window.location.protocol + "//" + window.location.host, '');

    if (menuItemAtual === undefined || menuItemAtual.html() === undefined)
        menuItemAtual = menuContainer.find('a[href="' + urlAtual + '"]');

    if (menuItemAtual === undefined || menuItemAtual.html() === undefined)
        return undefined;

    if (menuItemAtual.parent().parent().attr('id') !== 'side-menu')
        IdentificarBreadcumb(menuItemAtual.parent().parent().parent().children());

    if (window.location.pathname === menuItemAtual.attr('href')) {
        $('#breadcrumb').append('<li class="breadcrumb-item active"><strong>' + menuItemAtual.html().replace(regex, "") + '</strong></li>');
    } else {
        $('#breadcrumb').append('<li class="breadcrumb-item"><a href="' + menuItemAtual.attr('href') + '">' + menuItemAtual.html().replace(regex, "") + '</a></li>');
    }
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

    document.location.reload(true);
}

function AtivarPluginLista() {
    // Chosen
    $('.chosen-select').chosen({ width: "100%" });
}

function ResolverComputadorNomePorIp(htmlItem) {
    var ip = $(htmlItem).parent().parent().find('input[type="text"]').first().val();

    var jsonParametro = {
        'token': loginToken,
        'ip': ip
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Util/ResolverComputadorNomePorIp', JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var computadorNome = jsonObject['ComputadorNome']

        $(htmlItem).parent().parent().find('input[type="text"]').first().val(computadorNome);
    });
}

function ObterIpRemoto(htmlItem) {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Util/ObterIpRemoto', JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var ipRemoto = jsonObject['ip']

        $(htmlItem).parent().parent().find('input[type="text"]').first().val(ipRemoto);
    });
}

function EfetuarLogout() {
    ExibirConfirmacao('Login', 'Deseja realmente efetuar o logout?', function () {
        $.removeCookie('loginToken');

        window.location.href = '/Login/Index';
    }, null)
}

function RenderizarLoginNotificacaoDropdownLista(loginNotificacaoLista) {
    $(loginNotificacaoLista)
        .each(function (index, item) {
            if (item['IconeCss'] == '')
                item['IconeCss'] = 'fa-envelope';

            var html = '';

            html += '<li>\n'
            html += '    <input type="hidden" name="Id" value="' + item['Id'] + '"/>\n'
            html += '    <div class="row">\n'
            html += '        <div class="col-custom-32">\n'
            html += '            <a href="' + item['LinkUrl'] + '" class="dropdown-item">\n'
            html += '                <div><i class="fa ' + item['LoginNotificacaoTipoIconeCss'] + ' fa-fw"></i> <strong>' + item['Titulo'] + '</strong></div>\n'
            html += '                <div><span class="text-muted small">' + item['Conteudo'] + '</span></div>\n';
            html += '            </a>\n'
            html += '        </div>\n'
            html += '        <div class="col-custom-4">\n'
            html += '            <button type="button" onclick="RemoverLoginNotificacaoDropdownItem($(this));" class="btn btn-xs btn-primary btn-block"><i class="fa fa-trash-o"></i></button>\n'
            html += '        </div>\n'
            html += '    </div>\n'
            html += '</li>\n'
            html += '<li class="dropdown-divider"></li>\n'

            $(html).insertBefore('ul.dropdown-alerts li:last-child');
        });

    AtualizarLoginNotificacaoDropdownListaInterface();
}

function RemoverLoginNotificacaoDropdownItem(buttonHtml) {
    var loginNotificacaoDropdownItem = $(buttonHtml).closest('li');

    var loginNotificacaoItem = $(loginNotificacaoDropdownItem).ConverterParaObjetoItem();

    var jsonParametro = {
        'token': loginToken,
        'loginNotificacaoItem': loginNotificacaoItem
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Login/ExcluirLoginNotificacaoItem', JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        loginNotificacaoDropdownItem.next().remove();

        loginNotificacaoDropdownItem.remove();

        AtualizarLoginNotificacaoDropdownListaInterface();
    });
}

function ExibirLoginNotificacaoDropdownLista(dropdownContainer) {
    var OcultarLoginNotificacaoDropdownNoClick = function (e) {
        if (!dropdownContainer.parent().is(e.target) && dropdownContainer.parent().has(e.target).length === 0 && dropdownContainer.css('display') !== 'none') {
            dropdownContainer.hide();

            $(document).off('mousedown.login-notificacao-lista');
        }
    };

    if ($(dropdownContainer).css('display') === 'none') {
        dropdownContainer.show();

        $(document).on('mousedown.login-notificacao-lista', OcultarLoginNotificacaoDropdownNoClick);
    } else {
        dropdownContainer.hide();

        $(document).off('mousedown.login-notificacao-lista');
    }
}

function LimparLoginNotificacaoDropdownLista() {
    var loginNotificacaoLista = $('.login-notificacao-dropdown-lista ul').ConverterParaObjetoLista();

    var jsonParametro = {
        'token': loginToken,
        'loginNotificacaoLista': loginNotificacaoLista
    };

    return AjaxRequestPostAsync(apiUrl + '/Api/Login/ExcluirLoginNotificacaoLista', JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        $(loginNotificacaoLista).each(function (index, item) {
            var loginNotificacaoDropdownItem = $('.login-notificacao-dropdown-lista').find('[value="' + item['Id'] + '"]').parent();

            loginNotificacaoDropdownItem.next().remove();

            loginNotificacaoDropdownItem.remove();
        });

        AtualizarLoginNotificacaoDropdownListaInterface();
    });
}

function AtualizarLoginNotificacaoDropdownListaInterface() {
    var loginNotificacaoLista = $('.login-notificacao-dropdown-lista ul').ConverterParaObjetoLista();

    $('.login-notificacao-dropdown-lista span.label').removeClass('label-danger');

    $('.login-notificacao-dropdown-lista span.label').html(loginNotificacaoLista.length);

    if (loginNotificacaoLista.length > 0)
        $('.login-notificacao-dropdown-lista span.label').addClass('label-danger');
}