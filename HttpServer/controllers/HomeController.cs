using System.Net;
using System.Text;
using HttpServer.dto;
using HttpServer.providers;
using HttpServer.services;
using HttpServer.viewModels;

namespace HttpServer.controllers;

public class HomeController : BaseController
{
    private readonly EmployeeService _employeeService;
    private readonly HtmlBuilderService<ResponseDto<List<EmployeeViewModel>>> _commonHtmlBuilder;

    public HomeController(
        EmployeeService employeeService, 
        HtmlBuilderService<ResponseDto<List<EmployeeViewModel>>> commonHtmlBuilder)
    {
        _employeeService = employeeService;
        _commonHtmlBuilder = commonHtmlBuilder;
    }

    public override byte[] TryToProcessRequest(HttpListenerContext context, string fileName)
    {
        if (fileName.Contains("index.html"))
        {
            var htmlFilePath = $"{RootDirectoryProvider.GetRootDirectoryPath()}/views/{fileName}";
            var jsonPath = $"{RootDirectoryProvider.GetRootDirectoryPath()}/data/employees.json";
            ResponseDto<List<EmployeeViewModel>> response = _employeeService.GetAll
                (jsonPath);
            string content = _commonHtmlBuilder.BuildHtml(fileName, htmlFilePath, response);

            return Encoding.UTF8.GetBytes(content);
        }
        
        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, fileName);
        
        return Array.Empty<byte>();
    }
}