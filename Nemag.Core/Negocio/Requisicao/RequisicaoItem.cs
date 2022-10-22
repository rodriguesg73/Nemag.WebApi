using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Requisicao 
{ 
    public partial class RequisicaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Requisicao.IRequisicaoItem _persistenciaRequisicaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public RequisicaoItem() 
            : this(new Persistencia.Requisicao.RequisicaoItem()) 
        { } 

        public RequisicaoItem(Interface.Requisicao.IRequisicaoItem persistenciaRequisicaoItem) 
        { 
            this._persistenciaRequisicaoItem = persistenciaRequisicaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Requisicao.RequisicaoItem> CarregarLista() 
        { 
            return _persistenciaRequisicaoItem.CarregarLista(); 
        } 

        public List<Entidade.Requisicao.RequisicaoItem> CarregarListaPorLoginAcessoId(int loginAcessoId) 
        { 
            return _persistenciaRequisicaoItem.CarregarListaPorLoginAcessoId(loginAcessoId); 
        } 

        public Entidade.Requisicao.RequisicaoItem CarregarItem(int requisicaoId)
        {
            return _persistenciaRequisicaoItem.CarregarItem(requisicaoId);
        }

        public Entidade.Requisicao.RequisicaoItem InserirItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            return _persistenciaRequisicaoItem.InserirItem(requisicaoItem); 
        } 

        public Entidade.Requisicao.RequisicaoItem AtualizarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            return _persistenciaRequisicaoItem.AtualizarItem(requisicaoItem); 
        } 

        public Entidade.Requisicao.RequisicaoItem ExcluirItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            return _persistenciaRequisicaoItem.ExcluirItem(requisicaoItem); 
        } 

        public Entidade.Requisicao.RequisicaoItem InativarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            return _persistenciaRequisicaoItem.InativarItem(requisicaoItem); 
        } 

        public Entidade.Requisicao.RequisicaoItem SalvarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            if (requisicaoItem.Id.Equals(0))
                requisicaoItem = this.InserirItem(requisicaoItem);
            else
                requisicaoItem = this.AtualizarItem(requisicaoItem);

            return requisicaoItem;
        }

        #endregion 
    } 
} 
