using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nemag.Ferramenta.Core.Entidade.Tabela;
using Nemag.Ferramenta.Core.Entidade.Tabela.Coluna;
using Nemag.Ferramenta.Core.Entidade.Tabela.Relacionamento;
using Nemag.Ferramenta.Core.Interface.Codigo;
using Nemag.Database;

namespace Nemag.Ferramenta.Core.Negocio.Codigo.MySql
{

    public class MySqlItem : CodigoItem, ICodigoItem
    {
        public List<TabelaItem> ObterTabelaLista()
        {
            var databaseItem = new DatabaseItem();

            var sql = @"SELECT UPPER(A.TABLE_NAME) AS TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_SCHEMA = '" + databaseItem.DatabaseNome + "'";

            var dataTable = databaseItem.ExecutarRetornandoDataTable(sql);

            var tabelaLista = new List<TabelaItem>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var tabelaItem = ObterTabelaItem(dataRow["TABLE_NAME"].ToString());

                tabelaLista.Add(tabelaItem);
            }

            for (int i = 0; i < tabelaLista.Count; i++)
            {
                var tabelaItem = tabelaLista[i];

                var relationamentoNivel = IdentificarTabelaRelacionamentoNivel(tabelaItem, tabelaLista);

                tabelaLista[i].RelacionamentoNivel = relationamentoNivel;
            }

            return tabelaLista;
        }

        public TabelaItem ObterTabelaItem(string tabelaNome)
        {
            var tabelaColunaLista = ObterTabelaColunaLista(tabelaNome);

            if (tabelaColunaLista == null || tabelaColunaLista.Count.Equals(0))
                return null;

            var relationamentoLista = ObterTabelaRelacionamentoLista(tabelaNome);

            var tabelaItem = new TabelaItem
            {
                Nome = tabelaNome,

                ColunaLista = tabelaColunaLista,

                RelacionamentoLista = relationamentoLista
            };

            return tabelaItem;
        }

        public List<RelacionamentoItem> ObterTabelaRelacionamentoLista(string tabelaNome)
        {
            var databaseItem = new DatabaseItem();

            var sql = @"
                SELECT 
	                A.CONSTRAINT_NAME,
	                A.TABLE_NAME,
	                A.COLUMN_NAME,
	                A.REFERENCED_TABLE_NAME,
	                A.REFERENCED_COLUMN_NAME
                FROM
                  INFORMATION_SCHEMA.KEY_COLUMN_USAGE A
                WHERE
                  A.TABLE_SCHEMA = SCHEMA()
                  AND A.REFERENCED_TABLE_NAME IS NOT NULL
                  AND A.TABLE_NAME = '" + tabelaNome + "'";

            var dataTable = databaseItem.ExecutarRetornandoDataTable(sql);

            var tabelaRelacionamentoLista = new List<RelacionamentoItem>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var tabelaRelacionamentoItem = new RelacionamentoItem
                {
                    Nome = dataRow["CONSTRAINT_NAME"].ToString(),

                    TabelaNome = dataRow["TABLE_NAME"].ToString(),

                    ColunaNome = dataRow["COLUMN_NAME"].ToString(),

                    TabelaReferenciaNome = dataRow["REFERENCED_TABLE_NAME"].ToString(),

                    ColunaReferenciaNome = dataRow["REFERENCED_COLUMN_NAME"].ToString()
                };

                tabelaRelacionamentoLista.Add(tabelaRelacionamentoItem);
            }

