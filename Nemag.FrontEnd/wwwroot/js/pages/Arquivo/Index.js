$(function () {
    loginToken = '24c9ffc420e54795a9309038adcc974468e02bfef05b4820a699d26e98829248';

    VincularArquivoControle($('#inputArquivoLista'), $('#formArquivoLista'));
});

function VincularArquivoControle(input, form) {
    input = $(input);

    if (form === undefined || form === null)
        form = $(input).closest('form').parent().parent().find('form').last();

    input.on('change', function () {
        var files = this.files;

        var arquivoNovoLista = [];

        var arquivoAtualLista = form.toJsonList();

        var arquivoErroLista = [];

        var arquivoExistente = false;

        for (var i = 0; i < this.files.length; i++) {
            var item = {
                Id: 0,
                ArquivoId: 0,
                Nome: files[i].name,
                DataInclusao: ''
            };

            if (arquivoAtualLista != undefined && arquivoAtualLista != null)
                for (var a = 0; a < arquivoAtualLista.length; a++) {
                    if (arquivoAtualLista[a].Nome == item.Nome) {
                        arquivoExistente = true;

                        return;
                    }
                }

            if (!arquivoExistente)
                arquivoNovoLista.push(item);
            else
                arquivoErroLista.push(item);

            arquivoExistente = false;
        }

        if (arquivoErroLista.length > 0)
            ExibirMensagem('Atenção', 'Alguns arquivos já foram submetidos e/ou já existem na lista. Para que seja possível adicionar tais arquivos, remova os existentes e tente novamente.');

        if ($(this).attr('multiple') === undefined) {
            LimparArquivoLista(form);
        }

        $.each(arquivoNovoLista, function (arquivoIndex, arquivoItem) {
            AdicionarArquivoItem(form, arquivoItem);
        });

        EfetuarArquivoListaUpload(form, this, null, null);
    });
}

function EfetuarArquivoListaUpload(form, input, progress, completeCallback) {
    form = $(form);

    if (progress === undefined || progress === null)
        progress = $('.ajax-request-progress').find('.progress-bar');

    progress.parent().parent().fadeToggle();

    var formData = new FormData();

    var loginTokenUpload = loginToken;

    if (loginTokenUpload === undefined || loginTokenUpload === '')
        loginTokenUpload = '24c9ffc420e54795a9309038adcc974468e02bfef05b4820a699d26e98829248';

    formData.append("Token", loginTokenUpload);

    var inputFileLista = $(input).prop('files');

    $.each(inputFileLista, function (inputFileIndex, inputFileItem) {
        formData.append("FormFileLista", inputFileItem);
    });

    $.ajax(
        {
            url: apiUrl + "/Api/Arquivo/EfetuarArquivoUpload",
            data: formData,
            crossDomain: true,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) { },
            error: OnAjaxRequestError,
            complete: function (xhr, statusText) {
                OnAjaxComplete(xhr, function (data) {
                    var jsonObject = ProcessarResponseJson(data);

                    if (jsonObject === null) return;

                    var arquivoLista = jsonObject['arquivoLista'];

                    $.each(arquivoLista, function (arquivoIndex, arquivoItem) {
                        var itemHtml = form.find('[value="' + arquivoItem['Nome'] + '"]').parent();

                        itemHtml.find('[name="ArquivoId"]').val(arquivoItem['Id']);

                        itemHtml.find('[name="DataInclusao"]').val(arquivoItem['DataInclusao']);
                    });

                    AtualizarArquivoInterface(form);

                    progress.parent().parent().fadeToggle();

                    if ($(input).attr('multiple') === undefined) {
                        $(input).parent().removeClass('btn-warning').removeClass('btn-danger').addClass('btn-primary');

                        $(input).parent().find('span').find('i').removeClass('fa-plus').addClass('fa-check');

                        $(input).parent().parent().find('small').html(arquivoLista[0]['Nome']);
                    }
                }, undefined);
            },
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress",
                    function (evt) {
                        if (evt.lengthComputable) {
                            var progressValue = Math.round((evt.loaded / evt.total) * 100);

                            $(progress).css('width', progressValue + '%').attr('aria-valuenow', progressValue);
                        }
                    },
                    false);

                return xhr;
            }
        }
    );
}

function EfetuarDownloadArquivoItem(arquivoId) {
    window.open(apiUrl + '/Api/Arquivo/EfetuarDownload?arquivoId=' + arquivoId);
}

