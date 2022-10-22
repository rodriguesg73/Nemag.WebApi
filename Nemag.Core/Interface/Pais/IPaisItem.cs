using System.Collections.Generic;

namespace Nemag.Core.Interface.Pais 
{ 
    public partial interface IPaisItem
    { 
        List<Entidade.Pais.PaisItem> CarregarLista(); 

        List<Entidade.Pais.PaisItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Pais.PaisItem> CarregarListaPorPaisContinenteId(int paisContinenteId); 

        Entidade.Pais.PaisItem CarregarItem(int paisId);

        Entidade.Pais.PaisItem InserirItem(Entidade.Pais.PaisItem paisItem); 

        Entidade.Pais.PaisItem AtualizarItem(Entidade.Pais.PaisItem paisItem); 

        Entidade.Pais.PaisItem ExcluirItem(Entidade.Pais.PaisItem paisItem); 

        Entidade.Pais.PaisItem InativarItem(Entidade.Pais.PaisItem paisItem); 
    } 
} 