            return tabelaRelacionamentoLista;
        }

        public List<ColunaItem> ObterTabelaColunaLista(string tabelaNome)
        {
            var databaseItem = new DatabaseItem();

            var sql = "";

            sql = string.Format(@"
                SELECT 
	                UPPER(A.COLUMN_NAME) AS COLUMN_NAME, 
                    UPPER(A.DATA_TYPE) AS DATA_TYPE,
                    COALESCE(A.CHARACTER_MAXIMUM_LENGTH, 0) AS CHARACTER_MAXIMUM_LENGTH,
                    COALESCE(A.NUMERIC_PRECISION, 0) AS NUMERIC_PRECISION,
                    COALESCE(A.NUMERIC_SCALE, 0) AS NUMERIC_SCALE,
                    CASE WHEN A.IS_NULLABLE = 'YES' OR A.IS_NULLABLE = 'SIM' THEN 1 ELSE 0 END AS IS_NULLABLE,
                    CASE WHEN B.CONSTRAINT_NAME = 'PRIMARY' THEN 1 ELSE 0 END AS IS_PRIMARY_KEY,
                    CASE WHEN B.CONSTRAINT_NAME <> 'PRIMARY' AND B.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IS_FOREIGN_KEY,
                    CASE WHEN UPPER(A.EXTRA) = 'AUTO_INCREMENT' THEN 1 ELSE 0 END AS IS_AUTO_INCREMENT,
                    A.COLUMN_DEFAULT
                FROM 
	                INFORMATION_SCHEMA.COLUMNS A
                    LEFT OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE B ON B.CONSTRAINT_SCHEMA = A.TABLE_SCHEMA AND B.TABLE_NAME = A.TABLE_NAME AND B.COLUMN_NAME = A.COLUMN_NAME 
                WHERE 
	                A.TABLE_SCHEMA = '{0}' 
                    AND A.TABLE_NAME = '{1}';
                ;", databaseItem.DatabaseNome, tabelaNome);

            var dataTable = databaseItem.ExecutarRetornandoDataTable(sql);

            var tabelaColunaLista = new List<ColunaItem>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var tabelaColunaItem = new ColunaItem
                {
                    NomeOriginal = dataRow["COLUMN_NAME"].ToString(),

                    Tipo = dataRow["DATA_TYPE"].ToString(),

                    TextoTamanho = dataRow["CHARACTER_MAXIMUM_LENGTH"].ToString(),

                    NumeroTamanho = Convert.ToInt32(dataRow["NUMERIC_PRECISION"].ToString()),

                    NumeroEscala = Convert.ToInt32(dataRow["NUMERIC_SCALE"].ToString()),

                    ChavePrimaria = dataRow["IS_PRIMARY_KEY"].ToString().Equals("1"),

                    ChaveEstrangeira = dataRow["IS_FOREIGN_KEY"].ToString().Equals("1"),

                    Nulavel = dataRow["IS_NULLABLE"].ToString().Equals("1"),

                    AutoIncremental = dataRow["IS_AUTO_INCREMENT"].ToString().Equals("1"),

                    ValorPadrao = dataRow["COLUMN_DEFAULT"].ToString()
                };

                tabelaColunaLista.Add(tabelaColunaItem);
            }

            return tabelaColunaLista;
        }

        public string MontarSqlCreateTable(TabelaItem tabelaItem, List<TabelaItem> tabelaLista)
        {
            var sql = string.Empty;

            sql += "DROP TABLE IF EXISTS " + tabelaItem.Nome + ";\n";

            sql += "CREATE TABLE " + tabelaItem.Nome + " (\n";

            for (int i = 0; i < tabelaItem.ColunaLista.Count; i++)
            {
                var tabelaColunaItem = tabelaItem.ColunaLista[i];

                sql += "    " + tabelaColunaItem.NomeOriginal + " ";

                switch (tabelaColunaItem.Tipo.ToUpper())
                {
                    case "DECIMAL":
                        sql += tabelaColunaItem.Tipo + "(" + tabelaColunaItem.NumeroTamanho + ", " + tabelaColunaItem.NumeroEscala + ")";
                        break;

                    case "VARCHAR":
                        if (tabelaColunaItem.TextoTamanho.Equals("MAX"))
                            sql += "LONGTEXT";
                        else
                            sql += tabelaColunaItem.Tipo + "(" + tabelaColunaItem.TextoTamanho + ")";
                        break;

                    case "SMALLDATETIME":
                        sql += "DATETIME";
                        break;

                    default:
                        sql += tabelaColunaItem.Tipo;
                        break;
                }

                sql += " ";

                if (tabelaColunaItem.ChavePrimaria)
                    sql += "PRIMARY KEY ";

                if (tabelaColunaItem.AutoIncremental)
                    sql += "AUTO_INCREMENT ";

                if (string.IsNullOrEmpty(tabelaColunaItem.ValorPadrao))
                {
                    if (!tabelaColunaItem.Nulavel)
                        sql += "NOT ";

                    sql += "NULL,\n";
                }
                else
                {
                    switch (tabelaColunaItem.ValorPadrao)
                    {
                        case "(GETDATE())":
                            tabelaColunaItem.ValorPadrao = "CURRENT_TIMESTAMP";
                            break;
                    }

                    sql += "DEFAULT " + tabelaColunaItem.ValorPadrao + " NOT NULL,\n";
                }
            }

            for (int i = 0; i < tabelaItem.RelacionamentoLista.Count; i++)
            {
                var tabelaRelacionamentoItem = tabelaItem.RelacionamentoLista[i];

                sql += "    CONSTRAINT FK_" + tabelaItem.Nome.ToUpper().Replace("_TB", "_") + (i + 1) + " FOREIGN KEY (" + tabelaRelacionamentoItem.ColunaNome + ") REFERENCES " + tabelaRelacionamentoItem.TabelaReferenciaNome + "(" + tabelaRelacionamentoItem.ColunaReferenciaNome + "),\n";
            }

            sql = sql[0..^2] + "\n";

            sql += ");\n";

            _ = IdentificarTabelaRelacionamentoNivel(tabelaItem, tabelaLista);

            return sql;
        }

        public void AuditarTabelaItem(TabelaItem tabelaItem)
        {
            var databaseItem = new DatabaseItem();

            var sql = string.Empty;

            var tabelaColunaDataInclusao = tabelaItem.ColunaLista
                .Where(x => x.NomeOriginal.ToUpper().Equals("DATA_INCLUSAO"))
                .FirstOrDefault();

            var tabelaColunaDataAlteracao = tabelaItem.ColunaLista
                .Where(x => x.NomeOriginal.ToUpper().Equals("DATA_ALTERACAO"))
                .FirstOrDefault();

            var tabelaColunaRegistroLoginId = tabelaItem.ColunaLista
                .Where(x => x.NomeOriginal.ToUpper().Equals("REGISTRO_LOGIN_ID"))
                .FirstOrDefault();

            if (tabelaColunaDataInclusao == null)
                databaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD DATA_INCLUSAO DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL");
            else if (string.IsNullOrEmpty(tabelaColunaDataInclusao.ValorPadrao))
                databaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " MODIFY COLUMN DATA_INCLUSAO DATETIME DEFAULT CURRENT_TIMESTAMP");

            if (tabelaColunaDataAlteracao == null)
                databaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD DATA_ALTERACAO DATETIME NULL");

            var tabelaLoginTb = ObterTabelaItem("LOGIN_TB");

            if (tabelaLoginTb == null)
                return;

            if (!tabelaItem.Nome.ToUpper().Equals("PESSOA_TB") && !tabelaItem.Nome.ToUpper().Equals("LOGIN_TB"))
            {
                if (tabelaColunaRegistroLoginId == null)
                {
                    var linhaQuantidade = databaseItem.ExecutarRetornandoDataTable("SELECT TOP 1 * FROM " + tabelaItem.Nome)?.Rows.Count;

                    if (linhaQuantidade > 0)
                    {
                        var loginId = databaseItem.ExecutarRetornandoDataTable("SELECT TOP 1 * FROM LOGIN_TB A ORDER BY REGISTRO_LOGIN_ID")?.Rows[0]["REGISTRO_LOGIN_ID"].ToString();

                        databaseItem.ExecutarSemRetorno("UPDATE A SET A.REGISTRO_LOGIN_ID = " + loginId + " FROM " + tabelaItem.Nome + " A");

                        databaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ALTER COLUMN REGISTRO_LOGIN_ID INTEGER NOT NULL");
                    }
                    else
                    {
                        databaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD REGISTRO_LOGIN_ID INTEGER NOT NULL");
                    }

                    databaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD CONSTRAINT FK_" + tabelaItem.Nome.Replace("_TB", "_") + (tabelaItem.RelacionamentoLista.Count + 1) + " FOREIGN KEY (REGISTRO_LOGIN_ID) REFERENCES LOGIN_TB (REGISTRO_LOGIN_ID)");
                }
            }
        }

        public string MontarClasseEntidadeItem(string namespaceClasse, TabelaItem tabelaItem)
        {
            return MontarClasseEntidadeItem(namespaceClasse, tabelaItem, null);
        }

        public new string MontarClassePersistenciaItem(string namespaceClasse, string namespaceDatabase, TabelaItem tabelaItem)
        {
            var classeSql = base.MontarClassePersistenciaItem(namespaceClasse, namespaceDatabase, tabelaItem);

            return classeSql;
        }
    }
}
