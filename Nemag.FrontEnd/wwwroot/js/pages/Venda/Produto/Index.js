function AdicionarProdutoVendaLista(vendaProdutoLista) {
    if (vendaProdutoLista !== undefined && vendaProdutoLista !== null && vendaProdutoLista.length > 0)
        $.each(vendaProdutoLista, function (compraProdutoIndex, vendaProdutoItem) {
            AdicionarProdutoVendaItem(vendaProdutoItem);
        });
    else
        ValidarProdutoVendaListaCabecalho();
}

function AdicionarProdutoVendaItem(vendaProdutoItem) {
    var formProdutoVendaLista = $('#formProdutoVendaLista');

    var html = MontarProdutoVendaItemHtml(vendaProdutoItem);

    formProdutoVendaLista.append(html);

    ValidarProdutoVendaListaCabecalho();
}

function MontarProdutoVendaItemHtml(vendaProdutoItem) {
    var html = '';

    var guid = ObterGuid();

    if (vendaProdutoItem !== undefined && vendaProdutoItem !== null)
        vendaProdutoItem['GuidPrimario'] = guid;

    html += '<div class="row item"> \n';
    html += '    <input type="hidden" name="Id" value="0" /> \n';
    html += '    <input type="hidden" name="GuidPrimario" value="' + guid + '" /> \n';
    html += '    <input type="hidden" name="GuidEstrangeiro" value="" /> \n';
    html += '    <input type="hidden" name="VendaId" value="0" />\n';
    html += '    <input type="hidden" name="RegistroSituacaoId" value="0" />\n';
    html += '    <input type="hidden" name="ProdutoId" value="0" />\n';

    html += '    <div class="col-md-7"> \n';
    html += '        <div class="input-group">\n';
    html += '            <input type="text" name="ProdutoNome" class="form-control" required readonly />\n';
    html += '            <span class="input-group-append">\n';
    html += '                <button type="button" class="btn btn-primary" onclick="LimparPesquisaItemGenerica(this);">\n';
    html += '                    <i class="fa fa-eraser"></i>\n';
    html += '                </button>\n';
    html += '                <button type="button" class="btn btn-primary" onclick="EfetuarPesquisaProdutoItem($(this).parent().parent().parent().parent());">\n';
    html += '                    <i class="fa fa-search"></i>\n';
    html += '                </button>\n';
    html += '            </span>\n';
    html += '        </div>\n';
    html += '    </div> \n';

    html += '    <div class="col-md-2"> \n';
    html += '        <input type="text" name="ProdutoCodigo" class="form-control" disabled />\n';
    html += '    </div> \n';

    html += '    <div class="col-md-2"> \n';
    html += '        <div class="input-group">\n';
    html += '            <span class="input-group-append">\n';
    html += '                <button type="button" class="btn btn-dark" disabled>\n';
    html += '                    <i class="fa fa-money"></i>\n';
    html += '                </button>\n';
    html += '            </span>\n';
    html += '            <input type="text" name="ProdutoValor" class="form-control" disabled />\n';
    html += '        </div>\n';
    html += '    </div> \n';

    html += '    <div class="col-md-1"> \n';
    html += '        <button type="button" class="btn btn-primary btn-block" onclick="RemoverProdutoVendaItem($(this).parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    if (vendaProdutoItem !== null && vendaProdutoItem !== undefined && vendaProdutoItem !== {})
        ExibirObjetoNoFormulario(htmlObject, vendaProdutoItem);

    html = htmlObject;

    return html;
}

function RemoverProdutoVendaItem(compraProdutoHtml) {
    $(compraProdutoHtml).remove();

    ValidarProdutoVendaListaCabecalho();
}

function ValidarProdutoVendaListaCabecalho() {
    var formProdutoVendaLista = $('#formProdutoVendaLista');

    var vendaProdutoLista = $('#formProdutoVendaLista').find('.item');

    var formTituloItem = '';

    if (vendaProdutoLista === null || vendaProdutoLista === undefined || vendaProdutoLista.length === 0) {
        formTituloItem += '<div class="row titulo text-center"> \n';
        formTituloItem += '    <div class="col-md-12"><p>Não há registros a serem exibidos</p></div>';
        formTituloItem += '</div> \n';
    } else {
        formTituloItem += '<div class="row titulo"> \n';
        formTituloItem += '    <div class="col-md-7"><label>Produto:</label></div> \n';
        formTituloItem += '    <div class="col-md-2"><label>Codigo:</label></div> \n';
        formTituloItem += '    <div class="col-md-3"><label>Valor:</label></div> \n';
        formTituloItem += '</div> \n';
    }

    formProdutoVendaLista.find('.titulo').remove();

    formProdutoVendaLista.prepend(formTituloItem);
}

function EfetuarPesquisaProdutoItem(produtoVendahtml) {
    EfetuarPesquisa('Selecione um produto', apiUrl + '/Api/Produto/CarregarProdutoLista', null, EfetuarPesquisaProdutoItemCallback, null, produtoVendahtml);
}

function EfetuarPesquisaProdutoItemCallback(data, container) {
    $(container).find('[name="ProdutoId"]').val(data['Id']);
    $(container).find('[name="ProdutoNome"]').val(data['Nome']);
    $(container).find('[name="ProdutoCodigo"]').val(data['Codigo']);
    OnBlurValidarCampoDecimal($(container).find('[name="ProdutoValor"]').val(data['Valor']), 2);

    ValidarProdutoVendaListaInterface();
}

function ValidarProdutoVendaListaInterface() {
    var produtoVendaLista = $('#formProdutoVendaLista').toJsonList();

    var valorVendaFinal = 0;

    var vendaItem = $('#formVendaItem').toJsonObject();

    var valorVendaInicial = vendaItem['Valor'] / 1;

    $.each(produtoVendaLista, function (produtoVendaIndex, produtoVendaItem) {
        valorVendaFinal = valorVendaInicial + produtoVendaItem['ProdutoValor'] / 1;

        OnBlurValidarCampoDecimal($('#formVendaItem').find('[name="Valor"]').val(valorVendaFinal), 2);
    });
}