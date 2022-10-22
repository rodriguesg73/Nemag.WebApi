using System.Collections.Generic;

namespace Nemag.Core.Interface.Requisicao.Argumento 
{ 
    public partial interface IArgumentoItem
    { 
        List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarLista(); 

        List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarListaPorRequisicaoId(int requisicaoId); 

        Entidade.Requisicao.Argumento.ArgumentoItem CarregarItem(int requisicaoArgumentoId);

        Entidade.Requisicao.Argumento.ArgumentoItem InserirItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem); 

        Entidade.Requisicao.Argumento.ArgumentoItem AtualizarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem); 

        Entidade.Requisicao.Argumento.ArgumentoItem ExcluirItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem); 

        Entidade.Requisicao.Argumento.ArgumentoItem InativarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem); 
    } 
} 
