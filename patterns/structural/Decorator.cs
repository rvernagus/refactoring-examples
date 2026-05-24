
IDataSource source = new FileDataSource("data.txt");
source.WriteData("Hello, World!");
Console.WriteLine($"Content: {source.ReadData()}\n");

source = new EncryptionDecorator(source);
source.WriteData("Hello, World!");
Console.WriteLine($"Content: {source.ReadData()}\n");

source = new CompressionDecorator(source);
source.WriteData("Hello, World!");
Console.WriteLine($"Content: {source.ReadData()}\n");

// Component Interface
public interface IDataSource
{
    void WriteData(string data);

    string ReadData();
}

// Concrete Component
public class FileDataSource(string filename) : IDataSource
{
    private string _fileContent = "";

    public void WriteData(string data)
    {
        Console.WriteLine($"  Writing data:\n    File: {filename}\n    Data: {data}");
        _fileContent = data;
    }

    public string ReadData()
    {
        Console.WriteLine($"  Reading data:\n    File: {filename}\n    Data: {_fileContent}");
        return _fileContent;
    }
}

// Base Decorator
public abstract class DataSourceDecorator(IDataSource wrappee) : IDataSource
{
    public virtual void WriteData(string data)
    {
        wrappee.WriteData(data);
    }

    public virtual string ReadData()
    {
        return wrappee.ReadData();
    }
}

// Concrete Decorator
public class EncryptionDecorator(IDataSource wrappee) : DataSourceDecorator(wrappee)
{
    private string Encrypt(string data)
    {
        // Code to encrypt data
        return "encrypted " + data;
    }

    private string Decrypt(string data)
    {
        // Code to decrypt data
        return data.Replace("encrypted ", "");
    }

    public override void WriteData(string data)
    {
        var encryptedData = Encrypt(data);
        base.WriteData(encryptedData);
    }

    public override string ReadData()
    {
        var data = base.ReadData();
        return Decrypt(data);
    }
}

// Concrete Decorator
public class CompressionDecorator(IDataSource wrappee) : DataSourceDecorator(wrappee)
{
    private string Compress(string data)
    {
        // Code to compress data
        return "compressed " + data;
    }

    private string Decompress(string data)
    {
        // Code to decompress data
        return data.Replace("compressed ", "");
    }
    public override void WriteData(string data)
    {
        var compressedData = Compress(data);
        base.WriteData(compressedData);
    }

    public override string ReadData()
    {
        var data = base.ReadData();
        return Decompress(data);
    }
}
