function EfetuarPesquisa(titulo, apiPesquisaUrl, apiParametro, concluirCallback, cancelarCallback, container, propriedadeEspelhadaLista, propriedadeExibicaoLista) {
    var html = '';

    html += '<div class="modal inmodal fade" tabindex="-1" role="dialog" aria-hidden="true">\n';
    html += '    <div class="modal-dialog modal-lg">\n';
    html += '        <div class="modal-content">\n';
    html += '            <div class="modal-header">\n';
    html += '                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>\n';
    html += '                <h4 class="modal-title">' + titulo + '</h4>\n';
    html += '            </div>\n';
    html += '            <div class="modal-body">\n';

    html += '                <div class="table-responsive">\n';
    html += '                    <table id="tablePesquisaLista" class="table table-striped table-bordered table-hover dataTables-example" style="width:100%;">\n';
    html += '                        <thead>\n';
    html += '                            <tr>\n';
    html += '                            </tr>\n';
    html += '                        </thead>\n';
    html += '                    </table>\n';
    html += '                </div>\n';

    html += '            </div>\n';
    html += '            <div class="modal-footer">\n';
    html += '                <a href="javascript:;" class="btn btn-white">Fechar</a>\n';
    html += '                <a href="javascript:;" class="btn btn-primary">Selecionar</a>\n';
    html += '            </div>\n';
    html += '        </div>\n';
    html += '    </div>\n';
    html += '</div>\n';

    $('body').append(html);

    $('body').find('.modal:last').on('hidden.bs.modal', function (e) {
        $('body').find('.modal:last').remove();
    });

    $('body').find('.modal:last').find('.btn-white').on('click', function (e) {
        $('body').find('.modal:last').modal('hide');

        if (typeof cancelarCallback === 'function') {
            cancelarCallback(container);
        }
    });

    $('body').find('.modal:last').find('.btn-primary').on('click', function (e) {
        $('body').find('.modal:last').modal('hide');

        var objetoSelecionadoItem = ObterDataTableLinhaSelecionadaItem('#tablePesquisaLista');

        if (typeof concluirCallback === 'function') {
            concluirCallback(objetoSelecionadoItem, container);
        }
    });

    if (apiParametro === null || apiParametro === undefined)
        apiParametro = {};

    if (apiParametro['token'] === null || apiParametro['token'] === undefined)
        apiParametro['token'] = loginToken;

    return AjaxRequestPostAsync(apiPesquisaUrl, JSON.stringify(apiParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var resultadoLista = jsonObject[Object.keys(jsonObject)[0]];

        var tituloDicionarioItem = jsonObject['TituloDicionarioItem'];

        if (propriedadeEspelhadaLista !== undefined && propriedadeEspelhadaLista !== null && propriedadeEspelhadaLista.length > 0) {
            $.each(propriedadeEspelhadaLista, function (dataAliasIndex, dataAliasItem) {
                $.each(resultadoLista, function (resultadoIndex, resultadoItem) {
                    this[dataAliasItem['propriedadeEspelhadaNome']] = this[dataAliasItem['propriedadeAtualNome']];
                });
            });
        }

        var colunaLista = [];

        if (propriedadeExibicaoLista !== undefined && propriedadeExibicaoLista !== null && propriedadeExibicaoLista.length > 0) {
            $.each(propriedadeExibicaoLista, function (propriedadeExibicaoIndex, propriedadeExibicaoItem) {
                colunaLista.push({
                    'Titulo': propriedadeExibicaoItem['Titulo'],
                    'Nome': propriedadeExibicaoItem['Nome']
                });
            });
        }

        if ((propriedadeExibicaoLista === undefined || propriedadeExibicaoLista === null) && tituloDicionarioItem !== undefined && tituloDicionarioItem !== null) {
            var tituloDicionarioKeyLista = Object.keys(tituloDicionarioItem)

            if (tituloDicionarioKeyLista !== undefined && tituloDicionarioKeyLista !== null && tituloDicionarioKeyLista.length > 0) {
                $.each(tituloDicionarioKeyLista, function (tituloDicionarioKeyIndex, tituloDicionarioKeyItem) {
                    if (tituloDicionarioKeyItem.includes('Nome') || tituloDicionarioKeyItem.includes('Codigo') || tituloDicionarioKeyItem.includes('Titulo') || tituloDicionarioKeyItem.includes('Usuario'))
                        colunaLista.push({
                            'Titulo': tituloDicionarioItem[tituloDicionarioKeyItem],
                            'Nome': tituloDicionarioKeyItem
                        });
                });
            }
        }

        InicializarPesquisaDataTable(resultadoLista, colunaLista);

        AdicionarDataTableLinhaLista('#tablePesquisaLista', resultadoLista);

        ExibirModal($('body').find('.modal:last'));
    });
}

