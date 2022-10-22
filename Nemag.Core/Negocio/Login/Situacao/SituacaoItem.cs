using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Login.Situacao.ISituacaoItem _persistenciaSituacaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public SituacaoItem() 
            : this(new Persistencia.Login.Situacao.SituacaoItem()) 
        { } 

        public SituacaoItem(Interface.Login.Situacao.ISituacaoItem persistenciaSituacaoItem) 
        { 
            this._persistenciaSituacaoItem = persistenciaSituacaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Login.Situacao.SituacaoItem> CarregarLista() 
        { 
            return _persistenciaSituacaoItem.CarregarLista(); 
        } 

        public Entidade.Login.Situacao.SituacaoItem CarregarItem(int loginSituacaoId)
        {
            return _persistenciaSituacaoItem.CarregarItem(loginSituacaoId);
        }

        public Entidade.Login.Situacao.SituacaoItem InserirItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.InserirItem(situacaoItem); 
        } 

        public Entidade.Login.Situacao.SituacaoItem AtualizarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.AtualizarItem(situacaoItem); 
        } 

        public Entidade.Login.Situacao.SituacaoItem ExcluirItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.ExcluirItem(situacaoItem); 
        } 

        public Entidade.Login.Situacao.SituacaoItem InativarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.InativarItem(situacaoItem); 
        } 

        public Entidade.Login.Situacao.SituacaoItem SalvarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            if (situacaoItem.Id.Equals(0))
                situacaoItem = this.InserirItem(situacaoItem);
            else
                situacaoItem = this.AtualizarItem(situacaoItem);

            return situacaoItem;
        }

        #endregion 
    } 
} 
