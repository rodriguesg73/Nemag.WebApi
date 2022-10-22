using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Database.Tipo 
{ 
    public partial class TipoItem : _BaseItem, Interface.Database.Tipo.ITipoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public TipoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public TipoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Database.Tipo.TipoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Database.Tipo.TipoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Database.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Database.Tipo.TipoItem CarregarItem(int databaseTipoId)
        {
            var sql = this.PrepararSelecaoSql(databaseTipoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Database.Tipo.TipoItem InserirItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInsercaoSql(tipoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            tipoItem.Id = base.CarregarItem<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql).Id;

            return tipoItem;
        } 

        public Entidade.Database.Tipo.TipoItem AtualizarItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararAtualizacaoSql(tipoItem); 

            sql += this.PrepararSelecaoSql(tipoItem.Id, null, null);

            tipoItem.DataAlteracao = base.CarregarItem<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql).DataAlteracao;

            return tipoItem;
        } 

        public Entidade.Database.Tipo.TipoItem ExcluirItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararExclusaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Database.Tipo.TipoItem InativarItem(Entidade.Database.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInativacaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Database.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.DATABASE_TIPO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO\n";
            sql += "FROM \n";
            sql += "    DATABASE_TIPO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? databaseTipoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (databaseTipoId.HasValue)
				sql += "A.DATABASE_TIPO_ID = " + databaseTipoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!databaseTipoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Database.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO DATABASE_TIPO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + tipoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Database.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    DATABASE_TIPO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + tipoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    DATABASE_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Database.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    DATABASE_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    DATABASE_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Database.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    DATABASE_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    DATABASE_TIPO_ID = " + tipoItem.Id + "\n";
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
					sql += "    A.DATABASE_TIPO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.DATABASE_TIPO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
