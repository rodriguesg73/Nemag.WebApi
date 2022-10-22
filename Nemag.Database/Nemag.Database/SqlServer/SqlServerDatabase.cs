using Microsoft.SqlServer.Dac;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Nemag.Database.SqlServer
{
    public class SqlServerDatabase : Base._BaseItem, Interface.IDatabase
    {
        #region Propriedades

        public bool ManterConectado { get; set; }

        private SqlConnection Connection { get; set; }

        #endregion

        #region Construtores

        public SqlServerDatabase() : this(false, string.Empty) { }

        public SqlServerDatabase(bool manterConectado) : this(manterConectado, string.Empty) { }

        public SqlServerDatabase(string connectionString) : this(false, connectionString) { }

        public SqlServerDatabase(bool manterConectado, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                ConnectionString = ObterQueryString(Base.DATABASE_TIPO_ID.MSSQL);
            else
                ConnectionString = connectionString;

            ManterConectado = manterConectado;
        }

        #endregion

        #region Métodos Públicos/Virtuais

        public void Conectar()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConnectionString);

            if (Connection.State.Equals(ConnectionState.Broken))
                Connection.Close();

            if (Connection.State.Equals(ConnectionState.Closed))
                Connection.Open();
        }

        public void Desconectar()
        {
            if (!ManterConectado && Connection != null && !Connection.State.Equals(ConnectionState.Closed))
            {
                Connection.Close();

                Connection.Dispose();

                Connection = null;
            }
        }

        public virtual DataSet ExecutarRetornandoDataSet(string sql)
        {
            var dataSet = new DataSet();

            Conectar();

            using (var sqlCommand = new SqlCommand(sql, Connection) { CommandTimeout = 120000 })

            using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
            {
                sqlDataAdapter.Fill(dataSet);
            }

            Desconectar();

            return dataSet;
        }

        public virtual DataTable ExecutarRetornandoDataTable(string sql)
        {
            var dataSet = ExecutarRetornandoDataSet(sql);

            if (dataSet.Tables.Count > 0)
                return dataSet.Tables[0].Copy();

            return null;
        }

        public virtual void ExecutarSemRetorno(string sql)
        {
            Conectar();

            var sqlCommand = new SqlCommand(sql, Connection) { CommandTimeout = 120000 };

            sqlCommand.ExecuteNonQuery();

            Desconectar();
        }

        public virtual async Task ExecutarSemRetornoAsync(string sql)
        {
            await Task.Run(() => ExecutarSemRetorno(sql));
        }

        public virtual async Task<DataSet> ExecutarRetornandoDataSetAsync(string sql)
        {
            return await Task.Run(() => ExecutarRetornandoDataSet(sql));
        }

        public virtual async Task<DataTable> ExecutarRetornandoDataTableAsync(string sql)
        {
            return await Task.Run(() => ExecutarRetornandoDataTable(sql));
        }

        public virtual async Task RealizarBackup(string diretorioUrl)
        {
            await Task.Run(() => ExportarCamadaDados(diretorioUrl, DatabaseNome));
        }

        #endregion

        #region Métodos Privados

        private string ExportarCamadaDados(string diretorioUrl, string databaseNome)
        {
            var arquivoUrl = Path.Combine(diretorioUrl, string.Format(databaseNome + ".{0:yyyyMMdd.HHmmss}.bacpac", DateTime.Now));

            var dacExportOptions = new DacExportOptions()
            {
                VerifyExtraction = true
            };

            new DacServices(ConnectionString).ExportBacpac(arquivoUrl, databaseNome, dacExportOptions, null);

            return arquivoUrl;
        }

        #endregion
    }
}
