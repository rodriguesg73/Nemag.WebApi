using System.Collections.Generic;

namespace Nemag.Core.Interface.Registro.Situacao 
{ 
    public partial interface ISituacaoItem
    { 
        List<Entidade.Registro.Situacao.SituacaoItem> CarregarLista(); 

        Entidade.Registro.Situacao.SituacaoItem CarregarItem(int registroSituacaoId);

        Entidade.Registro.Situacao.SituacaoItem InserirItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem); 

        Entidade.Registro.Situacao.SituacaoItem AtualizarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem); 

        Entidade.Registro.Situacao.SituacaoItem ExcluirItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem); 

        Entidade.Registro.Situacao.SituacaoItem InativarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem); 
    } 
} 
