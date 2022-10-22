using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Endereco 
{ 
    public partial class EnderecoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.Endereco.IEnderecoItem _persistenciaEnderecoItem { get; set; } 

        #endregion 

        #region Construtores 

        public EnderecoItem() 
            : this(new Persistencia.Pessoa.Endereco.EnderecoItem()) 
        { } 

        public EnderecoItem(Interface.Pessoa.Endereco.IEnderecoItem persistenciaEnderecoItem) 
        { 
            this._persistenciaEnderecoItem = persistenciaEnderecoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarLista() 
        { 
            return _persistenciaEnderecoItem.CarregarLista(); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaEnderecoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            return _persistenciaEnderecoItem.CarregarListaPorPessoaId(pessoaId); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorPessoaEnderecoTipoId(int pessoaEnderecoTipoId) 
        { 
            return _persistenciaEnderecoItem.CarregarListaPorPessoaEnderecoTipoId(pessoaEnderecoTipoId); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem CarregarItem(int pessoaEnderecoId)
        {
            return _persistenciaEnderecoItem.CarregarItem(pessoaEnderecoId);
        }

        public Entidade.Pessoa.Endereco.EnderecoItem InserirItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            return _persistenciaEnderecoItem.InserirItem(enderecoItem); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem AtualizarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            return _persistenciaEnderecoItem.AtualizarItem(enderecoItem); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem ExcluirItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            return _persistenciaEnderecoItem.ExcluirItem(enderecoItem); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem InativarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            return _persistenciaEnderecoItem.InativarItem(enderecoItem); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem SalvarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            if (enderecoItem.Id.Equals(0))
                enderecoItem = this.InserirItem(enderecoItem);
            else
                enderecoItem = this.AtualizarItem(enderecoItem);

            return enderecoItem;
        }

        #endregion 
    } 
} 
