using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Produto 
{ 
    public partial class ProdutoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Produto.IProdutoItem _persistenciaProdutoItem { get; set; } 

        #endregion 

        #region Construtores 

        public ProdutoItem() 
            : this(new Persistencia.Produto.ProdutoItem()) 
        { } 

        public ProdutoItem(Interface.Produto.IProdutoItem persistenciaProdutoItem) 
        { 
            this._persistenciaProdutoItem = persistenciaProdutoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Produto.ProdutoItem> CarregarLista() 
        { 
            return _persistenciaProdutoItem.CarregarLista(); 
        } 

        public List<Entidade.Produto.ProdutoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaProdutoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Produto.ProdutoItem> CarregarListaPorProdutoCategoriaId(int produtoCategoriaId) 
        { 
            return _persistenciaProdutoItem.CarregarListaPorProdutoCategoriaId(produtoCategoriaId); 
        } 

        public Entidade.Produto.ProdutoItem CarregarItem(int produtoId)
        {
            return _persistenciaProdutoItem.CarregarItem(produtoId);
        }

        public Entidade.Produto.ProdutoItem InserirItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.InserirItem(produtoItem); 
        } 

        public Entidade.Produto.ProdutoItem AtualizarItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.AtualizarItem(produtoItem); 
        } 

        public Entidade.Produto.ProdutoItem ExcluirItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.ExcluirItem(produtoItem); 
        } 

        public Entidade.Produto.ProdutoItem InativarItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            return _persistenciaProdutoItem.InativarItem(produtoItem); 
        } 

        public Entidade.Produto.ProdutoItem SalvarItem(Entidade.Produto.ProdutoItem produtoItem)
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
