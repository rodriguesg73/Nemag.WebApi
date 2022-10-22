using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Nemag.Database.Oracle
{
    public class OracleDatabase : Base._BaseItem, Interface.IDatabase
    {
        public bool ManterConectado { get; set; }

        public OracleDatabase()
            : this(Base.DATABASE_TIPO_ID.ORACLE)
        { }

        public OracleDatabase(Base.DATABASE_TIPO_ID databaseTipoId)
        {
            ConnectionString = ObterQueryString(databaseTipoId);
        }

        public OracleDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Conectar()
        {
            
        }

        public void Desconectar()
        {

        }

        public virtual DataSet ExecutarRetornandoDataSet(string sql)
        {
            var dataSet = new DataSet();

            using (var mySqlConnection = new OracleConnection(ConnectionString))

            using (var mySqlCommand = new OracleCommand(sql, mySqlConnection))

            using (var mySqlDataAdapter = new OracleDataAdapter(mySqlCommand))
            {
                mySqlDataAdapter.Fill(dataSet);
            }

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
            _ = new DataSet();

            using var mySqlConnection = new OracleConnection(ConnectionString);
            
            mySqlConnection.Open();

            using var mySqlCommand = new OracleCommand(sql, mySqlConnection);
            
            mySqlCommand.ExecuteNonQuery();
        }

        public virtual async Task<DataSet> ExecutarRetornandoDataSetAsync(string sql)
        {
            return await Task.Run(() => ExecutarRetornandoDataSet(sql));
        }

        public virtual async Task<DataTable> ExecutarRetornandoDataTableAsync(string sql)
        {
            return await Task.Run(() => ExecutarRetornandoDataTable(sql));
        }

        public virtual async Task ExecutarSemRetornoAsync(string sql)
        {
            await Task.Run(() => ExecutarSemRetorno(sql));
        }

        public virtual async Task RealizarBackup(string diretorioUrl)
        {
            await Task.Run(() => throw new NotImplementedException());
        }
    }
}
