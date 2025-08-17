using Microsoft.EntityFrameworkCore;

namespace Auth
{
    public class DBConnectionSettings
    {
        public const string CONNECTION =
           "server=127.0.0.1;" +
           "port=3309;" +
           "user=user;" +
           "password=user1234;" +
           "database=VRRPGPractice;"+
            "AllowPublicKeyRetrieval=True;" +
            "SslMode=None;";

        public static readonly MySqlServerVersion MYSQL_SERVER_VERSION = new MySqlServerVersion(new Version(8, 4, 6));
    }
}
