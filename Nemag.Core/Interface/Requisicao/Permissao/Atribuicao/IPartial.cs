using System.Collections.Generic;

namespace Nemag.Core.Interface.Requisicao.Permissao.Atribuicao 
{ 
    public partial interface IAtribuicaoItem
    {
        List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorUrlDestino(string urlDestino); 
    } 
} 
