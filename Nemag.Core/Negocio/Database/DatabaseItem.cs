using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Database 
{ 
    public partial class DatabaseItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Database.IDatabaseItem _persistenciaDatabaseItem { get; set; } 

        #endregion 

        #region Construtores 

        public DatabaseItem() 
            : this(new Persistencia.Database.DatabaseItem()) 
        { } 

        public DatabaseItem(Interface.Database.IDatabaseItem persistenciaDatabaseItem) 
        { 
            this._persistenciaDatabaseItem = persistenciaDatabaseItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Database.DatabaseItem> CarregarLista() 
        { 
            return _persistenciaDatabaseItem.CarregarLista(); 
        } 

        public List<Entidade.Database.DatabaseItem> CarregarListaPorDatabaseTipoId(int databaseTipoId) 
        { 
            return _persistenciaDatabaseItem.CarregarListaPorDatabaseTipoId(databaseTipoId); 
        } 

        public List<Entidade.Database.DatabaseItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaDatabaseItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Database.DatabaseItem CarregarItem(int databaseId)
        {
            return _persistenciaDatabaseItem.CarregarItem(databaseId);
        }

        public Entidade.Database.DatabaseItem InserirItem(Entidade.Database.DatabaseItem databaseItem)
        {
            return _persistenciaDatabaseItem.InserirItem(databaseItem); 
        } 

        public Entidade.Database.DatabaseItem AtualizarItem(Entidade.Database.DatabaseItem databaseItem)
        {
            return _persistenciaDatabaseItem.AtualizarItem(databaseItem); 
        } 

        public Entidade.Database.DatabaseItem ExcluirItem(Entidade.Database.DatabaseItem databaseItem)
        {
            return _persistenciaDatabaseItem.ExcluirItem(databaseItem); 
        } 

        public Entidade.Database.DatabaseItem InativarItem(Entidade.Database.DatabaseItem databaseItem)
        {
            return _persistenciaDatabaseItem.InativarItem(databaseItem); 
        } 

        public Entidade.Database.DatabaseItem SalvarItem(Entidade.Database.DatabaseItem databaseItem)
        {
            if (databaseItem.Id.Equals(0))
                databaseItem = this.InserirItem(databaseItem);
            else
                databaseItem = this.AtualizarItem(databaseItem);

            return databaseItem;
        }

        #endregion 
    } 
} 
