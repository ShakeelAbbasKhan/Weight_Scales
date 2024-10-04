public class ConnectionService : IConnectionService
{
    public string ConnectionString { get; private set; }
    public bool IsConnected { get; private set; }

    public void SetConnectionString(string dataSource, string databaseName, string userId, string password)
    {
        ConnectionString = $"Data Source={dataSource};Initial Catalog={databaseName};Persist Security Info=True;User ID={userId};Password={password};";
        IsConnected = true; // Assuming the connection string is valid
    }

    public void ClearConnection()
    {
        ConnectionString = null;
        IsConnected = false;
    }
}