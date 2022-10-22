using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa 
{ 
    public partial interface IPessoaItem
    { 
        List<Entidade.Pessoa.PessoaItem> CarregarLista(); 

        Entidade.Pessoa.PessoaItem CarregarItem(int pessoaId);

        Entidade.Pessoa.PessoaItem InserirItem(Entidade.Pessoa.PessoaItem pessoaItem); 

        Entidade.Pessoa.PessoaItem AtualizarItem(Entidade.Pessoa.PessoaItem pessoaItem); 

        Entidade.Pessoa.PessoaItem ExcluirItem(Entidade.Pessoa.PessoaItem pessoaItem); 

        Entidade.Pessoa.PessoaItem InativarItem(Entidade.Pessoa.PessoaItem pessoaItem); 
    } 
} 
