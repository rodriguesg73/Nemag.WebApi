using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Grupo 
{ 
    public partial class GrupoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Grupo.IGrupoItem _persistenciaGrupoItem { get; set; } 

        #endregion 

        #region Construtores 

        public GrupoItem() 
            : this(new Persistencia.Login.Grupo.GrupoItem()) 
        { } 

        public GrupoItem(Interface.Login.Grupo.IGrupoItem persistenciaGrupoItem) 
        { 
            this._persistenciaGrupoItem = persistenciaGrupoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Grupo.GrupoItem> CarregarLista() 
        { 
            return _persistenciaGrupoItem.CarregarLista(); 
        } 

        public List<Entidade.Login.Grupo.GrupoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaGrupoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Login.Grupo.GrupoItem CarregarItem(int loginGrupoId)
        {
            return _persistenciaGrupoItem.CarregarItem(loginGrupoId);
        }

        public Entidade.Login.Grupo.GrupoItem InserirItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            return _persistenciaGrupoItem.InserirItem(grupoItem); 
        } 

        public Entidade.Login.Grupo.GrupoItem AtualizarItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            return _persistenciaGrupoItem.AtualizarItem(grupoItem); 
        } 

        public Entidade.Login.Grupo.GrupoItem ExcluirItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            return _persistenciaGrupoItem.ExcluirItem(grupoItem); 
        } 

        public Entidade.Login.Grupo.GrupoItem InativarItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            return _persistenciaGrupoItem.InativarItem(grupoItem); 
        } 

        public Entidade.Login.Grupo.GrupoItem SalvarItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            if (grupoItem.Id.Equals(0))
                grupoItem = this.InserirItem(grupoItem);
            else
                grupoItem = this.AtualizarItem(grupoItem);

            return grupoItem;
        }

        #endregion 
    } 
} 