function EfetuarPesquisaLista(titulo, apiPesquisaUrl, apiParametro, concluirCallback, cancelarCallback, container, propriedadeEspelhadaLista, propriedadeExibicaoLista) {
    var html = '';

    html += '<div class="modal inmodal fade" tabindex="-1" role="dialog" aria-hidden="true">\n';
    html += '    <div class="modal-dialog modal-lg">\n';
    html += '        <div class="modal-content">\n';
    html += '            <div class="modal-header">\n';
    html += '                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>\n';
    html += '                <h4 class="modal-title">' + titulo + '</h4>\n';
    html += '            </div>\n';
    html += '            <div class="modal-body">\n';

    html += '                <div class="table-responsive">\n';
    html += '                    <table id="tablePesquisaLista" class="table table-striped table-bordered table-hover dataTables-example" style="width:100%;">\n';
    html += '                        <thead>\n';
    html += '                            <tr>\n';
    html += '                            </tr>\n';
    html += '                        </thead>\n';
    html += '                    </table>\n';
    html += '                </div>\n';

    html += '            </div>\n';
    html += '            <div class="modal-footer">\n';
    html += '                <a href="javascript:;" class="btn btn-white">Fechar</a>\n';
    html += '                <a href="javascript:;" class="btn btn-primary">Selecionar</a>\n';
    html += '            </div>\n';
    html += '        </div>\n';
    html += '    </div>\n';
    html += '</div>\n';

    $('body').append(html);

    $('body').find('.modal:last').on('hidden.bs.modal', function (e) {
        $('body').find('.modal:last').remove();
    });

    $('body').find('.modal:last').find('.btn-white').on('click', function (e) {
        $('body').find('.modal:last').modal('hide');

        if (typeof cancelarCallback === 'function') {
            cancelarCallback(container);
        }
    });

    $('body').find('.modal:last').find('.btn-primary').on('click', function (e) {
        $('body').find('.modal:last').modal('hide');

        var objetoSelecionadoLista = ObterDataTableLinhaSelecionadaLista('#tablePesquisaLista');

        if (typeof concluirCallback === 'function') {
            concluirCallback(objetoSelecionadoLista, container);
        }
    });

    if (apiParametro === null || apiParametro === undefined)
        apiParametro = {};

    if (apiParametro['token'] === null || apiParametro['token'] === undefined)
        apiParametro['token'] = loginToken;

    return AjaxRequestPostAsync(apiPesquisaUrl, JSON.stringify(apiParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var resultadoLista = jsonObject[Object.keys(jsonObject)[0]];

        if (resultadoLista.length === 0) {
            ExibirMensagem('Erro', 'Não há registros a serem exibidos');

            return;
        }

        if (propriedadeEspelhadaLista !== undefined && propriedadeEspelhadaLista !== null && propriedadeEspelhadaLista.length > 0) {
            $.each(propriedadeEspelhadaLista, function (dataAliasIndex, dataAliasItem) {
                $.each(resultadoLista, function (resultadoIndex, resultadoItem) {
                    this[dataAliasItem['propriedadeEspelhadaNome']] = this[dataAliasItem['propriedadeAtualNome']];
                });
            });
        }

        var colunaLista = [];

        if (propriedadeExibicaoLista !== undefined && propriedadeExibicaoLista !== null && propriedadeExibicaoLista.length > 0) {
            $.each(propriedadeExibicaoLista, function (propriedadeExibicaoIndex, propriedadeExibicaoItem) {
                colunaLista.push({
                    'sTitle': propriedadeExibicaoItem['Titulo'],
                    'data': propriedadeExibicaoItem['Nome']
                });
            });
        }

        InicializarPesquisaDataTable(resultadoLista, colunaLista);

        HabilitarDataTableSelecaoMultipla('#tablePesquisaLista');

        AdicionarDataTableLinhaLista('#tablePesquisaLista', resultadoLista);

        ExibirModal($('body').find('.modal:last'));
    });
}

function InicializarPesquisaDataTable(resultadoLista, colunaExibicaoLista) {
    var colunaLista = [];

    if (colunaExibicaoLista !== undefined && colunaExibicaoLista !== null && colunaExibicaoLista.length > 0) {
        $.each(colunaExibicaoLista, function (colunaExibicaoIndex, colunaExibicaoItem) {
            var colunaItem = {
                'sTitle': colunaExibicaoItem['Titulo'],
                'data': colunaExibicaoItem['Nome']
            };

            colunaLista.push(colunaItem);
        });
    }
    else {
        var primeiroResultado = resultadoLista[0];

        if (primeiroResultado !== undefined && primeiroResultado !== null)
            for (var i = 0; i < Object.keys(primeiroResultado).length; i++) {
                var propriedadeNome = Object.keys(primeiroResultado)[i];

                var colunaItem = {
                    'sTitle': propriedadeNome,
                    'data': propriedadeNome
                };

                if (propriedadeNome === 'Id')
                    colunaLista.splice(0, 0, colunaItem);

                if (propriedadeNome.substring(propriedadeNome.length - 4, propriedadeNome.length) === 'Nome') {
                    colunaLista.push(colunaItem);
                }

                if (propriedadeNome.substring(propriedadeNome.length - 6, propriedadeNome.length) === 'Titulo') {
                    colunaLista.push(colunaItem);
                }

                if (propriedadeNome.substring(propriedadeNome.length - 6, propriedadeNome.length) == 'Codigo') {
                    colunaLista.push(colunaItem);
                }

                if (propriedadeNome.substring(propriedadeNome.length - 6, propriedadeNome.length) === 'Usuario') {
                    colunaLista.push(colunaItem);
                }
            }
        else {
            var colunaItem = {
                'sTitle': 'None'
            };

            colunaLista.push(colunaItem);
        }
    }

    for (var c = 0; c < colunaLista.length; c++)
        $('#tablePesquisaLista').find('thead').find('tr').append("<th></th>");

    InicializarDataTable('#tablePesquisaLista', colunaLista, []);
}