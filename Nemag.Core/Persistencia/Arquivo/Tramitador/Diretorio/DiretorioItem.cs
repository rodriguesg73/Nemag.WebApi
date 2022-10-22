using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo.Tramitador.Diretorio 
{ 
    public partial class DiretorioItem : _BaseItem, Interface.Arquivo.Tramitador.Diretorio.IDiretorioItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public DiretorioItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public DiretorioItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, arquivoTramitadorId, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, arquivoTramitadorAcaoId); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem CarregarItem(int arquivoTramitadorDiretorioId)
        {
            var sql = this.PrepararSelecaoSql(arquivoTramitadorDiretorioId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem InserirItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            var sql = this.PrepararInsercaoSql(diretorioItem); 

            sql += this.ObterUltimoItemInseridoSql();

            diretorioItem.Id = base.CarregarItem<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql).Id;

            return diretorioItem;
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem AtualizarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            var sql = this.PrepararAtualizacaoSql(diretorioItem); 

            sql += this.PrepararSelecaoSql(diretorioItem.Id, null, null, null, null);

            diretorioItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql).DataAlteracao;

            return diretorioItem;
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem ExcluirItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            var sql = this.PrepararExclusaoSql(diretorioItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem InativarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem)
        {
            var sql = this.PrepararInativacaoSql(diretorioItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_TRAMITADOR_DIRETORIO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.ARQUIVO_TRAMITADOR_ID,\n";
            sql += "    A.ARQUIVO_TRAMITADOR_ACAO_ID,\n";
            sql += "    A.DIRETORIO_URL,\n";
            sql += "    A1.ARQUIVO_TRAMITADOR_ID AS ARQUIVO_TRAMITADOR_ID,\n";
            sql += "    A1.NOME AS ARQUIVO_TRAMITADOR_NOME,\n";
            sql += "    A1.DESCRICAO AS ARQUIVO_TRAMITADOR_DESCRICAO,\n";
            sql += "    A2.ARQUIVO_TRAMITADOR_ACAO_ID AS ARQUIVO_TRAMITADOR_ACAO_ID,\n";
            sql += "    A2.NOME AS ARQUIVO_TRAMITADOR_ACAO_NOME\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_TB A\n";
            sql += "    INNER JOIN ARQUIVO_TRAMITADOR_TB A1 ON A1.ARQUIVO_TRAMITADOR_ID = A.ARQUIVO_TRAMITADOR_ID\n";
            sql += "    INNER JOIN ARQUIVO_TRAMITADOR_ACAO_TB A2 ON A2.ARQUIVO_TRAMITADOR_ACAO_ID = A.ARQUIVO_TRAMITADOR_ACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoTramitadorDiretorioId, int? registroSituacaoId, int? registroLoginId, int? arquivoTramitadorId, int? arquivoTramitadorAcaoId)
		{ 
			var sql = ""; 

			if (arquivoTramitadorDiretorioId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_DIRETORIO_ID = " + arquivoTramitadorDiretorioId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (arquivoTramitadorId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ID = " + arquivoTramitadorId.Value + "\n";

			if (arquivoTramitadorAcaoId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ACAO_ID = " + arquivoTramitadorAcaoId.Value + "\n";

			if (!arquivoTramitadorDiretorioId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_TRAMITADOR_DIRETORIO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    ARQUIVO_TRAMITADOR_ID,\n";

			sql += "    ARQUIVO_TRAMITADOR_ACAO_ID,\n";

			sql += "    DIRETORIO_URL,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + diretorioItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + diretorioItem.ArquivoTramitadorId.ToString() + ",\n";

			sql += "    " + diretorioItem.ArquivoTramitadorAcaoId.ToString() + ",\n";

			if (string.IsNullOrEmpty(diretorioItem.DiretorioUrl))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + diretorioItem.DiretorioUrl.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + diretorioItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    ARQUIVO_TRAMITADOR_ID = " + diretorioItem.ArquivoTramitadorId.ToString() + ",\n"; 

			sql += "    ARQUIVO_TRAMITADOR_ACAO_ID = " + diretorioItem.ArquivoTramitadorAcaoId.ToString() + ",\n"; 

			if (string.IsNullOrEmpty(diretorioItem.DiretorioUrl))
			    sql += "    DIRETORIO_URL = NULL,\n";
			else
				sql += "    DIRETORIO_URL = '" + diretorioItem.DiretorioUrl.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_ID = " + diretorioItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_ID = " + diretorioItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_DIRETORIO_ID = " + diretorioItem.Id + "\n";
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
					sql += "    A.ARQUIVO_TRAMITADOR_DIRETORIO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_TRAMITADOR_DIRETORIO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
