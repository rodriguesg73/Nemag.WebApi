using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Nemag.Core.Persistencia
{
    public partial class _BaseItem
    {
        #region Métodos Protegidos

        internal List<T> ParseDataTable<T>(DataTable dataTable) where T : new()
        {
            var lista = new List<T>();

            if (dataTable != null)
                foreach (var dataRow in dataTable.Rows)
                {
                    var item = ParseDataRow<T>((DataRow)dataRow);

                    lista.Add(item);
                }

            return lista;
        }

        internal T ParseDataRow<T>(DataRow dataRow) where T : new()
        {
            var entidade = new T();

            var entidadePropriedadeLista = entidade.GetType().GetProperties();

            dataRow.Table.Columns
                .Cast<DataColumn>()
                .Select(x => x.ColumnName)
                .ToList()
                .ForEach(x =>
                {
                    var namespaceNomeParcial = entidade.GetType().Namespace
                        .Split('.')
                        .ToList();

                    namespaceNomeParcial.RemoveRange(0, namespaceNomeParcial.IndexOf("Entidade") + 1);

                    var tabelaNomeParcial = string.Join("_", namespaceNomeParcial.ToArray()).ToUpper() + "_";

                    tabelaNomeParcial = tabelaNomeParcial.Replace("_HISTORICO_", "_H_");

                    var tabelaColunaNome = x;

                    if (tabelaColunaNome.Replace(tabelaNomeParcial, string.Empty).Equals("ID"))
                        tabelaNomeParcial = tabelaColunaNome.Replace(tabelaNomeParcial, string.Empty);
                    else
                        tabelaNomeParcial = tabelaColunaNome;

                    var entidadePropriedadeNome = Auxiliar.Util.ConverterDatabaseNomeParaClasseNome(tabelaNomeParcial);

                    if (entidadePropriedadeNome.Equals("PaisId"))
                        "".ToString();

                    var entidadePropriedadeItem = entidadePropriedadeLista
                        .Where(x => x.Name.Equals(entidadePropriedadeNome))
                        .FirstOrDefault();

                    if (entidadePropriedadeItem == null || !entidadePropriedadeItem.CanWrite)
                        return;

                    if (dataRow.IsNull(tabelaColunaNome) && !entidadePropriedadeItem.PropertyType.Name.Equals("String"))
                        return;

                    var propriedadeValor = Convert.ChangeType(dataRow[tabelaColunaNome], entidadePropriedadeItem.PropertyType);

                    if (entidadePropriedadeItem.PropertyType.Name.Equals("String") && string.IsNullOrEmpty((string)propriedadeValor))
                        propriedadeValor = string.Empty;

                    entidadePropriedadeItem.SetValue(entidade, propriedadeValor, null);
                });

            return entidade;
        }

        internal List<T> CarregarLista<T>(Nemag.Database.Interface.IDatabase database, string sql) where T : new()
        {
            var dataTable = database.ExecutarRetornandoDataTable(sql);

            return ParseDataTable<T>(dataTable);
        }

        internal T CarregarItem<T>(Nemag.Database.Interface.IDatabase database, string sql) where T : new()
        {
            var dataTable = database.ExecutarRetornandoDataTable(sql);

            var lista = ParseDataTable<T>(dataTable);

            return lista.FirstOrDefault();
        }

        internal DataTable CarregarDataTable(Nemag.Database.Interface.IDatabase database, string sql)
        {
            return database.ExecutarRetornandoDataTable(sql);
        }

        #endregion
    }
}
