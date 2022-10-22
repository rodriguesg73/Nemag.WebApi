using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Registro.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Registro.Situacao.ISituacaoItem _persistenciaSituacaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public SituacaoItem() 
            : this(new Persistencia.Registro.Situacao.SituacaoItem()) 
        { } 

        public SituacaoItem(Interface.Registro.Situacao.ISituacaoItem persistenciaSituacaoItem) 
        { 
            this._persistenciaSituacaoItem = persistenciaSituacaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Registro.Situacao.SituacaoItem> CarregarLista() 
        { 
            return _persistenciaSituacaoItem.CarregarLista(); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem CarregarItem(int registroSituacaoId)
        {
            return _persistenciaSituacaoItem.CarregarItem(registroSituacaoId);
        }

        public Entidade.Registro.Situacao.SituacaoItem InserirItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.InserirItem(situacaoItem); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem AtualizarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.AtualizarItem(situacaoItem); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem ExcluirItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.ExcluirItem(situacaoItem); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem InativarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            return _persistenciaSituacaoItem.InativarItem(situacaoItem); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem SalvarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
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
