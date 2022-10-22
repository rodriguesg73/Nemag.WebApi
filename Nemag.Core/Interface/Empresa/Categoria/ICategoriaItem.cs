using System.Collections.Generic;

namespace Nemag.Core.Interface.Empresa.Categoria 
{ 
    public partial interface ICategoriaItem
    { 
        List<Entidade.Empresa.Categoria.CategoriaItem> CarregarLista(); 

        List<Entidade.Empresa.Categoria.CategoriaItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Empresa.Categoria.CategoriaItem CarregarItem(int empresaCategoriaId);

        Entidade.Empresa.Categoria.CategoriaItem InserirItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem); 

        Entidade.Empresa.Categoria.CategoriaItem AtualizarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem); 

        Entidade.Empresa.Categoria.CategoriaItem ExcluirItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem); 

        Entidade.Empresa.Categoria.CategoriaItem InativarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem); 
    } 
} 
