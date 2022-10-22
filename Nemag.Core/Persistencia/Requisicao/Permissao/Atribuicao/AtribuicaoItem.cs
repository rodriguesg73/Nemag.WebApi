using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Requisicao.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem, Interface.Requisicao.Permissao.Atribuicao.IAtribuicaoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public AtribuicaoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public AtribuicaoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, loginGrupoId, null); 

            return base.CarregarLista<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, loginPerfilId); 

            return base.CarregarLista<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem CarregarItem(int requisicaoPermissaoAtribuicaoId)
        {
            var sql = this.PrepararSelecaoSql(requisicaoPermissaoAtribuicaoId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem InserirItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararInsercaoSql(atribuicaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            atribuicaoItem.Id = base.CarregarItem<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql).Id;

            return atribuicaoItem;
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(atribuicaoItem); 

            sql += this.PrepararSelecaoSql(atribuicaoItem.Id, null, null, null, null);

            atribuicaoItem.DataAlteracao = base.CarregarItem<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql).DataAlteracao;

            return atribuicaoItem;
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararExclusaoSql(atribuicaoItem); 

            return base.CarregarItem<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem InativarItem(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararInativacaoSql(atribuicaoItem); 

            return base.CarregarItem<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.REQUISICAO_PERMISSAO_ATRIBUICAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.IP,\n";
            sql += "    A.URL_ORIGEM,\n";
            sql += "    A.URL_DESTINO,\n";
            sql += "    A.LOGIN_GRUPO_ID,\n";
            sql += "    A.LOGIN_PERFIL_ID,\n";
            sql += "    A1.LOGIN_GRUPO_ID AS LOGIN_GRUPO_ID,\n";
            sql += "    A1.NOME AS LOGIN_GRUPO_NOME,\n";
            sql += "    A1.DESCRICAO AS LOGIN_GRUPO_DESCRICAO,\n";
            sql += "    A2.LOGIN_PERFIL_ID AS LOGIN_PERFIL_ID,\n";
            sql += "    A2.NOME AS LOGIN_PERFIL_NOME,\n";
            sql += "    A2.DESCRICAO AS LOGIN_PERFIL_DESCRICAO\n";
            sql += "FROM \n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_TB A\n";
            sql += "    INNER JOIN LOGIN_GRUPO_TB A1 ON A1.LOGIN_GRUPO_ID = A.LOGIN_GRUPO_ID\n";
            sql += "    INNER JOIN LOGIN_PERFIL_TB A2 ON A2.LOGIN_PERFIL_ID = A.LOGIN_PERFIL_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? requisicaoPermissaoAtribuicaoId, int? registroSituacaoId, int? registroLoginId, int? loginGrupoId, int? loginPerfilId)
		{ 
			var sql = ""; 

			if (requisicaoPermissaoAtribuicaoId.HasValue)
				sql += "A.REQUISICAO_PERMISSAO_ATRIBUICAO_ID = " + requisicaoPermissaoAtribuicaoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (loginGrupoId.HasValue)
				sql += "A.LOGIN_GRUPO_ID = " + loginGrupoId.Value + "\n";

			if (loginPerfilId.HasValue)
				sql += "A.LOGIN_PERFIL_ID = " + loginPerfilId.Value + "\n";

			if (!requisicaoPermissaoAtribuicaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO REQUISICAO_PERMISSAO_ATRIBUICAO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    IP,\n";

			sql += "    URL_ORIGEM,\n";

			sql += "    URL_DESTINO,\n";

			sql += "    LOGIN_GRUPO_ID,\n";

			sql += "    LOGIN_PERFIL_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + atribuicaoItem.RegistroLoginId.ToString() + ",\n";

			if (string.IsNullOrEmpty(atribuicaoItem.Ip))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + atribuicaoItem.Ip.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(atribuicaoItem.UrlOrigem))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + atribuicaoItem.UrlOrigem.Replace("'", "''") + "',\n";

			    sql += "    '" + atribuicaoItem.UrlDestino.Replace("'", "''") + "',\n";

			sql += "    " + atribuicaoItem.LoginGrupoId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.LoginPerfilId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + atribuicaoItem.RegistroLoginId.ToString() + ",\n"; 

			if (string.IsNullOrEmpty(atribuicaoItem.Ip))
			    sql += "    IP = NULL,\n";
			else
				sql += "    IP = '" + atribuicaoItem.Ip.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(atribuicaoItem.UrlOrigem))
			    sql += "    URL_ORIGEM = NULL,\n";
			else
				sql += "    URL_ORIGEM = '" + atribuicaoItem.UrlOrigem.Replace("'", "''") + "',\n";

			sql += "    URL_DESTINO = '" + atribuicaoItem.UrlDestino.Replace("'", "''") + "',\n";

			sql += "    LOGIN_GRUPO_ID = " + atribuicaoItem.LoginGrupoId.ToString() + ",\n"; 

			sql += "    LOGIN_PERFIL_ID = " + atribuicaoItem.LoginPerfilId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_PERMISSAO_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
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
					sql += "    A.REQUISICAO_PERMISSAO_ATRIBUICAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.REQUISICAO_PERMISSAO_ATRIBUICAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
