using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa.Contato 
{ 
    public partial class ContatoItem : _BaseItem, Interface.Pessoa.Contato.IContatoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ContatoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ContatoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorPessoaContatoTipoId(int pessoaContatoTipoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, pessoaContatoTipoId, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, pessoaId, null); 

            return base.CarregarLista<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem CarregarItem(int pessoaContatoId)
        {
            var sql = this.PrepararSelecaoSql(pessoaContatoId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pessoa.Contato.ContatoItem InserirItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            var sql = this.PrepararInsercaoSql(contatoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            contatoItem.Id = base.CarregarItem<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql).Id;

            return contatoItem;
        } 

        public Entidade.Pessoa.Contato.ContatoItem AtualizarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            var sql = this.PrepararAtualizacaoSql(contatoItem); 

            sql += this.PrepararSelecaoSql(contatoItem.Id, null, null, null, null);

            contatoItem.DataAlteracao = base.CarregarItem<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql).DataAlteracao;

            return contatoItem;
        } 

        public Entidade.Pessoa.Contato.ContatoItem ExcluirItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            var sql = this.PrepararExclusaoSql(contatoItem); 

            return base.CarregarItem<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Contato.ContatoItem InativarItem(Entidade.Pessoa.Contato.ContatoItem contatoItem)
        {
            var sql = this.PrepararInativacaoSql(contatoItem); 

            return base.CarregarItem<Entidade.Pessoa.Contato.ContatoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PESSOA_CONTATO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.PESSOA_CONTATO_TIPO_ID,\n";
            sql += "    A.PESSOA_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.VALOR,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A1.PESSOA_CONTATO_TIPO_ID AS PESSOA_CONTATO_TIPO_ID,\n";
            sql += "    A1.NOME AS PESSOA_CONTATO_TIPO_NOME,\n";
            sql += "    A1.DESCRICAO AS PESSOA_CONTATO_TIPO_DESCRICAO,\n";
            sql += "    A2.PESSOA_ID AS PESSOA_ID,\n";
            sql += "    A2.NOME AS NOME\n";
            sql += "FROM \n";
            sql += "    PESSOA_CONTATO_TB A\n";
            sql += "    INNER JOIN PESSOA_CONTATO_TIPO_TB A1 ON A1.PESSOA_CONTATO_TIPO_ID = A.PESSOA_CONTATO_TIPO_ID\n";
            sql += "    INNER JOIN PESSOA_TB A2 ON A2.PESSOA_ID = A.PESSOA_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? pessoaContatoId, int? registroSituacaoId, int? pessoaContatoTipoId, int? pessoaId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (pessoaContatoId.HasValue)
				sql += "A.PESSOA_CONTATO_ID = " + pessoaContatoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (pessoaContatoTipoId.HasValue)
				sql += "A.PESSOA_CONTATO_TIPO_ID = " + pessoaContatoTipoId.Value + "\n";

			if (pessoaId.HasValue)
				sql += "A.PESSOA_ID = " + pessoaId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!pessoaContatoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pessoa.Contato.ContatoItem contatoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PESSOA_CONTATO_TB(\n";
			sql += "    PESSOA_CONTATO_TIPO_ID,\n";

			sql += "    PESSOA_ID,\n";

			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    VALOR,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + contatoItem.PessoaContatoTipoId.ToString() + ",\n";

			sql += "    " + contatoItem.PessoaId.ToString() + ",\n";

			sql += "    " + contatoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + contatoItem.Valor.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pessoa.Contato.ContatoItem contatoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_CONTATO_TB \n";
            sql += "SET\n";
			sql += "    PESSOA_CONTATO_TIPO_ID = " + contatoItem.PessoaContatoTipoId.ToString() + ",\n"; 

			sql += "    PESSOA_ID = " + contatoItem.PessoaId.ToString() + ",\n"; 

			sql += "    REGISTRO_LOGIN_ID = " + contatoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    VALOR = '" + contatoItem.Valor.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PESSOA_CONTATO_ID = " + contatoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pessoa.Contato.ContatoItem contatoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_CONTATO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PESSOA_CONTATO_ID = " + contatoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pessoa.Contato.ContatoItem contatoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_CONTATO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PESSOA_CONTATO_ID = " + contatoItem.Id + "\n";
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
					sql += "    A.PESSOA_CONTATO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PESSOA_CONTATO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
