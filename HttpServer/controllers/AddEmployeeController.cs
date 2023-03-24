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

    public override byte[] TryToProcessRequest(HttpListenerContext context, string fileName)
    {
        if (fileName.Contains("addEmployee.html"))
        {
            ResponseDto<IndexViewModel> responseDto = new ResponseDto<IndexViewModel>{Result = new IndexViewModel()};
            string filePath = Path.Combine(RootDirectoryProvider.GetRootDirectoryPath(), $"views/{fileName}");
            var htmlString = _commonHtmlBuilder.BuildHtml(fileName,filePath, responseDto);
            return Encoding.UTF8.GetBytes(htmlString);
        }
        
        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, fileName);
        
        return Array.Empty<byte>();
    }
}