namespace HttpServer.managers;

public class FileManager
{
    public string GetContent( string filepath)
    {
        if (!File.Exists(filepath)) throw new FileNotFoundException($"Файл по пути {filepath} не найден");
        Console.WriteLine(filepath);

        return File.ReadAllText(filepath);
    }
    
    public byte[] GetImage(string filePath)
    {
        using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        byte[] bytes = new byte[1024 * 64];
        var _ = fileStream.Read(bytes);
        return bytes;
    }
}