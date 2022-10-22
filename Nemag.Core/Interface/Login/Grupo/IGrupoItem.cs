using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Grupo 
{ 
    public partial interface IGrupoItem
    { 
        List<Entidade.Login.Grupo.GrupoItem> CarregarLista(); 

        List<Entidade.Login.Grupo.GrupoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Login.Grupo.GrupoItem CarregarItem(int loginGrupoId);

        Entidade.Login.Grupo.GrupoItem InserirItem(Entidade.Login.Grupo.GrupoItem grupoItem); 

        Entidade.Login.Grupo.GrupoItem AtualizarItem(Entidade.Login.Grupo.GrupoItem grupoItem); 

        Entidade.Login.Grupo.GrupoItem ExcluirItem(Entidade.Login.Grupo.GrupoItem grupoItem); 

        Entidade.Login.Grupo.GrupoItem InativarItem(Entidade.Login.Grupo.GrupoItem grupoItem); 
    } 
} 
