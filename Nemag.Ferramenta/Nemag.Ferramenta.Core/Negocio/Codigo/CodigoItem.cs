using System.Collections.Generic;
using System.Linq;
using Nemag.Ferramenta.Core.Entidade.Tabela;
using Nemag.Ferramenta.Core.Entidade.Tabela.Coluna;
using Nemag.Database;

namespace Nemag.Ferramenta.Core.Negocio.Codigo
{
    public class CodigoItem
    {
        public static string TratarPrimeiraMaiuscula(string tabelaNome)
        {
            var palavraTratada = string.Empty;

            var palavraLista = tabelaNome
                .Split('_')
                .ToList();

            for (int i = 0; i < palavraLista.Count; i++)
            {
                var palavraItem = palavraLista[i];

                if (palavraItem.Equals("H"))
                    palavraTratada += "Historico";
                else
                    palavraTratada += palavraItem.Substring(0, 1).ToUpper() + palavraItem[1..].ToLower();
            }

            return palavraTratada;
        }

        public static string TratarPrimeiraMinuscula(string tabelaNome)
        {
            var palavraTratada = TratarPrimeiraMaiuscula(tabelaNome);

            palavraTratada = palavraTratada.Substring(0, 1).ToLower() + palavraTratada[1..].ToString();

            return palavraTratada;
        }

        public static string ObterNamespaceNome(string texto)
        {
            texto = texto.Replace("_TB", "");

            if (texto.EndsWith("_ID"))
                texto = texto.Remove(texto.Length - 3);

            var textoSplit = texto
                .Split('_')
                .ToList();

            texto = string.Empty;

            for (int a = 0; a < textoSplit.Count; a++)
            {
                if (textoSplit[a].Equals("H"))
                    texto += "Historico.";
                else
                    texto += TratarPrimeiraMaiuscula(textoSplit[a]) + ".";
            }

            texto = texto[0..^1];

            return texto;
        }

        public static string ObterClasseEntidadeNome(string texto)
        {
            texto = ObterNamespaceNome(texto);

            var textoSplit = texto
                .Split('.')
                .ToList();

            texto = textoSplit[textoSplit.Count - 1] + "Item";

            return texto;
        }

        public static string ObterClasseFiltroNome(string texto)
        {
            texto = ObterClasseEntidadeNome(texto);

            return texto;
        }

        public static string ObterClasseInterfaceNome(string texto)
        {
            texto = "I" + ObterClasseEntidadeNome(texto);

            return texto;
        }

        public static string ObterClasseNegocioNome(string texto)
        {
            return ObterClasseEntidadeNome(texto);
        }

        public static string ObterClassePersistenciaNome(string texto)
        {
            return ObterClasseEntidadeNome(texto);
        }

        public string MontarClasseEntidadeItem(string namespaceClasse, TabelaItem tabelaItem, List<TabelaItem> tabelaReferenciaLista)
        {
            var tabelaNome = tabelaItem.Nome;

            var tabelaColunaLista = tabelaItem.ColunaLista;

            var classeSql = string.Empty;

            var tabelaColunaChaveLista = tabelaColunaLista
                .Where(x => x.ChavePrimaria)
                .ToList();

            var tabelaColunaNormalLista = tabelaColunaLista
                .Where(x => !x.ChavePrimaria)
                .ToList();

            if (tabelaReferenciaLista != null)
            {
                var tabelaColunaReferenciaEntidadeLista = IdentificarTabelaColunaReferenciaEntidadeListaPorTabelaItem(tabelaItem, tabelaReferenciaLista, null);

                tabelaColunaNormalLista.AddRange(tabelaColunaReferenciaEntidadeLista);

                tabelaColunaNormalLista = tabelaColunaNormalLista
                    .GroupBy(x => x.NomeOriginal)
                    .Select(x => x.First())
                    .ToList();

                var tabelaColunaNormalNomeLista = tabelaColunaNormalLista
                    .Select(X => X.NomeOriginal)
                    .ToList();
            }

            tabelaNome = tabelaNome
                .Replace("_TB", "");

            var namespaceNome = ObterNamespaceNome(tabelaNome);

            var interfaceNome = ObterClasseInterfaceNome(tabelaNome);

            var persistenciaNome = ObterClassePersistenciaNome(tabelaNome);

            var negocioNome = ObterClasseNegocioNome(tabelaNome);

            var entidadeNome = ObterClasseEntidadeNome(tabelaNome);

            classeSql += "using System;\n";

            classeSql += "\n";

            classeSql += "namespace " + namespaceClasse + ".Core.Entidade." + namespaceNome + " \n{ \n";

            classeSql += "    public partial class " + entidadeNome + " : _BaseItem \n    { \n";

            for (int i = 0; i < tabelaColunaNormalLista.Count; i++)
            {
                var tabelaColunaNormalItem = tabelaColunaNormalLista[i];

                var tabelaColunaNome = TratarPrimeiraMaiuscula(tabelaColunaNormalItem.NomeOriginal);

                classeSql += "        public ";

                classeSql += tabelaColunaNormalItem.Tipo.ToLower() switch
                {
                    "decimal" or "numeric" => "decimal ",
                    "int" => "int ",
                    "bigint" => "long ",
                    "datetime" or "smalldatetime" => "DateTime ",
                    _ => "string ",
                };

                classeSql += tabelaColunaNome + " { get; set; } \n\n";
            }

            classeSql = classeSql[0..^3] + " \n";

            classeSql += "    } \n";

            classeSql += "} \n";

            return classeSql;
        }

        public string MontarClasseFiltroItem(string namespaceClasse, TabelaItem tabelaItem)
        {
            var tabelaNome = tabelaItem.Nome;

            var tabelaColunaLista = tabelaItem.ColunaLista;

            var classeSql = string.Empty;

            var tabelaColunaChavePrimariaLista = tabelaColunaLista
                .Where(x => x.ChavePrimaria)
                .ToList();

            var tabelaColunaNormalLista = tabelaColunaLista
                .Where(x => !x.ChavePrimaria)
                .ToList();

            tabelaNome = tabelaNome
                .Replace("_TB", "");

            var namespaceNome = ObterNamespaceNome(tabelaNome);

            var interfaceNome = ObterClasseInterfaceNome(tabelaNome);

            var persistenciaNome = ObterClassePersistenciaNome(tabelaNome);

            var negocioNome = ObterClasseNegocioNome(tabelaNome);

            var filtroNome = ObterClasseFiltroNome(tabelaNome);

            classeSql += "using System;\n";

            classeSql += "\n";

            classeSql += "namespace " + namespaceClasse + ".Core.Filtro." + namespaceNome + " \n{ \n";

            classeSql += "    public partial class " + filtroNome + " : _BaseItem \n    { \n";

            for (int i = 0; i < tabelaColunaNormalLista.Count; i++)
            {
                var tabelaColunaNormalItem = tabelaColunaNormalLista[i];

                var tabelaColunaNome = TratarPrimeiraMaiuscula(tabelaColunaNormalItem.NomeOriginal);

                var propriedadeParcial = "        public ";

                propriedadeParcial += tabelaColunaNormalItem.Tipo.ToLower() switch
                {
                    "decimal" or "numeric" => "decimal? ",
                    "int" => "int? ",
                    "bigint" => "long ",
                    "datetime" or "smalldatetime" => "DateTime? ",
                    _ => "string ",
                };
                if (!tabelaColunaNormalItem.ChaveEstrangeira && !tabelaColunaNormalItem.Tipo.ToLower().Equals("varchar"))
                {
                    classeSql += propriedadeParcial + tabelaColunaNome + "Inicial { get; set; }\n\n";

                    classeSql += propriedadeParcial + tabelaColunaNome + "Final { get; set; }\n\n";
                }
                else
                    classeSql += propriedadeParcial + tabelaColunaNome + " { get; set; }\n\n";
            }

            classeSql = classeSql[0..^2] + " \n";

            classeSql += "    } \n";

            classeSql += "} \n";

            return classeSql;
        }

