using Microsoft.Data.SqlClient;
using MyEnumerableIntegerRangeLibrary.Properties;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Builds the connection string from the settings and checks, whether the database is reachable.
    /// Shared by <see cref="MyIntegerSetFactory"/> and <see cref="MyAnimalSetFactory"/>.
    /// </summary>
    internal static class MyDatabaseSettings
    {
        internal static string GetConnectionString(string applicationName)
        {
            Settings settings = new Settings();

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = settings.DatabaseServer, // server address
                InitialCatalog = settings.DatabaseName, // database name
                IntegratedSecurity = false, // server auth(false)/win auth(true)
                MultipleActiveResultSets = false, // activate/deactivate MARS
                PersistSecurityInfo = true, // hide login credentials
                UserID = settings.DatabaseUser, // user name
                Password = settings.DatabasePassword, // password
                ApplicationName = applicationName,
                Encrypt = false,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }

        /// <summary>
        /// Opens and closes a connection once
        /// </summary>
        /// <returns>true, if the database is reachable</returns>
        internal static bool TestDatabaseConnection(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
