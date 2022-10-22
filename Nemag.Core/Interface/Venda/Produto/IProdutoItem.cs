using System.Collections.Generic;

namespace Nemag.Core.Interface.Venda.Produto 
{ 
    public partial interface IProdutoItem
    { 
        List<Entidade.Venda.Produto.ProdutoItem> CarregarLista(); 

        List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorProdutoId(int produtoId); 

        List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorVendaId(int vendaId); 

        Entidade.Venda.Produto.ProdutoItem CarregarItem(int vendaProdutoId);

        Entidade.Venda.Produto.ProdutoItem InserirItem(Entidade.Venda.Produto.ProdutoItem produtoItem); 

        Entidade.Venda.Produto.ProdutoItem AtualizarItem(Entidade.Venda.Produto.ProdutoItem produtoItem); 

        Entidade.Venda.Produto.ProdutoItem ExcluirItem(Entidade.Venda.Produto.ProdutoItem produtoItem); 

        Entidade.Venda.Produto.ProdutoItem InativarItem(Entidade.Venda.Produto.ProdutoItem produtoItem); 
    } 
} 
