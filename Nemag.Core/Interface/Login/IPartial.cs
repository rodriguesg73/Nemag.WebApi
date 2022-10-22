using System.Collections.Generic;

namespace Nemag.Core.Interface.Login
{
    public partial interface ILoginItem
    {
        public Entidade.Login.LoginItem CarregarItemPorUsuario(string loginUsuario);

        public List<Entidade.Login.LoginItem> CarregarListaPorAtribuicaoItem(int loginGrupoId, int loginPerfilId);


    }
}
