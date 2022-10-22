using System.Collections.Generic;

namespace Nemag.Core.Interface.Menu 
{ 
    public partial interface IMenuItem
    { 
        List<Entidade.Menu.MenuItem> CarregarLista(); 

        List<Entidade.Menu.MenuItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Menu.MenuItem CarregarItem(int menuId);

        Entidade.Menu.MenuItem InserirItem(Entidade.Menu.MenuItem menuItem); 

        Entidade.Menu.MenuItem AtualizarItem(Entidade.Menu.MenuItem menuItem); 

        Entidade.Menu.MenuItem ExcluirItem(Entidade.Menu.MenuItem menuItem); 

        Entidade.Menu.MenuItem InativarItem(Entidade.Menu.MenuItem menuItem); 
    } 
} 
