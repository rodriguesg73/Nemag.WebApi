using System.Collections.Generic;

namespace Nemag.Core.Interface.Requisicao.Permissao.Atribuicao 
{ 
    public partial interface IAtribuicaoItem
    { 
        List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarLista(); 

        List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId); 

        List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId); 

        Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem CarregarItem(int requisicaoPermissaoAtribuicaoId);

        Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem InserirItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem InativarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 
    } 
} 