        public string MontarClasseInterfaceItem(string namespaceClasse, TabelaItem tabelaItem)
        {
            var classeSql = string.Empty;

            var tabelaNome = tabelaItem.Nome;

            var tabelaColunaLista = tabelaItem.ColunaLista;

            var chavePrimariaLista = tabelaColunaLista
               .Where(x => x.ChavePrimaria)
               .ToList();

            var chaveEstrangeiraLista = tabelaColunaLista
               .Where(x => x.ChaveEstrangeira)
               .ToList();

            var existeCorporacaoId = chaveEstrangeiraLista
               .Where(x => x.NomeOriginal.Equals("CORPORACAO_ID"))
               .FirstOrDefault() != null;

            chaveEstrangeiraLista = chaveEstrangeiraLista
                .Where(x => !x.NomeOriginal.Equals("CORPORACAO_ID") && !x.NomeOriginal.Equals("REGISTRO_SITUACAO_ID"))
                .ToList();

            tabelaNome = tabelaNome
                .Replace("_TB", "");

            var namespaceNome = ObterNamespaceNome(tabelaNome);

            var interfaceNome = ObterClasseInterfaceNome(tabelaNome);

            var persistenciaNome = ObterClassePersistenciaNome(tabelaNome);

            var negocioNome = ObterClasseNegocioNome(tabelaNome);

            var entidadeNome = ObterClasseEntidadeNome(tabelaNome);

            classeSql += "using System.Collections.Generic;\n";

            classeSql += "\n";

            classeSql += "namespace " + namespaceClasse + ".Core.Interface." + namespaceNome + " \n{ \n";

            classeSql += "    public partial interface " + interfaceNome + "\n    { \n";

            classeSql += "        List<Entidade." + namespaceNome + "." + entidadeNome + "> CarregarLista(";

            if (existeCorporacaoId)
                classeSql += "int corporacaoId";

            classeSql += "); \n\n";

            for (int i = 0; i < chaveEstrangeiraLista.Count; i++)
            {
                var chaveEstrangeiraItem = chaveEstrangeiraLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chaveEstrangeiraItem.NomeOriginal);

                var tipoNome = IdentificarTabelaColunaTipoNome(chaveEstrangeiraItem);

                classeSql += "        List<Entidade." + namespaceNome + "." + entidadeNome + "> CarregarListaPor" + TratarPrimeiraMaiuscula(chaveEstrangeiraItem.NomeOriginal) + "(";

                if (existeCorporacaoId)
                    classeSql += "int corporacaoId, ";

                classeSql += tipoNome + " " + tabelaColunaNome + "); \n\n";
            }

            classeSql += "        Entidade." + namespaceNome + "." + entidadeNome + " CarregarItem(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                var tipoNome = IdentificarTabelaColunaTipoNome(chavePrimariaItem);

                classeSql += tipoNome + " " + tabelaColunaNome + ", ";
            }

            classeSql = classeSql[0..^2] + ");\n\n";

            classeSql += "        Entidade." + namespaceNome + "." + entidadeNome + " InserirItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "        Entidade." + namespaceNome + "." + entidadeNome + " AtualizarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "        Entidade." + namespaceNome + "." + entidadeNome + " ExcluirItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "        Entidade." + namespaceNome + "." + entidadeNome + " InativarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n";

            classeSql += "    } \n";

            classeSql += "} \n";