function VisualizarArquivoItem(arquivoGuid) {
    // ABRIR MODALL COM A EXECUÇÃO DO ARQUIVO EM QUESTÃO: SE FOR VIDEO: PLAYER, AUDIO: PLAYER, IMAGEM: GALLEY E ETC
}

function AdicionarArquivoLista(form, arquivoLista) {
    if (arquivoLista.length === 0 && $('form').find('p').html() !== undefined)
        return;

    LimparArquivoLista(form);

    $.each(arquivoLista, function (arquivoIndex, arquivoItem) {
        form = $(form);

        var htmlItem = MontarArquivoHtml(arquivoItem);

        form.append(htmlItem);
    });

    AtualizarArquivoInterface(form);

    ValidarArquivoListaCabecalho(form);
}

function AdicionarArquivoItem(form, arquivoItem) {
    form = $(form);

    var htmlItem = MontarArquivoHtml(arquivoItem);

    form.append(htmlItem);

    AtualizarArquivoInterface(form);

    ValidarArquivoListaCabecalho(form);
}

function MontarArquivoHtml(arquivoItem) {
    var html = '';

    html += '<div class="file-box col-md-3">\n';
    html += '    <div class="file">\n';
    html += '        <input type="hidden" name="Id" value="0" />\n';
    html += '        <input type="hidden" name="Guid" value="" />\n';
    html += '        <input type="hidden" name="Nome" value="" />\n';
    html += '        <input type="hidden" name="ArquivoGuid" value="" />\n';
    html += '        <input type="hidden" name="ArquivoId" value="0" />\n';
    html += '        <input type="hidden" name="ArquivoNome" value="" />\n';
    html += '        <input type="hidden" name="DataInclusao" value="" />\n';

    html += '        <div class="thumb"></div>\n';

    html += '        <div class="file-name" data-toggle="tooltip" data-placement="top" data-original-title="Tooltip on top"></div>\n';
    html += '        <div class="row">\n';
    html += '            <div class="col-lg-12">\n';
    html += '                <div class="btn-group">\n';
    html += '                    <a href="javascript:;" data-toggle="dropdown" class="btn btn-primary btn-xs dropdown-toggle">Opções&nbsp;</a>\n';
    html += '                    <ul class="dropdown-menu">\n';
    html += '                        <li><a href="javascript:;" class="dropdown-item" onclick="EfetuarDownloadArquivoItem(' + arquivoItem['ArquivoId'] + ');">Download</a></li>\n';
    html += '                        <li class="dropdown-divider"></li>\n';
    html += '                        <li><a href="javascript:;" class="dropdown-item" onclick="RemoverArquivoItem($(this));">Remover</a></li>\n';
    html += '                    </ul>\n';
    html += '                </div>\n';
    html += '            </div>\n';
    html += '        </div>\n';
    html += '    </div>\n';
    html += '</div>\n';

    var htmlObjeto = $.parseHTML(html);

    if (arquivoItem !== null && arquivoItem !== undefined)
        ExibirObjetoNoFormulario(htmlObjeto, arquivoItem);

    var guid = ObterGuid();

    $(htmlObjeto).find('[name="Guid"]').val(guid);

    $(htmlObjeto).find('[name="DataInclusao"]').val(arquivoItem['DataInclusao']);

    return htmlObjeto;
}

function RemoverArquivoItem(menuHtml) {
    var form = menuHtml.closest('form');

    var arquivoHtml = $(menuHtml);

    if (!arquivoHtml.hasClass('file-box'))
        arquivoHtml = $(menuHtml).closest('.file-box');

    arquivoHtml.remove();

    AtualizarArquivoInterface(form);

    ValidarArquivoListaCabecalho(form);
}

