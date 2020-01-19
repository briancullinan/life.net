using System.Data;
using SQLite;

namespace YALV.Providers
{
    public class SqliteEntriesProvider : AbstractEntriesProviderBase
    {
        protected override SQLiteConnection CreateConnection(string dataSource)
        {
            return new SQLiteConnection(dataSource);
        }
    }
}