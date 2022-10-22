using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Requisicao.Argumento 
{ 
    public partial class ArgumentoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Requisicao.Argumento.IArgumentoItem _persistenciaArgumentoItem { get; set; } 

        #endregion 

        #region Construtores 

        public ArgumentoItem() 
            : this(new Persistencia.Requisicao.Argumento.ArgumentoItem()) 
        { } 

        public ArgumentoItem(Interface.Requisicao.Argumento.IArgumentoItem persistenciaArgumentoItem) 
        { 
            this._persistenciaArgumentoItem = persistenciaArgumentoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarLista() 
        { 
            return _persistenciaArgumentoItem.CarregarLista(); 
        } 

        public List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarListaPorRequisicaoId(int requisicaoId) 
        { 
            return _persistenciaArgumentoItem.CarregarListaPorRequisicaoId(requisicaoId); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem CarregarItem(int requisicaoArgumentoId)
        {
            return _persistenciaArgumentoItem.CarregarItem(requisicaoArgumentoId);
        }

        public Entidade.Requisicao.Argumento.ArgumentoItem InserirItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            return _persistenciaArgumentoItem.InserirItem(argumentoItem); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem AtualizarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            return _persistenciaArgumentoItem.AtualizarItem(argumentoItem); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem ExcluirItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            return _persistenciaArgumentoItem.ExcluirItem(argumentoItem); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem InativarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            return _persistenciaArgumentoItem.InativarItem(argumentoItem); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem SalvarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            if (argumentoItem.Id.Equals(0))
                argumentoItem = this.InserirItem(argumentoItem);
            else
                argumentoItem = this.AtualizarItem(argumentoItem);

            return argumentoItem;
        }

        #endregion 
    } 
} 
