using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login 
{ 
    public partial class LoginItem : _BaseItem, Interface.Login.ILoginItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public LoginItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public LoginItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.LoginItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.LoginItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.LoginItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, pessoaId, null); 

            return base.CarregarLista<Entidade.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.LoginItem> CarregarListaPorLoginSituacaoId(int loginSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, loginSituacaoId); 

            return base.CarregarLista<Entidade.Login.LoginItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.LoginItem CarregarItem(int loginId)
        {
            var sql = this.PrepararSelecaoSql(loginId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.LoginItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.LoginItem InserirItem(Entidade.Login.LoginItem loginItem)
        {
            var sql = this.PrepararInsercaoSql(loginItem); 

            sql += this.ObterUltimoItemInseridoSql();

            loginItem.Id = base.CarregarItem<Entidade.Login.LoginItem>(_databaseItem, sql).Id;

            return loginItem;
        } 

        public Entidade.Login.LoginItem AtualizarItem(Entidade.Login.LoginItem loginItem)
        {
            var sql = this.PrepararAtualizacaoSql(loginItem); 

            sql += this.PrepararSelecaoSql(loginItem.Id, null, null, null);

            loginItem.DataAlteracao = base.CarregarItem<Entidade.Login.LoginItem>(_databaseItem, sql).DataAlteracao;

            return loginItem;
        } 

        public Entidade.Login.LoginItem ExcluirItem(Entidade.Login.LoginItem loginItem)
        {
            var sql = this.PrepararExclusaoSql(loginItem); 

            return base.CarregarItem<Entidade.Login.LoginItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.LoginItem InativarItem(Entidade.Login.LoginItem loginItem)
        {
            var sql = this.PrepararInativacaoSql(loginItem); 

            return base.CarregarItem<Entidade.Login.LoginItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.PESSOA_ID,\n";
            sql += "    A.USUARIO,\n";
            sql += "    A.SENHA,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.LOGIN_SITUACAO_ID,\n";
            sql += "    A.NOME_EXIBICAO,\n";
            sql += "    A1.PESSOA_ID AS PESSOA_ID,\n";
            sql += "    A1.NOME AS NOME,\n";
            sql += "    A2.LOGIN_SITUACAO_ID AS LOGIN_SITUACAO_ID,\n";
            sql += "    A2.NOME AS LOGIN_SITUACAO_NOME\n";
            sql += "FROM \n";
            sql += "    LOGIN_TB A\n";
            sql += "    INNER JOIN PESSOA_TB A1 ON A1.PESSOA_ID = A.PESSOA_ID\n";
            sql += "    INNER JOIN LOGIN_SITUACAO_TB A2 ON A2.LOGIN_SITUACAO_ID = A.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginId, int? registroSituacaoId, int? pessoaId, int? loginSituacaoId)
		{ 
			var sql = ""; 

			if (loginId.HasValue)
				sql += "A.LOGIN_ID = " + loginId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (pessoaId.HasValue)
				sql += "A.PESSOA_ID = " + pessoaId.Value + "\n";

			if (loginSituacaoId.HasValue)
				sql += "A.LOGIN_SITUACAO_ID = " + loginSituacaoId.Value + "\n";

			if (!loginId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID <> 3\n";

            if (!string.IsNullOrEmpty(sql))
            {
                sql = sql.Substring(0, sql.Length - 1);

                sql = sql.Replace("\n", "\nAND "); 

                sql = "WHERE\n\t" + sql; 
            } 

            sql = this.PrepararSelecaoSql() + " " + sql;

            return sql; 
        } 

        private string PrepararInsercaoSql(Entidade.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_TB(\n";
			sql += "    PESSOA_ID,\n";

			sql += "    USUARIO,\n";

			sql += "    SENHA,\n";

			sql += "    LOGIN_SITUACAO_ID,\n";

			sql += "    NOME_EXIBICAO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + loginItem.PessoaId.ToString() + ",\n";

			    sql += "    '" + loginItem.Usuario.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(loginItem.Senha))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + loginItem.Senha.Replace("'", "''") + "',\n";

			sql += "    " + loginItem.LoginSituacaoId.ToString() + ",\n";

			if (string.IsNullOrEmpty(loginItem.NomeExibicao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + loginItem.NomeExibicao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_TB \n";
            sql += "SET\n";
			sql += "    PESSOA_ID = " + loginItem.PessoaId.ToString() + ",\n"; 

			sql += "    USUARIO = '" + loginItem.Usuario.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(loginItem.Senha))
			    sql += "    SENHA = NULL,\n";
			else
				sql += "    SENHA = '" + loginItem.Senha.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    LOGIN_SITUACAO_ID = " + loginItem.LoginSituacaoId.ToString() + ",\n"; 

			if (string.IsNullOrEmpty(loginItem.NomeExibicao))
			    sql += "    NOME_EXIBICAO = NULL,\n";
			else
				sql += "    NOME_EXIBICAO = '" + loginItem.NomeExibicao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_ID = " + loginItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_ID = " + loginItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_ID = " + loginItem.Id + "\n";
            return sql; 
        } 

        #endregion 

		#region Métodos Específicos do Banco

		private string ObterUltimoItemInseridoSql()
		{
			var sql = this.PrepararSelecaoSql();

			sql += "WHERE \n";

			var databaseItem = new Nemag.Database.DatabaseItem();

			switch (databaseItem.DatabaseTipoId)
			{
				case Nemag.Database.Base.DATABASE_TIPO_ID.MSSQL:
					sql += "    A.LOGIN_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
