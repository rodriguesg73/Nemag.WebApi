using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Venda 
{ 
    public partial class VendaItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Venda.IVendaItem _persistenciaVendaItem { get; set; } 

        #endregion 

        #region Construtores 

        public VendaItem() 
            : this(new Persistencia.Venda.VendaItem()) 
        { } 

        public VendaItem(Interface.Venda.IVendaItem persistenciaVendaItem) 
        { 
            this._persistenciaVendaItem = persistenciaVendaItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Venda.VendaItem> CarregarLista() 
        { 
            return _persistenciaVendaItem.CarregarLista(); 
        } 

        public List<Entidade.Venda.VendaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaVendaItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Venda.VendaItem> CarregarListaPorClienteId(int clienteId) 
        { 
            return _persistenciaVendaItem.CarregarListaPorClienteId(clienteId); 
        } 

        public Entidade.Venda.VendaItem CarregarItem(int vendaId)
        {
            return _persistenciaVendaItem.CarregarItem(vendaId);
        }

        public Entidade.Venda.VendaItem InserirItem(Entidade.Venda.VendaItem vendaItem)
        {
            return _persistenciaVendaItem.InserirItem(vendaItem); 
        } 

        public Entidade.Venda.VendaItem AtualizarItem(Entidade.Venda.VendaItem vendaItem)
        {
            return _persistenciaVendaItem.AtualizarItem(vendaItem); 
        } 

        public Entidade.Venda.VendaItem ExcluirItem(Entidade.Venda.VendaItem vendaItem)
        {
            return _persistenciaVendaItem.ExcluirItem(vendaItem); 
        } 

        public Entidade.Venda.VendaItem InativarItem(Entidade.Venda.VendaItem vendaItem)
        {
            return _persistenciaVendaItem.InativarItem(vendaItem); 
        } 

        public Entidade.Venda.VendaItem SalvarItem(Entidade.Venda.VendaItem vendaItem)
        {
            if (vendaItem.Id.Equals(0))
                vendaItem = this.InserirItem(vendaItem);
            else
                vendaItem = this.AtualizarItem(vendaItem);

            return vendaItem;
        }

        #endregion 
    } 
} 
