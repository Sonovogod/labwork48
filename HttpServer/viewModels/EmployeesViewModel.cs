
namespace HttpServer.viewModels;

public class EmployeesViewModel
{
    public string Title { get; set; } = "Список сотрудников";
    public List<EmployeeViewModel> Employees { get; set; }
}