function IdentificarArquivoIcone(arquivoItem) {
    var arquivoNome = arquivoItem['Nome'];

    var arquivoNomeSplit = arquivoNome.split('.');

    var mimeType = arquivoNomeSplit[arquivoNomeSplit.length - 1].toLowerCase();

    var arquivoThumbUrl = '';

    switch (mimeType) {
        case 'zip':
        case 'rar':
        case 'gz':
        case 'tar':
            arquivoThumbUrl = 'fa-file-archive-o';
            break;

        case 'txt':
            arquivoThumbUrl = 'fa-file-text-o';
            break;

        case 'msg':
            arquivoThumbUrl = 'fa-envelope-o';
            break;

        case 'pdf':
            arquivoThumbUrl = 'fa-file-pdf-o';
            break;

        case 'doc':
        case 'docx':
            arquivoThumbUrl = 'fa-file-word-o';
            break;

        case 'xls':
        case 'xlsx':
        case 'csv':
            arquivoThumbUrl = 'fa-file-excel-o';
            break;

        case 'ppt':
        case 'pptx':
            arquivoThumbUrl = 'fa-file-powerpoint-o';
            break;

        case 'ogg':
        case 'mp3':
        case 'wma':
            arquivoThumbUrl = 'fa-file-audio-o';
            break;

        case 'png':
        case 'jpeg':
        case 'jpg':
        case 'bmp':
        case 'gif':
        case 'mpeg':
        case 'mp4':
        case 'wmv':
        case 'avi':
            arquivoThumbUrl = apiUrl + '/Api/Arquivo/CarregarMiniatura?arquivoId=' + arquivoItem['ArquivoId'] + '&token=' + loginToken;
            break;

        default:
            arquivoThumbUrl = 'fa-question';
            break;
    }

    return arquivoThumbUrl;

}

function AtualizarArquivoListaInterface(form, arquivoLista) {
    $.each(arquivoLista, function (arquivoIndex, arquivoItem) {
        AtualizarArquivoItemInterface(form, arquivoItem);
    });
}

function AtualizarArquivoItemInterface(form, arquivoItem) {
    var itemHtml = form
        .find('[value="' + arquivoItem['Guid'] + '"]')
        .parent()
        .parent();

    if (arquivoItem['DataInclusao'] === undefined || arquivoItem['DataInclusao'] === null)
        arquivoItem['DataInclusao'] = ObterDataAtualWebApi();

    if (arquivoItem['Nome'] === undefined || arquivoItem['Nome'] === null || arquivoItem['Nome'] === '')
        arquivoItem['Nome'] = arquivoItem['ArquivoNome'];

    var arquivoNome = arquivoItem['Nome'];

    var arquivoNomeSplit = arquivoNome.split('.');

    var mimeType = arquivoNomeSplit[arquivoNomeSplit.length - 1];

    var htmlFileName = arquivoNome + '<br><small>' + FormatarDataWebApi(arquivoItem['DataInclusao']) + ' ' + FormatarHoraWebApi(arquivoItem['DataInclusao']) + '</small>';

    itemHtml.find('.file-name').html(htmlFileName);

    itemHtml.find('.file-name').attr('data-original-title', arquivoNome);

    var arquivoThumbUrl = IdentificarArquivoIcone(arquivoItem);

    var arquivoThumbHtml = '';

    if (['png', 'jpeg', 'jpg', 'bmp', 'gif', 'mpeg', 'mp4', 'wmv', 'avi'].includes(mimeType)) {
        arquivoThumbHtml += '        <div class="image">\n';
        arquivoThumbHtml += '            <img alt="image" class="img-fluid" src="' + arquivoThumbUrl + '">\n';
        arquivoThumbHtml += '        </div>\n';
    } else {
        arquivoThumbHtml += '        <div class="icon">\n';
        arquivoThumbHtml += '            <i class="fa ' + arquivoThumbUrl + '"></i>\n';
        arquivoThumbHtml += '        </div>\n';
    }

    itemHtml.find('.thumb').html(arquivoThumbHtml);

    AtualizarArquivoTabPane(form);
}

function AtualizarArquivoInterface(form) {
    form = $(form);

    var arquivoLista = form.toJsonList();

    AtualizarArquivoListaInterface(form, arquivoLista);

    AtualizarArquivoTabPane(form);
}

function ValidarArquivoListaCabecalho(form) {
    form = $(form);

    var arquivoLista = form.toJsonList();

    form.find('p').first().remove();

    if (arquivoLista.length === 0)
        form.append('<p class="text-center">Nenhum arquivo a ser exibido</p>');
}

