using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo.Tramitador.Email 
{ 
    public partial class EmailItem : _BaseItem, Interface.Arquivo.Tramitador.Email.IEmailItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public EmailItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public EmailItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, arquivoTramitadorId, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, arquivoTramitadorAcaoId); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem CarregarItem(int arquivoTramitadorEmailId)
        {
            var sql = this.PrepararSelecaoSql(arquivoTramitadorEmailId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.Tramitador.Email.EmailItem InserirItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            var sql = this.PrepararInsercaoSql(emailItem); 

            sql += this.ObterUltimoItemInseridoSql();

            emailItem.Id = base.CarregarItem<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql).Id;

            return emailItem;
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem AtualizarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            var sql = this.PrepararAtualizacaoSql(emailItem); 

            sql += this.PrepararSelecaoSql(emailItem.Id, null, null, null, null);

            emailItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql).DataAlteracao;

            return emailItem;
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem ExcluirItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            var sql = this.PrepararExclusaoSql(emailItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Email.EmailItem InativarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem)
        {
            var sql = this.PrepararInativacaoSql(emailItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Email.EmailItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_TRAMITADOR_EMAIL_ID,\n";
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
            sql += "    A1.ARQUIVO_TRAMITADOR_ID AS ARQUIVO_TRAMITADOR_ID,\n";
            sql += "    A1.NOME AS ARQUIVO_TRAMITADOR_NOME,\n";
            sql += "    A1.DESCRICAO AS ARQUIVO_TRAMITADOR_DESCRICAO,\n";
            sql += "    A2.ARQUIVO_TRAMITADOR_ACAO_ID AS ARQUIVO_TRAMITADOR_ACAO_ID,\n";
            sql += "    A2.NOME AS ARQUIVO_TRAMITADOR_ACAO_NOME\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_TB A\n";
            sql += "    INNER JOIN ARQUIVO_TRAMITADOR_TB A1 ON A1.ARQUIVO_TRAMITADOR_ID = A.ARQUIVO_TRAMITADOR_ID\n";
            sql += "    INNER JOIN ARQUIVO_TRAMITADOR_ACAO_TB A2 ON A2.ARQUIVO_TRAMITADOR_ACAO_ID = A.ARQUIVO_TRAMITADOR_ACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoTramitadorEmailId, int? registroSituacaoId, int? registroLoginId, int? arquivoTramitadorId, int? arquivoTramitadorAcaoId)
		{ 
			var sql = ""; 

			if (arquivoTramitadorEmailId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_EMAIL_ID = " + arquivoTramitadorEmailId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (arquivoTramitadorId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ID = " + arquivoTramitadorId.Value + "\n";

			if (arquivoTramitadorAcaoId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ACAO_ID = " + arquivoTramitadorAcaoId.Value + "\n";

			if (!arquivoTramitadorEmailId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_TRAMITADOR_EMAIL_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    ARQUIVO_TRAMITADOR_ID,\n";

			sql += "    ARQUIVO_TRAMITADOR_ACAO_ID,\n";

			sql += "    SERVIDOR,\n";

			sql += "    PORTA,\n";

			sql += "    USUARIO,\n";

			sql += "    SENHA,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + emailItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + emailItem.ArquivoTramitadorId.ToString() + ",\n";

			sql += "    " + emailItem.ArquivoTramitadorAcaoId.ToString() + ",\n";

			if (string.IsNullOrEmpty(emailItem.Servidor))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + emailItem.Servidor.Replace("'", "''") + "',\n";

			sql += "    " + (emailItem.Porta > int.MinValue ? emailItem.Porta.ToString() : "NULL") + ",\n";

			    sql += "    '" + emailItem.Usuario.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(emailItem.Senha))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + emailItem.Senha.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + emailItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    ARQUIVO_TRAMITADOR_ID = " + emailItem.ArquivoTramitadorId.ToString() + ",\n"; 

			sql += "    ARQUIVO_TRAMITADOR_ACAO_ID = " + emailItem.ArquivoTramitadorAcaoId.ToString() + ",\n"; 

			if (string.IsNullOrEmpty(emailItem.Servidor))
			    sql += "    SERVIDOR = NULL,\n";
			else
				sql += "    SERVIDOR = '" + emailItem.Servidor.Replace("'", "''") + "',\n";

			sql += "    PORTA = " + (emailItem.Porta > int.MinValue ? emailItem.Porta.ToString() : "NULL") + ",\n"; 

			sql += "    USUARIO = '" + emailItem.Usuario.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(emailItem.Senha))
			    sql += "    SENHA = NULL,\n";
			else
				sql += "    SENHA = '" + emailItem.Senha.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_ID = " + emailItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_ID = " + emailItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_EMAIL_ID = " + emailItem.Id + "\n";
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
					sql += "    A.ARQUIVO_TRAMITADOR_EMAIL_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_TRAMITADOR_EMAIL_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
