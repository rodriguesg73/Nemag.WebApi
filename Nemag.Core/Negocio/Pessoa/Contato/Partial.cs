using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Contato
{
    public partial class ContatoItem : _BaseItem
    {
        public List<Core.Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorLoginId(int loginId)
        {
            return _persistenciaContatoItem.CarregarListaPorLoginId(loginId);
        }

        public Core.Entidade.Pessoa.Contato.ContatoItem CarregarItemPorLoginId(int loginId)
        {
            return _persistenciaContatoItem.CarregarItemPorLoginId(loginId);
        }
    }
}
