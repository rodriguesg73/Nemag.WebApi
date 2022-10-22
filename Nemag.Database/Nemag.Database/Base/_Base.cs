using Nemag.Database.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace Nemag.Database.Base
{
    public enum DATABASE_TIPO_ID
    {
        DEFAULT = 0,
        MSSQL = 1,
        ORACLE = 2,
        MYSQL = 3
    }

    public class _BaseItem
    {
        protected string ConnectionString { get; set; }

        private static IConfigurationRoot _configuration { get; set; }

        private static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var configurationBuilder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

                    _configuration = configurationBuilder.Build();
                }

                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }

        private static string CallingAssemblyName
        {
            get
            {
                return new StackTrace()
                    .GetFrames()
                    .Select(f => f.GetMethod()?.ReflectedType?.Assembly.GetName().Name)
                    .Distinct()
                    .Where(x => !string.IsNullOrEmpty(x) && !x.Equals(Assembly.GetExecutingAssembly().GetName().Name))
                    .FirstOrDefault();
            }
        }

        public DATABASE_TIPO_ID DatabaseTipoId { get; set; }

        public string DatabaseUrl { get; set; }

        public string DatabaseNome { get; set; }

        public string DatabaseUsuario { get; set; }

        public string DatabaseSenha { get; set; }

        public _BaseItem()
        {
            DatabaseTipoId = (DATABASE_TIPO_ID)Enum.Parse(typeof(DATABASE_TIPO_ID), ObterConfiguracaoValor("Database:Tipo:Id"));

            DatabaseUrl = ObterConfiguracaoValor("Database:Url");

            DatabaseNome = ObterConfiguracaoValor("Database:Nome");

            DatabaseUsuario = ObterConfiguracaoValor("Database:Usuario");

            DatabaseSenha = ObterConfiguracaoValor("Database:Senha");
        }

        public virtual string ObterQueryString(DATABASE_TIPO_ID databaseTipoId)
        {
            var connectionString = ObterQueryString(databaseTipoId, DatabaseUrl, DatabaseNome, DatabaseUsuario, DatabaseSenha);

            return connectionString;
        }

        public virtual string ObterQueryString(DATABASE_TIPO_ID databaseTipoId, string databaseUrl, string databaseNome, string databaseUsuario, string databaseSenha)
        {
            var connectionString = string.Empty;

            switch (databaseTipoId)
            {
                case DATABASE_TIPO_ID.MSSQL:
                    connectionString = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};Integrated Security=False;MultipleActiveResultSets=True;Connection Timeout=3000;", databaseUrl, databaseNome, databaseUsuario, databaseSenha);
                    break;

                case DATABASE_TIPO_ID.ORACLE:
                    connectionString = string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521)))(CONNECT_DATA=(SID={1})(SERVER=DEDICATED))); User Id={2}; Password={3}", databaseUrl, databaseNome, databaseUsuario, databaseSenha);
                    break;

                case DATABASE_TIPO_ID.MYSQL:
                    connectionString = string.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};SslMode=none;connect timeout=288000", databaseUrl, databaseNome, databaseUsuario, databaseSenha);
                    break;
            }

            return connectionString;
        }

        public List<T> ParseDataTable<T>(System.Data.DataTable dataTable, Dictionary<string, string> dicionario) where T : new()
        {
            var lista = new List<T>();

            if (dataTable != null)
                foreach (var dataRow in dataTable.Rows)
                {
                    var item = ParseDataRow<T>((System.Data.DataRow)dataRow, dicionario);

                    lista.Add(item);
                }

            return lista;
        }

        public List<T> CarregarLista<T>(string sql) where T : new()
        {
            var database = new DatabaseItem();

            return CarregarLista<T>(database, sql, null);
        }

        public List<T> CarregarLista<T>(IDatabase database, string sql, Dictionary<string, string> dicionario) where T : new()
        {
            var dataTable = database.ExecutarRetornandoDataTable(sql);

            return ParseDataTable<T>(dataTable, dicionario);
        }

        public T ParseDataRow<T>(System.Data.DataRow dataRow, Dictionary<string, string> dicionario) where T : new()
        {
            var entidade = new T();

            if (dicionario == null)
                dicionario = new Dictionary<string, string>();

            var propriedadeLista = entidade.GetType().GetProperties();

            foreach (var propriedadeItem in propriedadeLista)
            {
                var dicionarioItem = dicionario
                    .Where(x => x.Key.Equals(propriedadeItem.Name))
                    .FirstOrDefault();

                if (propriedadeItem != null && propriedadeItem.CanWrite)
                {
                    var propriedadeNome = string.Empty;

                    if (dicionarioItem.Value == null)
                        propriedadeNome = propriedadeItem.Name;
                    else
                        propriedadeNome = dicionarioItem.Value;

                    if (!dataRow.Table.Columns.Contains(propriedadeNome))
                        continue;

                    if (dataRow.IsNull(propriedadeNome) && !propriedadeItem.PropertyType.Name.Equals("String"))
                        continue;

                    var propriedadeValor = Convert.ChangeType(dataRow[propriedadeNome], propriedadeItem.PropertyType);

                    if (propriedadeItem.PropertyType.Name.Equals("String") && string.IsNullOrEmpty((string)propriedadeValor))
                        propriedadeValor = string.Empty;

                    propriedadeItem.SetValue(entidade, propriedadeValor, null);
                }
            }

            return entidade;
        }

        public T CarregarItem<T>(string sql) where T : new()
        {
            var database = new DatabaseItem();

            return CarregarItem<T>(database, sql, null);
        }

        public T CarregarItem<T>(IDatabase database, string sql, Dictionary<string, string> dicionario) where T : new()
        {
            var dataTable = database.ExecutarRetornandoDataTable(sql);

            var lista = ParseDataTable<T>(dataTable, dicionario);

            return lista.FirstOrDefault();
        }

        private string ObterConfiguracaoValor(string key)
        {
            var keySplit = key
                .Split(':')
                .ToList();

            for (int i = 0; i < keySplit.Count; i++)
            {
                var keyLista = keySplit.ToList();

                keyLista.Insert(i, CallingAssemblyName);

                var keyJoin = string.Join(':', keyLista.ToArray());

                var value = Configuration[keyJoin]?.ToString();

                if (!string.IsNullOrEmpty(value))
                    return value;
            }

            return Configuration[key]?.ToString();
        }
    }
}
