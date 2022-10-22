using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo.Tramitador.Acao 
{ 
    public partial class AcaoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.Tramitador.Acao.IAcaoItem _persistenciaAcaoItem { get; set; } 

        #endregion 

        #region Construtores 

        public AcaoItem() 
            : this(new Persistencia.Arquivo.Tramitador.Acao.AcaoItem()) 
        { } 

        public AcaoItem(Interface.Arquivo.Tramitador.Acao.IAcaoItem persistenciaAcaoItem) 
        { 
            this._persistenciaAcaoItem = persistenciaAcaoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarLista() 
        { 
            return _persistenciaAcaoItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaAcaoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem CarregarItem(int arquivoTramitadorAcaoId)
        {
            return _persistenciaAcaoItem.CarregarItem(arquivoTramitadorAcaoId);
        }

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem InserirItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            return _persistenciaAcaoItem.InserirItem(acaoItem); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem AtualizarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            return _persistenciaAcaoItem.AtualizarItem(acaoItem); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem ExcluirItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            return _persistenciaAcaoItem.ExcluirItem(acaoItem); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem InativarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            return _persistenciaAcaoItem.InativarItem(acaoItem); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem SalvarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            if (acaoItem.Id.Equals(0))
                acaoItem = this.InserirItem(acaoItem);
            else
                acaoItem = this.AtualizarItem(acaoItem);

            return acaoItem;
        }

        #endregion 
    } 
} 
