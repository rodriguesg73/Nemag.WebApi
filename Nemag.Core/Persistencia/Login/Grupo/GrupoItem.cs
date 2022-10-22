using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Grupo 
{ 
    public partial class GrupoItem : _BaseItem, Interface.Login.Grupo.IGrupoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public GrupoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public GrupoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.Grupo.GrupoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Grupo.GrupoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Grupo.GrupoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Grupo.GrupoItem CarregarItem(int loginGrupoId)
        {
            var sql = this.PrepararSelecaoSql(loginGrupoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Grupo.GrupoItem InserirItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            var sql = this.PrepararInsercaoSql(grupoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            grupoItem.Id = base.CarregarItem<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql).Id;

            return grupoItem;
        } 

        public Entidade.Login.Grupo.GrupoItem AtualizarItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            var sql = this.PrepararAtualizacaoSql(grupoItem); 

            sql += this.PrepararSelecaoSql(grupoItem.Id, null, null);

            grupoItem.DataAlteracao = base.CarregarItem<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql).DataAlteracao;

            return grupoItem;
        } 

        public Entidade.Login.Grupo.GrupoItem ExcluirItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            var sql = this.PrepararExclusaoSql(grupoItem); 

            return base.CarregarItem<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Grupo.GrupoItem InativarItem(Entidade.Login.Grupo.GrupoItem grupoItem)
        {
            var sql = this.PrepararInativacaoSql(grupoItem); 

            return base.CarregarItem<Entidade.Login.Grupo.GrupoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_GRUPO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO\n";
            sql += "FROM \n";
            sql += "    LOGIN_GRUPO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginGrupoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (loginGrupoId.HasValue)
				sql += "A.LOGIN_GRUPO_ID = " + loginGrupoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!loginGrupoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Grupo.GrupoItem grupoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_GRUPO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + grupoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + grupoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(grupoItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + grupoItem.Descricao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Grupo.GrupoItem grupoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_GRUPO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + grupoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + grupoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(grupoItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + grupoItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_GRUPO_ID = " + grupoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Grupo.GrupoItem grupoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_GRUPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_GRUPO_ID = " + grupoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Grupo.GrupoItem grupoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_GRUPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_GRUPO_ID = " + grupoItem.Id + "\n";
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
					sql += "    A.LOGIN_GRUPO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_GRUPO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
