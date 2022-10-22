using System.Collections.Generic;

namespace Nemag.Core.Interface.Database.Tipo 
{ 
    public partial interface ITipoItem
    { 
        List<Entidade.Database.Tipo.TipoItem> CarregarLista(); 

        List<Entidade.Database.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Database.Tipo.TipoItem CarregarItem(int databaseTipoId);

        Entidade.Database.Tipo.TipoItem InserirItem(Entidade.Database.Tipo.TipoItem tipoItem); 

        Entidade.Database.Tipo.TipoItem AtualizarItem(Entidade.Database.Tipo.TipoItem tipoItem); 

        Entidade.Database.Tipo.TipoItem ExcluirItem(Entidade.Database.Tipo.TipoItem tipoItem); 

        Entidade.Database.Tipo.TipoItem InativarItem(Entidade.Database.Tipo.TipoItem tipoItem); 
    } 
} 
