using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nemag.Database
{
    public class DatabaseItem : Base._BaseItem, Interface.IDatabase, ICloneable, IDisposable
    {
        private Interface.IDatabase _databaseItem = null;

        public bool ManterConectado { get { return _databaseItem.ManterConectado; } set { _databaseItem.ManterConectado = value; } }

        public void Conectar()
        {
            _databaseItem.Conectar();
        }

        public void Desconectar()
        {
            _databaseItem.Desconectar();
        }

        public DatabaseItem() : this(false) { }

        public DatabaseItem(bool manterConectado) : this(manterConectado, Base.DATABASE_TIPO_ID.DEFAULT, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public DatabaseItem(bool manterConectado, Base.DATABASE_TIPO_ID databaseTipoId, string databaseUrl, string databaseNome, string databaseUsuario, string databaseSenha)
        {
            if (!databaseTipoId.Equals(Base.DATABASE_TIPO_ID.DEFAULT))
                DatabaseTipoId = databaseTipoId;

            if (!string.IsNullOrEmpty(databaseUrl))
                DatabaseUrl = databaseUrl;

            if (!string.IsNullOrEmpty(databaseNome))
                DatabaseNome = databaseNome;

            if (!string.IsNullOrEmpty(databaseUsuario))
                DatabaseUsuario = databaseUsuario;

            if (!string.IsNullOrEmpty(databaseSenha))
                DatabaseSenha = databaseSenha;

            var connectionString = ObterQueryString(DatabaseTipoId);

            switch (DatabaseTipoId)
            {
                case Base.DATABASE_TIPO_ID.MSSQL:
                    _databaseItem = new SqlServer.SqlServerDatabase(manterConectado, connectionString);
                    break;

                case Base.DATABASE_TIPO_ID.ORACLE:
                    _databaseItem = new Oracle.OracleDatabase(connectionString);
                    break;

                case Base.DATABASE_TIPO_ID.MYSQL:
                    _databaseItem = new MySql.MySqlDatabase(manterConectado, connectionString);
                    break;
            }
        }

        public DataSet ExecutarRetornandoDataSet(string sql, bool manterConectado)
        {
            _databaseItem.ManterConectado = manterConectado;

            var dataSet = ExecutarRetornandoDataSet(sql);

            _databaseItem.ManterConectado = !manterConectado;

            return dataSet;
        }

        public DataTable ExecutarRetornandoDataTable(string sql, bool manterConectado)
        {
            _databaseItem.ManterConectado = manterConectado;

            var dataTable = _databaseItem.ExecutarRetornandoDataTable(sql);

            _databaseItem.ManterConectado = !manterConectado;

            return dataTable;
        }

        public void ExecutarSemRetorno(string sql, bool manterConectado)
        {
            _databaseItem.ManterConectado = manterConectado;

            ExecutarSemRetorno(sql);

            _databaseItem.ManterConectado = !manterConectado;
        }

        public DataSet ExecutarRetornandoDataSet(string sql)
        {
            try
            {
                var dataSet = _databaseItem.ExecutarRetornandoDataSet(sql);

                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ExecutarRetornandoDataTable(string sql)
        {
            try
            {
                var dataTable = _databaseItem.ExecutarRetornandoDataTable(sql);

                return dataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ExecutarSemRetorno(string sql)
        {
            lock (_databaseItem)
            {
                try
                {
                    _databaseItem.ExecutarSemRetorno(sql);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        public async Task<DataSet> ExecutarRetornandoDataSetAsync(string sql)
        {
            return await _databaseItem.ExecutarRetornandoDataSetAsync(sql);
        }

        public async Task<DataTable> ExecutarRetornandoDataTableAsync(string sql)
        {
            return await _databaseItem.ExecutarRetornandoDataTableAsync(sql);
        }

        public async Task ExecutarSemRetornoAsync(string sql)
        {
            await _databaseItem.ExecutarSemRetornoAsync(sql);
        }

        public async Task RealizarBackup(string diretorioUrl)
        {
            await _databaseItem.RealizarBackup(diretorioUrl);
        }

        public T Clone<T>() where T : new()
        {
            var itemOrigem = MemberwiseClone();

            var itemDestino = new T();

            var propriedadeOrigemLista = itemOrigem.GetType().GetProperties();

            var propriedadeDestinoLista = itemDestino.GetType().GetProperties();

            foreach (var propriedadeOrigemItem in propriedadeOrigemLista)
            {
                var propriedadeDestinoItem = propriedadeDestinoLista
                    .Where(x => x.Name.Equals(propriedadeOrigemItem.Name))
                    .FirstOrDefault();

                if (propriedadeDestinoItem == null)
                    continue;

                var valor = propriedadeOrigemItem.GetValue(itemOrigem, null);

                if (!propriedadeDestinoItem.PropertyType.Name.Equals(propriedadeOrigemItem.PropertyType.Name))
                    valor = Convert.ChangeType(valor, propriedadeDestinoItem.PropertyType);

                propriedadeDestinoItem.SetValue(itemDestino, valor, null);
            }

            return itemDestino;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Dispose()
        {
            _databaseItem = null;
        }
    }
}
