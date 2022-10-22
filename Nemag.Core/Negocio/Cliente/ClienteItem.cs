using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Cliente 
{ 
    public partial class ClienteItem : Pessoa.PessoaItem
    { 
        #region Propriedades 

        private Interface.Cliente.IClienteItem _persistenciaClienteItem { get; set; } 

        #endregion 

        #region Construtores 

        public ClienteItem() 
            : this(new Persistencia.Cliente.ClienteItem()) 
        { } 

        public ClienteItem(Interface.Cliente.IClienteItem persistenciaClienteItem) 
        { 
            this._persistenciaClienteItem = persistenciaClienteItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public new List<Entidade.Cliente.ClienteItem> CarregarLista() 
        { 
            return _persistenciaClienteItem.CarregarLista(); 
        } 

        public List<Entidade.Cliente.ClienteItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaClienteItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Cliente.ClienteItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            return _persistenciaClienteItem.CarregarListaPorPessoaId(pessoaId); 
        } 

        public new Entidade.Cliente.ClienteItem CarregarItem(int clienteId)
        {
            return _persistenciaClienteItem.CarregarItem(clienteId);
        }

        public Entidade.Cliente.ClienteItem InserirItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            return _persistenciaClienteItem.InserirItem(clienteItem); 
        } 

        public Entidade.Cliente.ClienteItem AtualizarItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            return _persistenciaClienteItem.AtualizarItem(clienteItem); 
        } 

        public Entidade.Cliente.ClienteItem ExcluirItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            return _persistenciaClienteItem.ExcluirItem(clienteItem); 
        } 

        public Entidade.Cliente.ClienteItem InativarItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            return _persistenciaClienteItem.InativarItem(clienteItem); 
        } 

        public Entidade.Cliente.ClienteItem SalvarItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            var pessoaItem = clienteItem.Clone<Entidade.Pessoa.PessoaItem>();

            pessoaItem.Id = clienteItem.PessoaId;

            pessoaItem = base.SalvarItem(pessoaItem);

            clienteItem.PessoaId = pessoaItem.Id;

            if (clienteItem.Id.Equals(0))
                clienteItem = this.InserirItem(clienteItem);
            else
                clienteItem = this.AtualizarItem(clienteItem);

            return clienteItem;
        }

        #endregion 
    } 
} 
