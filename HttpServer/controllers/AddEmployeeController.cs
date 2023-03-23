using System.Net;
using System.Text;
using HttpServer.dto;
using HttpServer.providers;
using HttpServer.services;
using HttpServer.viewModels;

namespace HttpServer.controllers;

public class AddEmployeeController : BaseController
{
    private readonly HtmlBuilderService<ResponseDto<IndexViewModel>> _commonHtmlBuilder;

    public AddEmployeeController(HtmlBuilderService<ResponseDto<IndexViewModel>> commonHtmlBuilder)
    {
        _commonHtmlBuilder = commonHtmlBuilder;
    }

    public override byte[] TryToProcessRequest(HttpListenerContext context, string filename)
    {
        if (filename.Contains("addEmployee.html"))
        {
            ResponseDto<IndexViewModel> responseDto = new ResponseDto<IndexViewModel>{Result = new IndexViewModel()};
            string filePath = Path.Combine(RootDirectoryProvider.GetRootDirectoryPath(), $"views/{filename}");
            var htmlString = _commonHtmlBuilder.BuildHtml(filename,filePath, responseDto);
            return Encoding.UTF8.GetBytes(htmlString);
        }
        
        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, filename);
        
        return Array.Empty<byte>();
    }
}