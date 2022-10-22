using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Menu.Permissao.Login 
{ 
    public partial class LoginItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Menu.Permissao.Login.ILoginItem _persistenciaLoginItem { get; set; } 

        #endregion 

        #region Construtores 

        public LoginItem() 
            : this(new Persistencia.Menu.Permissao.Login.LoginItem()) 
        { } 

        public LoginItem(Interface.Menu.Permissao.Login.ILoginItem persistenciaLoginItem) 
        { 
            this._persistenciaLoginItem = persistenciaLoginItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarLista() 
        { 
            return _persistenciaLoginItem.CarregarLista(); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaLoginItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorMenuId(int menuId) 
        { 
            return _persistenciaLoginItem.CarregarListaPorMenuId(menuId); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorLoginId(int loginId) 
        { 
            return _persistenciaLoginItem.CarregarListaPorLoginId(loginId); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem CarregarItem(int menuPermissaoLoginId)
        {
            return _persistenciaLoginItem.CarregarItem(menuPermissaoLoginId);
        }

        public Entidade.Menu.Permissao.Login.LoginItem InserirItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.InserirItem(loginItem); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem AtualizarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.AtualizarItem(loginItem); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem ExcluirItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.ExcluirItem(loginItem); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem InativarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.InativarItem(loginItem); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem SalvarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            if (loginItem.Id.Equals(0))
                loginItem = this.InserirItem(loginItem);
            else
                loginItem = this.AtualizarItem(loginItem);

            return loginItem;
        }

        #endregion 
    } 
} 
