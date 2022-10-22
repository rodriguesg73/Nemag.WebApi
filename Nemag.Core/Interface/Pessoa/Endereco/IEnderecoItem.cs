using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Endereco 
{ 
    public partial interface IEnderecoItem
    { 
        List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarLista(); 

        List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorPessoaId(int pessoaId); 

        List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorPessoaEnderecoTipoId(int pessoaEnderecoTipoId); 

        Entidade.Pessoa.Endereco.EnderecoItem CarregarItem(int pessoaEnderecoId);

        Entidade.Pessoa.Endereco.EnderecoItem InserirItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem); 

        Entidade.Pessoa.Endereco.EnderecoItem AtualizarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem); 

        Entidade.Pessoa.Endereco.EnderecoItem ExcluirItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem); 

        Entidade.Pessoa.Endereco.EnderecoItem InativarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem); 
    } 
} 
