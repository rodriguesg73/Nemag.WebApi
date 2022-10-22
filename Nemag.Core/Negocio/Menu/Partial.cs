using System.Collections.Generic;

namespace Nemag.Core.Negocio.Menu
{
    public partial class MenuItem : _BaseItem
    { 
        #region Métodos Públicos 

        public List<Entidade.Menu.MenuItem> CarregarListaPorLoginId(int loginId) 
        { 
            return _persistenciaMenuItem.CarregarListaPorLoginId(loginId); 
        } 

        #endregion 
    } 
} 
