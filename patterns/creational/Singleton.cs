ClientCode();

static void ClientCode()
{
    var db1 = Database.Instance;
    db1.Query("SELECT * FROM Users");

    var db2 = Database.Instance;
    db2.Query("SELECT * FROM Orders");
}

public sealed class Database
{
    private static readonly Lazy<Database> _lazyInstance = 
        new(() => new Database(), LazyThreadSafetyMode.ExecutionAndPublication);

    private Database()
    {
        // Heavy resource/file setup goes here safely
    }

    // Clean, thread-safe, and self-contained double-checked lock replacement
    public static Database Instance => _lazyInstance.Value;

    public void Query(string sql)
    {
        Console.WriteLine($"Executing SQL: {sql}");
    }
}
