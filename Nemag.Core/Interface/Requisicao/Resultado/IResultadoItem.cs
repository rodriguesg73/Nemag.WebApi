using System.Collections.Generic;

namespace Nemag.Core.Interface.Requisicao.Resultado 
{ 
    public partial interface IResultadoItem
    { 
        List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarLista(); 

        List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarListaPorRequisicaoId(int requisicaoId); 

        Entidade.Requisicao.Resultado.ResultadoItem CarregarItem(int requisicaoResultadoId);

        Entidade.Requisicao.Resultado.ResultadoItem InserirItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem); 

        Entidade.Requisicao.Resultado.ResultadoItem AtualizarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem); 

        Entidade.Requisicao.Resultado.ResultadoItem ExcluirItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem); 

        Entidade.Requisicao.Resultado.ResultadoItem InativarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem); 
    } 
} 
