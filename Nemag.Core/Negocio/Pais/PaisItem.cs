using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pais 
{ 
    public partial class PaisItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pais.IPaisItem _persistenciaPaisItem { get; set; } 

        #endregion 

        #region Construtores 

        public PaisItem() 
            : this(new Persistencia.Pais.PaisItem()) 
        { } 

        public PaisItem(Interface.Pais.IPaisItem persistenciaPaisItem) 
        { 
            this._persistenciaPaisItem = persistenciaPaisItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pais.PaisItem> CarregarLista() 
        { 
            return _persistenciaPaisItem.CarregarLista(); 
        } 

        public List<Entidade.Pais.PaisItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaPaisItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Pais.PaisItem> CarregarListaPorPaisContinenteId(int paisContinenteId) 
        { 
            return _persistenciaPaisItem.CarregarListaPorPaisContinenteId(paisContinenteId); 
        } 

        public Entidade.Pais.PaisItem CarregarItem(int paisId)
        {
            return _persistenciaPaisItem.CarregarItem(paisId);
        }

        public Entidade.Pais.PaisItem InserirItem(Entidade.Pais.PaisItem paisItem)
        {
            return _persistenciaPaisItem.InserirItem(paisItem); 
        } 

        public Entidade.Pais.PaisItem AtualizarItem(Entidade.Pais.PaisItem paisItem)
        {
            return _persistenciaPaisItem.AtualizarItem(paisItem); 
        } 

        public Entidade.Pais.PaisItem ExcluirItem(Entidade.Pais.PaisItem paisItem)
        {
            return _persistenciaPaisItem.ExcluirItem(paisItem); 
        } 

        public Entidade.Pais.PaisItem InativarItem(Entidade.Pais.PaisItem paisItem)
        {
            return _persistenciaPaisItem.InativarItem(paisItem); 
        } 

        public Entidade.Pais.PaisItem SalvarItem(Entidade.Pais.PaisItem paisItem)
        {
            if (paisItem.Id.Equals(0))
                paisItem = this.InserirItem(paisItem);
            else
                paisItem = this.AtualizarItem(paisItem);

            return paisItem;
        }

        #endregion 
    } 
} 
