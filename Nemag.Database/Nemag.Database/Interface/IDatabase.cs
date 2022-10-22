using System.Threading.Tasks;

namespace Nemag.Database.Interface
{
    public interface IDatabase
    {
        bool ManterConectado { get; set; }

        void Conectar();

        void Desconectar();

        System.Data.DataSet ExecutarRetornandoDataSet(string sql);

        System.Data.DataTable ExecutarRetornandoDataTable(string sql);

        void ExecutarSemRetorno(string sql);

        Task<System.Data.DataSet> ExecutarRetornandoDataSetAsync(string sql);

        Task<System.Data.DataTable> ExecutarRetornandoDataTableAsync(string sql);

        Task ExecutarSemRetornoAsync(string sql);

        Task RealizarBackup(string diretorioUrl);
    }
}
