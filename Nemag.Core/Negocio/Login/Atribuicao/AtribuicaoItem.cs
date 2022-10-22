using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Atribuicao.IAtribuicaoItem _persistenciaAtribuicaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public AtribuicaoItem() 
            : this(new Persistencia.Login.Atribuicao.AtribuicaoItem()) 
        { } 

        public AtribuicaoItem(Interface.Login.Atribuicao.IAtribuicaoItem persistenciaAtribuicaoItem) 
        { 
            this._persistenciaAtribuicaoItem = persistenciaAtribuicaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarLista() 
        { 
            return _persistenciaAtribuicaoItem.CarregarLista(); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginGrupoId(loginGrupoId); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginPerfilId(loginPerfilId); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginId(int loginId) 
        { 
            return _persistenciaAtribuicaoItem.CarregarListaPorLoginId(loginId); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem CarregarItem(int loginAtribuicaoId)
        {
            return _persistenciaAtribuicaoItem.CarregarItem(loginAtribuicaoId);
        }

        public Entidade.Login.Atribuicao.AtribuicaoItem InserirItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.InserirItem(atribuicaoItem); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.AtualizarItem(atribuicaoItem); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.ExcluirItem(atribuicaoItem); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem InativarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            return _persistenciaAtribuicaoItem.InativarItem(atribuicaoItem); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem SalvarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
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
