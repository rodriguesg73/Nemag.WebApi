using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Database.Tipo 
{ 
    public partial class TipoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Database.Tipo.ITipoItem _persistenciaTipoItem { get; set; } 

        #endregion 

        #region Construtores 

        public TipoItem() 
            : this(new Persistencia.Database.Tipo.TipoItem()) 
        { } 

        public TipoItem(Interface.Database.Tipo.ITipoItem persistenciaTipoItem) 
        { 
            this._persistenciaTipoItem = persistenciaTipoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Database.Tipo.TipoItem> CarregarLista() 
        { 
            return _persistenciaTipoItem.CarregarLista(); 
        } 

        public List<Entidade.Database.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaTipoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Database.Tipo.TipoItem CarregarItem(int databaseTipoId)
        {
            return _persistenciaTipoItem.CarregarItem(databaseTipoId);
        }

        public Entidade.Database.Tipo.TipoItem InserirItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InserirItem(tipoItem); 
        } 

        public Entidade.Database.Tipo.TipoItem AtualizarItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.AtualizarItem(tipoItem); 
        } 

        public Entidade.Database.Tipo.TipoItem ExcluirItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.ExcluirItem(tipoItem); 
        } 

        public Entidade.Database.Tipo.TipoItem InativarItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InativarItem(tipoItem); 
        } 

        public Entidade.Database.Tipo.TipoItem SalvarItem(Entidade.Database.Tipo.TipoItem tipoItem)
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
