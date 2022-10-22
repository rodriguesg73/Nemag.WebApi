using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo.Tramitador.Acao 
{ 
    public partial interface IAcaoItem
    { 
        List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarLista(); 

        List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Arquivo.Tramitador.Acao.AcaoItem CarregarItem(int arquivoTramitadorAcaoId);

        Entidade.Arquivo.Tramitador.Acao.AcaoItem InserirItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem); 

        Entidade.Arquivo.Tramitador.Acao.AcaoItem AtualizarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem); 

        Entidade.Arquivo.Tramitador.Acao.AcaoItem ExcluirItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem); 

        Entidade.Arquivo.Tramitador.Acao.AcaoItem InativarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem); 
    } 
} 
