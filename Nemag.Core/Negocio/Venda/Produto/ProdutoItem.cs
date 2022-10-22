using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Venda.Produto 
{ 
    public partial class ProdutoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Venda.Produto.IProdutoItem _persistenciaProdutoItem { get; set; } 

        #endregion 

        #region Construtores 

        public ProdutoItem() 
            : this(new Persistencia.Venda.Produto.ProdutoItem()) 
        { } 

        public ProdutoItem(Interface.Venda.Produto.IProdutoItem persistenciaProdutoItem) 
        { 
            this._persistenciaProdutoItem = persistenciaProdutoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarLista() 
        { 
            return _persistenciaProdutoItem.CarregarLista(); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaProdutoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorProdutoId(int produtoId) 
        { 
            return _persistenciaProdutoItem.CarregarListaPorProdutoId(produtoId); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorVendaId(int vendaId) 
        { 
            return _persistenciaProdutoItem.CarregarListaPorVendaId(vendaId); 
        } 

        public Entidade.Venda.Produto.ProdutoItem CarregarItem(int vendaProdutoId)
        {
            return _persistenciaProdutoItem.CarregarItem(vendaProdutoId);
        }

        public Entidade.Venda.Produto.ProdutoItem InserirItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.InserirItem(produtoItem); 
        } 

        public Entidade.Venda.Produto.ProdutoItem AtualizarItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.AtualizarItem(produtoItem); 
        } 

        public Entidade.Venda.Produto.ProdutoItem ExcluirItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.ExcluirItem(produtoItem); 
        } 

        public Entidade.Venda.Produto.ProdutoItem InativarItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.InativarItem(produtoItem); 
        } 

        public Entidade.Venda.Produto.ProdutoItem SalvarItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            if (produtoItem.Id.Equals(0))
                produtoItem = this.InserirItem(produtoItem);
            else
                produtoItem = this.AtualizarItem(produtoItem);

            return produtoItem;
        }

        #endregion 
    } 
} 
