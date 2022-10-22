using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Perfil 
{ 
    public partial interface IPerfilItem
    { 
        List<Entidade.Login.Perfil.PerfilItem> CarregarLista(); 

        List<Entidade.Login.Perfil.PerfilItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Login.Perfil.PerfilItem CarregarItem(int loginPerfilId);

        Entidade.Login.Perfil.PerfilItem InserirItem(Entidade.Login.Perfil.PerfilItem perfilItem); 

        Entidade.Login.Perfil.PerfilItem AtualizarItem(Entidade.Login.Perfil.PerfilItem perfilItem); 

        Entidade.Login.Perfil.PerfilItem ExcluirItem(Entidade.Login.Perfil.PerfilItem perfilItem); 

        Entidade.Login.Perfil.PerfilItem InativarItem(Entidade.Login.Perfil.PerfilItem perfilItem); 
    } 
} 
