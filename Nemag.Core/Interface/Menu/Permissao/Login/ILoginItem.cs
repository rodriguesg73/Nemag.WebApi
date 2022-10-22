using System.Collections.Generic;

namespace Nemag.Core.Interface.Menu.Permissao.Login 
{ 
    public partial interface ILoginItem
    { 
        List<Entidade.Menu.Permissao.Login.LoginItem> CarregarLista(); 

        List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorMenuId(int menuId); 

        List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorLoginId(int loginId); 

        Entidade.Menu.Permissao.Login.LoginItem CarregarItem(int menuPermissaoLoginId);

        Entidade.Menu.Permissao.Login.LoginItem InserirItem(Entidade.Menu.Permissao.Login.LoginItem loginItem); 

        Entidade.Menu.Permissao.Login.LoginItem AtualizarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem); 

        Entidade.Menu.Permissao.Login.LoginItem ExcluirItem(Entidade.Menu.Permissao.Login.LoginItem loginItem); 

        Entidade.Menu.Permissao.Login.LoginItem InativarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem); 
    } 
} 
