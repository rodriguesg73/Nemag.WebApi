using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Menu.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Menu.Permissao.Atribuicao.IAtribuicaoItem _persistenciaAtribuicaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public AtribuicaoItem() 
            : this(new Persistencia.Menu.Permissao.Atribuicao.AtribuicaoItem()) 
        { } 

        public AtribuicaoItem(Interface.Menu.Permissao.Atribuicao.IAtribuicaoItem persistenciaAtribuicaoItem) 
        { 
            this._persistenciaAtribuicaoItem = persistenciaAtribuicaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarLista() 
        { 
            return _persistenciaAtribuicaoItem.CarregarLista(); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorMenuId(int menuId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorMenuId(menuId); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginGrupoId(loginGrupoId); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginPerfilId(loginPerfilId); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem CarregarItem(int menuPermissaoAtribuicaoId)
        {
            return _persistenciaAtribuicaoItem.CarregarItem(menuPermissaoAtribuicaoId);
        }

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem InserirItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.InserirItem(atribuicaoItem); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.AtualizarItem(atribuicaoItem); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.ExcluirItem(atribuicaoItem); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem InativarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.InativarItem(atribuicaoItem); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem SalvarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            if (atribuicaoItem.Id.Equals(0))
                atribuicaoItem = this.InserirItem(atribuicaoItem);
            else
                atribuicaoItem = this.AtualizarItem(atribuicaoItem);

            return atribuicaoItem;
        }

        #endregion 
    } 
} 
