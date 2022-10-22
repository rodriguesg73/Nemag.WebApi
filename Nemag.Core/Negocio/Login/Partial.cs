using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login
{
    public partial class LoginItem
    {
        public List<Core.Entidade.Login.LoginItem> CarregarListaPorAtribuicaoItem(int loginGrupoId, int loginPerfilId)
        {
            return _persistenciaLoginItem.CarregarListaPorAtribuicaoItem(loginGrupoId, loginPerfilId);
        }

        public Entidade.Login.LoginItem CarregarItemPorUsuario(string loginUsuario)
        {
            return _persistenciaLoginItem.CarregarItemPorUsuario(loginUsuario);
        }
    }
}
