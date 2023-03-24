using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using FluentValidation.Results;
using HttpServer.dto;
using HttpServer.providers;
using HttpServer.services;
using HttpServer.validators;
using HttpServer.viewModels;

namespace HttpServer.controllers;

public class AddEmployeeController : BaseController
{
    private readonly HtmlBuilderService<ResponseDto<List<EmployeeViewModel>>> _commonHtmlBuilder;
    private readonly CreateEmployeeValidator _createEmployeeValidator;
    private readonly EmployeeService _employeeService;
    public AddEmployeeController(HtmlBuilderService<ResponseDto<List<EmployeeViewModel>>> commonHtmlBuilder, CreateEmployeeValidator createEmployeeValidator, EmployeeService employeeService)
    {
        _commonHtmlBuilder = commonHtmlBuilder;
        _createEmployeeValidator = createEmployeeValidator;
        _employeeService = employeeService;
    }

    public override byte[] TryToProcessRequest(HttpListenerContext context, string fileName)
    {
        string filePath = Path.Combine(RootDirectoryProvider.GetRootDirectoryPath(), $"views/{fileName}");
        
        if (fileName.Contains("addEmployee.html") && context.Request.HttpMethod.Equals("GET"))
        {
            ResponseDto<List<EmployeeViewModel>> responseDto = new ResponseDto<List<EmployeeViewModel>> {Result = new List<EmployeeViewModel>()};
            var htmlString = _commonHtmlBuilder.BuildHtml(fileName,filePath, responseDto);
            
            return Encoding.UTF8.GetBytes(htmlString);
        }

        if (fileName.Contains("addEmployee.html") && context.Request.HttpMethod.Equals("POST"))
        {
            if (context.Request.HasEntityBody)
            {
                using StreamReader streamReader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
                var body = streamReader.ReadToEnd();
                NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(body);
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    About = nameValueCollection["About"],
                    Age = int.Parse(nameValueCollection["Age"]),
                    Surname = nameValueCollection["Surname"],
                    Name = nameValueCollection["Name"],
                    Id = Guid.NewGuid()
                };
                
                var response = Post(employeeViewModel);
                var content = _commonHtmlBuilder.BuildHtml(fileName, filePath, response);
                return Encoding.UTF8.GetBytes(content);
            }
        }
        
        if (Controller is not null) 
            return Controller.TryToProcessRequest(context, fileName);
        
        return Array.Empty<byte>();
    }

    private ResponseDto<List<EmployeeViewModel>> Post(EmployeeViewModel model)
    {
        ValidationResult? validationResult = _createEmployeeValidator.Validate(model);

        if (validationResult.IsValid)
        {
            _employeeService.Create(model);
            ResponseDto<List<EmployeeViewModel>> response = _employeeService.GetAll();
            
            return response;
        }

        ResponseDto<List<EmployeeViewModel>> viewModel = new ResponseDto<List<EmployeeViewModel>>()
        {
            Result = new List<EmployeeViewModel>(),
            Errors = validationResult.Errors
        };
        return viewModel;
    }
}