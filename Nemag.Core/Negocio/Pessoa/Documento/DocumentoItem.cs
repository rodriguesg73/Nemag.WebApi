using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Documento 
{ 
    public partial class DocumentoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.Documento.IDocumentoItem _persistenciaDocumentoItem { get; set; } 

        #endregion 

        #region Construtores 

        public DocumentoItem() 
            : this(new Persistencia.Pessoa.Documento.DocumentoItem()) 
        { } 

        public DocumentoItem(Interface.Pessoa.Documento.IDocumentoItem persistenciaDocumentoItem) 
        { 
            this._persistenciaDocumentoItem = persistenciaDocumentoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Documento.DocumentoItem> CarregarLista() 
        { 
            return _persistenciaDocumentoItem.CarregarLista(); 
        } 

        public List<Entidade.Pessoa.Documento.DocumentoItem> CarregarListaPorPessoaDocumentoTipoId(int pessoaDocumentoTipoId) 
        { 
            return _persistenciaDocumentoItem.CarregarListaPorPessoaDocumentoTipoId(pessoaDocumentoTipoId); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem CarregarItem(int pessoaDocumentoId)
        {
            return _persistenciaDocumentoItem.CarregarItem(pessoaDocumentoId);
        }

        public Entidade.Pessoa.Documento.DocumentoItem InserirItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            return _persistenciaDocumentoItem.InserirItem(documentoItem); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem AtualizarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            return _persistenciaDocumentoItem.AtualizarItem(documentoItem); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem ExcluirItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            return _persistenciaDocumentoItem.ExcluirItem(documentoItem); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem InativarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            return _persistenciaDocumentoItem.InativarItem(documentoItem); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem SalvarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            if (documentoItem.Id.Equals(0))
                documentoItem = this.InserirItem(documentoItem);
            else
                documentoItem = this.AtualizarItem(documentoItem);

            return documentoItem;
        }

        #endregion 
    } 
} 
