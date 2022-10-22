using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Menu 
{ 
    public partial class MenuItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Menu.IMenuItem _persistenciaMenuItem { get; set; } 

        #endregion 

        #region Construtores 

        public MenuItem() 
            : this(new Persistencia.Menu.MenuItem()) 
        { } 

        public MenuItem(Interface.Menu.IMenuItem persistenciaMenuItem) 
        { 
            this._persistenciaMenuItem = persistenciaMenuItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Menu.MenuItem> CarregarLista() 
        { 
            return _persistenciaMenuItem.CarregarLista(); 
        } 

        public List<Entidade.Menu.MenuItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaMenuItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Menu.MenuItem CarregarItem(int menuId)
        {
            return _persistenciaMenuItem.CarregarItem(menuId);
        }

        public Entidade.Menu.MenuItem InserirItem(Entidade.Menu.MenuItem menuItem)
        {
            return _persistenciaMenuItem.InserirItem(menuItem); 
        } 

        public Entidade.Menu.MenuItem AtualizarItem(Entidade.Menu.MenuItem menuItem)
        {
            return _persistenciaMenuItem.AtualizarItem(menuItem); 
        } 

        public Entidade.Menu.MenuItem ExcluirItem(Entidade.Menu.MenuItem menuItem)
        {
            return _persistenciaMenuItem.ExcluirItem(menuItem); 
        } 

        public Entidade.Menu.MenuItem InativarItem(Entidade.Menu.MenuItem menuItem)
        {
            return _persistenciaMenuItem.InativarItem(menuItem); 
        } 

        public Entidade.Menu.MenuItem SalvarItem(Entidade.Menu.MenuItem menuItem)
        {
            if (menuItem.Id.Equals(0))
                menuItem = this.InserirItem(menuItem);
            else
                menuItem = this.AtualizarItem(menuItem);

            return menuItem;
        }

        #endregion 
    } 
} 
