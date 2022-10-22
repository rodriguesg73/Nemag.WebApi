var _pessoaContatoTipoLista = undefined;

function CarregarPessoaContatoTipoLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Pessoa/CarregarPessoaContatoTipoLista", JSON.stringify(jsonParametro), null);
}

function AdicionarPessoaContatoLista(pessoaContatoLista) {
    if (pessoaContatoLista !== undefined && pessoaContatoLista !== null && pessoaContatoLista.length > 0)
        $.each(pessoaContatoLista, function (pessoaContatoIndex, pessoaContatoItem) {
            AdicionarPessoaContatoItem(pessoaContatoItem);
        });
    else
        ValidarPessoaContatoListaCabecalho();
}

function AdicionarPessoaContatoItem(pessoaContatoItem) {
    var formPessoaContatoLista = $('#formPessoaContatoLista');

    var html = MontarPessoaContatoItemHtml(pessoaContatoItem);

    formPessoaContatoLista.append(html);

    ValidarPessoaContatoListaCabecalho();
}

function MontarPessoaContatoItemHtml(pessoaContatoItem) {
    var html = '';

    var guid = ObterGuid();

    if (pessoaContatoItem !== undefined && pessoaContatoItem !== null)
        pessoaContatoItem['GuidPrimario'] = guid;

    html += '<div class="row item"> \n';
    html += '    <input type="hidden" name="Id" value="0" /> \n';
    html += '    <input type="hidden" name="GuidPrimario" value="' + guid + '" /> \n';

    html += '    <div class="col-custom-9">\n';
    html += '        <select name="PessoaContatoTipoId" class="form-control" required></select>\n';
    html += '    </div>\n';

    html += '    <div class="col-custom-24"> \n';
    html += '        <input type="text" name="Valor" class="form-control" required /> \n';
    html += '    </div> \n';

    html += '    <div class="col-custom-3"> \n';
    html += '        <button type="button" class="btn btn-primary btn-block" onclick="RemoverPessoaContatoItem($(this).parent().parent().parent());"><i class="fa fa-trash-o"></i></button> \n';
    html += '    </div> \n';
    html += '</div> \n';

    var htmlObject = $.parseHTML(html);

    ExibirDropdownListaNoObjeto($(htmlObject).find('[name="PessoaContatoTipoId"]'), _pessoaContatoTipoLista);

    if (pessoaContatoItem !== null && pessoaContatoItem !== undefined && pessoaContatoItem !== {})
        ExibirObjetoNoFormulario(htmlObject, pessoaContatoItem);

    html = htmlObject;

    return html;
}

function RemoverPessoaContatoItem(pessoaContatoHtml) {
    $(pessoaContatoHtml).remove();

    ValidarPessoaContatoListaCabecalho();
}

function ValidarPessoaContatoListaCabecalho() {
    var formPessoaContatoLista = $('#formPessoaContatoLista');

    var pessoaContatoLista = $('#formPessoaContatoLista').find('.item');

    var formTituloItem = '';

    if (pessoaContatoLista === null || pessoaContatoLista === undefined || pessoaContatoLista.length === 0) {
        formTituloItem += '<div class="row titulo text-center"> \n';
        formTituloItem += '    <div class="col-md-12"><p>Não há registros a serem exibidos</p></div>';
        formTituloItem += '</div> \n';
    } else {
        formTituloItem += '<div class="row titulo"> \n';
        formTituloItem += '    <div class="col-custom-9"><label>Tipo de contato</label></div> \n';
        formTituloItem += '    <div class="col-custom-27"><label>Valor</label></div> \n';
        formTituloItem += '</div> \n';
    }

    formPessoaContatoLista.find('.titulo').remove();

    formPessoaContatoLista.prepend(formTituloItem);
}