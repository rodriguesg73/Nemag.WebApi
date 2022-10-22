using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem, Interface.Login.Situacao.ISituacaoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public SituacaoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public SituacaoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.Situacao.SituacaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null); 

            return base.CarregarLista<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Situacao.SituacaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId); 

            return base.CarregarLista<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Situacao.SituacaoItem CarregarItem(int loginSituacaoId)
        {
            var sql = this.PrepararSelecaoSql(loginSituacaoId, null); 

            var retorno = base.CarregarItem<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Situacao.SituacaoItem InserirItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararInsercaoSql(situacaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            situacaoItem.Id = base.CarregarItem<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql).Id;

            return situacaoItem;
        } 

        public Entidade.Login.Situacao.SituacaoItem AtualizarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(situacaoItem); 

            sql += this.PrepararSelecaoSql(situacaoItem.Id, null);

            situacaoItem.DataAlteracao = base.CarregarItem<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql).DataAlteracao;

            return situacaoItem;
        } 

        public Entidade.Login.Situacao.SituacaoItem ExcluirItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararExclusaoSql(situacaoItem); 

            return base.CarregarItem<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Situacao.SituacaoItem InativarItem(Entidade.Login.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararInativacaoSql(situacaoItem); 

            return base.CarregarItem<Entidade.Login.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_SITUACAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    LOGIN_SITUACAO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginSituacaoId, int? registroSituacaoId)
		{ 
			var sql = ""; 

			if (loginSituacaoId.HasValue)
				sql += "A.LOGIN_SITUACAO_ID = " + loginSituacaoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (!loginSituacaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_SITUACAO_TB(\n";
			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			    sql += "    '" + situacaoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_SITUACAO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    NOME = '" + situacaoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_SITUACAO_ID = " + situacaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_SITUACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_SITUACAO_ID = " + situacaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_SITUACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_SITUACAO_ID = " + situacaoItem.Id + "\n";
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
					sql += "    A.LOGIN_SITUACAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_SITUACAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
