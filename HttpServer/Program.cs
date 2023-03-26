using System.Net;
using System.Text;
using HttpServer;
using HttpServer.controllers;
using HttpServer.dto;
using HttpServer.managers;
using HttpServer.providers;
using HttpServer.services;
using HttpServer.validators;
using HttpServer.viewModels;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var fileManager = new FileManager();
var listener = new HttpListener();
AddressConnectionProvider.RootDomain = "http://localhost:";
AddressConnectionProvider.Port = 8000;
AddressConnectionProvider.Address = AddressConnectionProvider.RootDomain + AddressConnectionProvider.Port + "/";

var commonHtmlBuilder = new HtmlBuilderService<ResponseDto<IndexViewModel>>();
var employeesHtmlBuilder = new HtmlBuilderService<ResponseDto<List<EmployeeViewModel>>>();
var employeeHtmlBuilder = new HtmlBuilderService<ResponseDto<EmployeeViewModel>>();
var employeeService = new EmployeeService(fileManager);

var createEmployeeValidator = new CreateEmployeeValidator();


var imageController = new ImageController(fileManager);
var homeController = new HomeController(employeeService, employeesHtmlBuilder);
var serviceController = new ServiceController(fileManager);
var addEmployeeController = new AddEmployeeController(employeesHtmlBuilder, createEmployeeValidator, employeeService);
var allInfoEmployeeController = new AllInfoEmployeeController(employeeHtmlBuilder, employeeService);

homeController.Controller = imageController;
imageController.Controller = serviceController;
serviceController.Controller = addEmployeeController;
addEmployeeController.Controller = allInfoEmployeeController;

Server server = new Server(listener, homeController);

server.Start();
