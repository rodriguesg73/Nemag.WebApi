using System.Collections.Generic;

namespace Nemag.Core.Negocio.Requisicao.Permissao.Atribuicao
{
    public partial class AtribuicaoItem : _BaseItem
    {
        #region Métodos Públicos 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorUrlDestino(string urlDestino)
        {
            return _persistenciaAtribuicaoItem.CarregarListaPorUrlDestino(urlDestino);
        }

        #endregion 
    }
} 
