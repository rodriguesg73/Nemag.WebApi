using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace Nemag.Database.MySql
{
    public class MySqlDatabase : Base._BaseItem, Interface.IDatabase
    {
        public bool ManterConectado { get; set; }

        private MySqlConnection Connection { get; set; }

        public MySqlDatabase() : this(false, string.Empty) { }

        public MySqlDatabase(bool manterConectado) : this(manterConectado, string.Empty) { }

        public MySqlDatabase(string connectionString) : this(false, connectionString) { }

        public MySqlDatabase(bool manterConectado, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                ConnectionString = ObterQueryString(Base.DATABASE_TIPO_ID.MYSQL);
            else
                ConnectionString = connectionString;

            ManterConectado = manterConectado;
        }

        public virtual System.Data.DataSet ExecutarRetornandoDataSet(string sql)
        {
            var dataSet = new System.Data.DataSet();

            Conectar();

            using (var mySqlCommand = new MySqlCommand(sql, Connection) { CommandTimeout = 3600 })

            using (var mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand))
            {
                mySqlDataAdapter.Fill(dataSet);
            }

            Desconectar();

            return dataSet;
        }

        public virtual System.Data.DataTable ExecutarRetornandoDataTable(string sql)
        {
            var dataSet = ExecutarRetornandoDataSet(sql);

            if (dataSet.Tables.Count > 0)
                return dataSet.Tables[0].Copy();

            return null;
        }

        public virtual void ExecutarSemRetorno(string sql)
        {
            Conectar();

            var mySqlCommand = new MySqlCommand(sql, Connection) { CommandTimeout = 3600 };

            mySqlCommand.ExecuteNonQuery();

            Desconectar();
        }

        public virtual async Task<System.Data.DataSet> ExecutarRetornandoDataSetAsync(string sql)
        {
            return await Task.Run(() => ExecutarRetornandoDataSet(sql));
        }

        public virtual async Task<System.Data.DataTable> ExecutarRetornandoDataTableAsync(string sql)
        {
            return await Task.Run(() => ExecutarRetornandoDataTable(sql));
        }

        public virtual async Task ExecutarSemRetornoAsync(string sql)
        {
            await Task.Run(() => ExecutarSemRetorno(sql));
        }

        public void Conectar()
        {
            if (Connection == null)
                Connection = new MySqlConnection(ConnectionString);

            if (Connection.State.Equals(System.Data.ConnectionState.Broken))
                Connection.Close();

            if (Connection.State.Equals(System.Data.ConnectionState.Closed))
                Connection.Open();
        }

        public void Desconectar()
        {
            if (!ManterConectado)
            {
                Connection.Close();

                Connection.Dispose();

                Connection = null;
            }
        }

        public virtual async Task RealizarBackup(string diretorioUrl)
        {
            await Task.Run(() => throw new NotImplementedException());
        }
    }
}
