using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Venda 
{ 
    public partial class VendaItem : _BaseItem, Interface.Venda.IVendaItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public VendaItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public VendaItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Venda.VendaItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.VendaItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.VendaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null); 

            return base.CarregarLista<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.VendaItem> CarregarListaPorClienteId(int clienteId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, clienteId); 

            return base.CarregarLista<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.VendaItem CarregarItem(int vendaId)
        {
            var sql = this.PrepararSelecaoSql(vendaId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Venda.VendaItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Venda.VendaItem InserirItem(Entidade.Venda.VendaItem vendaItem)
        {
            var sql = this.PrepararInsercaoSql(vendaItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.VendaItem AtualizarItem(Entidade.Venda.VendaItem vendaItem)
        {
            var sql = this.PrepararAtualizacaoSql(vendaItem); 

            sql += this.PrepararSelecaoSql(vendaItem.Id, null, null, null);

            return base.CarregarItem<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.VendaItem ExcluirItem(Entidade.Venda.VendaItem vendaItem)
        {
            var sql = this.PrepararExclusaoSql(vendaItem); 

            return base.CarregarItem<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.VendaItem InativarItem(Entidade.Venda.VendaItem vendaItem)
        {
            var sql = this.PrepararInativacaoSql(vendaItem); 

            return base.CarregarItem<Entidade.Venda.VendaItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.VENDA_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.CLIENTE_ID,\n";
            sql += "    A.VALOR,\n";
            sql += "    A2.PESSOA_ID AS CLIENTE_PESSOA_ID,\n";
            sql += "    A2.NOME AS CLIENTE_NOME,\n";
            sql += "    A1.CLIENTE_ID AS CLIENTE_ID\n";
            sql += "FROM \n";
            sql += "    VENDA_TB A\n";
            sql += "    INNER JOIN CLIENTE_TB A1 ON A1.CLIENTE_ID = A.CLIENTE_ID\n";
            sql += "    INNER JOIN PESSOA_TB A2 ON A2.PESSOA_ID = A1.PESSOA_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? vendaId, int? registroSituacaoId, int? registroLoginId, int? clienteId)
		{ 
			var sql = ""; 

			if (vendaId.HasValue)
				sql += "A.VENDA_ID = " + vendaId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (clienteId.HasValue)
				sql += "A.CLIENTE_ID = " + clienteId.Value + "\n";

			if (!vendaId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Venda.VendaItem vendaItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO VENDA_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    CLIENTE_ID,\n";

			sql += "    VALOR,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + vendaItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + vendaItem.ClienteId.ToString() + ",\n";

			sql += "    " + vendaItem.Valor.ToString().Replace(",", ".") + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Venda.VendaItem vendaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    VENDA_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + vendaItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    CLIENTE_ID = " + vendaItem.ClienteId.ToString() + ",\n"; 

			sql += "    VALOR = " + vendaItem.Valor.ToString().Replace(",", ".") + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    VENDA_ID = " + vendaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Venda.VendaItem vendaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    VENDA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    VENDA_ID = " + vendaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Venda.VendaItem vendaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    VENDA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    VENDA_ID = " + vendaItem.Id + "\n";
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
					sql += "    A.VENDA_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.VENDA_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
