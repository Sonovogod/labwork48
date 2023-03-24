using System.Text.Json;
using HttpServer.dto;
using HttpServer.managers;
using HttpServer.models;
using HttpServer.viewModels;

namespace HttpServer.services;

public class EmployeeService
{
    private readonly FileManager _fileManager;

    public EmployeeService(FileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public ResponseDto<List<EmployeeViewModel>> GetAll(string filePath)
    {
        var jsonString = _fileManager.GetContent(filePath);
        List<Employee>? employees = JsonSerializer.Deserialize<List<Employee>>(jsonString);
        
        ResponseDto<List<EmployeeViewModel>> responseDto = new ResponseDto<List<EmployeeViewModel>>()
        {
            Result = employees is null ? new List<EmployeeViewModel>() :
                employees.Select(x => new EmployeeViewModel
                {
                    About = x.About,
                    Age = x.Age,
                    Id = x.Id,
                    Surname = x.Surname,
                    Name = x.Name
                }).ToList()
        };

        return responseDto;
    }
}