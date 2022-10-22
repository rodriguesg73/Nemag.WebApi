using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa 
{ 
    public partial class PessoaItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.IPessoaItem _persistenciaPessoaItem { get; set; } 

        #endregion 

        #region Construtores 

        public PessoaItem() 
            : this(new Persistencia.Pessoa.PessoaItem()) 
        { } 

        public PessoaItem(Interface.Pessoa.IPessoaItem persistenciaPessoaItem) 
        { 
            this._persistenciaPessoaItem = persistenciaPessoaItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.PessoaItem> CarregarLista() 
        { 
            return _persistenciaPessoaItem.CarregarLista(); 
        } 

        public Entidade.Pessoa.PessoaItem CarregarItem(int pessoaId)
        {
            return _persistenciaPessoaItem.CarregarItem(pessoaId);
        }

        public Entidade.Pessoa.PessoaItem InserirItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            return _persistenciaPessoaItem.InserirItem(pessoaItem); 
        } 

        public Entidade.Pessoa.PessoaItem AtualizarItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            return _persistenciaPessoaItem.AtualizarItem(pessoaItem); 
        } 

        public Entidade.Pessoa.PessoaItem ExcluirItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            return _persistenciaPessoaItem.ExcluirItem(pessoaItem); 
        } 

        public Entidade.Pessoa.PessoaItem InativarItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            return _persistenciaPessoaItem.InativarItem(pessoaItem); 
        } 

        public Entidade.Pessoa.PessoaItem SalvarItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            if (pessoaItem.Id.Equals(0))
                pessoaItem = this.InserirItem(pessoaItem);
            else
                pessoaItem = this.AtualizarItem(pessoaItem);

            return pessoaItem;
        }

        #endregion 
    } 
} 
