using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo.Acesso 
{ 
    public partial interface IAcessoItem
    { 
        List<Entidade.Arquivo.Acesso.AcessoItem> CarregarLista(); 

        List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorArquivoId(int arquivoId); 

        List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Arquivo.Acesso.AcessoItem CarregarItem(int arquivoAcessoId);

        Entidade.Arquivo.Acesso.AcessoItem InserirItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem); 

        Entidade.Arquivo.Acesso.AcessoItem AtualizarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem); 

        Entidade.Arquivo.Acesso.AcessoItem ExcluirItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem); 

        Entidade.Arquivo.Acesso.AcessoItem InativarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem); 
    } 
} 
