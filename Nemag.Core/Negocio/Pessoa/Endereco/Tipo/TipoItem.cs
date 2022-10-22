using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Endereco.Tipo 
{ 
    public partial class TipoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.Endereco.Tipo.ITipoItem _persistenciaTipoItem { get; set; } 

        #endregion 

        #region Construtores 

        public TipoItem() 
            : this(new Persistencia.Pessoa.Endereco.Tipo.TipoItem()) 
        { } 

        public TipoItem(Interface.Pessoa.Endereco.Tipo.ITipoItem persistenciaTipoItem) 
        { 
            this._persistenciaTipoItem = persistenciaTipoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Endereco.Tipo.TipoItem> CarregarLista() 
        { 
            return _persistenciaTipoItem.CarregarLista(); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem CarregarItem(int pessoaEnderecoTipoId)
        {
            return _persistenciaTipoItem.CarregarItem(pessoaEnderecoTipoId);
        }

        public Entidade.Pessoa.Endereco.Tipo.TipoItem InserirItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InserirItem(tipoItem); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.AtualizarItem(tipoItem); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.ExcluirItem(tipoItem); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem InativarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InativarItem(tipoItem); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem SalvarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
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
