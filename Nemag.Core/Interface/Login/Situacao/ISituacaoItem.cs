using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Situacao 
{ 
    public partial interface ISituacaoItem
    { 
        List<Entidade.Login.Situacao.SituacaoItem> CarregarLista(); 

        Entidade.Login.Situacao.SituacaoItem CarregarItem(int loginSituacaoId);

        Entidade.Login.Situacao.SituacaoItem InserirItem(Entidade.Login.Situacao.SituacaoItem situacaoItem); 

        Entidade.Login.Situacao.SituacaoItem AtualizarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem); 

        Entidade.Login.Situacao.SituacaoItem ExcluirItem(Entidade.Login.Situacao.SituacaoItem situacaoItem); 

        Entidade.Login.Situacao.SituacaoItem InativarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem); 
    } 
} 
