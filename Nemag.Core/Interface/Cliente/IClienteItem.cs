using System.Collections.Generic;

namespace Nemag.Core.Interface.Cliente 
{ 
    public partial interface IClienteItem
    { 
        List<Entidade.Cliente.ClienteItem> CarregarLista(); 

        List<Entidade.Cliente.ClienteItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Cliente.ClienteItem> CarregarListaPorPessoaId(int pessoaId); 

        Entidade.Cliente.ClienteItem CarregarItem(int clienteId);

        Entidade.Cliente.ClienteItem InserirItem(Entidade.Cliente.ClienteItem clienteItem); 

        Entidade.Cliente.ClienteItem AtualizarItem(Entidade.Cliente.ClienteItem clienteItem); 

        Entidade.Cliente.ClienteItem ExcluirItem(Entidade.Cliente.ClienteItem clienteItem); 

        Entidade.Cliente.ClienteItem InativarItem(Entidade.Cliente.ClienteItem clienteItem); 
    } 
} 
