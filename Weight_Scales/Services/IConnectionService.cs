public interface IConnectionService
{
    string ConnectionString { get; }
    bool IsConnected { get; }
    void SetConnectionString(string dataSource, string databaseName, string userId, string password);
    void ClearConnection();
}
