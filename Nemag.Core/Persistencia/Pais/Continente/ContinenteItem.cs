using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pais.Continente 
{ 
    public partial class ContinenteItem : _BaseItem, Interface.Pais.Continente.IContinenteItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ContinenteItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ContinenteItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Pais.Continente.ContinenteItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pais.Continente.ContinenteItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pais.Continente.ContinenteItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql); 
        } 

        public Entidade.Pais.Continente.ContinenteItem CarregarItem(int paisContinenteId)
        {
            var sql = this.PrepararSelecaoSql(paisContinenteId, null, null); 

            var retorno = base.CarregarItem<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pais.Continente.ContinenteItem InserirItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            var sql = this.PrepararInsercaoSql(continenteItem); 

            sql += this.ObterUltimoItemInseridoSql();

            continenteItem.Id = base.CarregarItem<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql).Id;

            return continenteItem;
        } 

        public Entidade.Pais.Continente.ContinenteItem AtualizarItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            var sql = this.PrepararAtualizacaoSql(continenteItem); 

            sql += this.PrepararSelecaoSql(continenteItem.Id, null, null);

            continenteItem.DataAlteracao = base.CarregarItem<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql).DataAlteracao;

            return continenteItem;
        } 

        public Entidade.Pais.Continente.ContinenteItem ExcluirItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            var sql = this.PrepararExclusaoSql(continenteItem); 

            return base.CarregarItem<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql); 
        } 

        public Entidade.Pais.Continente.ContinenteItem InativarItem(Entidade.Pais.Continente.ContinenteItem continenteItem)
        {
            var sql = this.PrepararInativacaoSql(continenteItem); 

            return base.CarregarItem<Entidade.Pais.Continente.ContinenteItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PAIS_CONTINENTE_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    PAIS_CONTINENTE_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? paisContinenteId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (paisContinenteId.HasValue)
				sql += "A.PAIS_CONTINENTE_ID = " + paisContinenteId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!paisContinenteId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pais.Continente.ContinenteItem continenteItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PAIS_CONTINENTE_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + continenteItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + continenteItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pais.Continente.ContinenteItem continenteItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PAIS_CONTINENTE_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + continenteItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + continenteItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PAIS_CONTINENTE_ID = " + continenteItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pais.Continente.ContinenteItem continenteItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PAIS_CONTINENTE_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PAIS_CONTINENTE_ID = " + continenteItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pais.Continente.ContinenteItem continenteItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PAIS_CONTINENTE_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PAIS_CONTINENTE_ID = " + continenteItem.Id + "\n";
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
					sql += "    A.PAIS_CONTINENTE_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PAIS_CONTINENTE_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
