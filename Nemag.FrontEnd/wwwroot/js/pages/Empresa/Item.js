$(function () {
    InicializarEmpresaMercadoriaCategoriaDataTable();

    InicializarEmpresaDepartamentoDataTable();

    ExecutarMetodoLista(
        CarregarEmpresaCategoriaLista()
    ).then(metodoResultadoLista => {
        var formEmpresaItem = $('#formEmpresaItem');

        ExibirDropdownListaNoObjeto(formEmpresaItem.find('[name="EmpresaCategoriaId"]'), ProcessarMetodoResultadoLista(metodoResultadoLista)['EmpresaCategoriaLista']);

        var empresaId = ObterQueryString('empresaId');

        if (empresaId !== '')
            CarregarEmpresaItem(empresaId);
    });
});

function CarregarEmpresaItem(empresaId) {
    var jsonParametro = {
        'token': loginToken,
        'empresaId': empresaId
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Empresa/CarregarEmpresaItem", JSON.stringify(jsonParametro), ResponseCarregarEmpresaItem);
}

function ResponseCarregarEmpresaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    var empresaItem = jsonObject['EmpresaItem'];

    var empresaDepartamentoLista = jsonObject['EmpresaDepartamentoLista'];

    var empresaMercadoriaCategoriaLista = jsonObject['EmpresaMercadoriaCategoriaLista'];

    ExibirObjetoNoFormulario($('#formEmpresaItem'), empresaItem);

    AdicionarEmpresaDepartamentoDataTableLinhaLista(empresaDepartamentoLista);

    AdicionarEmpresaMercadoriaCategoriaDataTableLinhaLista(empresaMercadoriaCategoriaLista);
}

function CarregarEmpresaCategoriaLista() {
    var jsonParametro = {
        'token': loginToken
    };

    return AjaxRequestPostAsync(apiUrl + "/Api/Empresa/CarregarEmpresaCategoriaLista", JSON.stringify(jsonParametro), null);
}

function ValidarFormularioCompleto() {
    var formularioValido = ValidarFormulario('#formEmpresaItem');

    return formularioValido;
}

function ObterApiParametro() {
    var empresaItem = $('#formEmpresaItem').toJsonObject();

    var empresaMercadoriaCategoriaLista = ObterDataTableLinhaLista('#tableEmpresaMercadoriaCategoriaLista');

    var empresaDepartamentoLista = ObterDataTableLinhaLista('#tableEmpresaDepartamentoLista');

    var jsonParametro = {
        'token': loginToken,
        'empresaItem': empresaItem,
        'empresaMercadoriaCategoriaLista': empresaMercadoriaCategoriaLista,
        'empresaDepartamentoLista': empresaDepartamentoLista
    };

    return jsonParametro;
}

function SalvarEmpresaItem() {
    var formularioValido = ValidarFormularioCompleto();

    if (!formularioValido) {
        ExibirMensagem('Erro', 'Existem informações inválidas ou pendentes. Por gentileza, verifique tais informações e tente novamente.');

        return;
    }

    ExibirConfirmacao('Confirmação', 'Deseja realmente salvar as informações?', CallbackSalvarEmpresaItem, null);
}

function CallbackSalvarEmpresaItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Empresa/SalvarEmpresaItem", JSON.stringify(jsonParametro), ResponseSalvarEmpresaItem);
}

function ResponseSalvarEmpresaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações salvas com sucesso!', Voltar);
}

function InicializarEmpresaMercadoriaCategoriaDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px', 'bVisible': false },
        { 'title': 'Id', 'data': 'MercadoriaCategoriaId', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'MercadoriaCategoriaNome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableEmpresaMercadoriaCategoriaLista', colunaLista);
}

function EfetuarPesquisaEmpresaMercadoriaCategoriaLista() {
    var propriedadeEspelhadaLista = [];

    propriedadeEspelhadaLista.push({ 'propriedadeAtualNome': 'Id', 'propriedadeEspelhadaNome': 'MercadoriaCategoriaId' });

    propriedadeEspelhadaLista.push({ 'propriedadeAtualNome': 'Nome', 'propriedadeEspelhadaNome': 'MercadoriaCategoriaNome' });

    EfetuarPesquisaLista('Selecione as categorias', apiUrl + '/Api/Mercadoria/CarregarMercadoriaCategoriaLista', null, EfetuarPesquisaEmpresaMercadoriaCategoriaListaCallback, null, null, propriedadeEspelhadaLista);
}

