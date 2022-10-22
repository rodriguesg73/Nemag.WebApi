using System.Collections.Generic;

namespace Nemag.Core.Interface.Pais.Continente 
{ 
    public partial interface IContinenteItem
    { 
        List<Entidade.Pais.Continente.ContinenteItem> CarregarLista(); 

        List<Entidade.Pais.Continente.ContinenteItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Pais.Continente.ContinenteItem CarregarItem(int paisContinenteId);

        Entidade.Pais.Continente.ContinenteItem InserirItem(Entidade.Pais.Continente.ContinenteItem continenteItem); 

        Entidade.Pais.Continente.ContinenteItem AtualizarItem(Entidade.Pais.Continente.ContinenteItem continenteItem); 

        Entidade.Pais.Continente.ContinenteItem ExcluirItem(Entidade.Pais.Continente.ContinenteItem continenteItem); 

        Entidade.Pais.Continente.ContinenteItem InativarItem(Entidade.Pais.Continente.ContinenteItem continenteItem); 
    } 
} 
