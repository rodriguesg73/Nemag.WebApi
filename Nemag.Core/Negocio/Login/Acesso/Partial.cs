namespace Nemag.Core.Negocio.Login.Acesso
{
    public partial class AcessoItem
    {
        #region Métodos Públicos

        public Entidade.Login.Acesso.AcessoItem CarregarItemPorToken(string token)
        {
            return _persistenciaAcessoItem.CarregarItemPorToken(token);
        }

        public Entidade.Login.Acesso.AcessoItem CarregarItemValidoPorRegistroLoginId(int loginId, string ip)
        {
            return _persistenciaAcessoItem.CarregarItemValidoPorRegistroLoginId(loginId, ip);
        }

        #endregion
    }
}
