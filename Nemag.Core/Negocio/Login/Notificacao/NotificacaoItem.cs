using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Notificacao 
{ 
    public partial class NotificacaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Notificacao.INotificacaoItem _persistenciaNotificacaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public NotificacaoItem() 
            : this(new Persistencia.Login.Notificacao.NotificacaoItem()) 
        { } 

        public NotificacaoItem(Interface.Login.Notificacao.INotificacaoItem persistenciaNotificacaoItem) 
        { 
            this._persistenciaNotificacaoItem = persistenciaNotificacaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarLista() 
        { 
            return _persistenciaNotificacaoItem.CarregarLista(); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaNotificacaoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorLoginId(int loginId) 
        { 
            return _persistenciaNotificacaoItem.CarregarListaPorLoginId(loginId); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorLoginNotificacaoTipoId(int loginNotificacaoTipoId) 
        { 
            return _persistenciaNotificacaoItem.CarregarListaPorLoginNotificacaoTipoId(loginNotificacaoTipoId); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem CarregarItem(int loginNotificacaoId)
        {
            return _persistenciaNotificacaoItem.CarregarItem(loginNotificacaoId);
        }

        public Entidade.Login.Notificacao.NotificacaoItem InserirItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            return _persistenciaNotificacaoItem.InserirItem(notificacaoItem); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem AtualizarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            return _persistenciaNotificacaoItem.AtualizarItem(notificacaoItem); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem ExcluirItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            return _persistenciaNotificacaoItem.ExcluirItem(notificacaoItem); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem InativarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            return _persistenciaNotificacaoItem.InativarItem(notificacaoItem); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem SalvarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            if (notificacaoItem.Id.Equals(0))
                notificacaoItem = this.InserirItem(notificacaoItem);
            else
                notificacaoItem = this.AtualizarItem(notificacaoItem);

            return notificacaoItem;
        }

        #endregion 
    } 
} 
