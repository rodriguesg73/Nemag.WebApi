using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo.Tramitador.Diretorio 
{ 
    public partial class DiretorioItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.Tramitador.Diretorio.IDiretorioItem _persistenciaDiretorioItem { get; set; } 

        #endregion 

        #region Construtores 

        public DiretorioItem() 
            : this(new Persistencia.Arquivo.Tramitador.Diretorio.DiretorioItem()) 
        { } 

        public DiretorioItem(Interface.Arquivo.Tramitador.Diretorio.IDiretorioItem persistenciaDiretorioItem) 
        { 
            this._persistenciaDiretorioItem = persistenciaDiretorioItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarLista() 
        { 
            return _persistenciaDiretorioItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaDiretorioItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId) 
        { 
            return _persistenciaDiretorioItem.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId) 
        { 
            return _persistenciaDiretorioItem.CarregarListaPorArquivoTramitadorAcaoId(arquivoTramitadorAcaoId); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem CarregarItem(int arquivoTramitadorDiretorioId)
        {
            return _persistenciaDiretorioItem.CarregarItem(arquivoTramitadorDiretorioId);
        }

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem InserirItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            return _persistenciaDiretorioItem.InserirItem(diretorioItem); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem AtualizarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            return _persistenciaDiretorioItem.AtualizarItem(diretorioItem); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem ExcluirItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            return _persistenciaDiretorioItem.ExcluirItem(diretorioItem); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem InativarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            return _persistenciaDiretorioItem.InativarItem(diretorioItem); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem SalvarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            if (diretorioItem.Id.Equals(0))
                diretorioItem = this.InserirItem(diretorioItem);
            else
                diretorioItem = this.AtualizarItem(diretorioItem);

            return diretorioItem;
        }

        #endregion 
    } 
} 
