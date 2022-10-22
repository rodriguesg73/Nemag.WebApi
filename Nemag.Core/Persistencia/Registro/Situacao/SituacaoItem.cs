using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Registro.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem, Interface.Registro.Situacao.ISituacaoItem
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

        public List<Entidade.Registro.Situacao.SituacaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null); 

            return base.CarregarLista<Entidade.Registro.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem CarregarItem(int registroSituacaoId)
        {
            var sql = this.PrepararSelecaoSql(registroSituacaoId); 

            var retorno = base.CarregarItem<Entidade.Registro.Situacao.SituacaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Registro.Situacao.SituacaoItem InserirItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararInsercaoSql(situacaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            situacaoItem.Id = base.CarregarItem<Entidade.Registro.Situacao.SituacaoItem>(_databaseItem, sql).Id;

            return situacaoItem;
        } 

        public Entidade.Registro.Situacao.SituacaoItem AtualizarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(situacaoItem); 

            sql += this.PrepararSelecaoSql(situacaoItem.Id);

            situacaoItem.DataAlteracao = base.CarregarItem<Entidade.Registro.Situacao.SituacaoItem>(_databaseItem, sql).DataAlteracao;

            return situacaoItem;
        } 

        public Entidade.Registro.Situacao.SituacaoItem ExcluirItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararExclusaoSql(situacaoItem); 

            return base.CarregarItem<Entidade.Registro.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Registro.Situacao.SituacaoItem InativarItem(Entidade.Registro.Situacao.SituacaoItem situacaoItem)
        {
            var sql = this.PrepararInativacaoSql(situacaoItem); 

            return base.CarregarItem<Entidade.Registro.Situacao.SituacaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    REGISTRO_SITUACAO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? registroSituacaoId)
		{ 
			var sql = ""; 

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (!registroSituacaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Registro.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO REGISTRO_SITUACAO_TB(\n";
			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			    sql += "    '" + situacaoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Registro.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REGISTRO_SITUACAO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    NOME = '" + situacaoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    REGISTRO_SITUACAO_ID = " + situacaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Registro.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REGISTRO_SITUACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    REGISTRO_SITUACAO_ID = " + situacaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Registro.Situacao.SituacaoItem situacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REGISTRO_SITUACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    REGISTRO_SITUACAO_ID = " + situacaoItem.Id + "\n";
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
					sql += "    A.REGISTRO_SITUACAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.REGISTRO_SITUACAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
