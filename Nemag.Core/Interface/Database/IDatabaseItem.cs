using System.Collections.Generic;

namespace Nemag.Core.Interface.Database 
{ 
    public partial interface IDatabaseItem
    { 
        List<Entidade.Database.DatabaseItem> CarregarLista(); 

        List<Entidade.Database.DatabaseItem> CarregarListaPorDatabaseTipoId(int databaseTipoId); 

        List<Entidade.Database.DatabaseItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Database.DatabaseItem CarregarItem(int databaseId);

        Entidade.Database.DatabaseItem InserirItem(Entidade.Database.DatabaseItem databaseItem); 

        Entidade.Database.DatabaseItem AtualizarItem(Entidade.Database.DatabaseItem databaseItem); 

        Entidade.Database.DatabaseItem ExcluirItem(Entidade.Database.DatabaseItem databaseItem); 

        Entidade.Database.DatabaseItem InativarItem(Entidade.Database.DatabaseItem databaseItem); 
    } 
} 
