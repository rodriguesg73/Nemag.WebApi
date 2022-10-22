using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo 
{ 
    public partial class ArquivoItem : _BaseItem, Interface.Arquivo.IArquivoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ArquivoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ArquivoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Arquivo.ArquivoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.ArquivoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.ArquivoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.ArquivoItem CarregarItem(int arquivoId)
        {
            var sql = this.PrepararSelecaoSql(arquivoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.ArquivoItem InserirItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var sql = this.PrepararInsercaoSql(arquivoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            arquivoItem.Id = base.CarregarItem<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql).Id;

            return arquivoItem;
        } 

        public Entidade.Arquivo.ArquivoItem AtualizarItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var sql = this.PrepararAtualizacaoSql(arquivoItem); 

            sql += this.PrepararSelecaoSql(arquivoItem.Id, null, null);

            arquivoItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql).DataAlteracao;

            return arquivoItem;
        } 

        public Entidade.Arquivo.ArquivoItem ExcluirItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var sql = this.PrepararExclusaoSql(arquivoItem); 

            return base.CarregarItem<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.ArquivoItem InativarItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var sql = this.PrepararInativacaoSql(arquivoItem); 

            return base.CarregarItem<Entidade.Arquivo.ArquivoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.DIRETORIO_LOCAL_URL,\n";
            sql += "    A.GUID,\n";
            sql += "    A.CHECKSUN\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (arquivoId.HasValue)
				sql += "A.ARQUIVO_ID = " + arquivoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!arquivoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.ArquivoItem arquivoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql += "    DIRETORIO_LOCAL_URL,\n";

			sql += "    GUID,\n";

			sql += "    CHECKSUN,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + arquivoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + arquivoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(arquivoItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + arquivoItem.Descricao.Replace("'", "''") + "',\n";

			    sql += "    '" + arquivoItem.DiretorioLocalUrl.Replace("'", "''") + "',\n";

			    sql += "    '" + arquivoItem.Guid.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(arquivoItem.Checksun))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + arquivoItem.Checksun.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.ArquivoItem arquivoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + arquivoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    NOME = '" + arquivoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(arquivoItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + arquivoItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    DIRETORIO_LOCAL_URL = '" + arquivoItem.DiretorioLocalUrl.Replace("'", "''") + "',\n";

			sql += "    GUID = '" + arquivoItem.Guid.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(arquivoItem.Checksun))
			    sql += "    CHECKSUN = NULL,\n";
			else
				sql += "    CHECKSUN = '" + arquivoItem.Checksun.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_ID = " + arquivoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.ArquivoItem arquivoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_ID = " + arquivoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.ArquivoItem arquivoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_ID = " + arquivoItem.Id + "\n";
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
					sql += "    A.ARQUIVO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
