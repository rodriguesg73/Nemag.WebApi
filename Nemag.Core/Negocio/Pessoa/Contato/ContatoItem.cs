using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Contato 
{ 
    public partial class ContatoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.Contato.IContatoItem _persistenciaContatoItem { get; set; } 

        #endregion 

        #region Construtores 

        public ContatoItem() 
            : this(new Persistencia.Pessoa.Contato.ContatoItem()) 
        { } 

        public ContatoItem(Interface.Pessoa.Contato.IContatoItem persistenciaContatoItem) 
        { 
            this._persistenciaContatoItem = persistenciaContatoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarLista() 
        { 
            return _persistenciaContatoItem.CarregarLista(); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorPessoaContatoTipoId(int pessoaContatoTipoId) 
        { 
            return _persistenciaContatoItem.CarregarListaPorPessoaContatoTipoId(pessoaContatoTipoId); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            return _persistenciaContatoItem.CarregarListaPorPessoaId(pessoaId); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaContatoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem CarregarItem(int pessoaContatoId)
        {
            return _persistenciaContatoItem.CarregarItem(pessoaContatoId);
        }

        public Entidade.Pessoa.Contato.ContatoItem InserirItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            return _persistenciaContatoItem.InserirItem(contatoItem); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem AtualizarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            return _persistenciaContatoItem.AtualizarItem(contatoItem); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem ExcluirItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            return _persistenciaContatoItem.ExcluirItem(contatoItem); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem InativarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            return _persistenciaContatoItem.InativarItem(contatoItem); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem SalvarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            if (contatoItem.Id.Equals(0))
                contatoItem = this.InserirItem(contatoItem);
            else
                contatoItem = this.AtualizarItem(contatoItem);

            return contatoItem;
        }

        #endregion 
    } 
} 
