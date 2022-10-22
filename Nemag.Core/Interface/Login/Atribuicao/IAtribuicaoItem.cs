using System.Collections.Generic;

namespace Nemag.Core.Interface.Login.Atribuicao 
{ 
    public partial interface IAtribuicaoItem
    { 
        List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarLista(); 

        List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId); 

        List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId); 

        List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginId(int loginId); 

        Entidade.Login.Atribuicao.AtribuicaoItem CarregarItem(int loginAtribuicaoId);

        Entidade.Login.Atribuicao.AtribuicaoItem InserirItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Login.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Login.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem); 

        Entidade.Login.Atribuicao.AtribuicaoItem InativarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem); 
    } 
} 
