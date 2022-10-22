using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Acesso 
{ 
    public partial class AcessoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Acesso.IAcessoItem _persistenciaAcessoItem { get; set; } 

        #endregion 

        #region Construtores 

        public AcessoItem() 
            : this(new Persistencia.Login.Acesso.AcessoItem()) 
        { } 

        public AcessoItem(Interface.Login.Acesso.IAcessoItem persistenciaAcessoItem) 
        { 
            this._persistenciaAcessoItem = persistenciaAcessoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarLista() 
        { 
            return _persistenciaAcessoItem.CarregarLista(); 
        } 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaAcessoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorLoginId(int loginId) 
        { 
            return _persistenciaAcessoItem.CarregarListaPorLoginId(loginId); 
        } 

        public Entidade.Login.Acesso.AcessoItem CarregarItem(int loginAcessoId)
        {
            return _persistenciaAcessoItem.CarregarItem(loginAcessoId);
        }

        public Entidade.Login.Acesso.AcessoItem InserirItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.InserirItem(acessoItem); 
        } 

        public Entidade.Login.Acesso.AcessoItem AtualizarItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.AtualizarItem(acessoItem); 
        } 

        public Entidade.Login.Acesso.AcessoItem ExcluirItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.ExcluirItem(acessoItem); 
        } 

        public Entidade.Login.Acesso.AcessoItem InativarItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.InativarItem(acessoItem); 
        } 

        public Entidade.Login.Acesso.AcessoItem SalvarItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            if (acessoItem.Id.Equals(0))
                acessoItem = this.InserirItem(acessoItem);
            else
                acessoItem = this.AtualizarItem(acessoItem);

            return acessoItem;
        }

        #endregion 
    } 
} 
