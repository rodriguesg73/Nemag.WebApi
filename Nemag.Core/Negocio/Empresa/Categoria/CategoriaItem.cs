using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Empresa.Categoria 
{ 
    public partial class CategoriaItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Empresa.Categoria.ICategoriaItem _persistenciaCategoriaItem { get; set; } 

        #endregion 

        #region Construtores 

        public CategoriaItem() 
            : this(new Persistencia.Empresa.Categoria.CategoriaItem()) 
        { } 

        public CategoriaItem(Interface.Empresa.Categoria.ICategoriaItem persistenciaCategoriaItem) 
        { 
            this._persistenciaCategoriaItem = persistenciaCategoriaItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Empresa.Categoria.CategoriaItem> CarregarLista() 
        { 
            return _persistenciaCategoriaItem.CarregarLista(); 
        } 

        public List<Entidade.Empresa.Categoria.CategoriaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaCategoriaItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem CarregarItem(int empresaCategoriaId)
        {
            return _persistenciaCategoriaItem.CarregarItem(empresaCategoriaId);
        }

        public Entidade.Empresa.Categoria.CategoriaItem InserirItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.InserirItem(categoriaItem); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem AtualizarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.AtualizarItem(categoriaItem); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem ExcluirItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.ExcluirItem(categoriaItem); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem InativarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            return _persistenciaCategoriaItem.InativarItem(categoriaItem); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem SalvarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
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
