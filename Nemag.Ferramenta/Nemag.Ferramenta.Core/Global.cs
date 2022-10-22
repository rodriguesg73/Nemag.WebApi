using System.Collections.Concurrent;

namespace Nemag.Ferramenta.Core
{
    public static class Global
    {
        public static ConcurrentDictionary<int, Database.DatabaseItem> DatabaseLista = new();
    }
}
