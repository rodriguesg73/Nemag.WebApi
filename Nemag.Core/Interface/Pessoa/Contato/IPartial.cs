using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Contato
{
    public partial interface IContatoItem
    {
        List<Core.Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorLoginId(int loginId);

        Core.Entidade.Pessoa.Contato.ContatoItem CarregarItemPorLoginId(int loginId);
    }
}
