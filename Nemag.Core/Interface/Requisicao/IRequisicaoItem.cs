using System.Collections.Generic;

namespace Nemag.Core.Interface.Requisicao 
{ 
    public partial interface IRequisicaoItem
    { 
        List<Entidade.Requisicao.RequisicaoItem> CarregarLista(); 

        List<Entidade.Requisicao.RequisicaoItem> CarregarListaPorLoginAcessoId(int loginAcessoId); 

        Entidade.Requisicao.RequisicaoItem CarregarItem(int requisicaoId);

        Entidade.Requisicao.RequisicaoItem InserirItem(Entidade.Requisicao.RequisicaoItem requisicaoItem); 

        Entidade.Requisicao.RequisicaoItem AtualizarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem); 

        Entidade.Requisicao.RequisicaoItem ExcluirItem(Entidade.Requisicao.RequisicaoItem requisicaoItem); 

        Entidade.Requisicao.RequisicaoItem InativarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem); 
    } 
} 
