namespace HttpServer.models;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string About { get; set; }
    public int Age { get; set; }

    public Employee(string name, string surname, string about, int age)
    {
        Id = new Guid();
        Name = name;
        Surname = surname;
        About = about;
        Age = age;
    }
}