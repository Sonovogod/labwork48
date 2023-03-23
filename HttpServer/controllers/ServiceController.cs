using System.Net;
using System.Text;
using HttpServer.managers;
using HttpServer.providers;

namespace HttpServer.controllers;

public class ServiceController : BaseController 
{
    private readonly FileManager _fileManager;

    public ServiceController(FileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public override byte[] TryToProcessRequest(HttpListenerContext context, string filename)
    {
        if (filename.Contains(".css"))
        {
            string filePath = Path.Combine(RootDirectoryProvider.GetRootDirectoryPath(), filename);
            var content = _fileManager.GetContent(filePath);

            return Encoding.UTF8.GetBytes(content);
        }
        
        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, filename);
        
        return Array.Empty<byte>();
    }
}