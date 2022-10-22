using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Requisicao.Resultado 
{ 
    public partial class ResultadoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Requisicao.Resultado.IResultadoItem _persistenciaResultadoItem { get; set; } 

        #endregion 

        #region Construtores 

        public ResultadoItem() 
            : this(new Persistencia.Requisicao.Resultado.ResultadoItem()) 
        { } 

        public ResultadoItem(Interface.Requisicao.Resultado.IResultadoItem persistenciaResultadoItem) 
        { 
            this._persistenciaResultadoItem = persistenciaResultadoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarLista() 
        { 
            return _persistenciaResultadoItem.CarregarLista(); 
        } 

        public List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarListaPorRequisicaoId(int requisicaoId) 
        { 
            return _persistenciaResultadoItem.CarregarListaPorRequisicaoId(requisicaoId); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem CarregarItem(int requisicaoResultadoId)
        {
            return _persistenciaResultadoItem.CarregarItem(requisicaoResultadoId);
        }

        public Entidade.Requisicao.Resultado.ResultadoItem InserirItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            return _persistenciaResultadoItem.InserirItem(resultadoItem); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem AtualizarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            return _persistenciaResultadoItem.AtualizarItem(resultadoItem); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem ExcluirItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            return _persistenciaResultadoItem.ExcluirItem(resultadoItem); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem InativarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            return _persistenciaResultadoItem.InativarItem(resultadoItem); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem SalvarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            if (resultadoItem.Id.Equals(0))
                resultadoItem = this.InserirItem(resultadoItem);
            else
                resultadoItem = this.AtualizarItem(resultadoItem);

            return resultadoItem;
        }

        #endregion 
    } 
} 
