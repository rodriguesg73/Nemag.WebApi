function InicializarDataTable(dataTableId, dataTableColunaLista, dataTableConfiguracaoPersonalizada) {
    $(dataTableId).addClass('noselect');

    var tableHtml = '';

    $.each(dataTableColunaLista, function (dataTableColunaIndex, dataTableColunaItem) {
        tableHtml += '<th></th>';
    });

    tableHtml = '<thead><tr>' + tableHtml + '</tr></thead>';

    var dom = '<\'html5buttons\'B>lTfg<"database-scroll"t><"row"<"col-md-4"i><"col-md-8"p<"datatable-controle-lista dataTables_paginate paging_simple_numbers">>>';

    var dataTableConfiguracaoInicial = {
        'dom': dom,
        'buttons': [
            { extend: 'copy', text: 'Copiar' },
            { extend: 'csv' },
            { extend: 'excel', title: 'Exportação' },
            { extend: 'pdf', title: 'Exportação' },
            {
                extend: 'print',
                text: 'Imprimir',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        'order': [],
        'autoWidth': false,
        'language': {
            'sEmptyTable': 'Nenhum registro encontrado',
            'sInfo': 'Mostrando de _START_ até _END_ de _TOTAL_ registros',
            'sInfoEmpty': 'Mostrando 0 até 0 de 0 registros',
            'sInfoFiltered': '(filtrados de _MAX_ registros)',
            'sInfoPostFix': '',
            'sInfoThousands': '.',
            'sLengthMenu': '_MENU_ resultados por página',
            'sLoadingRecords': 'Carregando...',
            'sProcessing': 'Processando...',
            'sZeroRecords': 'Nenhum registro encontrado',
            'sSearch': 'Pesquisar',
            'oPaginate': {
                'sNext': 'Próximo',
                'sPrevious': 'Anterior',
                'sFirst': 'Primeiro',
                'sLast': 'Último'
            },
            'oAria': {
                'sSortAscending': ': Ordenar colunas de forma ascendente',
                'sSortDescending': ': Ordenar colunas de forma descendente'
            }
        }
    };

    if (dataTableConfiguracaoPersonalizada !== null && dataTableConfiguracaoPersonalizada !== undefined) {
        dataTableConfiguracaoInicial['buttons'] = [];

        $.extend(dataTableConfiguracaoInicial, dataTableConfiguracaoPersonalizada);
    }

    if (dataTableColunaLista !== null && dataTableColunaLista !== undefined && dataTableColunaLista.length > 0)
        dataTableConfiguracaoInicial['aoColumns'] = dataTableColunaLista;

    if ($.fn.dataTable.isDataTable(dataTableId)) {
        $(dataTableId).DataTable().destroy();

        $(dataTableId + ' tbody').off('click', 'tr');

        $(dataTableId + ' tbody').off('dblclick', 'td');
    }

    $(dataTableId).html(tableHtml)

    var dataTable = $(dataTableId).DataTable(dataTableConfiguracaoInicial);

    dataTable
        .columns
        .adjust()
        .draw();


    // SELEÇÃO DE REGISTRO
    $(dataTableId + ' tbody').on('click', 'tr', function () {
        if ($(this).hasClass('table-active')) {
            $(this).removeClass('table-active');
        }
        else {
            $(dataTableId).DataTable().$('tr.table-active').removeClass('table-active');

            $(this).addClass('table-active');
        }
    });

    // EDIÇÃO DE VALOR
    $(dataTableId + ' tbody').on('dblclick', 'td', function () {
        $(this).closest('tr').removeClass('table-active');

        var columnIndex = dataTable.cell(this).index().column;

        var editable = dataTableConfiguracaoInicial['aoColumns'][columnIndex]['editable'];

        if (editable === undefined || editable === null || !editable)
            return;

        EditarDataTableCelula(this);
    });

    if (dataTableConfiguracaoInicial['controls'] !== undefined && dataTableConfiguracaoInicial['controls'] !== null) {
        dataTableConfiguracaoInicial['controls'].reverse();

        var dataTableControleHtml = '';

        dataTableControleHtml += '<ul class="pagination" style="margin-right: 5px;">';

        $.each(dataTableConfiguracaoInicial['controls'], function (dataTableControleIndex, dataTableControleItem) {
            dataTableControleHtml += '<li class="paginate_button page-item next"><a href="javascript:;" onclick="' + dataTableControleItem['action'].name + '();" class="page-link">' + dataTableControleItem['text'] + '</a></li>';
        });

        dataTableControleHtml += '</ul>'

        $(dataTableId).parent().parent().find('.datatable-controle-lista').html(dataTableControleHtml);
    }

    return dataTable;
}

function EditarDataTableCelula(tdHtml) {
    var tableHtml = $(tdHtml).closest('table');

    var dataTable = $(tableHtml).DataTable();

    var dataTableCell = dataTable.cell(tdHtml);

    var valorOriginal = dataTableCell.data();

    dataTableCell.data('<input type="text" class="form-control" value="' + valorOriginal + '" onkeypress="AoPressionarEnter(this, event, DispararEventoOnBlurNoInput, this);" onkeyup="AoPressionarEsc(this, event, DispararEventoOnBlurNoInput, this);" onblur="OnBlurEditarDataTableCelula(this);" required />');

    $(tdHtml).find('input').select();
}

function OnBlurEditarDataTableCelula(input) {
    var tableHtml = $(input).closest('table');

    var dataTable = $(tableHtml).DataTable();

    var tdHtml = $(input).closest('td');

    var dataTableCell = dataTable.cell(tdHtml);

    var valorNovo = $(input).val();

    dataTableCell.data(valorNovo);
}

function RenderizarDataTableData(data, type, row, meta) {
    if (data === undefined)
        return null;

    if (data.startsWith('<input'))
        return data;

    var valor = FormatarDataWebApi(data);

    if (valor === '' || valor === null || valor === undefined)
        return null;

    return FormatarDataWebApi(data);
}

function RenderizarDataTableHora(data, type, row, meta) {
    if (data === undefined)
        return null;

    if (data.startsWith('<input'))
        return data;

    var valor = FormatarDataWebApi(data);

    if (valor === '' || valor === null || valor === undefined)
        return null;

    return FormatarHoraWebApi(data);
}

function RenderizarDataTableDataHora(data, type, row, meta) {
    if (data === undefined)
        return null;

    if (data.startsWith('<input'))
        return data;

    var valor = FormatarDataWebApi(data);

    if (valor === '' || valor === null || valor === undefined)
        return null;

    return FormatarDataWebApi(data) + ' ' + FormatarHoraWebApi(data);
}

function RenderizarDataTableDataComBotao(data, type, row, meta) {
    var dataTableId = '#' + meta.settings.sTableId;

    var rowIndex = meta.row;

    var columnIndex = meta.col;

    var dataAtual = FormatarDataWebApi(data);

    var html = '<span>' + dataAtual + '</span><button type="button" class="btn btn-xs btn-primary float-right" style="margin-left: 5px;" onclick="ObterDataTableDataHoraAtual(\'' + dataTableId + '\', ' + rowIndex + ', ' + columnIndex + '); "><i class="fa fa-clock-o"></i></button>';

    return html;
}

function RenderizarDataTableHoraComBotao(data, type, row, meta) {
    var dataTableId = '#' + meta.settings.sTableId;

    var rowIndex = meta.row;

    var columnIndex = meta.col;

    var horaAtual = FormatarHoraWebApi(data);

    var html = '<span>' + dataHoraAtual + '</span><button type="button" class="btn btn-xs btn-primary float-right" style="margin-left: 5px;" onclick="ObterDataTableDataHoraAtual(\'' + dataTableId + '\', ' + rowIndex + ', ' + columnIndex + '); "><i class="fa fa-clock-o"></i></button>';

    return html;
}

function RenderizarDataTableDataHoraComBotao(data, type, row, meta) {
    var dataTableId = '#' + meta.settings.sTableId;

    var rowIndex = meta.row;

    var columnIndex = meta.col;

    var dataHoraAtual = FormatarDataHoraWebApi(data);

    var html = '<span>' + dataHoraAtual + '</span><button type="button" class="btn btn-xs btn-primary float-right" style="margin-left: 5px;" onclick="ObterDataTableDataHoraAtual(\'' + dataTableId + '\', ' + rowIndex + ', ' + columnIndex + '); "><i class="fa fa-clock-o"></i></button>';

    return html;
}

function RenderizarDataTableMegabyte(data, type, row, meta) {
    var valor = data;

    if (valor === '' || valor === null || valor === undefined)
        return null;

    return (valor / 1024 / 1024).toFixed(2);
}

function RenderizarDataTableGigabyte(data, type, row, meta) {
    var valor = data;

    if (valor === '' || valor === null || valor === undefined)
        return null;

    return (valor / 1024 / 1024 / 1024).toFixed(2);
}

function RenderizarDataTableDecimal(data, type, row, meta) {
    var valor = data;

    if (valor === '' || valor === null || valor === undefined)
        return null;

    return (valor / 1).toFixed(2);
}

function RenderizarDataTableMoeda(data, type, row, meta) {
    var valor = data;

    if (valor === '' || valor === null || valor === undefined)
        return null;

    var valorTratado = ((valor / 1).toFixed(2)).toString();

    var moeda = 'R$'

    return (moeda + valorTratado);
}

function AdicionarDataTableLinhaLista(dataTableId, linhaLista) {
    var dataTable = $(dataTableId).DataTable();

    dataTable
        .clear()
        .draw();

    if (linhaLista !== null && linhaLista !== undefined && linhaLista.length > 0) {
        dataTable
            .rows
            .add(linhaLista)
            .draw();
    }
}

function AdicionarDataTableLinhaItem(dataTableId, linhaItem) {
    var dataTable = $(dataTableId).DataTable();

    if (linhaItem !== null && linhaItem !== undefined) {
        dataTable
            .row
            .add(linhaItem)
            .draw(false);
    }
}

function RemoverDataTableLinhaItem(dataTableId, dataTableRowIndex) {
    var dataTable = $(dataTableId).DataTable();

    dataTable
        .row(dataTableRowIndex)
        .remove()
        .draw(false);
}

function ObterDataTableLinhaLista(dataTableId) {
    var dataTable = $(dataTableId).dataTable();

    var rows = dataTable.fnGetNodes();

    var settings = dataTable.fnSettings();

    var retorno = [];

    $.each(rows, function (linhaIndex, linhaItem) {
        var objetoItem = settings.aoData[linhaIndex]._aData;

        retorno.push(objetoItem);
    });

    return retorno;
}

function ObterDataTableLinhaSelecionadaItem(tabelaId) {
    var dataTable = $(tabelaId).DataTable();

    var rows = dataTable.rows({ order: 'applied' }).nodes();

    var retorno = undefined;

    $.each(rows, function (linhaIndex, linhaItem) {
        var linha = $(this);

        var selecionada = linha.hasClass('table-active');

        if (selecionada) {
            retorno = dataTable.row(linhaItem).data();

            return;
        }
    });

    return retorno;
}

function ObterDataTableLinhaSelecionadaLista(tabelaId) {
    var dataTable = $(tabelaId).DataTable();

    var rows = dataTable.rows({ order: 'applied' }).nodes();

    var retorno = [];

    $.each(rows, function (linhaIndex, linhaItem) {
        var linha = $(this);

        var selecionada = linha.hasClass('table-active');

        if (selecionada)
            retorno.push(dataTable.row(linhaItem).data());
    });

    return retorno;
}

function ObterDataTableLinhaSelecionadaIndexItem(tabelaId) {
    var dataTable = $(tabelaId).DataTable();

    var rows = dataTable.rows({ order: 'applied' }).nodes();

    var retorno = undefined;

    $.each(rows, function (linhaIndex, linhaItem) {
        if (retorno != undefined)
            return;

        linhaItem = $(this);

        var selecionada = linhaItem.hasClass('table-active');

        if (selecionada) {
            retorno = linhaIndex;

            return;
        }
    });

    return retorno;
}

function ObterDataTableLinhaSelecionadaIndexLista(tabelaId) {
    var dataTable = $(tabelaId).DataTable();

    var rows = dataTable.rows({ order: 'applied' }).nodes();

    var retorno = [];

    $.each(rows, function (linhaIndex, linhaItem) {
        linhaItem = $(this);

        var selecionada = linhaItem.hasClass('table-active');

        if (selecionada)
            retorno.push(linhaIndex);
    });

    return retorno;
}

function RemoverDataTableLinhaSelecionada(tabelaId) {
    var dataTable = $(tabelaId).dataTable();

    var linhaLista = ObterDataTableLinhaSelecionadaIndexLista(tabelaId);

    $.each(linhaLista, function (linhaIndex, linhaItem) {
        dataTable.fnDeleteRow(linhaItem)
    });
}

function SelecionarDataTableLinhaLista(tabelaId, selecaoLista, campoChave) {
    var dataTable = $(tabelaId).dataTable();

    var rows = dataTable.fnGetNodes();

    var settings = dataTable.fnSettings();

    $.each(rows, function (linhaIndex, linhaItem) {
        var linha = $(this);

        var jsonObjeto = settings.aoData[linhaIndex]._aData;

        $.each(selecaoLista, function (selecaoIndex, selecaoItem) {
            if (jsonObjeto[campoChave] === selecaoItem[campoChave]) {
                linha.addClass('table-active');

                return;
            }
        });
    });
}

function HabilitarDataTableSelecaoMultipla(dataTableItem) {
    dataTableItem = $(dataTableItem);

    dataTableItem.find('tbody').off('click');

    dataTableItem.find('tbody').on('click', 'tr', function () {
        if ($(this).hasClass('table-active')) {
            $(this).removeClass('table-active');
        }
        else {
            $(this).addClass('table-active');
        }
    });
}

function DesabilitarDataTableSelecao(dataTableItem) {
    dataTableItem = $(dataTableItem);

    dataTableItem.find('tbody').off('click');
}

function ObterDataTableDataAtual(tableId, linhaIndex, objetoPropriedadeNome) {
    var dataAtual = ObterDataHoraAtualWebApi();

    var dataTable = $('#' + tableId).DataTable();

    var objetoItem = dataTable.row(linhaIndex).data();

    objetoItem[objetoPropriedadeNome] = dataAtual;

    dataTable.row(linhaIndex).data(objetoItem).draw(false);

    return dataAtual;
}

function ObterDataTableHoraAtual(tableId, linhaIndex, objetoPropriedadeNome) {
    var horaAtual = ObterHoraAtualWebApi();

    var dataTable = $('#' + tableId).DataTable();

    var objetoItem = dataTable.row(linhaIndex).data();

    objetoItem[objetoPropriedadeNome] = horaAtual;

    dataTable.row(linhaIndex).data(objetoItem).draw(false);

    return dataHoraAtual;
}

function ObterDataTableDataHoraAtual(dataTableId, rowIndex, columnIndex) {
    var dataHoraAtualApi = ObterDataHoraAtualWebApi();

    $(dataTableId).DataTable().cell({ row: rowIndex, column: columnIndex }).data(dataHoraAtualApi);

    return dataHoraAtualApi;
}

function LimparDataTableSelecao(tableId) {
    tableId = $(tableId);

    tableId.find('tr').removeClass('table-active');
}

function LimparDataTableLinhaLista(dataTableId) {
    var dataTable = $(dataTableId).DataTable();

    dataTable
        .clear()
        .draw();
}