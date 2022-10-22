using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pais 
{ 
    public partial class PaisItem : _BaseItem, Interface.Pais.IPaisItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public PaisItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public PaisItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Pais.PaisItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Pais.PaisItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pais.PaisItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Pais.PaisItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pais.PaisItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null); 

            return base.CarregarLista<Entidade.Pais.PaisItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pais.PaisItem> CarregarListaPorPaisContinenteId(int paisContinenteId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, paisContinenteId); 

            return base.CarregarLista<Entidade.Pais.PaisItem>(_databaseItem, sql); 
        } 

        public Entidade.Pais.PaisItem CarregarItem(int paisId)
        {
            var sql = this.PrepararSelecaoSql(paisId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Pais.PaisItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pais.PaisItem InserirItem(Entidade.Pais.PaisItem paisItem)
        {
            var sql = this.PrepararInsercaoSql(paisItem); 

            sql += this.ObterUltimoItemInseridoSql();

            paisItem.Id = base.CarregarItem<Entidade.Pais.PaisItem>(_databaseItem, sql).Id;

            return paisItem;
        } 

        public Entidade.Pais.PaisItem AtualizarItem(Entidade.Pais.PaisItem paisItem)
        {
            var sql = this.PrepararAtualizacaoSql(paisItem); 

            sql += this.PrepararSelecaoSql(paisItem.Id, null, null, null);

            paisItem.DataAlteracao = base.CarregarItem<Entidade.Pais.PaisItem>(_databaseItem, sql).DataAlteracao;

            return paisItem;
        } 

        public Entidade.Pais.PaisItem ExcluirItem(Entidade.Pais.PaisItem paisItem)
        {
            var sql = this.PrepararExclusaoSql(paisItem); 

            return base.CarregarItem<Entidade.Pais.PaisItem>(_databaseItem, sql); 
        } 

        public Entidade.Pais.PaisItem InativarItem(Entidade.Pais.PaisItem paisItem)
        {
            var sql = this.PrepararInativacaoSql(paisItem); 

            return base.CarregarItem<Entidade.Pais.PaisItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PAIS_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.CODIGO,\n";
            sql += "    A.PAIS_CONTINENTE_ID,\n";
            sql += "    A1.PAIS_CONTINENTE_ID AS PAIS_CONTINENTE_ID,\n";
            sql += "    A1.NOME AS PAIS_CONTINENTE_NOME\n";
            sql += "FROM \n";
            sql += "    PAIS_TB A\n";
            sql += "    LEFT JOIN PAIS_CONTINENTE_TB A1 ON A1.PAIS_CONTINENTE_ID = A.PAIS_CONTINENTE_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? paisId, int? registroSituacaoId, int? registroLoginId, int? paisContinenteId)
		{ 
			var sql = ""; 

			if (paisId.HasValue)
				sql += "A.PAIS_ID = " + paisId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (paisContinenteId.HasValue)
				sql += "A.PAIS_CONTINENTE_ID = " + paisContinenteId.Value + "\n";

			if (!paisId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pais.PaisItem paisItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PAIS_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    CODIGO,\n";

			sql += "    PAIS_CONTINENTE_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + paisItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + paisItem.Nome.Replace("'", "''") + "',\n";

			    sql += "    '" + paisItem.Codigo.Replace("'", "''") + "',\n";

			sql += "    " + (!paisItem.PaisContinenteId.Equals(0) ? paisItem.PaisContinenteId.ToString() : "NULL") + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pais.PaisItem paisItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PAIS_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + paisItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + paisItem.Nome.Replace("'", "''") + "',\n";

			sql += "    CODIGO = '" + paisItem.Codigo.Replace("'", "''") + "',\n";

			sql += "    PAIS_CONTINENTE_ID = " + (!paisItem.PaisContinenteId.Equals(0) ? paisItem.PaisContinenteId.ToString() : "NULL") + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PAIS_ID = " + paisItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pais.PaisItem paisItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PAIS_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PAIS_ID = " + paisItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pais.PaisItem paisItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PAIS_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PAIS_ID = " + paisItem.Id + "\n";
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
					sql += "    A.PAIS_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PAIS_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
