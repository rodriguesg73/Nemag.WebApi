using System.Collections.Generic;

namespace Nemag.Core.Interface.Venda 
{ 
    public partial interface IVendaItem
    { 
        List<Entidade.Venda.VendaItem> CarregarLista(); 

        List<Entidade.Venda.VendaItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Venda.VendaItem> CarregarListaPorClienteId(int clienteId); 

        Entidade.Venda.VendaItem CarregarItem(int vendaId);

        Entidade.Venda.VendaItem InserirItem(Entidade.Venda.VendaItem vendaItem); 

        Entidade.Venda.VendaItem AtualizarItem(Entidade.Venda.VendaItem vendaItem); 

        Entidade.Venda.VendaItem ExcluirItem(Entidade.Venda.VendaItem vendaItem); 

        Entidade.Venda.VendaItem InativarItem(Entidade.Venda.VendaItem vendaItem); 
    } 
} 
