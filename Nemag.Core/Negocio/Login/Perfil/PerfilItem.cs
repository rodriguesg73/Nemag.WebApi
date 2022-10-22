using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Perfil 
{ 
    public partial class PerfilItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Perfil.IPerfilItem _persistenciaPerfilItem { get; set; } 

        #endregion 

        #region Construtores 

        public PerfilItem() 
            : this(new Persistencia.Login.Perfil.PerfilItem()) 
        { } 

        public PerfilItem(Interface.Login.Perfil.IPerfilItem persistenciaPerfilItem) 
        { 
            this._persistenciaPerfilItem = persistenciaPerfilItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Perfil.PerfilItem> CarregarLista() 
        { 
            return _persistenciaPerfilItem.CarregarLista(); 
        } 

        public List<Entidade.Login.Perfil.PerfilItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaPerfilItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Login.Perfil.PerfilItem CarregarItem(int loginPerfilId)
        {
            return _persistenciaPerfilItem.CarregarItem(loginPerfilId);
        }

        public Entidade.Login.Perfil.PerfilItem InserirItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            return _persistenciaPerfilItem.InserirItem(perfilItem); 
        } 

        public Entidade.Login.Perfil.PerfilItem AtualizarItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            return _persistenciaPerfilItem.AtualizarItem(perfilItem); 
        } 

        public Entidade.Login.Perfil.PerfilItem ExcluirItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            return _persistenciaPerfilItem.ExcluirItem(perfilItem); 
        } 

        public Entidade.Login.Perfil.PerfilItem InativarItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            return _persistenciaPerfilItem.InativarItem(perfilItem); 
        } 

        public Entidade.Login.Perfil.PerfilItem SalvarItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            if (perfilItem.Id.Equals(0))
                perfilItem = this.InserirItem(perfilItem);
            else
                perfilItem = this.AtualizarItem(perfilItem);

            return perfilItem;
        }

        #endregion 
    } 
} 
