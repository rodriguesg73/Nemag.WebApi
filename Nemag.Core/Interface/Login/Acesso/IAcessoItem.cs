using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Acesso 
{ 
    public partial interface IAcessoItem
    { 
        List<Entidade.Login.Acesso.AcessoItem> CarregarLista(); 

        List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorLoginId(int loginId); 

        Entidade.Login.Acesso.AcessoItem CarregarItem(int loginAcessoId);

        Entidade.Login.Acesso.AcessoItem InserirItem(Entidade.Login.Acesso.AcessoItem acessoItem); 

        Entidade.Login.Acesso.AcessoItem AtualizarItem(Entidade.Login.Acesso.AcessoItem acessoItem); 

        Entidade.Login.Acesso.AcessoItem ExcluirItem(Entidade.Login.Acesso.AcessoItem acessoItem); 

        Entidade.Login.Acesso.AcessoItem InativarItem(Entidade.Login.Acesso.AcessoItem acessoItem); 
    } 
} 
