using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Produto.Categoria 
{ 
    public partial class CategoriaItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Produto.Categoria.ICategoriaItem _persistenciaCategoriaItem { get; set; } 

        #endregion 

        #region Construtores 

        public CategoriaItem() 
            : this(new Persistencia.Produto.Categoria.CategoriaItem()) 
        { } 

        public CategoriaItem(Interface.Produto.Categoria.ICategoriaItem persistenciaCategoriaItem) 
        { 
            this._persistenciaCategoriaItem = persistenciaCategoriaItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Produto.Categoria.CategoriaItem> CarregarLista() 
        { 
            return _persistenciaCategoriaItem.CarregarLista(); 
        } 

        public List<Entidade.Produto.Categoria.CategoriaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaCategoriaItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem CarregarItem(int produtoCategoriaId)
        {
            return _persistenciaCategoriaItem.CarregarItem(produtoCategoriaId);
        }

        public Entidade.Produto.Categoria.CategoriaItem InserirItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.InserirItem(categoriaItem); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem AtualizarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.AtualizarItem(categoriaItem); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem ExcluirItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.ExcluirItem(categoriaItem); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem InativarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.InativarItem(categoriaItem); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem SalvarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            if (categoriaItem.Id.Equals(0))
                categoriaItem = this.InserirItem(categoriaItem);
            else
                categoriaItem = this.AtualizarItem(categoriaItem);

            return categoriaItem;
        }

        #endregion 
    } 
} 
