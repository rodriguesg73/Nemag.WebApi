namespace Nemag.Core.Interface.Login.Acesso
{
    public partial interface IAcessoItem
    {
        Entidade.Login.Acesso.AcessoItem CarregarItemPorToken(string token);

        Entidade.Login.Acesso.AcessoItem CarregarItemValidoPorRegistroLoginId(int loginId, string ip);

    }
}
