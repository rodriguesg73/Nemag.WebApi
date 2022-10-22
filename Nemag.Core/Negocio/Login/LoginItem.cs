using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Login 
{ 
    public partial class LoginItem : Pessoa.PessoaItem
    { 
        #region Propriedades 

        private Interface.Login.ILoginItem _persistenciaLoginItem { get; set; } 

        #endregion 

        #region Construtores 

        public LoginItem() 
            : this(new Persistencia.Login.LoginItem()) 
        { } 

        public LoginItem(Interface.Login.ILoginItem persistenciaLoginItem) 
        { 
            this._persistenciaLoginItem = persistenciaLoginItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public new List<Entidade.Login.LoginItem> CarregarLista() 
        { 
            return _persistenciaLoginItem.CarregarLista(); 
        } 

        public List<Entidade.Login.LoginItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            return _persistenciaLoginItem.CarregarListaPorPessoaId(pessoaId); 
        } 

        public List<Entidade.Login.LoginItem> CarregarListaPorLoginSituacaoId(int loginSituacaoId) 
        { 
            return _persistenciaLoginItem.CarregarListaPorLoginSituacaoId(loginSituacaoId); 
        } 

        public new Entidade.Login.LoginItem CarregarItem(int loginId)
        {
            return _persistenciaLoginItem.CarregarItem(loginId);
        }

        public Entidade.Login.LoginItem InserirItem(Entidade.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.InserirItem(loginItem); 
        } 

        public Entidade.Login.LoginItem AtualizarItem(Entidade.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.AtualizarItem(loginItem); 
        } 

        public Entidade.Login.LoginItem ExcluirItem(Entidade.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.ExcluirItem(loginItem); 
        } 

        public Entidade.Login.LoginItem InativarItem(Entidade.Login.LoginItem loginItem)
        {
            return _persistenciaLoginItem.InativarItem(loginItem); 
        } 

        public Entidade.Login.LoginItem SalvarItem(Entidade.Login.LoginItem loginItem)
        {
            var pessoaItem = loginItem.Clone<Entidade.Pessoa.PessoaItem>();

            pessoaItem.Id = loginItem.PessoaId;

            pessoaItem = base.SalvarItem(pessoaItem);

            loginItem.PessoaId = pessoaItem.Id;

            if (loginItem.Id.Equals(0))
                loginItem = this.InserirItem(loginItem);
            else
                loginItem = this.AtualizarItem(loginItem);

            return loginItem;
        }

        #endregion 
    } 
} 
