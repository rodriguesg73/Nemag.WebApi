using System;
using System.Linq;

namespace Nemag.Core.Entidade
{
    public partial class _BaseItem : IDisposable, ICloneable
    {
        public int Id { get; set; }

        public string GuidPrimario { get; set; }

        public string GuidEstrangeiro { get; set; }

        public void Dispose()
        { }

        public object Clone()
        {
            return MemberwiseClone();
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
    }
}
