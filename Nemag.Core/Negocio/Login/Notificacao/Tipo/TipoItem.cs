using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Notificacao.Tipo 
{ 
    public partial class TipoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Notificacao.Tipo.ITipoItem _persistenciaTipoItem { get; set; } 

        #endregion 

        #region Construtores 

        public TipoItem() 
            : this(new Persistencia.Login.Notificacao.Tipo.TipoItem()) 
        { } 

        public TipoItem(Interface.Login.Notificacao.Tipo.ITipoItem persistenciaTipoItem) 
        { 
            this._persistenciaTipoItem = persistenciaTipoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarLista() 
        { 
            return _persistenciaTipoItem.CarregarLista(); 
        } 

        public List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaTipoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem CarregarItem(int loginNotificacaoTipoId)
        {
            return _persistenciaTipoItem.CarregarItem(loginNotificacaoTipoId);
        }

        public Entidade.Login.Notificacao.Tipo.TipoItem InserirItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InserirItem(tipoItem); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem AtualizarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.AtualizarItem(tipoItem); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem ExcluirItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.ExcluirItem(tipoItem); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem InativarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InativarItem(tipoItem); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem SalvarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            if (tipoItem.Id.Equals(0))
                tipoItem = this.InserirItem(tipoItem);
            else
                tipoItem = this.AtualizarItem(tipoItem);

            return tipoItem;
        }

        #endregion 
    } 
} 
