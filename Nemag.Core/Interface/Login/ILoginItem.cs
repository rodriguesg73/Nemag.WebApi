using System.Collections.Generic;

namespace Nemag.Core.Interface.Login 
{ 
    public partial interface ILoginItem
    { 
        List<Entidade.Login.LoginItem> CarregarLista(); 

        List<Entidade.Login.LoginItem> CarregarListaPorPessoaId(int pessoaId); 

        List<Entidade.Login.LoginItem> CarregarListaPorLoginSituacaoId(int loginSituacaoId); 

        Entidade.Login.LoginItem CarregarItem(int loginId);

        Entidade.Login.LoginItem InserirItem(Entidade.Login.LoginItem loginItem); 

        Entidade.Login.LoginItem AtualizarItem(Entidade.Login.LoginItem loginItem); 

        Entidade.Login.LoginItem ExcluirItem(Entidade.Login.LoginItem loginItem); 

        Entidade.Login.LoginItem InativarItem(Entidade.Login.LoginItem loginItem); 
    } 
} 