function EfetuarPesquisaEmpresaMercadoriaCategoriaListaCallback(data) {
    AdicionarEmpresaMercadoriaCategoriaDataTableLinhaLista(data);
}

function AdicionarEmpresaMercadoriaCategoriaDataTableLinhaLista(empresaMercadoriaCategoriaLista) {
    if (empresaMercadoriaCategoriaLista !== undefined && empresaMercadoriaCategoriaLista !== null)
        for (var i = 0; i < empresaMercadoriaCategoriaLista.length; i++)
            empresaMercadoriaCategoriaLista[i]['Id'] = '0';

    AdicionarDataTableLinhaLista('#tableEmpresaMercadoriaCategoriaLista', empresaMercadoriaCategoriaLista);
}

function RemoverEmpresaMercadoriaCategoriaDataTableLinhaLista() {
    RemoverDataTableLinhaSelecionada('#tableEmpresaMercadoriaCategoriaLista');
}

function InicializarEmpresaDepartamentoDataTable() {
    var colunaLista = [
        { 'title': 'Id', 'data': 'Id', 'sWidth': '50px', 'bVisible': false },
        { 'title': 'Id', 'data': 'DepartamentoId', 'sWidth': '50px' },
        { 'title': 'Nome', 'data': 'DepartamentoNome' },
        { 'title': 'Data de Inclusão', 'data': 'DataInclusao', 'mRender': RenderizarDataTableDataHora },
        { 'title': 'Data de Alteração', 'data': 'DataAlteracao', 'mRender': RenderizarDataTableDataHora }
    ];

    InicializarDataTable('#tableEmpresaDepartamentoLista', colunaLista);
}

function EfetuarPesquisaEmpresaDepartamentoLista() {
    var propriedadeEspelhadaLista = [];

    propriedadeEspelhadaLista.push({ 'propriedadeAtualNome': 'Id', 'propriedadeEspelhadaNome': 'DepartamentoId' });

    propriedadeEspelhadaLista.push({ 'propriedadeAtualNome': 'Nome', 'propriedadeEspelhadaNome': 'DepartamentoNome' });

    EfetuarPesquisaLista('Selecione os departamentos', apiUrl + '/Api/Departamento/CarregarDepartamentoLista', null, EfetuarPesquisaEmpresaDepartamentoListaCallback, null, null, propriedadeEspelhadaLista);
}

function EfetuarPesquisaEmpresaDepartamentoListaCallback(data) {
    AdicionarEmpresaDepartamentoDataTableLinhaLista(data);
}

function AdicionarEmpresaDepartamentoDataTableLinhaLista(empresaDepartamentoLista) {
    if (empresaDepartamentoLista !== undefined && empresaDepartamentoLista !== null)
        for (var i = 0; i < empresaDepartamentoLista.length; i++)
            empresaDepartamentoLista[i]['Id'] = '0';

    AdicionarDataTableLinhaLista('#tableEmpresaDepartamentoLista', empresaDepartamentoLista);
}

function RemoverEmpresaDepartamentoDataTableLinhaLista() {
    RemoverDataTableLinhaSelecionada('#tableEmpresaDepartamentoLista');
}

function ExcluirEmpresaItem() {
    ExibirConfirmacao('Confirmação', 'Deseja realmente excluir?', CallbackExcluirEmpresaItem, null);
}

function CallbackExcluirEmpresaItem() {
    var jsonParametro = ObterApiParametro();

    return AjaxRequestPostAsync(apiUrl + "/Api/Empresa/ExcluirEmpresaItem", JSON.stringify(jsonParametro), ResponseExcluirEmpresaItem);
}

function ResponseExcluirEmpresaItem(data) {
    var jsonObject = ProcessarResponseJson(data);

    if (jsonObject === null) return;

    ExibirMensagem('Sucesso!', 'Informações excluídas com sucesso!', Voltar);
}

