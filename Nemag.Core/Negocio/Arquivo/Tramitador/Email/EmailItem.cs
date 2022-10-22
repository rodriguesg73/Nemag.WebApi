using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo.Tramitador.Email 
{ 
    public partial class EmailItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.Tramitador.Email.IEmailItem _persistenciaEmailItem { get; set; } 

        #endregion 

        #region Construtores 

        public EmailItem() 
            : this(new Persistencia.Arquivo.Tramitador.Email.EmailItem()) 
        { } 

        public EmailItem(Interface.Arquivo.Tramitador.Email.IEmailItem persistenciaEmailItem) 
        { 
            this._persistenciaEmailItem = persistenciaEmailItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarLista() 
        { 
            return _persistenciaEmailItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaEmailItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId) 
        { 
            return _persistenciaEmailItem.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId) 
        { 
            return _persistenciaEmailItem.CarregarListaPorArquivoTramitadorAcaoId(arquivoTramitadorAcaoId); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem CarregarItem(int arquivoTramitadorEmailId)
        {
            return _persistenciaEmailItem.CarregarItem(arquivoTramitadorEmailId);
        }

        public Entidade.Arquivo.Tramitador.Email.EmailItem InserirItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            return _persistenciaEmailItem.InserirItem(emailItem); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem AtualizarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            return _persistenciaEmailItem.AtualizarItem(emailItem); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem ExcluirItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            return _persistenciaEmailItem.ExcluirItem(emailItem); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem InativarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            return _persistenciaEmailItem.InativarItem(emailItem); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem SalvarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            if (emailItem.Id.Equals(0))
                emailItem = this.InserirItem(emailItem);
            else
                emailItem = this.AtualizarItem(emailItem);

            return emailItem;
        }

        #endregion 
    } 
} 
