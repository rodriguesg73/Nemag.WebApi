using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Cliente 
{ 
    public partial class ClienteItem : _BaseItem, Interface.Cliente.IClienteItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ClienteItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ClienteItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Cliente.ClienteItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Cliente.ClienteItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Cliente.ClienteItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null); 

            return base.CarregarLista<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Cliente.ClienteItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, pessoaId); 

            return base.CarregarLista<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public Entidade.Cliente.ClienteItem CarregarItem(int clienteId)
        {
            var sql = this.PrepararSelecaoSql(clienteId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Cliente.ClienteItem InserirItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            var sql = this.PrepararInsercaoSql(clienteItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public Entidade.Cliente.ClienteItem AtualizarItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            var sql = this.PrepararAtualizacaoSql(clienteItem); 

            sql += this.PrepararSelecaoSql(clienteItem.Id, null, null, null);

            return base.CarregarItem<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public Entidade.Cliente.ClienteItem ExcluirItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            var sql = this.PrepararExclusaoSql(clienteItem); 

            return base.CarregarItem<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        public Entidade.Cliente.ClienteItem InativarItem(Entidade.Cliente.ClienteItem clienteItem)
        {
            var sql = this.PrepararInativacaoSql(clienteItem); 

            return base.CarregarItem<Entidade.Cliente.ClienteItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.CLIENTE_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.PESSOA_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A1.PESSOA_ID AS PESSOA_ID,\n";
            sql += "    A1.NOME AS NOME\n";
            sql += "FROM \n";
            sql += "    CLIENTE_TB A\n";
            sql += "    INNER JOIN PESSOA_TB A1 ON A1.PESSOA_ID = A.PESSOA_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? clienteId, int? registroSituacaoId, int? registroLoginId, int? pessoaId)
		{ 
			var sql = ""; 

			if (clienteId.HasValue)
				sql += "A.CLIENTE_ID = " + clienteId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (pessoaId.HasValue)
				sql += "A.PESSOA_ID = " + pessoaId.Value + "\n";

			if (!clienteId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Cliente.ClienteItem clienteItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO CLIENTE_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    PESSOA_ID,\n";

			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + clienteItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + clienteItem.PessoaId.ToString() + ",\n";

			    sql += "    '" + clienteItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Cliente.ClienteItem clienteItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    CLIENTE_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + clienteItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    PESSOA_ID = " + clienteItem.PessoaId.ToString() + ",\n"; 

			sql += "    NOME = '" + clienteItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    CLIENTE_ID = " + clienteItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Cliente.ClienteItem clienteItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    CLIENTE_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    CLIENTE_ID = " + clienteItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Cliente.ClienteItem clienteItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    CLIENTE_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    CLIENTE_ID = " + clienteItem.Id + "\n";
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
					sql += "    A.CLIENTE_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.CLIENTE_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
