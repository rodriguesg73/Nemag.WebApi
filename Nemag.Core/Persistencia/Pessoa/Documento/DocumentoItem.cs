using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa.Documento 
{ 
    public partial class DocumentoItem : _BaseItem, Interface.Pessoa.Documento.IDocumentoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public DocumentoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public DocumentoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Documento.DocumentoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Documento.DocumentoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Documento.DocumentoItem> CarregarListaPorPessoaDocumentoTipoId(int pessoaDocumentoTipoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, pessoaDocumentoTipoId); 

            return base.CarregarLista<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem CarregarItem(int pessoaDocumentoId)
        {
            var sql = this.PrepararSelecaoSql(pessoaDocumentoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pessoa.Documento.DocumentoItem InserirItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            var sql = this.PrepararInsercaoSql(documentoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            documentoItem.Id = base.CarregarItem<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql).Id;

            return documentoItem;
        } 

        public Entidade.Pessoa.Documento.DocumentoItem AtualizarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            var sql = this.PrepararAtualizacaoSql(documentoItem); 

            sql += this.PrepararSelecaoSql(documentoItem.Id, null, null);

            documentoItem.DataAlteracao = base.CarregarItem<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql).DataAlteracao;

            return documentoItem;
        } 

        public Entidade.Pessoa.Documento.DocumentoItem ExcluirItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            var sql = this.PrepararExclusaoSql(documentoItem); 

            return base.CarregarItem<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Documento.DocumentoItem InativarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem)
        {
            var sql = this.PrepararInativacaoSql(documentoItem); 

            return base.CarregarItem<Entidade.Pessoa.Documento.DocumentoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PESSOA_DOCUMENTO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.PESSOA_DOCUMENTO_TIPO_ID,\n";
            sql += "    A.VALOR,\n";
            sql += "    A1.PESSOA_DOCUMENTO_TIPO_ID AS PESSOA_DOCUMENTO_TIPO_ID,\n";
            sql += "    A1.NOME AS PESSOA_DOCUMENTO_TIPO_NOME\n";
            sql += "FROM \n";
            sql += "    PESSOA_DOCUMENTO_TB A\n";
            sql += "    INNER JOIN PESSOA_DOCUMENTO_TIPO_TB A1 ON A1.PESSOA_DOCUMENTO_TIPO_ID = A.PESSOA_DOCUMENTO_TIPO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? pessoaDocumentoId, int? registroSituacaoId, int? pessoaDocumentoTipoId)
		{ 
			var sql = ""; 

			if (pessoaDocumentoId.HasValue)
				sql += "A.PESSOA_DOCUMENTO_ID = " + pessoaDocumentoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (pessoaDocumentoTipoId.HasValue)
				sql += "A.PESSOA_DOCUMENTO_TIPO_ID = " + pessoaDocumentoTipoId.Value + "\n";

			if (!pessoaDocumentoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pessoa.Documento.DocumentoItem documentoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PESSOA_DOCUMENTO_TB(\n";
			sql += "    PESSOA_DOCUMENTO_TIPO_ID,\n";

			sql += "    VALOR,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + documentoItem.PessoaDocumentoTipoId.ToString() + ",\n";

			    sql += "    '" + documentoItem.Valor.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pessoa.Documento.DocumentoItem documentoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_DOCUMENTO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    PESSOA_DOCUMENTO_TIPO_ID = " + documentoItem.PessoaDocumentoTipoId.ToString() + ",\n"; 

			sql += "    VALOR = '" + documentoItem.Valor.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PESSOA_DOCUMENTO_ID = " + documentoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pessoa.Documento.DocumentoItem documentoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_DOCUMENTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PESSOA_DOCUMENTO_ID = " + documentoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pessoa.Documento.DocumentoItem documentoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_DOCUMENTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PESSOA_DOCUMENTO_ID = " + documentoItem.Id + "\n";
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
					sql += "    A.PESSOA_DOCUMENTO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PESSOA_DOCUMENTO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
