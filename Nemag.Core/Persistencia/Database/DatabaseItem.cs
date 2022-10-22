using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Database 
{ 
    public partial class DatabaseItem : _BaseItem, Interface.Database.IDatabaseItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public DatabaseItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public DatabaseItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Database.DatabaseItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Database.DatabaseItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Database.DatabaseItem> CarregarListaPorDatabaseTipoId(int databaseTipoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, databaseTipoId, null, null); 

            return base.CarregarLista<Entidade.Database.DatabaseItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Database.DatabaseItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Database.DatabaseItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Database.DatabaseItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Database.DatabaseItem>(_databaseItem, sql); 
        } 

        public Entidade.Database.DatabaseItem CarregarItem(int databaseId)
        {
            var sql = this.PrepararSelecaoSql(databaseId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Database.DatabaseItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Database.DatabaseItem InserirItem(Entidade.Database.DatabaseItem databaseItem)
        {
            var sql = this.PrepararInsercaoSql(databaseItem); 

            sql += this.ObterUltimoItemInseridoSql();

            databaseItem.Id = base.CarregarItem<Entidade.Database.DatabaseItem>(_databaseItem, sql).Id;

            return databaseItem;
        } 

        public Entidade.Database.DatabaseItem AtualizarItem(Entidade.Database.DatabaseItem databaseItem)
        {
            var sql = this.PrepararAtualizacaoSql(databaseItem); 

            sql += this.PrepararSelecaoSql(databaseItem.Id, null, null, null);

            databaseItem.DataAlteracao = base.CarregarItem<Entidade.Database.DatabaseItem>(_databaseItem, sql).DataAlteracao;

            return databaseItem;
        } 

        public Entidade.Database.DatabaseItem ExcluirItem(Entidade.Database.DatabaseItem databaseItem)
        {
            var sql = this.PrepararExclusaoSql(databaseItem); 

            return base.CarregarItem<Entidade.Database.DatabaseItem>(_databaseItem, sql); 
        } 

        public Entidade.Database.DatabaseItem InativarItem(Entidade.Database.DatabaseItem databaseItem)
        {
            var sql = this.PrepararInativacaoSql(databaseItem); 

            return base.CarregarItem<Entidade.Database.DatabaseItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.DATABASE_ID,\n";
            sql += "    A.DATABASE_TIPO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.IP,\n";
            sql += "    A.PORTA,\n";
            sql += "    A.USUARIO,\n";
            sql += "    A.SENHA,\n";
            sql += "    A.DATABASE_NOME,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A1.DATABASE_TIPO_ID AS DATABASE_TIPO_ID,\n";
            sql += "    A1.NOME AS DATABASE_TIPO_NOME\n";
            sql += "FROM \n";
            sql += "    DATABASE_TB A\n";
            sql += "    INNER JOIN DATABASE_TIPO_TB A1 ON A1.DATABASE_TIPO_ID = A.DATABASE_TIPO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? databaseId, int? databaseTipoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (databaseId.HasValue)
				sql += "A.DATABASE_ID = " + databaseId.Value + "\n";

			if (databaseTipoId.HasValue)
				sql += "A.DATABASE_TIPO_ID = " + databaseTipoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!databaseId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Database.DatabaseItem databaseItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO DATABASE_TB(\n";
			sql += "    DATABASE_TIPO_ID,\n";

			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql += "    IP,\n";

			sql += "    PORTA,\n";

			sql += "    USUARIO,\n";

			sql += "    SENHA,\n";

			sql += "    DATABASE_NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + databaseItem.DatabaseTipoId.ToString() + ",\n";

			sql += "    " + databaseItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + databaseItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(databaseItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + databaseItem.Descricao.Replace("'", "''") + "',\n";

			    sql += "    '" + databaseItem.Ip.Replace("'", "''") + "',\n";

			sql += "    " + (databaseItem.Porta > int.MinValue ? databaseItem.Porta.ToString() : "NULL") + ",\n";

			    sql += "    '" + databaseItem.Usuario.Replace("'", "''") + "',\n";

			    sql += "    '" + databaseItem.Senha.Replace("'", "''") + "',\n";

			    sql += "    '" + databaseItem.DatabaseNome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Database.DatabaseItem databaseItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    DATABASE_TB \n";
            sql += "SET\n";
			sql += "    DATABASE_TIPO_ID = " + databaseItem.DatabaseTipoId.ToString() + ",\n"; 

			sql += "    REGISTRO_LOGIN_ID = " + databaseItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + databaseItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(databaseItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + databaseItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    IP = '" + databaseItem.Ip.Replace("'", "''") + "',\n";

			sql += "    PORTA = " + (databaseItem.Porta > int.MinValue ? databaseItem.Porta.ToString() : "NULL") + ",\n"; 

			sql += "    USUARIO = '" + databaseItem.Usuario.Replace("'", "''") + "',\n";

			sql += "    SENHA = '" + databaseItem.Senha.Replace("'", "''") + "',\n";

			sql += "    DATABASE_NOME = '" + databaseItem.DatabaseNome.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    DATABASE_ID = " + databaseItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Database.DatabaseItem databaseItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    DATABASE_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    DATABASE_ID = " + databaseItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Database.DatabaseItem databaseItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    DATABASE_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    DATABASE_ID = " + databaseItem.Id + "\n";
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
					sql += "    A.DATABASE_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.DATABASE_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
