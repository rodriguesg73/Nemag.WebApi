using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo.Tramitador 
{ 
    public partial class TramitadorItem : _BaseItem, Interface.Arquivo.Tramitador.ITramitadorItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public TramitadorItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public TramitadorItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.TramitadorItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem CarregarItem(int arquivoTramitadorId)
        {
            var sql = this.PrepararSelecaoSql(arquivoTramitadorId, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.Tramitador.TramitadorItem InserirItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            var sql = this.PrepararInsercaoSql(tramitadorItem); 

            sql += this.ObterUltimoItemInseridoSql();

            tramitadorItem.Id = base.CarregarItem<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql).Id;

            return tramitadorItem;
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem AtualizarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            var sql = this.PrepararAtualizacaoSql(tramitadorItem); 

            sql += this.PrepararSelecaoSql(tramitadorItem.Id, null, null);

            tramitadorItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql).DataAlteracao;

            return tramitadorItem;
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem ExcluirItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            var sql = this.PrepararExclusaoSql(tramitadorItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.TramitadorItem InativarItem(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem)
        {
            var sql = this.PrepararInativacaoSql(tramitadorItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.TramitadorItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_TRAMITADOR_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_TRAMITADOR_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoTramitadorId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (arquivoTramitadorId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ID = " + arquivoTramitadorId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!arquivoTramitadorId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_TRAMITADOR_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + tramitadorItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + tramitadorItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(tramitadorItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + tramitadorItem.Descricao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + tramitadorItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + tramitadorItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(tramitadorItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + tramitadorItem.Descricao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_ID = " + tramitadorItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_ID = " + tramitadorItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.Tramitador.TramitadorItem tramitadorItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_ID = " + tramitadorItem.Id + "\n";
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
					sql += "    A.ARQUIVO_TRAMITADOR_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_TRAMITADOR_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
