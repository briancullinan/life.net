namespace YALV.Providers
{
    using System;

    using YALV.Domain;

    public static class EntriesProviderFactory
    {
        public static AbstractEntriesProvider GetProvider(EntriesProviderType type = EntriesProviderType.Xml)
        {
            switch (type)
            {
                case EntriesProviderType.Xml:
                    return new XmlEntriesProvider();

                //case EntriesProviderType.Sqlite:
                //    return new SqliteEntriesProvider();

                case EntriesProviderType.MsSqlServer:
                    return new MsSqlServerEntriesProvider();

                default:
                    var message = String.Format("Type {0} not supported", type);
                    throw new NotImplementedException(message);
            }
        }
    }
}