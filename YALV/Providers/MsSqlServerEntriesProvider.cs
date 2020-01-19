namespace YALV.Providers
{
    using System.Data;
    using System.Data.SqlClient;

    public class MsSqlServerEntriesProvider : AbstractEntriesProviderBase
    {
        protected override IDbConnection CreateConnection(string dataSource)
        {
            return new SqlConnection(dataSource);
        }
    }
}