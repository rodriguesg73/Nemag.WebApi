using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Contato.Tipo 
{ 
    public partial interface ITipoItem
    { 
        List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarLista(); 

        List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Pessoa.Contato.Tipo.TipoItem CarregarItem(int pessoaContatoTipoId);

        Entidade.Pessoa.Contato.Tipo.TipoItem InserirItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Contato.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Contato.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Contato.Tipo.TipoItem InativarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem); 
    } 
} 
