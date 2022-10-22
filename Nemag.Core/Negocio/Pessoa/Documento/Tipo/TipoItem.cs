using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Documento.Tipo 
{ 
    public partial class TipoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.Documento.Tipo.ITipoItem _persistenciaTipoItem { get; set; } 

        #endregion 

        #region Construtores 

        public TipoItem() 
            : this(new Persistencia.Pessoa.Documento.Tipo.TipoItem()) 
        { } 

        public TipoItem(Interface.Pessoa.Documento.Tipo.ITipoItem persistenciaTipoItem) 
        { 
            this._persistenciaTipoItem = persistenciaTipoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Documento.Tipo.TipoItem> CarregarLista() 
        { 
            return _persistenciaTipoItem.CarregarLista(); 
        } 

        public Entidade.Pessoa.Documento.Tipo.TipoItem CarregarItem(int pessoaDocumentoTipoId)
        {
            return _persistenciaTipoItem.CarregarItem(pessoaDocumentoTipoId);
        }

        public Entidade.Pessoa.Documento.Tipo.TipoItem InserirItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InserirItem(tipoItem); 
        } 

        public Entidade.Pessoa.Documento.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.AtualizarItem(tipoItem); 
        } 

        public Entidade.Pessoa.Documento.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.ExcluirItem(tipoItem); 
        } 

        public Entidade.Pessoa.Documento.Tipo.TipoItem InativarItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InativarItem(tipoItem); 
        } 

        public Entidade.Pessoa.Documento.Tipo.TipoItem SalvarItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem)
        {
            if (tipoItem.Id.Equals(0))
                tipoItem = this.InserirItem(tipoItem);
            else
                tipoItem = this.AtualizarItem(tipoItem);

            return tipoItem;
        }

        #endregion 
    } 
} 
