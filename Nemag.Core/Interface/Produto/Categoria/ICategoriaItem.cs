using System.Collections.Generic;

namespace Nemag.Core.Interface.Produto.Categoria 
{ 
    public partial interface ICategoriaItem
    { 
        List<Entidade.Produto.Categoria.CategoriaItem> CarregarLista(); 

        List<Entidade.Produto.Categoria.CategoriaItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Produto.Categoria.CategoriaItem CarregarItem(int produtoCategoriaId);

        Entidade.Produto.Categoria.CategoriaItem InserirItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem); 

        Entidade.Produto.Categoria.CategoriaItem AtualizarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem); 

        Entidade.Produto.Categoria.CategoriaItem ExcluirItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem); 

        Entidade.Produto.Categoria.CategoriaItem InativarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem); 
    } 
} 
