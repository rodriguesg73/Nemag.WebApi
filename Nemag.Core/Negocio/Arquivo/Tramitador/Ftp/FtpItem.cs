using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo.Tramitador.Ftp 
{ 
    public partial class FtpItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.Tramitador.Ftp.IFtpItem _persistenciaFtpItem { get; set; } 

        #endregion 

        #region Construtores 

        public FtpItem() 
            : this(new Persistencia.Arquivo.Tramitador.Ftp.FtpItem()) 
        { } 

        public FtpItem(Interface.Arquivo.Tramitador.Ftp.IFtpItem persistenciaFtpItem) 
        { 
            this._persistenciaFtpItem = persistenciaFtpItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarLista() 
        { 
            return _persistenciaFtpItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaFtpItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId) 
        { 
            return _persistenciaFtpItem.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId) 
        { 
            return _persistenciaFtpItem.CarregarListaPorArquivoTramitadorAcaoId(arquivoTramitadorAcaoId); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem CarregarItem(int arquivoTramitadorFtpId)
        {
            return _persistenciaFtpItem.CarregarItem(arquivoTramitadorFtpId);
        }

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem InserirItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            return _persistenciaFtpItem.InserirItem(ftpItem); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem AtualizarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            return _persistenciaFtpItem.AtualizarItem(ftpItem); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem ExcluirItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            return _persistenciaFtpItem.ExcluirItem(ftpItem); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem InativarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            return _persistenciaFtpItem.InativarItem(ftpItem); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem SalvarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            if (ftpItem.Id.Equals(0))
                ftpItem = this.InserirItem(ftpItem);
            else
                ftpItem = this.AtualizarItem(ftpItem);

            return ftpItem;
        }

        #endregion 
    } 
} 
