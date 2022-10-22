using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Requisicao.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Requisicao.Permissao.Atribuicao.IAtribuicaoItem _persistenciaAtribuicaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public AtribuicaoItem() 
            : this(new Persistencia.Requisicao.Permissao.Atribuicao.AtribuicaoItem()) 
        { } 

        public AtribuicaoItem(Interface.Requisicao.Permissao.Atribuicao.IAtribuicaoItem persistenciaAtribuicaoItem) 
        { 
            this._persistenciaAtribuicaoItem = persistenciaAtribuicaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarLista() 
        { 
            return _persistenciaAtribuicaoItem.CarregarLista(); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginGrupoId(loginGrupoId); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginPerfilId(loginPerfilId); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem CarregarItem(int requisicaoPermissaoAtribuicaoId)
        {
            return _persistenciaAtribuicaoItem.CarregarItem(requisicaoPermissaoAtribuicaoId);
        }

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem InserirItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.InserirItem(atribuicaoItem); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.AtualizarItem(atribuicaoItem); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.ExcluirItem(atribuicaoItem); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem InativarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.InativarItem(atribuicaoItem); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem SalvarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
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
