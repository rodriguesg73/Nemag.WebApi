using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Requisicao.Resultado 
{ 
    public partial class ResultadoItem : _BaseItem, Interface.Requisicao.Resultado.IResultadoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ResultadoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ResultadoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Resultado.ResultadoItem> CarregarListaPorRequisicaoId(int requisicaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, requisicaoId); 

            return base.CarregarLista<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem CarregarItem(int requisicaoResultadoId)
        {
            var sql = this.PrepararSelecaoSql(requisicaoResultadoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Requisicao.Resultado.ResultadoItem InserirItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            var sql = this.PrepararInsercaoSql(resultadoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            resultadoItem.Id = base.CarregarItem<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql).Id;

            return resultadoItem;
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem AtualizarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            var sql = this.PrepararAtualizacaoSql(resultadoItem); 

            sql += this.PrepararSelecaoSql(resultadoItem.Id, null, null);

            resultadoItem.DataAlteracao = base.CarregarItem<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql).DataAlteracao;

            return resultadoItem;
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem ExcluirItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            var sql = this.PrepararExclusaoSql(resultadoItem); 

            return base.CarregarItem<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.Resultado.ResultadoItem InativarItem(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem)
        {
            var sql = this.PrepararInativacaoSql(resultadoItem); 

            return base.CarregarItem<Entidade.Requisicao.Resultado.ResultadoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.REQUISICAO_RESULTADO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REQUISICAO_ID,\n";
            sql += "    A.CONTEUDO,\n";
            sql += "    A.TIPO,\n";
            sql += "    A.CODIGO,\n";
            sql += "    A4.PESSOA_ID AS REQUISICAO_LOGIN_ACESSO_LOGIN_PESSOA_ID,\n";
            sql += "    A4.NOME AS REQUISICAO_LOGIN_ACESSO_LOGIN_NOME,\n";
            sql += "    A5.LOGIN_SITUACAO_ID AS REQUISICAO_LOGIN_ACESSO_LOGIN_SITUACAO_ID,\n";
            sql += "    A5.NOME AS REQUISICAO_LOGIN_ACESSO_LOGIN_SITUACAO_NOME,\n";
            sql += "    A3.LOGIN_ID AS REQUISICAO_LOGIN_ACESSO_LOGIN_ID,\n";
            sql += "    A3.USUARIO AS REQUISICAO_LOGIN_ACESSO_LOGIN_USUARIO,\n";
            sql += "    A3.SENHA AS REQUISICAO_LOGIN_ACESSO_LOGIN_SENHA,\n";
            sql += "    A3.NOME_EXIBICAO AS REQUISICAO_LOGIN_ACESSO_LOGIN_NOME_EXIBICAO,\n";
            sql += "    A2.LOGIN_ACESSO_ID AS REQUISICAO_LOGIN_ACESSO_ID,\n";
            sql += "    A2.TOKEN AS REQUISICAO_LOGIN_ACESSO_TOKEN,\n";
            sql += "    A2.IP AS REQUISICAO_LOGIN_ACESSO_IP,\n";
            sql += "    A2.DATA_VALIDADE AS REQUISICAO_LOGIN_ACESSO_DATA_VALIDADE,\n";
            sql += "    A1.REQUISICAO_ID AS REQUISICAO_ID,\n";
            sql += "    A1.IP AS REQUISICAO_IP,\n";
            sql += "    A1.URL_REFERENCIA AS REQUISICAO_URL_REFERENCIA,\n";
            sql += "    A1.URL_ORIGEM AS REQUISICAO_URL_ORIGEM,\n";
            sql += "    A1.URL_DESTINO AS REQUISICAO_URL_DESTINO\n";
            sql += "FROM \n";
            sql += "    REQUISICAO_RESULTADO_TB A\n";
            sql += "    INNER JOIN REQUISICAO_TB A1 ON A1.REQUISICAO_ID = A.REQUISICAO_ID\n";
            sql += "    LEFT JOIN LOGIN_ACESSO_TB A2 ON A2.LOGIN_ACESSO_ID = A1.LOGIN_ACESSO_ID\n";
            sql += "    LEFT JOIN LOGIN_TB A3 ON A3.LOGIN_ID = A2.LOGIN_ID\n";
            sql += "    LEFT JOIN PESSOA_TB A4 ON A4.PESSOA_ID = A3.PESSOA_ID\n";
            sql += "    LEFT JOIN LOGIN_SITUACAO_TB A5 ON A5.LOGIN_SITUACAO_ID = A3.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? requisicaoResultadoId, int? registroSituacaoId, int? requisicaoId)
		{ 
			var sql = ""; 

			if (requisicaoResultadoId.HasValue)
				sql += "A.REQUISICAO_RESULTADO_ID = " + requisicaoResultadoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (requisicaoId.HasValue)
				sql += "A.REQUISICAO_ID = " + requisicaoId.Value + "\n";

			if (!requisicaoResultadoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO REQUISICAO_RESULTADO_TB(\n";
			sql += "    REQUISICAO_ID,\n";

			sql += "    CONTEUDO,\n";

			sql += "    TIPO,\n";

			sql += "    CODIGO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + resultadoItem.RequisicaoId.ToString() + ",\n";

			    sql += "    '" + resultadoItem.Conteudo.Replace("'", "''") + "',\n";

			    sql += "    '" + resultadoItem.Tipo.Replace("'", "''") + "',\n";

			sql += "    " + resultadoItem.Codigo.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_RESULTADO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REQUISICAO_ID = " + resultadoItem.RequisicaoId.ToString() + ",\n"; 

			sql += "    CONTEUDO = '" + resultadoItem.Conteudo.Replace("'", "''") + "',\n";

			sql += "    TIPO = '" + resultadoItem.Tipo.Replace("'", "''") + "',\n";

			sql += "    CODIGO = " + resultadoItem.Codigo.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    REQUISICAO_RESULTADO_ID = " + resultadoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_RESULTADO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_RESULTADO_ID = " + resultadoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Requisicao.Resultado.ResultadoItem resultadoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_RESULTADO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_RESULTADO_ID = " + resultadoItem.Id + "\n";
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
					sql += "    A.REQUISICAO_RESULTADO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.REQUISICAO_RESULTADO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
