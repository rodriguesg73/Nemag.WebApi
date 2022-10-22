using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nemag.Core.Negocio
{
    // CRIAR UMA ROTINA DE VALIDAÇÃO DE DIVERGENCIA DE LISTAS, EXCLUINDO AS PROPRIEDADES DO ITEM_BASE 
    //  USAR LINQ PARA FAZER UM WHERE LINKANDO COM TODAS AS PROPRIEDADES DA CLASSE... SE RETORNAR 0, A LISTA É IGUAL (valido nas duas vias)
    // 28/01/2021 - tirar sarro pela demora na implementação

    public partial class _BaseItem : IDisposable, ICloneable
    {
        #region Propriedades 

        protected static string ApiUrl => ObterConfiguracaoValor("Api:Url");

        protected static string ApiToken => ObterConfiguracaoValor("Api:Token");

        #endregion

        #region Métodos Publicos

        public bool ValidarDivergenciaItem<T>(T entidadeOrigem, T entidadeDestino)
        {
            return ValidarDivergenciaItem(entidadeOrigem, entidadeDestino, new List<string>());
        }

        public bool ValidarDivergenciaItem<T>(T entidadeOrigem, T entidadeDestino, List<string> excecao)
        {
            var propriedadeOrigemLista = entidadeOrigem.GetType().GetProperties();

            var propriedadeDestinoLista = entidadeDestino.GetType().GetProperties();

            foreach (var propriedadeOrigemItem in propriedadeOrigemLista)
            {
                if (excecao.Contains(propriedadeOrigemItem.Name))
                    continue;

                var propriedadeDestinoItem = propriedadeDestinoLista.Where(x => x.Name.Equals(propriedadeOrigemItem.Name)).FirstOrDefault();

                var propriedadeOrigemValor = propriedadeOrigemItem.GetValue(entidadeOrigem, null);

                propriedadeOrigemValor = Convert.ChangeType(propriedadeOrigemValor, propriedadeOrigemItem.PropertyType);

                if (propriedadeOrigemItem.PropertyType.Name.ToLower().Equals("string") && string.IsNullOrEmpty((string)propriedadeOrigemValor))
                    propriedadeOrigemValor = "";

                var propriedadeDestinoValor = propriedadeDestinoItem.GetValue(entidadeDestino, null);

                propriedadeDestinoValor = Convert.ChangeType(propriedadeDestinoValor, propriedadeDestinoItem.PropertyType);

                if (propriedadeDestinoItem.PropertyType.Name.ToLower().Equals("string") && string.IsNullOrEmpty((string)propriedadeDestinoValor))
                    propriedadeDestinoValor = "";

                if (!propriedadeOrigemValor.Equals(propriedadeDestinoValor))
                    return true;
            }

            return false;
        }

        public void Dispose()
        { }

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Métodos Protegidos

        public List<E> CarregarListaPorFiltroItem<E, F>(F filtroItem) where E : new() where F : new()
        {
            if (filtroItem == null)
                filtroItem = new();

            var filtroType = filtroItem.GetType();

            var persistenciaType = Type.GetType(filtroType.Namespace.Replace(".Filtro.", ".Persistencia.") + "." + filtroType.Name);

            if (persistenciaType == null)
                throw new Exception("Persistência inexistente");

            var persistencia = Activator.CreateInstance(persistenciaType);

            var methodInfoLista = persistencia.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            var methodInfoPrepararSelecaoSqlItem = methodInfoLista
                .Where(x => x.Name.Equals("PrepararSelecaoSql") && x.GetParameters().Count().Equals(0))
                .FirstOrDefault();

            var sqlSelectOriginal = methodInfoPrepararSelecaoSqlItem?.Invoke(persistencia, Array.Empty<object>()).ToString();

            var methodInfoComplementarSelecaoSqlItem = methodInfoLista
                .Where(x => x.Name.Equals("ComplementarSelecaoSql") && x.GetParameters().Count().Equals(1))
                .FirstOrDefault();

            var sqlSelectComplementado = methodInfoComplementarSelecaoSqlItem?.Invoke(persistencia, new List<string>() { sqlSelectOriginal }.ToArray()).ToString();

            if (string.IsNullOrEmpty(sqlSelectComplementado))
                sqlSelectComplementado = sqlSelectOriginal;

            var sqlCompleto = PrepararFiltroSql(sqlSelectComplementado, filtroItem);

            var databaseItem = new Nemag.Database.DatabaseItem();

            var persistenciaItem = new Persistencia._BaseItem();

            return persistenciaItem.CarregarLista<E>(databaseItem, sqlCompleto);
        }

        protected internal static string ObterConfiguracaoValor(string key)
        {
            var keySplit = key
                .Split(':')
                .ToList();

            for (int i = 0; i < keySplit.Count; i++)
            {
                var keyLista = keySplit.ToList();

                var keyJoin = String.Join(':', keyLista.ToArray());

                var value = Configuration[keyJoin]?.ToString();

                if (!string.IsNullOrEmpty(value))
                    return value;
            }

            return Configuration[key]?.ToString();
        }

        protected internal static string DiretorioAtualUrl
        {
            get
            {
                var localPath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;

                var directoryName = Path.GetDirectoryName(localPath);

                //var codeBaseUrl = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                return directoryName;
            }
        }

        #endregion

        #region Métodos Privados

        private static IConfigurationRoot _configuration { get; set; }

        private static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var configurationBuilder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);

                    _configuration = configurationBuilder.Build();
                }

                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }

        private string PrepararFiltroSql<F>(string sqlSelect, F filtroItem)
        {
            var filtroPropriedadeLista = filtroItem
                .GetType()
                .GetProperties()
                .ToList();

            var colunaSelectLista = sqlSelect
                .Split('\n')
                .Select(x => x.Replace("\t", string.Empty).Replace(",", string.Empty).Trim())
                .ToList();

            colunaSelectLista.RemoveAt(0);

            colunaSelectLista.RemoveRange(colunaSelectLista.LastIndexOf("FROM"), colunaSelectLista.Count - colunaSelectLista.LastIndexOf("FROM"));

            var sqlWhere = "";

            filtroPropriedadeLista
                .ForEach(x =>
                {
                    var propertyValue = x.GetValue(filtroItem);

                    if (propertyValue == null)
                        return;

                    var propertyName = x.Name;

                    var propertyType = x.PropertyType;

                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        propertyType = propertyType.GetGenericArguments()[0];

                    if (propertyType.Equals(typeof(int)) && propertyValue.Equals(0)) // pra compensar os IDs que são zero, ou seja, não estão sendo fornecidos no filtro
                        return;

                    var propriedadeValor = Convert.ChangeType(propertyValue, propertyType);

                    if (propertyType.Equals(typeof(DateTime)) && propriedadeValor.Equals(DateTime.MinValue))
                        return;

                    var tabelaColunaNome = Auxiliar.Util.ConverterClasseNomeParaDatabaseNome(propertyName);

                    var colunaSelectItem = colunaSelectLista
                        .Where(x => x.IndexOf(tabelaColunaNome) >= 0)
                        .FirstOrDefault();

                    if (colunaSelectItem.IndexOf(" ") >= 0)
                        tabelaColunaNome = colunaSelectItem.Split(" ")[0];
                    else
                        tabelaColunaNome = colunaSelectItem;

                    sqlWhere += tabelaColunaNome;

                    if (propertyName.EndsWith("Inicial") && !propertyType.Equals(typeof(string)))
                        sqlWhere += " >= ";
                    else if (propertyName.EndsWith("Final") && !propertyType.Equals(typeof(string)))
                        sqlWhere += " <= ";
                    else if (propertyType.Equals(typeof(string)))
                        sqlWhere += " LIKE ";
                    else
                        sqlWhere += " = ";

                    if (propertyType.Equals(typeof(DateTime)))
                        sqlWhere += "'" + string.Format("{0:yyyy-MM-dd}", propriedadeValor) + "'\n";
                    else if (propertyType.Equals(typeof(string)))
                        sqlWhere += "'%" + propriedadeValor.ToString() + "%'\n";
                    else if (propertyType.Equals(typeof(decimal)) || propertyType.Equals(typeof(double)) || propertyType.Equals(typeof(float)))
                        sqlWhere += propriedadeValor.ToString().Replace(",", ".") + "\n";
                    else
                        sqlWhere += propriedadeValor.ToString() + "\n";
                });

            sqlWhere += "A.REGISTRO_SITUACAO_ID <> 3\n";

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = sqlWhere[0..^1];

                sqlWhere = sqlWhere.Replace("\n", "\nAND ");

                sqlWhere = "WHERE\n\t" + sqlWhere;
            }

            return sqlSelect + sqlWhere;
        }

        #endregion
    }
}
