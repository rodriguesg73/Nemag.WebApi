using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo.Tramitador.Ftp 
{ 
    public partial class FtpItem : _BaseItem, Interface.Arquivo.Tramitador.Ftp.IFtpItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public FtpItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public FtpItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, arquivoTramitadorId, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, arquivoTramitadorAcaoId); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem CarregarItem(int arquivoTramitadorFtpId)
        {
            var sql = this.PrepararSelecaoSql(arquivoTramitadorFtpId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem InserirItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            var sql = this.PrepararInsercaoSql(ftpItem); 

            sql += this.ObterUltimoItemInseridoSql();

            ftpItem.Id = base.CarregarItem<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql).Id;

            return ftpItem;
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem AtualizarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            var sql = this.PrepararAtualizacaoSql(ftpItem); 

            sql += this.PrepararSelecaoSql(ftpItem.Id, null, null, null, null);

            ftpItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql).DataAlteracao;

            return ftpItem;
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem ExcluirItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            var sql = this.PrepararExclusaoSql(ftpItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Ftp.FtpItem InativarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem)
        {
            var sql = this.PrepararInativacaoSql(ftpItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Ftp.FtpItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_TRAMITADOR_FTP_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.ARQUIVO_TRAMITADOR_ID,\n";
            sql += "    A.ARQUIVO_TRAMITADOR_ACAO_ID,\n";
            sql += "    A.SERVIDOR,\n";
            sql += "    A.PORTA,\n";
            sql += "    A.USUARIO,\n";
            sql += "    A.SENHA,\n";
            sql += "    A.DIRETORIO_URL,\n";
            sql += "    A1.ARQUIVO_TRAMITADOR_ID AS ARQUIVO_TRAMITADOR_ID,\n";
            sql += "    A1.NOME AS ARQUIVO_TRAMITADOR_NOME,\n";
            sql += "    A1.DESCRICAO AS ARQUIVO_TRAMITADOR_DESCRICAO,\n";
            sql += "    A2.ARQUIVO_TRAMITADOR_ACAO_ID AS ARQUIVO_TRAMITADOR_ACAO_ID,\n";
            sql += "    A2.NOME AS ARQUIVO_TRAMITADOR_ACAO_NOME\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_TB A\n";
            sql += "    INNER JOIN ARQUIVO_TRAMITADOR_TB A1 ON A1.ARQUIVO_TRAMITADOR_ID = A.ARQUIVO_TRAMITADOR_ID\n";
            sql += "    INNER JOIN ARQUIVO_TRAMITADOR_ACAO_TB A2 ON A2.ARQUIVO_TRAMITADOR_ACAO_ID = A.ARQUIVO_TRAMITADOR_ACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoTramitadorFtpId, int? registroSituacaoId, int? registroLoginId, int? arquivoTramitadorId, int? arquivoTramitadorAcaoId)
		{ 
			var sql = ""; 

			if (arquivoTramitadorFtpId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_FTP_ID = " + arquivoTramitadorFtpId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (arquivoTramitadorId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ID = " + arquivoTramitadorId.Value + "\n";

			if (arquivoTramitadorAcaoId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ACAO_ID = " + arquivoTramitadorAcaoId.Value + "\n";

			if (!arquivoTramitadorFtpId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_TRAMITADOR_FTP_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    ARQUIVO_TRAMITADOR_ID,\n";

			sql += "    ARQUIVO_TRAMITADOR_ACAO_ID,\n";

			sql += "    SERVIDOR,\n";

			sql += "    PORTA,\n";

			sql += "    USUARIO,\n";

			sql += "    SENHA,\n";

			sql += "    DIRETORIO_URL,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + ftpItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + ftpItem.ArquivoTramitadorId.ToString() + ",\n";

			sql += "    " + ftpItem.ArquivoTramitadorAcaoId.ToString() + ",\n";

			    sql += "    '" + ftpItem.Servidor.Replace("'", "''") + "',\n";

			sql += "    " + (ftpItem.Porta > int.MinValue ? ftpItem.Porta.ToString() : "NULL") + ",\n";

			    sql += "    '" + ftpItem.Usuario.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(ftpItem.Senha))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + ftpItem.Senha.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(ftpItem.DiretorioUrl))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + ftpItem.DiretorioUrl.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + ftpItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    ARQUIVO_TRAMITADOR_ID = " + ftpItem.ArquivoTramitadorId.ToString() + ",\n"; 

			sql += "    ARQUIVO_TRAMITADOR_ACAO_ID = " + ftpItem.ArquivoTramitadorAcaoId.ToString() + ",\n"; 

			sql += "    SERVIDOR = '" + ftpItem.Servidor.Replace("'", "''") + "',\n";

			sql += "    PORTA = " + (ftpItem.Porta > int.MinValue ? ftpItem.Porta.ToString() : "NULL") + ",\n"; 

			sql += "    USUARIO = '" + ftpItem.Usuario.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(ftpItem.Senha))
			    sql += "    SENHA = NULL,\n";
			else
				sql += "    SENHA = '" + ftpItem.Senha.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(ftpItem.DiretorioUrl))
			    sql += "    DIRETORIO_URL = NULL,\n";
			else
				sql += "    DIRETORIO_URL = '" + ftpItem.DiretorioUrl.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_ID = " + ftpItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_ID = " + ftpItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_FTP_ID = " + ftpItem.Id + "\n";
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
					sql += "    A.ARQUIVO_TRAMITADOR_FTP_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_TRAMITADOR_FTP_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
