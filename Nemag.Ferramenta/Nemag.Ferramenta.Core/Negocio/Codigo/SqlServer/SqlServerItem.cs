using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nemag.Ferramenta.Core.Entidade.Tabela;
using Nemag.Ferramenta.Core.Entidade.Tabela.Coluna;
using Nemag.Ferramenta.Core.Entidade.Tabela.Relacionamento;
using Nemag.Ferramenta.Core.Interface.Codigo;
using Nemag.Database;

namespace Nemag.Ferramenta.Core.Negocio.Codigo.SqlServer
{
    public class SqlServerItem : CodigoItem, ICodigoItem
    {
        #region Propriedades Privadas

        private DatabaseItem DatabaseItem { get; set; }

        #endregion

        #region Construtores

        public SqlServerItem() : this(new()) { }

        public SqlServerItem(DatabaseItem _databaseItem)
        {
            DatabaseItem = _databaseItem;
        }

        #endregion

        #region Métodos Públicos

        public List<TabelaItem> ObterTabelaLista()
        {
            var sql = @"
                SELECT 
                    * 
                FROM 
                    SYSOBJECTS 
                WHERE 
                    XTYPE = 'U'
                ;";

            var dataTable = DatabaseItem.ExecutarRetornandoDataTable(sql);

            var tabelaLista = new List<TabelaItem>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var tabelaItem = ObterTabelaItem(dataRow["NAME"].ToString());

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
            var sql = @"
                SELECT  
	                B.NAME AS [CONSTRAINT_NAME],
                    C.NAME AS [TABLE_NAME],
                    E.NAME AS [COLUMN_NAME],
                    F.NAME AS [REFERENCED_TABLE_NAME],
                    G.NAME AS [REFERENCED_COLUMN_NAME]
                FROM 
	                SYS.FOREIGN_KEY_COLUMNS A
	                INNER JOIN SYS.OBJECTS B ON B.OBJECT_ID = A.CONSTRAINT_OBJECT_ID
	                INNER JOIN SYS.TABLES C ON C.OBJECT_ID = A.PARENT_OBJECT_ID
	                INNER JOIN SYS.SCHEMAS D ON C.SCHEMA_ID = D.SCHEMA_ID
	                INNER JOIN SYS.COLUMNS E ON E.COLUMN_ID = PARENT_COLUMN_ID AND E.OBJECT_ID = C.OBJECT_ID
	                INNER JOIN SYS.TABLES F ON F.OBJECT_ID = A.REFERENCED_OBJECT_ID
	                INNER JOIN SYS.COLUMNS G ON G.COLUMN_ID = REFERENCED_COLUMN_ID AND G.OBJECT_ID = F.OBJECT_ID
                WHERE
	                C.NAME = '" + tabelaNome + "'";

            var dataTable = DatabaseItem.ExecutarRetornandoDataTable(sql);

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
            var sql = string.Format(@"
                SELECT DISTINCT
	                A.COLUMN_ID,
                    UPPER(A.NAME) AS NAME,
                    UPPER(B.NAME) AS TYPE,
                    A.MAX_LENGTH AS LENGTH,
                    A.PRECISION ,
                    A.SCALE ,
                    CASE WHEN A.IS_NULLABLE = 1 THEN 1 ELSE 0 END AS IS_NULLABLE,
                    CASE WHEN D.IS_PRIMARY_KEY IS NOT NULL THEN D.IS_PRIMARY_KEY ELSE 0 END AS IS_PRIMARY_KEY,
	                CASE WHEN E.CONSTRAINT_OBJECT_ID IS NOT NULL THEN 1 ELSE 0 END AS IS_FOREIGN_KEY,
                    CASE WHEN A.IS_IDENTITY = 1 THEN 1 ELSE 0 END AS IS_IDENTITY,
                    UPPER(OBJECT_DEFINITION(a.DEFAULT_OBJECT_ID)) AS DEFAULT_VALUE
                FROM 
                    SYS.COLUMNS A
                    INNER JOIN SYS.TYPES B ON B.USER_TYPE_ID = A.USER_TYPE_ID
                    LEFT OUTER JOIN SYS.INDEX_COLUMNS C ON A.OBJECT_ID = C.OBJECT_ID AND A.COLUMN_ID = C.COLUMN_ID
                    LEFT OUTER JOIN SYS.INDEXES D ON D.OBJECT_ID = C.OBJECT_ID AND D.INDEX_ID = C.INDEX_ID
	                LEFT OUTER JOIN SYS.FOREIGN_KEY_COLUMNS E ON E.PARENT_OBJECT_ID = A.OBJECT_ID AND E.PARENT_COLUMN_ID = A.COLUMN_ID
                WHERE
                    A.OBJECT_ID = OBJECT_ID('{0}')
                ORDER BY
                    A.COLUMN_ID
                ;", tabelaNome);

            var dataTable = DatabaseItem.ExecutarRetornandoDataTable(sql);

            var tabelaLista = new List<ColunaItem>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var tabelaColunaItem = new ColunaItem
                {
                    NomeOriginal = dataRow["NAME"].ToString(),

                    Tipo = dataRow["TYPE"].ToString()
                };

                var tamanho = Convert.ToInt32(dataRow["LENGTH"].ToString());

                if (tamanho < 0)
                    tabelaColunaItem.TextoTamanho = "MAX";
                else
                    tabelaColunaItem.TextoTamanho = tamanho.ToString();

                tabelaColunaItem.NumeroTamanho = Convert.ToInt32(dataRow["PRECISION"].ToString());

                tabelaColunaItem.NumeroEscala = Convert.ToInt32(dataRow["SCALE"].ToString());

                tabelaColunaItem.ChavePrimaria = dataRow["IS_PRIMARY_KEY"].ToString().Equals("1");

                tabelaColunaItem.ChaveEstrangeira = dataRow["IS_FOREIGN_KEY"].ToString().Equals("1");

                tabelaColunaItem.Nulavel = dataRow["IS_NULLABLE"].ToString().Equals("1");

                tabelaColunaItem.AutoIncremental = dataRow["IS_IDENTITY"].ToString().Equals("1");

                tabelaColunaItem.ValorPadrao = dataRow["DEFAULT_VALUE"].ToString();

                tabelaLista.Add(tabelaColunaItem);
            }

            return tabelaLista;
        }

