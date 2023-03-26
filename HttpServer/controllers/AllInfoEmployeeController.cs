using System.Net;
using System.Text;
using HttpServer.dto;
using HttpServer.providers;
using HttpServer.services;
using HttpServer.validators;
using HttpServer.viewModels;

namespace HttpServer.controllers;

public class AllInfoEmployeeController : BaseController
{
    private readonly HtmlBuilderService<ResponseDto<EmployeeViewModel>> _employeeHtmlBuilder;
    private readonly EmployeeService _employeeService;

    public AllInfoEmployeeController(HtmlBuilderService<ResponseDto<EmployeeViewModel>> employeeHtmlBuilder, EmployeeService employeeService)
    {
        _employeeHtmlBuilder = employeeHtmlBuilder;
        _employeeService = employeeService;
    }

    public override byte[] TryToProcessRequest(HttpListenerContext context, string fileName)
    {
        string filePath = Path.Combine(RootDirectoryProvider.GetRootDirectoryPath(), $"views/{fileName}");
        ResponseDto<List<EmployeeViewModel>> response = _employeeService.GetAll();
        
        if (fileName.Contains("employee.html") && context.Request.HttpMethod.Equals("GET"))
        {
            var query = context.Request.QueryString;
            if (query.HasKeys())
            {
                var id = query["Id"];
                EmployeeViewModel? employee = response.Result.Find(x => x.Id.Equals(id));
                ResponseDto<EmployeeViewModel> responseDto = new ResponseDto<EmployeeViewModel> {Result = employee};
                var htmlString = _employeeHtmlBuilder.BuildHtml(fileName,filePath, responseDto);
               
                return Encoding.UTF8.GetBytes(htmlString);
            }
        }
        
        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, fileName);
        
        return Array.Empty<byte>();
    }
}