            return classeSql;
        }

        public string MontarClasseNegocioItem(string namespaceClasse, TabelaItem tabelaItem)
        {
            var classeSql = string.Empty;

            var tabelaNome = tabelaItem.Nome;

            var tabelaColunaLista = tabelaItem.ColunaLista;

            var tabelaColunaPessoaId = tabelaColunaLista
                .Where(x => x.NomeOriginal.ToUpper().Equals("PESSOA_ID") && !tabelaItem.Nome.StartsWith("PESSOA"))
                .FirstOrDefault();

            var chavePrimariaLista = tabelaColunaLista
               .Where(x => x.ChavePrimaria)
               .ToList();

            var chaveEstrangeiraLista = tabelaColunaLista
               .Where(x => x.ChaveEstrangeira)
               .ToList();

            chaveEstrangeiraLista = chaveEstrangeiraLista
                .Where(x => !x.NomeOriginal.Equals("REGISTRO_SITUACAO_ID"))
                .ToList();

            tabelaNome = tabelaNome
                .Replace("_TB", "");

            var namespaceNome = ObterNamespaceNome(tabelaNome);

            var interfaceNome = ObterClasseInterfaceNome(tabelaNome);

            var persistenciaNome = ObterClassePersistenciaNome(tabelaNome);

            var negocioNome = ObterClasseNegocioNome(tabelaNome);

            var entidadeNome = ObterClasseEntidadeNome(tabelaNome);

            classeSql += "using System;\n";

            classeSql += "using System.Collections.Generic;\n";

            classeSql += "\n";

            classeSql += "namespace " + namespaceClasse + ".Core.Negocio." + namespaceNome + " \n{ \n";

            classeSql += "    public partial class " + negocioNome + " : ";

            if (tabelaColunaPessoaId != null)
                classeSql += "Pessoa.PessoaItem";
            else
                classeSql += "_BaseItem";

            classeSql += "\n    { \n";

            classeSql += "        #region Propriedades \n\n";

            classeSql += "        private Interface." + namespaceNome + "." + interfaceNome + " _persistencia" + persistenciaNome + " { get; set; } \n\n";

            classeSql += "        #endregion \n\n";

            classeSql += "        #region Construtores \n\n";

            classeSql += "        public " + negocioNome + "() \n";
            classeSql += "            : this(new Persistencia." + namespaceNome + "." + persistenciaNome + "()) \n";
            classeSql += "        { } \n\n";

            classeSql += "        public " + negocioNome + "(Interface." + namespaceNome + "." + interfaceNome + " persistencia" + persistenciaNome + ") \n";
            classeSql += "        { \n";
            classeSql += "            this._persistencia" + persistenciaNome + " = persistencia" + persistenciaNome + "; \n";
            classeSql += "        } \n\n";

            classeSql += "        #endregion \n\n";

            classeSql += "        #region Métodos Públicos \n\n";

            classeSql += "        public " + ((tabelaColunaPessoaId != null) ? "new " : string.Empty) + "List<Entidade." + namespaceNome + "." + entidadeNome + "> CarregarLista(";

            classeSql += ") \n        { \n";

            classeSql += "            return _persistencia" + persistenciaNome + ".CarregarLista(";

            classeSql += "); \n";

            classeSql += "        } \n\n";

            for (int i = 0; i < chaveEstrangeiraLista.Count; i++)
            {
                var chaveEstrangeiraItem = chaveEstrangeiraLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chaveEstrangeiraItem.NomeOriginal);

                var tipoNome = IdentificarTabelaColunaTipoNome(chaveEstrangeiraItem);

                classeSql += "        public List<Entidade." + namespaceNome + "." + entidadeNome + "> CarregarListaPor" + TratarPrimeiraMaiuscula(chaveEstrangeiraItem.NomeOriginal) + "(";

                classeSql += tipoNome + " " + tabelaColunaNome;

                classeSql += ") \n        { \n";

                classeSql += "            return _persistencia" + persistenciaNome + ".CarregarListaPor" + TratarPrimeiraMaiuscula(chaveEstrangeiraItem.NomeOriginal) + "(";

                classeSql += tabelaColunaNome + "); \n";

                classeSql += "        } \n\n";
            }

            classeSql += "        public " + ((tabelaColunaPessoaId != null) ? "new " : string.Empty) + "Entidade." + namespaceNome + "." + entidadeNome + " CarregarItem(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                var tipoNome = IdentificarTabelaColunaTipoNome(chavePrimariaItem);

                classeSql += tipoNome + " " + tabelaColunaNome + ", ";
            }

            classeSql = classeSql[0..^2] + ")\n        {\n";

            classeSql += "            return _persistencia" + persistenciaNome + ".CarregarItem(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                classeSql += tabelaColunaNome + ", ";
            }

            classeSql = classeSql[0..^2] + ");\n        }\n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " InserirItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            return _persistencia" + persistenciaNome + ".InserirItem(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " AtualizarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            return _persistencia" + persistenciaNome + ".AtualizarItem(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " ExcluirItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            return _persistencia" + persistenciaNome + ".ExcluirItem(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " InativarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            return _persistencia" + persistenciaNome + ".InativarItem(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " SalvarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            if (tabelaColunaPessoaId != null)
            {
                classeSql += "            var pessoaItem = " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Clone<Entidade.Pessoa.PessoaItem>();\n\n";

                classeSql += "            pessoaItem.Id = " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".PessoaId;\n\n";

                classeSql += "            pessoaItem = base.SalvarItem(pessoaItem);\n\n";

                classeSql += "            " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".PessoaId = pessoaItem.Id;\n\n";
            }

            classeSql += "            if (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Id.Equals(0))\n";

            classeSql += "                " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + " = this.InserirItem(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ");\n";

            classeSql += "            else\n";

            classeSql += "                " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + " = this.AtualizarItem(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ");\n\n";

            classeSql += "            return " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ";\n";

            classeSql += "        }\n\n";

            classeSql += "        #endregion \n";

            classeSql += "    } \n";

            classeSql += "} \n";

            return classeSql;
        }

        public string MontarClassePersistenciaItem(string namespaceClasse, string namespaceDatabase, TabelaItem tabelaItem)
        {
            return MontarClassePersistenciaItem(namespaceClasse, namespaceDatabase, tabelaItem, null);
        }

        public string MontarClassePersistenciaItem(string namespaceClasse, string namespaceDatabase, TabelaItem tabelaItem, List<TabelaItem> tabelaReferenciaLista)
        {
            var classeSql = string.Empty;

            var tabelaNome = tabelaItem.Nome;

            var tabelaColunaLista = tabelaItem.ColunaLista;

            var chavePrimariaLista = tabelaColunaLista
              .Where(x => x.ChavePrimaria)
              .ToList();

            var chaveEstrangeiraLista = tabelaColunaLista
               .Where(x => x.ChaveEstrangeira)
               .ToList();

            chaveEstrangeiraLista = chaveEstrangeiraLista
                .ToList();

            var tabelaColunaNormalLista = tabelaColunaLista
               .Where(x => !x.ChavePrimaria)
               .ToList();

            var namespaceNome = ObterNamespaceNome(tabelaNome);

            var interfaceNome = ObterClasseInterfaceNome(tabelaNome);

            var persistenciaNome = ObterClassePersistenciaNome(tabelaNome);

            var negocioNome = ObterClasseNegocioNome(tabelaNome);

            var entidadeNome = ObterClasseEntidadeNome(tabelaNome);

            var objetoNome = entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString();

            classeSql += "using System;\n";

            classeSql += "using System.Collections.Generic;\n";

            classeSql += "\n";

            classeSql += "namespace " + namespaceClasse + ".Core.Persistencia." + namespaceNome + " \n{ \n";

            classeSql += "    public partial class " + persistenciaNome + " : _BaseItem, Interface." + namespaceNome + "." + interfaceNome + "\n    { \n";

            classeSql += "        #region Propriedades\n\n";

            classeSql += "        private " + namespaceDatabase + ".DatabaseItem _databaseItem { get; set; }\n\n";

            classeSql += "        #endregion\n\n";

            classeSql += "        #region Construtores\n\n";

            classeSql += "        public " + persistenciaNome + "() : this(new " + namespaceDatabase + ".DatabaseItem())\n";
            classeSql += "        { }\n\n";

            classeSql += "        public " + persistenciaNome + "(" + namespaceDatabase + ".DatabaseItem databaseItem)\n";
            classeSql += "        {\n\t";
            classeSql += "            _databaseItem = databaseItem;\n";
            classeSql += "        }\n\n";

            classeSql += "        #endregion\n\n";

            classeSql += "        #region Métodos Públicos \n\n";

            classeSql += "        public List<Entidade." + namespaceNome + "." + entidadeNome + "> CarregarLista(";

            classeSql += ") \n        { \n";
            classeSql += "            var sql = this.PrepararSelecaoSql(";

            for (int a = 0; a < chavePrimariaLista.Count; a++)
            {
                classeSql += chavePrimariaLista[a].Tipo switch
                {
                    "string" => "string.Empty, ",
                    "datetime" or "smalldatetime" => "DateTime.MinValue, ",
                    _ => "null, ",
                };
            }

            for (int a = 0; a < chaveEstrangeiraLista.Count; a++)
            {
                classeSql += chaveEstrangeiraLista[a].Tipo switch
                {
                    "string" => "string.Empty, ",
                    "datetime" or "smalldatetime" => "DateTime.MinValue, ",
                    _ => "null, ",
                };
            }

            classeSql = classeSql[0..^2];

            classeSql += "); \n\n";

            classeSql += "            return base.CarregarLista<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n";

            classeSql += "        } \n\n";

            for (int i = 0; i < chaveEstrangeiraLista.Count; i++)
            {
                var chaveEstrangeiraItem = chaveEstrangeiraLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chaveEstrangeiraItem.NomeOriginal);

                var tipoNome = IdentificarTabelaColunaTipoNome(chaveEstrangeiraItem);

                classeSql += "        public List<Entidade." + namespaceNome + "." + entidadeNome + "> CarregarListaPor" + TratarPrimeiraMaiuscula(chaveEstrangeiraItem.NomeOriginal) + "(";

                classeSql += tipoNome + " " + tabelaColunaNome + ") \n        { \n";

                classeSql += "            var sql = this.PrepararSelecaoSql(null, ";

                for (int a = 0; a < chaveEstrangeiraLista.Count; a++)
                {
                    if (a.Equals(i))
                        classeSql += tabelaColunaNome + ", ";
                    else
                        classeSql += chaveEstrangeiraLista[a].Tipo switch
                        {
                            "string" => "string.Empty, ",
                            "smalldatetime" => "DateTime.MinValue, ",
                            _ => "null, ",
                        };
                }

                classeSql = classeSql[0..^2] + "); \n\n";

                classeSql += "            return base.CarregarLista<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n";

                classeSql += "        } \n\n";
            }

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " CarregarItem(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                var tipoNome = IdentificarTabelaColunaTipoNome(chavePrimariaItem);

                classeSql += tipoNome + " " + tabelaColunaNome + ", ";
            }

            classeSql = classeSql[0..^2] + ")\n        {\n";

            classeSql += "            var sql = this.PrepararSelecaoSql(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                classeSql += tabelaColunaNome + ", ";
            }

            for (int a = 0; a < chaveEstrangeiraLista.Count; a++)
            {
                classeSql += chaveEstrangeiraLista[a].Tipo switch
                {
                    "string" => "string.Empty, ",
                    "smalldatetime" => "DateTime.MinValue, ",
                    _ => "null, ",
                };
            }

            classeSql = classeSql[0..^2] + "); \n\n";

            classeSql += "            var retorno = base.CarregarItem<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n\n";

            classeSql += "            return retorno; \n";

            classeSql += "        }\n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " InserirItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            var sql = this.PrepararInsercaoSql(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "            sql += this.ObterUltimoItemInseridoSql();\n\n";

            classeSql += "            return base.CarregarItem<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " AtualizarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            var sql = this.PrepararAtualizacaoSql(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "            sql += this.PrepararSelecaoSql(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                classeSql += objetoNome + "." + tabelaColunaNome.Replace(TratarPrimeiraMinuscula(tabelaNome).Replace("Tb", ""), "") + ", ";
            }

            for (int a = 0; a < chaveEstrangeiraLista.Count; a++)
            {
                classeSql += chaveEstrangeiraLista[a].Tipo switch
                {
                    "string" => "string.Empty, ",
                    "smalldatetime" => "DateTime.MinValue, ",
                    _ => "null, ",
                };
            }

            classeSql = classeSql[0..^2] + ");\n\n";

            classeSql += "            return base.CarregarItem<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " ExcluirItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            var sql = this.PrepararExclusaoSql(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "            return base.CarregarItem<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n";

            classeSql += "        } \n\n";

            classeSql += "        public Entidade." + namespaceNome + "." + entidadeNome + " InativarItem(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ")\n        {\n";

            classeSql += "            var sql = this.PrepararInativacaoSql(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + "); \n\n";

            classeSql += "            return base.CarregarItem<Entidade." + namespaceNome + "." + entidadeNome + ">(_databaseItem, sql); \n";

            classeSql += "        } \n\n";

            classeSql += "        #endregion \n\n";

            classeSql += "        #region Métodos Privados \n\n";

            classeSql += "        private string PrepararSelecaoSql()\n        { \n";

            classeSql += "            var sql = \"\"; \n\n";

            classeSql += "            sql += \"SELECT \\n\";\n";

            for (int i = 0; i < tabelaColunaLista.Count; i++)
                classeSql += "            sql += \"    A." + tabelaColunaLista[i].NomeOriginal + ",\\n\";\n";

            if (tabelaReferenciaLista != null)
            {
                var tabelaColunaReferenciaLista = IdentificarTabelaColunaReferenciaPersistenciaListaPorTabelaItem(tabelaItem, tabelaReferenciaLista, null);

                tabelaColunaReferenciaLista = tabelaColunaReferenciaLista
                    .GroupBy(x => x.NomeExibicao)
                    .Select(x => x.First())
                    .ToList();

                tabelaColunaReferenciaLista.ForEach(x =>
                {
                    classeSql += "            sql += \"    " + x.NomeOriginal + ",\\n\";\n";
                });

                tabelaReferenciaLista
                    .ForEach(x => x.Id = 0);
            }

            classeSql = classeSql[0..^6] + "\\n\";\n";

            classeSql += "            sql += \"FROM \\n\";\n";

            classeSql += "            sql += \"    " + tabelaNome + " A\\n\";\n";

            if (tabelaReferenciaLista != null)
            {
                var classeReferenciaSql = MontarClassePersistenciaSelectReferenciaSql(tabelaItem, tabelaReferenciaLista);

                classeSql += classeReferenciaSql;
            }

            classeSql += "\n            return sql; \n";

            classeSql += "        } \n\n";

            classeSql += "        private string PrepararSelecaoSql(";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                classeSql += chavePrimariaItem.Tipo.ToLower() switch
                {
                    "int" => "int? ",
                    "smalldatetime" or "datetime" or "timestamp" => "DateTime ",
                    _ => "string ",
                };
                classeSql += tabelaColunaNome + ", ";
            }

            for (int i = 0; i < chaveEstrangeiraLista.Count; i++)
            {
                var chaveEstrangeiraItem = chaveEstrangeiraLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chaveEstrangeiraItem.NomeOriginal);

                classeSql += chaveEstrangeiraItem.Tipo.ToLower() switch
                {
                    "int" => "int? ",
                    "smalldatetime" or "datetime" or "timestamp" => "DateTime ",
                    _ => "string ",
                };
                classeSql += tabelaColunaNome + ", ";
            }

            classeSql = classeSql[0..^2] + ")\n\t\t{ \n";

            classeSql += "\t\t\tvar sql = \"\"; \n\n";

            for (int i = 0; i < chavePrimariaLista.Count; i++)
            {
                var chavePrimariaItem = chavePrimariaLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                classeSql += "\t\t\tif (";

                classeSql += chavePrimariaItem.Tipo.ToLower() switch
                {
                    "int" => tabelaColunaNome + ".HasValue",
                    "smalldatetime" or "datetime" or "timestamp" => tabelaColunaNome + " > DateTime.MinValue",
                    _ => "string.IsNullOrEmpty(" + tabelaColunaNome + ")",
                };
                classeSql += ")\n";

                classeSql += chavePrimariaItem.Tipo.ToLower() switch
                {
                    "int" => "\t\t\t\tsql += \"A." + chavePrimariaItem.NomeOriginal + " = \" + " + tabelaColunaNome + ".Value + \"\\n\";\n\n",
                    "smalldatetime" or "datetime" or "timestamp" => "\t\t\t\tsql += \"A." + chavePrimariaItem.NomeOriginal + " = '\" + string.Format(\"{0:yyyy-MM-dd HH:mm:ss}\", " + tabelaColunaNome + ") + \"\\n\";\n\n",
                    "nvarchar" => "\t\t\t\tsql += \"A." + chavePrimariaItem.NomeOriginal + " = \"N'" + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"'\\n\";\n\n",
                    _ => "\t\t\t\tsql += \"A." + chavePrimariaItem.NomeOriginal + " = \"'" + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"'\\n\";\n\n",
                };
            }

            for (int i = 0; i < chaveEstrangeiraLista.Count; i++)
            {
                var chaveEstrangeiraItem = chaveEstrangeiraLista[i];

                var tabelaColunaNome = TratarPrimeiraMinuscula(chaveEstrangeiraItem.NomeOriginal);

                classeSql += "\t\t\tif (";

                classeSql += chaveEstrangeiraItem.Tipo.ToLower() switch
                {
                    "int" => tabelaColunaNome + ".HasValue",
                    "smalldatetime" or "datetime" or "timestamp" => tabelaColunaNome + " > DateTime.MinValue",
                    _ => "string.IsNullOrEmpty(" + tabelaColunaNome + ")",
                };
                classeSql += ")\n";

                classeSql += chaveEstrangeiraItem.Tipo.ToLower() switch
                {
                    "int" => "\t\t\t\tsql += \"A." + chaveEstrangeiraItem.NomeOriginal + " = \" + " + tabelaColunaNome + ".Value + \"\\n\";\n\n",
                    "smalldatetime" or "datetime" or "timestamp" => "\t\t\t\tsql += \"A." + chaveEstrangeiraItem.NomeOriginal + " = '\" + string.Format(\"{0:yyyy-MM-dd HH:mm:ss}\", " + tabelaColunaNome + ") + \"\\n\";\n\n",
                    _ => "\t\t\t\tsql += \"A." + chaveEstrangeiraItem.NomeOriginal + " = \"'" + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"'\\n\";\n\n",
                };
            }

            if (tabelaColunaLista.Where(x => x.NomeOriginal.Equals("REGISTRO_SITUACAO_ID")).FirstOrDefault() != null)
            {
                classeSql += "\t\t\tif (";

                for (int i = 0; i < chavePrimariaLista.Count; i++)
                {
                    var chavePrimariaItem = chavePrimariaLista[i];

                    var tabelaColunaNome = TratarPrimeiraMinuscula(chavePrimariaItem.NomeOriginal);

                    classeSql += chavePrimariaItem.Tipo.ToLower() switch
                    {
                        "int" => "!" + tabelaColunaNome + ".HasValue",
                        "smalldatetime" or "datetime" or "timestamp" => "!" + tabelaColunaNome + " > DateTime.MinValue",
                        _ => "!string.IsNullOrEmpty(" + tabelaColunaNome + ")",
                    };
                }

                classeSql += ")\n";

                classeSql += "\t\t\t\tsql += \"A.REGISTRO_SITUACAO_ID <> 3\\n\";\n\n";
            }

            classeSql += "            if (!string.IsNullOrEmpty(sql))\n";
            classeSql += "            {\n";
            classeSql += "                sql = sql.Substring(0, sql.Length - 1);\n\n";

            classeSql += "                sql = sql.Replace(\"\\n\", \"\\nAND \"); \n\n";

            classeSql += "                sql = \"WHERE\\n\\t\" + sql; \n";
            classeSql += "            } \n\n";

            classeSql += "            sql = this.PrepararSelecaoSql() + \" \" + sql;\n\n";

            classeSql += "            return sql; \n";

            classeSql += "        } \n\n";

            classeSql += "        private string PrepararInsercaoSql(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ") \n        { \n";

            classeSql += "            var sql = string.Empty; \n\n";

            classeSql += "            sql += \"INSERT INTO " + tabelaNome + "(\\n\";\n\t\t\t";

            for (int i = 0; i < tabelaColunaLista.Count; i++)
            {
                var tabelaColunaItem = tabelaColunaLista[i];

                if (tabelaColunaItem.AutoIncremental)
                    continue;

                var tabelaColunaNome = TratarPrimeiraMaiuscula(tabelaColunaItem.NomeOriginal);

                if (tabelaColunaItem.ChavePrimaria)
                    if (tabelaColunaNome.Equals(namespaceNome.Replace(".", "") + "Id"))
                        tabelaColunaNome = "Id";
                    else
                        tabelaColunaNome = tabelaColunaNome.Replace(entidadeNome.Replace("Item", ""), "");

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("DATA_INCLUSAO"))
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("DATA_ALTERACAO"))
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("REGISTRO_SITUACAO_ID"))
                    continue;

                if (tabelaColunaItem.Nulavel || tabelaColunaItem.NomeOriginal.Equals("DataInclusao") || tabelaColunaItem.NomeOriginal.Equals("DataAlteracao"))
                    switch (tabelaColunaItem.Tipo.ToLower())
                    {
                        case "smalldatetime":
                        case "datetime":
                        case "timestamp":
                            //classeSql += "if (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome.Substring(1) + "." + tabelaColunaNome + " > DateTime.MinValue)\n\t\t\t\t";
                            break;

                        case "varchar":
                            //classeSql += "if (!string.IsNullOrEmpty(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome.Substring(1) + "." + tabelaColunaNome + "))\n\t\t\t\t";
                            break;
                    }

                classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + ",\\n\";\n\n\t\t\t";
            }

            classeSql += "sql = sql.Substring(0, sql.Length - 2) + \"\\n\";\n\n\t\t\t";

            classeSql += "sql += \") VALUES (\\n\";\n\t\t\t";

            for (int i = 0; i < tabelaColunaLista.Count; i++)
            {
                var tabelaColunaItem = tabelaColunaLista[i];

                if (tabelaColunaItem.AutoIncremental)
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("DATA_INCLUSAO"))
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("DATA_ALTERACAO"))
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("REGISTRO_SITUACAO_ID"))
                    continue;

                var tabelaColunaNome = TratarPrimeiraMaiuscula(tabelaColunaItem.NomeOriginal);

                if (tabelaColunaItem.ChavePrimaria)
                    if (tabelaColunaNome.Equals(namespaceNome.Replace(".", "") + "Id"))
                        tabelaColunaNome = "Id";
                    else
                        tabelaColunaNome = tabelaColunaNome.Replace(entidadeNome.Replace("Item", ""), "");

                if (tabelaColunaItem.Nulavel || tabelaColunaItem.NomeOriginal.Equals("DataInclusao") || tabelaColunaItem.NomeOriginal.Equals("DataAlteracao"))
                    switch (tabelaColunaItem.Tipo.ToLower())
                    {
                        case "smalldatetime":
                        case "datetime":
                        case "timestamp":
                            classeSql += "if (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(DateTime.MinValue))\n\t\t\t\t";
                            classeSql += "    sql += \"    NULL,\\n\";\n\t\t\t";
                            classeSql += "else\n\t\t\t";
                            break;

                        case "varchar":
                        case "nvarchar":
                            classeSql += "if (string.IsNullOrEmpty(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + "))\n\t\t\t";
                            classeSql += "    sql += \"    NULL,\\n\";\n\t\t\t";
                            classeSql += "else\n\t\t\t";

                            break;
                    }

                switch (tabelaColunaItem.Tipo.ToLower())
                {
                    case "decimal":
                    case "numeric":
                        if (tabelaColunaItem.Nulavel)
                            if (tabelaColunaItem.ChaveEstrangeira)
                                classeSql += "sql += \"    \" + (!" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(0) ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString().Replace(\",\", \".\") : \"NULL\") + \",\\n\";\n\n\t\t\t";
                            else
                                classeSql += "sql += \"    \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > decimal.MinValue ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString().Replace(\",\", \".\") : \"NULL\") + \",\\n\";\n\n\t\t\t";
                        else
                            classeSql += "sql += \"    \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString().Replace(\",\", \".\") + \",\\n\";\n\n\t\t\t";

                        break;

                    case "integer":
                    case "int":
                        if (tabelaColunaItem.Nulavel)
                            if (tabelaColunaItem.ChaveEstrangeira)
                                classeSql += "sql += \"    \" + (!" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(0) ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\";\n\n\t\t\t";
                            else
                                classeSql += "sql += \"    \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > int.MinValue ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\";\n\n\t\t\t";
                        else
                            classeSql += "sql += \"    \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() + \",\\n\";\n\n\t\t\t";

                        break;

                    case "bigint":
                        if (tabelaColunaItem.Nulavel)
                            if (tabelaColunaItem.ChaveEstrangeira)
                                classeSql += "sql += \"    \" + (!" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(0) ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\";\n\n\t\t\t";
                            else
                                classeSql += "sql += \"    \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > long.MinValue ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\";\n\n\t\t\t";
                        else
                            classeSql += "sql += \"    \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() + \",\\n\";\n\n\t\t\t";

                        break;

                    case "smalldatetime":
                    case "datetime":
                    case "timestamp":
                        classeSql += "sql += \"    '\" + string.Format(\"{0:yyyy-MM-dd HH:mm:ss}\", " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ") + \"',\\n\";\n\n\t\t\t";
                        break;

                    case "nvarchar":
                        classeSql += "    sql += \"    N'\" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"',\\n\";\n\n\t\t\t";
                        break;

                    case "varchar":
                    default:
                        classeSql += "    sql += \"    '\" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"',\\n\";\n\n\t\t\t";
                        break;
                }
            }

            classeSql += "sql = sql.Substring(0, sql.Length - 2) + \"\\n\";\n\n";

            classeSql += "            sql += \");\\n\";\n\n";

            classeSql += "            return sql; \n";

            classeSql += "        } \n\n";

            classeSql += "        private string PrepararAtualizacaoSql(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ") \n        { \n";

            classeSql += "            var sql = string.Empty; \n\n";

            classeSql += "            sql += \"UPDATE \\n\";\n";

            classeSql += "            sql += \"    " + tabelaNome + " \\n\";\n";

            classeSql += "            sql += \"SET\\n\";\n\t\t\t";

            for (int i = 0; i < tabelaColunaLista.Count; i++)
            {
                var tabelaColunaItem = tabelaColunaLista[i];

                if (tabelaColunaItem.ChavePrimaria)
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("DATA_INCLUSAO"))
                    continue;

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("DATA_ALTERACAO"))
                {
                    classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = CURRENT_TIMESTAMP,\\n\";\n\n\t\t\t";

                    continue;
                }

                if (tabelaColunaItem.NomeOriginal.ToUpper().Equals("REGISTRO_SITUACAO_ID"))
                    continue;

                var tabelaColunaNome = TratarPrimeiraMaiuscula(tabelaColunaItem.NomeOriginal);

                if (tabelaColunaItem.Nulavel)
                    switch (tabelaColunaItem.Tipo.ToLower())
                    {
                        case "smalldatetime":
                        case "datetime":
                        case "timestamp":
                            //classeSql += "if (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome.Substring(1) + "." + tabelaColunaNome + " > DateTime.MinValue)\n\t\t\t\t";
                            break;

                        default:
                            break;
                    }

                switch (tabelaColunaItem.Tipo.ToLower())
                {
                    case "decimal":
                    case "numeric":
                        if (tabelaColunaItem.Nulavel)
                            if (tabelaColunaItem.ChaveEstrangeira)
                                classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + (!" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(0) ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString().Replace(\",\", \".\") : \"NULL\") + \",\\n\"; \n\n\t\t\t";
                            else
                                classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > decimal.MinValue ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString().Replace(\",\", \".\") : \"NULL\") + \",\\n\"; \n\n\t\t\t";
                        else
                            classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString().Replace(\",\", \".\") + \",\\n\"; \n\n\t\t\t";

                        break;

                    case "integer":
                    case "int":
                        if (tabelaColunaItem.Nulavel)
                            if (tabelaColunaItem.ChaveEstrangeira)
                                classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + (!" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(0) ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\"; \n\n\t\t\t";
                            else
                                classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > int.MinValue ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\"; \n\n\t\t\t";
                        else
                            classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() + \",\\n\"; \n\n\t\t\t";

                        break;

                    case "bigint":
                        if (tabelaColunaItem.Nulavel)
                            if (tabelaColunaItem.ChaveEstrangeira)
                                classeSql += "sql += \"    \" + (!" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Equals(0) ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\";\n\n\t\t\t";
                            else
                                classeSql += "sql += \"    \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > long.MinValue ? " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() : \"NULL\") + \",\\n\";\n\n\t\t\t";
                        else
                            classeSql += "sql += \"    \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".ToString() + \",\\n\";\n\n\t\t\t";

                        break;

                    case "smalldatetime":
                    case "datetime":
                    case "timestamp":
                        classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = \" + (" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + " > DateTime.MinValue ? \"'\" + string.Format(\"{0:yyyy-MM-dd HH:mm:ss}\", " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ") + \"'\" : \"NULL\") + \",\\n\"; \n\n\t\t\t";
                        //classeSql += "sql += \"    A." + tabelaColunaItem.Nome + " = '\" + string.Format(\"{0:yyyy-MM-dd HH:mm:ss}\", " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome.Substring(1) + "." + tabelaColunaNome + ") + \"',\\n\"; \n\n\t\t\t";
                        break;

                    case "nvarchar":
                        if (tabelaColunaItem.Nulavel)
                        {
                            classeSql += "if (string.IsNullOrEmpty(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + "))\n\t\t\t";
                            classeSql += "    sql += \"    " + tabelaColunaItem.NomeOriginal + " = NULL,\\n\";\n\t\t\t";
                            classeSql += "else\n\t\t\t\t";
                        }

                        classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = N'\" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"',\\n\";\n\n\t\t\t";
                        break;

                    case "varchar":
                    default:
                        if (tabelaColunaItem.Nulavel)
                        {
                            classeSql += "if (string.IsNullOrEmpty(" + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + "))\n\t\t\t";
                            classeSql += "    sql += \"    " + tabelaColunaItem.NomeOriginal + " = NULL,\\n\";\n\t\t\t";
                            classeSql += "else\n\t\t\t\t";
                        }

                        classeSql += "sql += \"    " + tabelaColunaItem.NomeOriginal + " = '\" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..] + "." + tabelaColunaNome + ".Replace(\"'\", \"''\") + \"',\\n\";\n\n\t\t\t";

                        break;
                }
            }

            classeSql += "sql = sql.Substring(0, sql.Length - 2) + \"\\n\";\n\n";

            classeSql += "            sql += \"WHERE\\n\";\n";

            classeSql += "            sql += \"    " + chavePrimariaLista.FirstOrDefault().NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Id + \"\\n\";\n";

            classeSql += "            return sql; \n";

            classeSql += "        } \n\n";

            classeSql += "        private string PrepararExclusaoSql(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ") \n        { \n";

            classeSql += "            var sql = string.Empty; \n\n";

            if (tabelaItem.ColunaLista.Where(x => x.NomeOriginal.Equals("REGISTRO_SITUACAO_ID")).FirstOrDefault() != null)
            {
                classeSql += "            sql += \"UPDATE \\n\";\n";

                classeSql += "            sql += \"    " + tabelaNome + "\\n\";\n";

                classeSql += "            sql += \"SET\\n\";\n";

                classeSql += "            sql += \"    REGISTRO_SITUACAO_ID = 3\\n\";\n";

                classeSql += "            sql += \"WHERE\\n\";\n";

                classeSql += "            sql += \"    " + chavePrimariaLista.FirstOrDefault().NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Id + \"\\n\";\n";
            }
            else
            {
                classeSql += "            sql += \"DELETE \\n\";\n";

                classeSql += "            sql += \"FROM\\n\";\n";

                classeSql += "            sql += \"    " + tabelaNome + " \\n\";\n";

                classeSql += "            sql += \"WHERE\\n\";\n";

                classeSql += "            sql += \"    " + chavePrimariaLista.FirstOrDefault().NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Id + \"\\n\";\n";
            }

            classeSql += "            return sql; \n";

            classeSql += "        } \n\n";

            classeSql += "        private string PrepararInativacaoSql(Entidade." + namespaceNome + "." + entidadeNome + " " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ") \n        { \n";

            classeSql += "            var sql = string.Empty; \n\n";

            if (tabelaItem.ColunaLista.Where(x => x.NomeOriginal.Equals("REGISTRO_SITUACAO_ID")).FirstOrDefault() != null)
            {
                classeSql += "            sql += \"UPDATE \\n\";\n";

                classeSql += "            sql += \"    " + tabelaNome + "\\n\";\n";

                classeSql += "            sql += \"SET\\n\";\n";

                classeSql += "            sql += \"    REGISTRO_SITUACAO_ID = 2\\n\";\n";

                classeSql += "            sql += \"WHERE\\n\";\n";

                classeSql += "            sql += \"    " + chavePrimariaLista.FirstOrDefault().NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Id + \"\\n\";\n";
            }
            else
            {
                classeSql += "            sql += \"DELETE \\n\";\n";

                classeSql += "            sql += \"    *\\n\";\n";

                classeSql += "            sql += \"FROM\\n\";\n";

                classeSql += "            sql += \"    " + tabelaNome + " \\n\";\n";

                classeSql += "            sql += \"WHERE\\n\";\n";

                classeSql += "            sql += \"    " + chavePrimariaLista.FirstOrDefault().NomeOriginal + " = \" + " + entidadeNome.Substring(0, 1).ToLower() + entidadeNome[1..].ToString() + ".Id + \"\\n\";\n";
            }

            classeSql += "            return sql; \n";

            classeSql += "        } \n\n";

            classeSql += "        #endregion \n";

            classeSql += "\n\t\t#region Métodos Específicos do Banco\n\n";

            classeSql += "\t\tprivate string ObterUltimoItemInseridoSql()\n\t\t{\n";

            classeSql += "\t\t\tvar sql = this.PrepararSelecaoSql();\n\n";

            classeSql += "\t\t\tsql += \"WHERE \\n\";\n\n";

            var tabelaColunaAutoIncrementoLista = tabelaItem.ColunaLista
                .Where(x => x.AutoIncremental)
                .ToList();

            classeSql += "\t\t\tvar databaseItem = new Nemag.Database.DatabaseItem();\n\n";

            classeSql += "\t\t\tswitch (databaseItem.DatabaseTipoId)\n";

            classeSql += "\t\t\t{\n";

            classeSql += "\t\t\t\tcase Nemag.Database.Base.DATABASE_TIPO_ID.MSSQL:\n";

            for (int i = 0; i < tabelaColunaAutoIncrementoLista.Count; i++)
            {
                var tabelaColunaAutoIncrementoItem = tabelaColunaAutoIncrementoLista[i];

                if (i > 0)
                    classeSql += "\t\t\t\t\tsql += \"    AND ";
                else
                    classeSql += "\t\t\t\t\tsql += \"    ";

                classeSql += "A." + tabelaColunaAutoIncrementoItem.NomeOriginal + " = SCOPE_IDENTITY()\\n\";\n\n";
            }

            classeSql += "\t\t\t\t\tbreak;\n\n";


            classeSql += "\t\t\t\tcase Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:\n";

            for (int i = 0; i < tabelaColunaAutoIncrementoLista.Count; i++)
            {
                var tabelaColunaAutoIncrementoItem = tabelaColunaAutoIncrementoLista[i];

                if (i > 0)
                    classeSql += "\t\t\t\t\tsql += \"    AND ";
                else
                    classeSql += "\t\t\t\t\tsql += \"    ";

                classeSql += "A." + tabelaColunaAutoIncrementoItem.NomeOriginal + " = LAST_INSERT_ID()\\n\";\n\n";
            }

            classeSql += "\t\t\t\t\tbreak;\n";

            classeSql += "\t\t\t}\n\n";

            classeSql += "\t\t\treturn sql;\n";

            classeSql += "\t\t}\n\n";

            classeSql += "\t\t#endregion\n";

            classeSql += "    }\n";

            classeSql += "}\n";

            return classeSql;
        }

        public string ObterRegistroInicialLista(List<TabelaItem> _tabelaLista)
        {
            var databaseItem = new DatabaseItem();

            var sql = string.Empty;

            var tabelaLista = _tabelaLista
                .Where(x => new List<string>
                {
                    "REGISTRO_SITUACAO_TB",
                    "PESSOA_TB",
                    "LOGIN_TB",
                    "LOGIN_GRUPO_TB",
                    "LOGIN_PERFIL_TB",
                    "LOGIN_ATRIBUICAO_TB",
                    "PESSOA_CONTATO_TIPO_TB",
                    "PESSOA_DOCUMENTO_TIPO_TB",
                    "MENU_TB",
                    "MENU_PERMISSAO_LOGIN_GRUPO_TB",
                    "MENU_PERMISSAO_LOGIN_PERFIL_TB",
                    "MENU_PERMISSAO_LOGIN_TB",
                    "MOEDA_TB",
                    "PAIS_TB",
                    "NAVIO_TIPO_TB",
                    "PROFORMA_TIPO_TB",
                    "PROFORMA_SITUACAO_TB",
                    "PROFORMA_PRIORIDADE_TB",
                    "PROFORMA_ARQUIVO_CATEGORIA_TB",
                    "APONTAMENTO_TIPO_TB",
                    "APONTAMENTO_SITUACAO_TB",
                    "DESPESA_CATEGORIA_TB"
                }.Contains(x.Nome))
                .OrderBy(x => x.RelacionamentoNivel)
                .ThenBy(x => x.Nome)
                .ToList();

            for (int i = 0; i < tabelaLista.Count; i++)
            {
                var tabelaItem = tabelaLista[i];

                if (tabelaItem.Nome.Equals("PESSOA_TB"))
                {
                    sql += "INSERT INTO PESSOA_TB (NOME) VALUES ('Administrador');\n";

                    continue;
                }

                if (tabelaItem.Nome.Equals("LOGIN_TB"))
                {
                    sql += "INSERT INTO LOGIN_TB (PESSOA_ID, USUARIO, SENHA) VALUES (1, 'admin', '21232f297a57a5a743894a0e4a801fc3');\n";

                    continue;
                }

                var tabelaColunaLista = tabelaItem.ColunaLista
                    .Where(x => !x.ChavePrimaria && string.IsNullOrEmpty(x.ValorPadrao) && !x.NomeOriginal.Equals("DATA_ALTERACAO"))
                    .ToList();

                var dataTable = databaseItem.ExecutarRetornandoDataTable("SELECT * FROM " + tabelaItem.Nome);

                for (int b = 0; b < dataTable.Rows.Count; b++)
                {
                    sql += "INSERT INTO " + tabelaItem.Nome + " (";

                    for (int z = 0; z < tabelaColunaLista.Count; z++)
                    {
                        var tabelaColunaItem = tabelaColunaLista[z];

                        sql += tabelaColunaItem.NomeOriginal;

                        sql += ", ";
                    }

                    sql = sql[0..^2];

                    sql += ") VALUES (";

                    for (int c = 0; c < tabelaColunaLista.Count; c++)
                    {
                        var tabelaColunaItem = tabelaColunaLista[c];

                        if (string.IsNullOrEmpty(dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString()) && tabelaColunaItem.Nulavel)
                        {
                            sql += "NULL, ";

                            continue;
                        }

                        sql += tabelaColunaItem.Tipo.ToLower() switch
                        {
                            "int" => dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString() + ", ",
                            "smalldatetime" or "datetime" or "timestamp" => "'" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString()) + "', ",
                            _ => "'" + dataTable.Rows[b][tabelaColunaItem.NomeOriginal].ToString() + "', ",
                        };
                    }

                    sql = sql[0..^2];

                    sql += ");\n";
                }
            }

            return sql;
        }

        protected string IdentificarTabelaColunaTipoNome(ColunaItem tabelaColunaItem)
        {
            return tabelaColunaItem.Tipo.ToLower() switch
            {
                "decimal" or "numeric" => "decimal",
                "int" => "int",
                "bigint" => "long",
                "datetime" or "smalldatetime" => "DateTime",
                _ => "string",
            };
        }

        public int IdentificarTabelaRelacionamentoNivel(TabelaItem tabelaItem, List<TabelaItem> tabelaLista)
        {
            var tabelaRelacionamentoNivelMaior = 0;

            for (int i = 0; i < tabelaItem.RelacionamentoLista.Count; i++)
            {
                var tabelaRelacionamentoNivelAtual = 0;

                var tabelaRelacionamentoItem = tabelaItem.RelacionamentoLista[i];

                if (tabelaRelacionamentoItem.TabelaNome.Equals(tabelaRelacionamentoItem.TabelaReferenciaNome))
                {
                    tabelaRelacionamentoNivelAtual = 1;

                    continue;
                }

                if (tabelaItem.Nome.Equals(tabelaRelacionamentoItem.TabelaReferenciaNome))
                {
                    tabelaRelacionamentoNivelAtual = 1;

                    continue;
                }

                var tabelaInferiorItem = tabelaLista
                    .Where(x => x.Nome.Equals(tabelaRelacionamentoItem.TabelaReferenciaNome))
                    .FirstOrDefault();

                if (tabelaInferiorItem?.RelacionamentoLista?.Count > 0)
                    tabelaRelacionamentoNivelAtual = IdentificarTabelaRelacionamentoNivel(tabelaInferiorItem, tabelaLista) + 1;
                else
                    tabelaRelacionamentoNivelAtual = 1;

                if (tabelaRelacionamentoNivelAtual > tabelaRelacionamentoNivelMaior)
                    tabelaRelacionamentoNivelMaior = tabelaRelacionamentoNivelAtual;
            }

            return tabelaRelacionamentoNivelMaior;
        }

        private List<ColunaItem> IdentificarTabelaColunaReferenciaEntidadeListaPorTabelaItem(TabelaItem tabelaAtualItem, List<TabelaItem> tabelaReferenciaLista, List<TabelaItem> tabelaSuperiorLista)
        {
            var tabelaColunaOrganizadaLista = new List<ColunaItem>();

            if (tabelaSuperiorLista == null)
                tabelaSuperiorLista = new List<TabelaItem>();

            var tabelaItem = tabelaAtualItem.Clone<TabelaItem>();

            tabelaItem
                .RelacionamentoLista
                .Where(x => !new List<string> { "REGISTRO_SITUACAO_ID", "REGISTRO_LOGIN_ID" }.Contains(x.ColunaNome))
                .ToList()
                .ForEach(x =>
                {
                    var tabelaRelacionamentoItem = x.Clone<Entidade.Tabela.Relacionamento.RelacionamentoItem>();

                    var tabelaEncontradaItem = tabelaReferenciaLista
                        .Where(t => t.Nome.Equals(tabelaRelacionamentoItem.TabelaReferenciaNome) && t.Id.Equals(0))
                        .Select((item, index) => new
                        {
                            Item = item,
                            Index = index
                        })
                        .FirstOrDefault();

                    var tabelaReferenciaItem = tabelaEncontradaItem?.Item;

                    if (tabelaReferenciaItem == null)
                        return;

                    tabelaReferenciaItem.Id = -1;

                    tabelaReferenciaItem = tabelaReferenciaItem.Clone<TabelaItem>();

                    tabelaReferenciaItem.RelacionamentoLista = tabelaReferenciaItem.RelacionamentoLista
                        .Select(c =>
                        {
                            var tabelaReferenciaRelacionamentoItem = c.Clone<Entidade.Tabela.Relacionamento.RelacionamentoItem>();

                            if (tabelaReferenciaRelacionamentoItem.ColunaNome.Equals("PESSOA_ID"))
                                tabelaReferenciaRelacionamentoItem.ColunaNome = tabelaRelacionamentoItem.ColunaNome;

                            return tabelaReferenciaRelacionamentoItem;
                        })
                        .ToList();

                    if (!tabelaReferenciaItem.Indice.Equals(0))
                    {
                        tabelaRelacionamentoItem.TabelaReferenciaNome = tabelaRelacionamentoItem.ColunaNome[0..^2] + "TB";

                        tabelaReferenciaItem.Nome = tabelaRelacionamentoItem.ColunaNome[0..^2] + "TB";

                        tabelaSuperiorLista.Add(tabelaReferenciaItem);
                    }

                    var tabelaColunaNomeCompleto = IdentificarTabelaColunaNomeCompleto(tabelaSuperiorLista);

                    if (tabelaReferenciaItem.Nome.Equals("PESSOA_TB"))
                        tabelaColunaNomeCompleto = string.Empty;

                    var tabelaColunaOrganizadaNome = tabelaColunaNomeCompleto;

                    var tabelaColunaOrganizadaRecursivaLista = IdentificarTabelaColunaReferenciaEntidadeListaPorTabelaItem(tabelaReferenciaItem, tabelaReferenciaLista, tabelaSuperiorLista);

                    tabelaSuperiorLista.Remove(tabelaReferenciaItem);

                    tabelaColunaOrganizadaLista.AddRange(tabelaColunaOrganizadaRecursivaLista);

                    if (tabelaReferenciaItem.Nome.Equals("LOGIN_TB"))
                        return;

                    var tabelaReferenciaColunaLista = tabelaReferenciaItem.ColunaLista
                        .Where(c => !c.ChaveEstrangeira)
                        .Where(c => !new List<string> { "DATA_INCLUSAO", "DATA_ALTERACAO" }.Contains(c.NomeOriginal))
                        .Select(c =>
                        {
                            var colunaItem = c.Clone<ColunaItem>();

                            var tabelaColunaNomeCompleto = string.Empty;

                            var tabelaColunaAlias = tabelaColunaOrganizadaNome;

                            if (colunaItem.ChavePrimaria && !colunaItem.NomeOriginal.Equals("PESSOA_ID"))
                                tabelaColunaAlias += "ID";
                            else
                                tabelaColunaAlias += colunaItem.NomeOriginal;

                            colunaItem.NomeOriginal = tabelaColunaAlias;

                            colunaItem.NomeExibicao = tabelaColunaAlias;

                            return colunaItem;
                        })
                        .ToList();

                    tabelaColunaOrganizadaLista.AddRange(tabelaReferenciaColunaLista);
                });

            return tabelaColunaOrganizadaLista;
        }

        private List<ColunaItem> IdentificarTabelaColunaReferenciaPersistenciaListaPorTabelaItem(TabelaItem tabelaAtualItem, List<TabelaItem> tabelaReferenciaLista, List<TabelaItem> tabelaSuperiorLista)
        {
            var tabelaColunaOrganizadaLista = new List<ColunaItem>();

            if (tabelaSuperiorLista == null)
                tabelaSuperiorLista = new List<TabelaItem>();

            var tabelaItem = tabelaAtualItem.Clone<TabelaItem>();

            tabelaItem
                .RelacionamentoLista
                .Where(x => !new List<string> { "REGISTRO_SITUACAO_ID", "REGISTRO_LOGIN_ID" }.Contains(x.ColunaNome))
                .ToList()
                .ForEach(x =>
                {
                    var tabelaRelacionamentoItem = x.Clone<Entidade.Tabela.Relacionamento.RelacionamentoItem>();

                    var tabelaEncontradaItem = tabelaReferenciaLista
                        .Where(t => t.Nome.Equals(tabelaRelacionamentoItem.TabelaReferenciaNome) && t.Id.Equals(0))
                        .Select((item, index) => new
                        {
                            Item = item,
                            Index = index
                        })
                        .FirstOrDefault();

                    var tabelaReferenciaItem = tabelaEncontradaItem?.Item;

                    if (tabelaReferenciaItem == null)
                        return;

                    tabelaReferenciaItem.Id = -1;

                    tabelaReferenciaItem = tabelaReferenciaItem.Clone<TabelaItem>();

                    tabelaReferenciaItem.RelacionamentoLista = tabelaReferenciaItem.RelacionamentoLista
                        .Select(c =>
                        {
                            var tabelaReferenciaRelacionamentoItem = c.Clone<Entidade.Tabela.Relacionamento.RelacionamentoItem>();

                            if (tabelaReferenciaRelacionamentoItem.ColunaNome.Equals("PESSOA_ID"))
                                tabelaReferenciaRelacionamentoItem.ColunaNome = tabelaRelacionamentoItem.ColunaNome;

                            return tabelaReferenciaRelacionamentoItem;
                        })
                        .ToList();

                    if (!tabelaReferenciaItem.Indice.Equals(0))
                    {
                        tabelaRelacionamentoItem.TabelaReferenciaNome = tabelaRelacionamentoItem.ColunaNome[0..^2] + "TB";

                        tabelaReferenciaItem.Nome = tabelaRelacionamentoItem.ColunaNome[0..^2] + "TB";

                        tabelaSuperiorLista.Add(tabelaReferenciaItem);
                    }

                    var tabelaColunaNomeCompleto = IdentificarTabelaColunaNomeCompleto(tabelaSuperiorLista);

                    if (tabelaReferenciaItem.Nome.Equals("PESSOA_TB"))
                        tabelaColunaNomeCompleto = string.Empty;

                    var tabelaColunaOrganizadaNome = tabelaColunaNomeCompleto;

                    var tabelaColunaOrganizadaRecursivaLista = IdentificarTabelaColunaReferenciaPersistenciaListaPorTabelaItem(tabelaReferenciaItem, tabelaReferenciaLista, tabelaSuperiorLista);

                    tabelaSuperiorLista.Remove(tabelaReferenciaItem);

                    tabelaColunaOrganizadaLista.AddRange(tabelaColunaOrganizadaRecursivaLista);

                    if (tabelaReferenciaItem.Nome.Equals("LOGIN_TB"))
                        return;

                    if (tabelaReferenciaItem.Nome.Equals("PORTO_TB"))
                        "".ToString();

                    var tabelaReferenciaIdentificadorChar = ObterTabelaIdentificador(tabelaReferenciaItem.Indice);

                    var tabelaReferenciaItemColunaLista = tabelaReferenciaItem.ColunaLista
                        .Where(c => !c.ChaveEstrangeira)
                        .Where(c => !new List<string> { "DATA_INCLUSAO", "DATA_ALTERACAO" }.Contains(c.NomeOriginal))
                        .Where(c => !x.ColunaNome.Equals(c.NomeOriginal) || c.ChavePrimaria)
                        .Select(c =>
                        {
                            var colunaItem = c.Clone<ColunaItem>();

                            var tabelaColunaNomeCompleto = string.Empty;

                            var tabelaColunaAlias = tabelaColunaOrganizadaNome;

                            if (colunaItem.ChavePrimaria && !colunaItem.NomeOriginal.Equals("PESSOA_ID"))
                                tabelaColunaAlias += "ID";
                            else
                                tabelaColunaAlias += colunaItem.NomeOriginal;

                            colunaItem.NomeOriginal = tabelaReferenciaIdentificadorChar + "." + colunaItem.NomeOriginal + " AS " + tabelaColunaAlias;

                            colunaItem.NomeExibicao = tabelaColunaAlias;

                            return colunaItem;
                        })
                        .ToList();

                    tabelaColunaOrganizadaLista.AddRange(tabelaReferenciaItemColunaLista);
                });

            return tabelaColunaOrganizadaLista;
        }

        private string MontarClassePersistenciaSelectReferenciaSql(TabelaItem tabelaItem, List<TabelaItem> tabelaReferenciaLista)
        {
            var classeSql = string.Empty;

            var tabelaIdentificadorChar = ObterTabelaIdentificador(tabelaItem.Indice);

            tabelaItem
                .RelacionamentoLista
                .Where(x => tabelaReferenciaLista.Select(c => c.Nome).Contains(x.TabelaReferenciaNome) && !new List<string> { "REGISTRO_LOGIN_ID" }.Contains(x.ColunaNome))
                .ToList()
                .ForEach(x =>
                {
                    var tabelaReferenciaItem = tabelaReferenciaLista
                        .Where(c => c.Nome.Equals(x.TabelaReferenciaNome) && c.Id.Equals(0))
                        .FirstOrDefault();

                    if (tabelaReferenciaItem == null)
                        return;

                    var tabelaReferenciaIdentificadorChar = ObterTabelaIdentificador(tabelaReferenciaItem.Indice);

                    tabelaReferenciaItem.Id = -1;

                    if (x.ColunaReferenciaNome.Equals("PESSOA_ID"))
                        x.ColunaNome = x.ColunaReferenciaNome;

                    var tabelaColunaItem = tabelaItem
                        .ColunaLista
                        .Where(c => c.NomeOriginal.Equals(x.ColunaNome))
                        .FirstOrDefault();

                    var tabelaColunaReferenciaItem = tabelaReferenciaItem
                        .ColunaLista
                        .Where(c => c.NomeOriginal.Equals(x.ColunaReferenciaNome))
                        .FirstOrDefault();

                    classeSql += "            sql += \"    ";

                    if (tabelaColunaItem.Nulavel)
                    {
                        classeSql += "LEFT ";

                        tabelaReferenciaItem
                            .ColunaLista
                            .Where(c => c.ChaveEstrangeira)
                            .ToList()
                            .ForEach(c => c.Nulavel = true);
                    }
                    else
                        classeSql += "INNER ";

                    classeSql += "JOIN " + tabelaReferenciaItem.Nome + " " + tabelaReferenciaIdentificadorChar + " ON " + tabelaReferenciaIdentificadorChar + "." + x.ColunaReferenciaNome + " = " + tabelaIdentificadorChar + "." + x.ColunaNome + "\\n\";\n";

                    classeSql += MontarClassePersistenciaSelectReferenciaSql(tabelaReferenciaItem, tabelaReferenciaLista);
                });

            return classeSql;
        }

        private string ObterTabelaIdentificador(int tabelaIndice)
        {
            var identificadorChar = "A" + (tabelaIndice.Equals(0) ? string.Empty : tabelaIndice);

            return identificadorChar;
        }

        private string IdentificarTabelaColunaNomeCompleto(List<TabelaItem> tabelaSuperiorLista)
        {
            var tabelaColunaNomeCompleto = string.Empty;

            if (tabelaSuperiorLista == null || tabelaSuperiorLista.Count.Equals(0))
                return tabelaColunaNomeCompleto;

            tabelaSuperiorLista = tabelaSuperiorLista
                .Select((tabelaSuperiorItem, tabelaSuperiorIndex) =>
                {
                    //if (tabelaSuperiorIndex > 0 && tabelaSuperiorLista[tabelaSuperiorIndex - 1].Nome[0..^2].StartsWith(tabelaSuperiorItem.Nome[0..^2]) && !tabelaSuperiorLista[tabelaSuperiorIndex - 1].Nome[0..^2].Equals(tabelaSuperiorItem.Nome[0..^2]))
                    //    return tabelaSuperiorItem;

                    if ((tabelaSuperiorLista.Count - 1) > tabelaSuperiorIndex && tabelaSuperiorLista[tabelaSuperiorIndex + 1].Nome[0..^2].StartsWith(tabelaSuperiorItem.Nome[0..^2]))
                        return tabelaSuperiorItem;

                    tabelaColunaNomeCompleto += tabelaSuperiorItem.Nome[0..^2];

                    return tabelaSuperiorItem;
                })
                .ToList();

            return tabelaColunaNomeCompleto;
        }
    }
}
