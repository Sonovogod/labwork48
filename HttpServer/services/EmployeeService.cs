using System.Data.Common;
using System.Text.Json;
using HttpServer.dto;
using HttpServer.managers;
using HttpServer.models;
using HttpServer.providers;
using HttpServer.viewModels;

namespace HttpServer.services;

public class EmployeeService
{
    private readonly FileManager _fileManager;
    private string _pathToJson;

    public EmployeeService(FileManager fileManager)
    {
        _fileManager = fileManager;
        _pathToJson = $"{RootDirectoryProvider.GetRootDirectoryPath()}/data/employees.json";
    }

    public ResponseDto<List<EmployeeViewModel>> GetAll()
    {
        var jsonString = _fileManager.GetContent(_pathToJson);
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

    public void Create(EmployeeViewModel employeeViewModel)
    {
        var jsonString = _fileManager.GetContent(_pathToJson);
        var employees = JsonSerializer.Deserialize<List<Employee>>(jsonString);
        Employee newEmployee = new Employee()
        {
            Id = employeeViewModel.Id,
            Name = employeeViewModel.Name,
            Surname = employeeViewModel.Surname,
            About = employeeViewModel.About,
            Age = employeeViewModel.Age
        };

        employees?.Add(newEmployee);
        jsonString = JsonSerializer.Serialize(newEmployee);
        _fileManager.SaveData(jsonString, _pathToJson);
    }
}