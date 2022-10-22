using System.Collections.Generic;

namespace Nemag.Core.Interface.Produto 
{ 
    public partial interface IProdutoItem
    { 
        List<Entidade.Produto.ProdutoItem> CarregarLista(); 

        List<Entidade.Produto.ProdutoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Produto.ProdutoItem> CarregarListaPorProdutoCategoriaId(int produtoCategoriaId); 

        Entidade.Produto.ProdutoItem CarregarItem(int produtoId);

        Entidade.Produto.ProdutoItem InserirItem(Entidade.Produto.ProdutoItem produtoItem); 

        Entidade.Produto.ProdutoItem AtualizarItem(Entidade.Produto.ProdutoItem produtoItem); 

        Entidade.Produto.ProdutoItem ExcluirItem(Entidade.Produto.ProdutoItem produtoItem); 

        Entidade.Produto.ProdutoItem InativarItem(Entidade.Produto.ProdutoItem produtoItem); 
    } 
} 
