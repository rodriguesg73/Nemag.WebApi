using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo.Tramitador 
{ 
    public partial class TramitadorItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.Tramitador.ITramitadorItem _persistenciaTramitadorItem { get; set; } 

        #endregion 

        #region Construtores 

        public TramitadorItem() 
            : this(new Persistencia.Arquivo.Tramitador.TramitadorItem()) 
        { } 

        public TramitadorItem(Interface.Arquivo.Tramitador.ITramitadorItem persistenciaTramitadorItem) 
        { 
            this._persistenciaTramitadorItem = persistenciaTramitadorItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarLista() 
        { 
            return _persistenciaTramitadorItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaTramitadorItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem CarregarItem(int arquivoTramitadorId)
        {
            return _persistenciaTramitadorItem.CarregarItem(arquivoTramitadorId);
        }

        public Entidade.Arquivo.Tramitador.TramitadorItem InserirItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            return _persistenciaTramitadorItem.InserirItem(tramitadorItem); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem AtualizarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            return _persistenciaTramitadorItem.AtualizarItem(tramitadorItem); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem ExcluirItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            return _persistenciaTramitadorItem.ExcluirItem(tramitadorItem); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem InativarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            return _persistenciaTramitadorItem.InativarItem(tramitadorItem); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem SalvarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            if (tramitadorItem.Id.Equals(0))
                tramitadorItem = this.InserirItem(tramitadorItem);
            else
                tramitadorItem = this.AtualizarItem(tramitadorItem);

            return tramitadorItem;
        }

        #endregion 
    } 
} 