        public string MontarSqlCreateTable(TabelaItem tabelaItem, List<TabelaItem> tabelaLista)
        {
            var sql = string.Empty;

            sql += "IF EXISTS(SELECT A.NAME FROM SYS.TABLES A WHERE A.NAME = '" + tabelaItem.Nome + "') DROP TABLE " + tabelaItem.Nome + ";\n";

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

                    case "LONGTEXT":
                        sql += "VARCHAR(MAX)";
                        break;

                    case "VARCHAR":
                    case "NVARCHAR":
                        if (!string.IsNullOrEmpty(tabelaColunaItem.TextoTamanho) && int.TryParse(tabelaColunaItem.TextoTamanho, out int textoTamanho) && textoTamanho <= 8000)
                            sql += tabelaColunaItem.Tipo + "(" + tabelaColunaItem.TextoTamanho + ")";
                        else
                            sql += tabelaColunaItem.Tipo + "(MAX)";
                        break;

                    case "SMALLDATETIME":
                    case "DATETIME":
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
                    sql += "IDENTITY ";

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
                        case "CURRENT_TIMESTAMP":
                            tabelaColunaItem.ValorPadrao = "(GETDATE())";
                            break;
                    }

                    sql += "DEFAULT " + tabelaColunaItem.ValorPadrao + " NOT NULL,\n";
                }
            }

            for (int i = 0; i < tabelaItem.RelacionamentoLista?.Count; i++)
            {
                var tabelaRelacionamentoItem = tabelaItem.RelacionamentoLista[i];

                sql += "    CONSTRAINT FK_" + tabelaItem.Nome.ToUpper().Replace("_TB", "_") + (i + 1) + " FOREIGN KEY (" + tabelaRelacionamentoItem.ColunaNome + ") REFERENCES " + tabelaRelacionamentoItem.TabelaReferenciaNome + "(" + tabelaRelacionamentoItem.ColunaReferenciaNome + "),\n";
            }

            sql = sql[0..^2] + "\n";

            sql += ");\n";

            return sql;
        }

        public string MontarSqlInsertInto(TabelaItem tabelaItem)
        {
            var sql = string.Empty;

            var dataTable = DatabaseItem.ExecutarRetornandoDataTable("SELECT * FROM " + tabelaItem.Nome + " A;");

            sql += "SET IDENTITY_INSERT " + tabelaItem.Nome + " ON;\n";

            for (int b = 0; b < dataTable.Rows.Count; b++)
            {
                sql += "INSERT INTO " + tabelaItem.Nome + " (";

                for (int z = 0; z < tabelaItem.ColunaLista.Count; z++)
                {
                    var tabelaColunaItem = tabelaItem.ColunaLista[z];

                    sql += tabelaColunaItem.NomeOriginal;

                    sql += ", ";
                }

                sql = sql[0..^2];

                sql += ") VALUES (";

                for (int c = 0; c < tabelaItem.ColunaLista.Count; c++)
                {
                    var tabelaColunaItem = tabelaItem.ColunaLista[c];

                    if (string.IsNullOrEmpty(dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString()) && tabelaColunaItem.Nulavel)
                    {
                        sql += "NULL, ";

                        continue;
                    }

                    sql += tabelaColunaItem.Tipo.ToLower() switch
                    {
                        "int" => dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString() + ", ",
                        "smalldatetime" or "datetime" or "timestamp" => "'" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString())) + "', ",
                        _ => "'" + dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString() + "', ",
                    };
                }

                sql = sql[0..^2];

                sql += ");\n";
            }

            sql += "SET IDENTITY_INSERT " + tabelaItem.Nome + " OFF;\n";

            sql += "GO\n";

            return sql;
        }

        public void AuditarTabelaItem(TabelaItem tabelaItem)
        {
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
                DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD DATA_INCLUSAO DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL");
            else if (string.IsNullOrEmpty(tabelaColunaDataInclusao.ValorPadrao))
                DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD DEFAULT CURRENT_TIMESTAMP FOR DATA_INCLUSAO");

            if (tabelaColunaDataAlteracao == null)
                DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD DATA_ALTERACAO DATETIME NULL");

            var tabelaRegistroSituacaoTb = ObterTabelaItem("REGISTRO_SITUACAO_TB");

            if (tabelaRegistroSituacaoTb != null && !tabelaItem.Nome.Equals("REGISTRO_SITUACAO_TB"))
            {
                var tabelaColunaRegistroSituacaoId = tabelaItem.ColunaLista
                    .Where(x => x.NomeOriginal.Equals("REGISTRO_SITUACAO_ID"))
                    .FirstOrDefault();

                var tabelaRelacionamentoItem = tabelaItem.RelacionamentoLista
                    .Where(x => x.ColunaNome.Equals("REGISTRO_SITUACAO_ID"))
                    .FirstOrDefault();

                var registroSituacaoId = DatabaseItem.ExecutarRetornandoDataTable("SELECT TOP 1 * FROM REGISTRO_SITUACAO_TB A ORDER BY REGISTRO_SITUACAO_ID")?.Rows[0]["REGISTRO_SITUACAO_ID"].ToString();

                if (tabelaColunaRegistroSituacaoId == null)
                    DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD REGISTRO_SITUACAO_ID INTEGER DEFAULT (" + registroSituacaoId + ") NOT NULL");

                if (tabelaRelacionamentoItem == null)
                    DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD CONSTRAINT FK_" + tabelaItem.Nome.Replace("_TB", "_") + (tabelaItem.RelacionamentoLista.Count + 1) + " FOREIGN KEY (REGISTRO_SITUACAO_ID) REFERENCES REGISTRO_SITUACAO_TB (REGISTRO_SITUACAO_ID)");
            }

            var tabelaLoginTb = ObterTabelaItem("LOGIN_TB");

            if (tabelaLoginTb == null)
                return;

            if (!tabelaItem.Nome.ToUpper().Equals("PESSOA_TB") && !tabelaItem.Nome.ToUpper().Equals("LOGIN_TB") && !tabelaItem.Nome.Equals("REGISTRO_SITUACAO_TB"))
            {
                if (tabelaColunaRegistroLoginId == null)
                {
                    var linhaQuantidade = DatabaseItem.ExecutarRetornandoDataTable("SELECT TOP 1 * FROM " + tabelaItem.Nome)?.Rows.Count;

                    if (linhaQuantidade > 0)
                    {
                        var loginId = DatabaseItem.ExecutarRetornandoDataTable("SELECT TOP 1 * FROM LOGIN_TB A ORDER BY REGISTRO_LOGIN_ID")?.Rows[0]["REGISTRO_LOGIN_ID"].ToString();

                        DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD REGISTRO_LOGIN_ID INTEGER NULL");

                        DatabaseItem.ExecutarSemRetorno("UPDATE A SET A.REGISTRO_LOGIN_ID = " + loginId + " FROM " + tabelaItem.Nome + " A");

                        DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ALTER COLUMN REGISTRO_LOGIN_ID INTEGER NOT NULL");
                    }
                    else
                    {
                        DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD REGISTRO_LOGIN_ID INTEGER NOT NULL");
                    }

                    DatabaseItem.ExecutarSemRetorno("ALTER TABLE " + tabelaItem.Nome + " ADD CONSTRAINT FK_" + tabelaItem.Nome.Replace("_TB", "_") + (tabelaItem.RelacionamentoLista.Count + 1) + " FOREIGN KEY (REGISTRO_LOGIN_ID) REFERENCES LOGIN_TB (REGISTRO_LOGIN_ID)");
                }
            }
        }

        public string MontarClasseEntidadeItem(string namespaceClasse, TabelaItem tabelaItem)
        {
            var tabelaReferenciaLista = IdentificarTabelaReferenciaListaPorTabelaItem(tabelaItem);

            var classeSql = MontarClasseEntidadeItem(namespaceClasse, tabelaItem, tabelaReferenciaLista);

            return classeSql;
        }

        public new string MontarClassePersistenciaItem(string namespaceClasse, string namespaceDatabase, TabelaItem tabelaItem)
        {
            var tabelaReferenciaLista = IdentificarTabelaReferenciaListaPorTabelaItem(tabelaItem);

            var classeSql = MontarClassePersistenciaItem(namespaceClasse, namespaceDatabase, tabelaItem, tabelaReferenciaLista);

            return classeSql;
        }

        #endregion

        #region Métodos Privados

        private List<TabelaItem> IdentificarTabelaReferenciaListaPorTabelaItem(TabelaItem tabelaItem)
        {
            return IdentificarTabelaReferenciaListaPorTabelaItem(tabelaItem, null);
        }

        private List<TabelaItem> IdentificarTabelaReferenciaListaPorTabelaItem(TabelaItem tabelaItem, List<TabelaItem> tabelaResultadoLista)
        {
            var primeiraExecucao = false;

            if (tabelaResultadoLista == null)
            {
                primeiraExecucao = true;

                tabelaResultadoLista = new();
            }

            tabelaItem
                .RelacionamentoLista
                .Where(x => !new List<string>() { "REGISTRO_SITUACAO_TB" }.Contains(x.TabelaReferenciaNome))
                .Where(x => !new List<string>() { "REGISTRO_LOGIN_ID" }.Contains(x.ColunaNome))
                .ToList()
                .ForEach(x =>
                {
                    var tabelaReferenciaItem = ObterTabelaItem(x.TabelaReferenciaNome);

                    tabelaResultadoLista.Add(tabelaReferenciaItem);

                    if (x.TabelaReferenciaNome.Equals(tabelaItem.Nome))
                        return;

                    tabelaResultadoLista = IdentificarTabelaReferenciaListaPorTabelaItem(tabelaReferenciaItem, tabelaResultadoLista);
                });

            if (primeiraExecucao)
            {
                var tabelaRepetidaLista = tabelaResultadoLista
                    .Where(x => x.Nome.Equals(tabelaItem.Nome))
                    .ToList();

                tabelaRepetidaLista
                    .ForEach(x =>
                    {
                        tabelaResultadoLista
                            .Where(f => !f.Nome.Equals(tabelaItem.Nome))
                            .ToList()
                            .ForEach(f =>
                            {
                                var tabelaResultadoItem = f.Clone<TabelaItem>();

                                tabelaResultadoLista.Add(tabelaResultadoItem);
                            });
                    });
            }

            tabelaResultadoLista = tabelaResultadoLista
                .Select(x =>
                {
                    x.RelacionamentoNivel = IdentificarTabelaRelacionamentoNivel(x, tabelaResultadoLista);

                    return x;
                })
                .OrderByDescending(x => x.RelacionamentoNivel)
                .Select((x, i) =>
                {
                    x.Indice = i + 1;

                    return x;
                })
                .ToList();

            return tabelaResultadoLista;
        }

        #endregion
    }
}
