using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Notificacao.Tipo 
{ 
    public partial interface ITipoItem
    { 
        List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarLista(); 

        List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Login.Notificacao.Tipo.TipoItem CarregarItem(int loginNotificacaoTipoId);

        Entidade.Login.Notificacao.Tipo.TipoItem InserirItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem); 

        Entidade.Login.Notificacao.Tipo.TipoItem AtualizarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem); 

        Entidade.Login.Notificacao.Tipo.TipoItem ExcluirItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem); 

        Entidade.Login.Notificacao.Tipo.TipoItem InativarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem); 
    } 
} 
