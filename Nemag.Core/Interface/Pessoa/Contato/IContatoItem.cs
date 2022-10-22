using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Contato 
{ 
    public partial interface IContatoItem
    { 
        List<Entidade.Pessoa.Contato.ContatoItem> CarregarLista(); 

        List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorPessoaContatoTipoId(int pessoaContatoTipoId); 

        List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorPessoaId(int pessoaId); 

        List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Pessoa.Contato.ContatoItem CarregarItem(int pessoaContatoId);

        Entidade.Pessoa.Contato.ContatoItem InserirItem(Entidade.Pessoa.Contato.ContatoItem contatoItem); 

        Entidade.Pessoa.Contato.ContatoItem AtualizarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem); 

        Entidade.Pessoa.Contato.ContatoItem ExcluirItem(Entidade.Pessoa.Contato.ContatoItem contatoItem); 

        Entidade.Pessoa.Contato.ContatoItem InativarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem); 
    } 
} 
