using System.Net;
using HttpServer.managers;
using HttpServer.providers;

namespace HttpServer.controllers;

public class ImageController : BaseController
{
    private readonly FileManager _fileManager;

    public ImageController(FileManager fileManager)
    {
        _fileManager = fileManager;
    }


    private byte[] Get(string filePath)
        => _fileManager.GetImage(filePath);

    public override byte[] TryToProcessRequest(HttpListenerContext context, string filename)
    {
        if (filename.Contains("ico"))
        {
            string filePath = Path.Combine(RootDirectoryProvider.GetRootDirectoryPath(), filename);
            return Get(filePath);
        }

        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, filename);
        
        return Array.Empty<byte>();
    }
}