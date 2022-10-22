using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Acesso 
{ 
    public partial class AcessoItem : _BaseItem, Interface.Login.Acesso.IAcessoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public AcessoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public AcessoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null); 

            return base.CarregarLista<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Acesso.AcessoItem> CarregarListaPorLoginId(int loginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, loginId); 

            return base.CarregarLista<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Acesso.AcessoItem CarregarItem(int loginAcessoId)
        {
            var sql = this.PrepararSelecaoSql(loginAcessoId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Acesso.AcessoItem InserirItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararInsercaoSql(acessoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            acessoItem.Id = base.CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql).Id;

            return acessoItem;
        } 

        public Entidade.Login.Acesso.AcessoItem AtualizarItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararAtualizacaoSql(acessoItem); 

            sql += this.PrepararSelecaoSql(acessoItem.Id, null, null, null);

            acessoItem.DataAlteracao = base.CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql).DataAlteracao;

            return acessoItem;
        } 

        public Entidade.Login.Acesso.AcessoItem ExcluirItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararExclusaoSql(acessoItem); 

            return base.CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Acesso.AcessoItem InativarItem(Entidade.Login.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararInativacaoSql(acessoItem); 

            return base.CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_ACESSO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.TOKEN,\n";
            sql += "    A.IP,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_VALIDADE,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.LOGIN_ID,\n";
            sql += "    A2.PESSOA_ID AS LOGIN_PESSOA_ID,\n";
            sql += "    A2.NOME AS LOGIN_NOME,\n";
            sql += "    A3.LOGIN_SITUACAO_ID AS LOGIN_SITUACAO_ID,\n";
            sql += "    A3.NOME AS LOGIN_SITUACAO_NOME,\n";
            sql += "    A1.LOGIN_ID AS LOGIN_ID,\n";
            sql += "    A1.USUARIO AS LOGIN_USUARIO,\n";
            sql += "    A1.SENHA AS LOGIN_SENHA,\n";
            sql += "    A1.NOME_EXIBICAO AS LOGIN_NOME_EXIBICAO\n";
            sql += "FROM \n";
            sql += "    LOGIN_ACESSO_TB A\n";
            sql += "    INNER JOIN LOGIN_TB A1 ON A1.LOGIN_ID = A.LOGIN_ID\n";
            sql += "    INNER JOIN PESSOA_TB A2 ON A2.PESSOA_ID = A1.PESSOA_ID\n";
            sql += "    INNER JOIN LOGIN_SITUACAO_TB A3 ON A3.LOGIN_SITUACAO_ID = A1.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginAcessoId, int? registroSituacaoId, int? registroLoginId, int? loginId)
		{ 
			var sql = ""; 

			if (loginAcessoId.HasValue)
				sql += "A.LOGIN_ACESSO_ID = " + loginAcessoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (loginId.HasValue)
				sql += "A.LOGIN_ID = " + loginId.Value + "\n";

			if (!loginAcessoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_ACESSO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    TOKEN,\n";

			sql += "    IP,\n";

			sql += "    DATA_VALIDADE,\n";

			sql += "    LOGIN_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + acessoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + acessoItem.Token.Replace("'", "''") + "',\n";

			    sql += "    '" + acessoItem.Ip.Replace("'", "''") + "',\n";

			if (acessoItem.DataValidade.Equals(DateTime.MinValue))
				    sql += "    NULL,\n";
			else
			sql += "    '" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", acessoItem.DataValidade) + "',\n";

			sql += "    " + acessoItem.LoginId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_ACESSO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + acessoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    TOKEN = '" + acessoItem.Token.Replace("'", "''") + "',\n";

			sql += "    IP = '" + acessoItem.Ip.Replace("'", "''") + "',\n";

			sql += "    DATA_VALIDADE = " + (acessoItem.DataValidade > DateTime.MinValue ? "'" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", acessoItem.DataValidade) + "'" : "NULL") + ",\n"; 

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    LOGIN_ID = " + acessoItem.LoginId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_ACESSO_ID = " + acessoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_ACESSO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_ACESSO_ID = " + acessoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_ACESSO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_ACESSO_ID = " + acessoItem.Id + "\n";
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
					sql += "    A.LOGIN_ACESSO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_ACESSO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
