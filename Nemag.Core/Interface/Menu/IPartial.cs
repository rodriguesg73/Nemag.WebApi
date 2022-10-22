using System.Collections.Generic;

namespace Nemag.Core.Interface.Menu 
{ 
    public partial interface IMenuItem
    { 
        List<Entidade.Menu.MenuItem> CarregarListaPorLoginId(int loginId); 
    } 
} 
