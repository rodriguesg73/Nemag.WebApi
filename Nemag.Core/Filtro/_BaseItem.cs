using System;

namespace Nemag.Core.Filtro
{
    public partial class _BaseItem : IDisposable, ICloneable
    {
        public int? Id { get; set; }

        public void Dispose() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