function EfetuarArquivoPesquisa(input) {
    var formArquivoLista = $(input).closest('form').parent().parent().find('form').last();

    var arquivoLista = formArquivoLista.toJsonList();

    var termo = $(input).val();

    termo = termo.replace(/[^\x20-\x7E]/g, '').toUpperCase();

    $.each(arquivoLista, function (arquivoIndex, arquivoItem) {
        var itemHtml = formArquivoLista
            .find('[value="' + arquivoItem['ArquivoGuid'] + '"]')
            .parent()
            .parent();

        var arquivoNome = arquivoItem['ArquivoNome'].toUpperCase();

        if (!arquivoNome.includes(termo) && termo !== '')
            itemHtml.css('display', 'none');
        else
            itemHtml.css('display', '');
    });
}

function AtualizarArquivoTabPane(form) {
    form = $(form);

    var arquivoLista = form.toJsonList();

    var tabPane = form.closest('.tab-pane');

    var tabId = tabPane.attr('id');

    $(tabPane).parent().parent().find('[href="#' + tabId + '"] span').remove();

    if (arquivoLista.length > 0)
        $(tabPane).parent().parent().find('[href="#' + tabId + '"]').append('<span class="badge badge-warning" style="margin-left: 5px;padding-left: 5px;">' + arquivoLista.length + '</span>');
}

function LimparArquivoLista(form) {
    $(form).html('');
}

function CompactarArquivoLista(input) {
    ExibirConfirmacao('Confirmação', 'Deseja continuar?', function () {
        var form = $(input).closest('form').parent().parent().find('form').last();

        var arquivoLista = $(form).ConverterParaObjetoLista();

        $.each(arquivoLista, function (index, item) {
            item['Id'] = item['ArquivoId'];
        });

        var jsonParametro = {
            'token': loginToken,
            'arquivoLista': arquivoLista
        }

        return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/CompactarArquivoLista", JSON.stringify(jsonParametro), function (data) {
            var jsonObject = ProcessarResponseJson(data);

            if (jsonObject === null) return;

            var arquivoItem = jsonObject['ArquivoItem'];

            LimparArquivoLista(form);

            arquivoItem['ArquivoId'] = arquivoItem['Id'];

            arquivoItem['Id'] = '0';

            AdicionarArquivoItem(form, arquivoItem);

            ExibirMensagem('Sucesso!', 'Arquivos compactados com sucesso!');
        });
    }, null);
}

function DescompactarArquivoLista(input) {
    ExibirConfirmacao('Confirmação', 'Deseja continuar?', function () {
        var form = $(input).closest('form').parent().parent().find('form').last();

        var arquivoLista = $(form).ConverterParaObjetoLista();

        $.each(arquivoLista, function (index, item) {
            item['Id'] = item['ArquivoId'];
        });

        var jsonParametro = {
            'token': loginToken,
            'arquivoLista': arquivoLista
        }

        return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/DescompactarArquivoLista", JSON.stringify(jsonParametro), function (data) {
            var jsonObject = ProcessarResponseJson(data);

            if (jsonObject === null) return;

            var arquivoLista = jsonObject['ArquivoLista'];

            $.each(arquivoLista, function (index, item) {
                item['ArquivoId'] = item['Id'];

                item['Id'] = '0';
            })

            LimparArquivoLista(form);

            AdicionarArquivoLista(form, arquivoLista);

            ExibirMensagem('Sucesso!', 'Arquivos descompactados com sucesso!');
        });
    }, null);
}

function ExcluirArquivoLista(input) {
    ExibirConfirmacao('Confirmação', 'Deseja realmente excluir tudo?', function () {
        var form = $(input).closest('form').parent().parent().find('form').last();

        LimparArquivoLista(form);

        AtualizarArquivoInterface(form);

        ValidarArquivoListaCabecalho(form);
    }, null);
}

function EfetuarDownloadArquivoLista(input) {
    ExibirConfirmacao('Confirmação', 'Deseja continuar?', function () {
        var form = $(input).closest('form').parent().parent().find('form').last();

        var arquivoLista = $(form).ConverterParaObjetoLista();

        $.each(arquivoLista, function (index, item) {
            item['Id'] = item['ArquivoId'];
        });

        var jsonParametro = {
            'token': loginToken,
            'arquivoLista': arquivoLista
        }

        return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/CompactarArquivoLista", JSON.stringify(jsonParametro), function (data) {
            var jsonObject = ProcessarResponseJson(data);

            if (jsonObject === null) return;

            var arquivoItem = jsonObject['ArquivoItem'];

            window.open(apiUrl + '/Api/Arquivo/EfetuarDownload?arquivoId=' + arquivoItem['Id']);
        });
    }, null);
}
