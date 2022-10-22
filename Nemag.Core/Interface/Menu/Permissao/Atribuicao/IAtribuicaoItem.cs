using System.Collections.Generic;

namespace Nemag.Core.Interface.Menu.Permissao.Atribuicao 
{ 
    public partial interface IAtribuicaoItem
    { 
        List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarLista(); 

        List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorMenuId(int menuId); 

        List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId); 

        List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId); 

        Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem CarregarItem(int menuPermissaoAtribuicaoId);

        Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem InserirItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem InativarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem); 
    } 
} 
