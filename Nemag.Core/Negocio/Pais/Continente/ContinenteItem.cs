using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pais.Continente 
{ 
    public partial class ContinenteItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pais.Continente.IContinenteItem _persistenciaContinenteItem { get; set; } 

        #endregion 

        #region Construtores 

        public ContinenteItem() 
            : this(new Persistencia.Pais.Continente.ContinenteItem()) 
        { } 

        public ContinenteItem(Interface.Pais.Continente.IContinenteItem persistenciaContinenteItem) 
        { 
            this._persistenciaContinenteItem = persistenciaContinenteItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pais.Continente.ContinenteItem> CarregarLista() 
        { 
            return _persistenciaContinenteItem.CarregarLista(); 
        } 

        public List<Entidade.Pais.Continente.ContinenteItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaContinenteItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Pais.Continente.ContinenteItem CarregarItem(int paisContinenteId)
        {
            return _persistenciaContinenteItem.CarregarItem(paisContinenteId);
        }

        public Entidade.Pais.Continente.ContinenteItem InserirItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            return _persistenciaContinenteItem.InserirItem(continenteItem); 
        } 

        public Entidade.Pais.Continente.ContinenteItem AtualizarItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            return _persistenciaContinenteItem.AtualizarItem(continenteItem); 
        } 

        public Entidade.Pais.Continente.ContinenteItem ExcluirItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            return _persistenciaContinenteItem.ExcluirItem(continenteItem); 
        } 

        public Entidade.Pais.Continente.ContinenteItem InativarItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            return _persistenciaContinenteItem.InativarItem(continenteItem); 
        } 

        public Entidade.Pais.Continente.ContinenteItem SalvarItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            if (continenteItem.Id.Equals(0))
                continenteItem = this.InserirItem(continenteItem);
            else
                continenteItem = this.AtualizarItem(continenteItem);

            return continenteItem;
        }

        #endregion 
    } 
} 
