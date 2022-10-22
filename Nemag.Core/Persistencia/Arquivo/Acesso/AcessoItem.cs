using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo.Acesso 
{ 
    public partial class AcessoItem : _BaseItem, Interface.Arquivo.Acesso.IAcessoItem
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

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorArquivoId(int arquivoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, arquivoId, null); 

            return base.CarregarLista<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem CarregarItem(int arquivoAcessoId)
        {
            var sql = this.PrepararSelecaoSql(arquivoAcessoId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.Acesso.AcessoItem InserirItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararInsercaoSql(acessoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            acessoItem.Id = base.CarregarItem<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql).Id;

            return acessoItem;
        } 

        public Entidade.Arquivo.Acesso.AcessoItem AtualizarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararAtualizacaoSql(acessoItem); 

            sql += this.PrepararSelecaoSql(acessoItem.Id, null, null, null);

            acessoItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql).DataAlteracao;

            return acessoItem;
        } 

        public Entidade.Arquivo.Acesso.AcessoItem ExcluirItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararExclusaoSql(acessoItem); 

            return base.CarregarItem<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem InativarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            var sql = this.PrepararInativacaoSql(acessoItem); 

            return base.CarregarItem<Entidade.Arquivo.Acesso.AcessoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_ACESSO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.ARQUIVO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.IP,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A1.ARQUIVO_ID AS ARQUIVO_ID,\n";
            sql += "    A1.NOME AS ARQUIVO_NOME,\n";
            sql += "    A1.DESCRICAO AS ARQUIVO_DESCRICAO,\n";
            sql += "    A1.DIRETORIO_LOCAL_URL AS ARQUIVO_DIRETORIO_LOCAL_URL,\n";
            sql += "    A1.GUID AS ARQUIVO_GUID,\n";
            sql += "    A1.CHECKSUN AS ARQUIVO_CHECKSUN\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_ACESSO_TB A\n";
            sql += "    INNER JOIN ARQUIVO_TB A1 ON A1.ARQUIVO_ID = A.ARQUIVO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoAcessoId, int? registroSituacaoId, int? arquivoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (arquivoAcessoId.HasValue)
				sql += "A.ARQUIVO_ACESSO_ID = " + arquivoAcessoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (arquivoId.HasValue)
				sql += "A.ARQUIVO_ID = " + arquivoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!arquivoAcessoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_ACESSO_TB(\n";
			sql += "    ARQUIVO_ID,\n";

			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    IP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + acessoItem.ArquivoId.ToString() + ",\n";

			sql += "    " + acessoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + acessoItem.Ip.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_ACESSO_TB \n";
            sql += "SET\n";
			sql += "    ARQUIVO_ID = " + acessoItem.ArquivoId.ToString() + ",\n"; 

			sql += "    REGISTRO_LOGIN_ID = " + acessoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    IP = '" + acessoItem.Ip.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_ACESSO_ID = " + acessoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_ACESSO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_ACESSO_ID = " + acessoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.Acesso.AcessoItem acessoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_ACESSO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_ACESSO_ID = " + acessoItem.Id + "\n";
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
					sql += "    A.ARQUIVO_ACESSO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_ACESSO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
