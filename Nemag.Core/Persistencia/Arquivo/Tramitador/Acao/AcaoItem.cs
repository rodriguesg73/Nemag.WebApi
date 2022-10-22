using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Arquivo.Tramitador.Acao 
{ 
    public partial class AcaoItem : _BaseItem, Interface.Arquivo.Tramitador.Acao.IAcaoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public AcaoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public AcaoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Arquivo.Tramitador.Acao.AcaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem CarregarItem(int arquivoTramitadorAcaoId)
        {
            var sql = this.PrepararSelecaoSql(arquivoTramitadorAcaoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem InserirItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            var sql = this.PrepararInsercaoSql(acaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            acaoItem.Id = base.CarregarItem<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql).Id;

            return acaoItem;
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem AtualizarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(acaoItem); 

            sql += this.PrepararSelecaoSql(acaoItem.Id, null, null);

            acaoItem.DataAlteracao = base.CarregarItem<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql).DataAlteracao;

            return acaoItem;
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem ExcluirItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            var sql = this.PrepararExclusaoSql(acaoItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Arquivo.Tramitador.Acao.AcaoItem InativarItem(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem)
        {
            var sql = this.PrepararInativacaoSql(acaoItem); 

            return base.CarregarItem<Entidade.Arquivo.Tramitador.Acao.AcaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.ARQUIVO_TRAMITADOR_ACAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? arquivoTramitadorAcaoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (arquivoTramitadorAcaoId.HasValue)
				sql += "A.ARQUIVO_TRAMITADOR_ACAO_ID = " + arquivoTramitadorAcaoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!arquivoTramitadorAcaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO ARQUIVO_TRAMITADOR_ACAO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + acaoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + acaoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + acaoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + acaoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_ID = " + acaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_ID = " + acaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Arquivo.Tramitador.Acao.AcaoItem acaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    ARQUIVO_TRAMITADOR_ACAO_ID = " + acaoItem.Id + "\n";
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
					sql += "    A.ARQUIVO_TRAMITADOR_ACAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.ARQUIVO_TRAMITADOR_ACAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
