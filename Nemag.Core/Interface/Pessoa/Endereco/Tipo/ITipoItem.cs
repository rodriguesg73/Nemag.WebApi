using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Endereco.Tipo 
{ 
    public partial interface ITipoItem
    { 
        List<Entidade.Pessoa.Endereco.Tipo.TipoItem> CarregarLista(); 

        Entidade.Pessoa.Endereco.Tipo.TipoItem CarregarItem(int pessoaEnderecoTipoId);

        Entidade.Pessoa.Endereco.Tipo.TipoItem InserirItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Endereco.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Endereco.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Endereco.Tipo.TipoItem InativarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem); 
    } 
} 
