using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo.Tramitador 
{ 
    public partial interface ITramitadorItem
    { 
        List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarLista(); 

        List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Arquivo.Tramitador.TramitadorItem CarregarItem(int arquivoTramitadorId);

        Entidade.Arquivo.Tramitador.TramitadorItem InserirItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem); 

        Entidade.Arquivo.Tramitador.TramitadorItem AtualizarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem); 

        Entidade.Arquivo.Tramitador.TramitadorItem ExcluirItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem); 

        Entidade.Arquivo.Tramitador.TramitadorItem InativarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem); 
    } 
} 
