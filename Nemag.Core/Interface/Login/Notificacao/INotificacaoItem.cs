using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Notificacao 
{ 
    public partial interface INotificacaoItem
    { 
        List<Entidade.Login.Notificacao.NotificacaoItem> CarregarLista(); 

        List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorLoginId(int loginId); 

        List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorLoginNotificacaoTipoId(int loginNotificacaoTipoId); 

        Entidade.Login.Notificacao.NotificacaoItem CarregarItem(int loginNotificacaoId);

        Entidade.Login.Notificacao.NotificacaoItem InserirItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem); 

        Entidade.Login.Notificacao.NotificacaoItem AtualizarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem); 

        Entidade.Login.Notificacao.NotificacaoItem ExcluirItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem); 

        Entidade.Login.Notificacao.NotificacaoItem InativarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem); 
    } 
} 
