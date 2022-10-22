$(function () {
    InicializarArquivoTramitadorDataTable();

    CarregarFiltroItemPorCookie($('#formFiltroItem'));

    EfetuarPesquisaArquivoTramitadorLista();
});

function EfetuarPesquisaArquivoTramitadorLista() {
    var filtroItem = $('#formFiltroItem').toJsonObject();

    SalvarFiltroItemNoCookie(filtroItem);

    var jsonParametro = {
        'token': loginToken,
        'arquivoTramitadorFiltroItem': filtroItem
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Arquivo/CarregarArquivoTramitadorListaPorFiltroItem", JSON.stringify(jsonParametro), function (data) {
        var jsonObject = ProcessarResponseJson(data);

        if (jsonObject === null) return;

        var arquivoTramitadorLista = jsonObject['ArquivoTramitadorLista'];

        AdicionarArquivoTramitadorDataTableLinhaLista(arquivoTramitadorLista);
    });
}

function InicializarArquivoTramitadorDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'Nome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableArquivoTramitadorLista', colunaLista);
}

function AdicionarArquivoTramitadorDataTableLinhaLista(arquivoTramitadorLista) {
    AdicionarDataTableLinhaLista('#tableArquivoTramitadorLista', arquivoTramitadorLista);
}

function EditarArquivoTramitadorItem() {
    var linhaSelecionada = ObterDataTableLinhaSelecionadaItem('#tableArquivoTramitadorLista');

    if (linhaSelecionada === undefined || linhaSelecionada['Id'] === undefined) {
        ExibirMensagem('Seleção', 'Por favor, selecione um registro e tente novamente!');

        return;
    }

    window.location.href = '/Arquivo/Tramitador/Item?arquivoTramitadorId=' + linhaSelecionada['Id'];
}

function AdicionarArquivoTramitadorItem() {
    window.location.href = '/Arquivo/Tramitador/Item';
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formFiltroItem');

    return formularioValido;
}
