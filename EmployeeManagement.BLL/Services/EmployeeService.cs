using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BLL.Models;
using EmployeeManagement.Domian.Entity;
using EmployeeManagement.Domian.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IUnitOfWork = EmployeeManagement.BLL.Interfaces.IUnitOfWork;

namespace EmployeeManagement.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly Audit.BLL.Interfaces.IAuditService _auditService;

        public EmployeeService(IUnitOfWork unitOfWork, Audit.BLL.Interfaces.IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
        }

        public int Add(EmployeeCreateDTO employeeCreateDTO)
        {
            var transaction = _unitOfWork.BeginTransaction();
            int retValue = 0;
            try
            {

                Employee employee = new Employee()
                {

                    Email = employeeCreateDTO.Email,
                    Name = employeeCreateDTO.Name,
                    Projects = employeeCreateDTO.Projects.Select(dto => new Project
                    {
                        StartDate = DateTime.Parse(dto.StartDate),
                        EndDate = string.IsNullOrEmpty(dto.EndDate) ? null : DateTime.Parse(dto.EndDate),
                        Name = dto.Name,
                        Description = dto.Description,
                    }).ToList()
                };
                var result = _unitOfWork.Employee.Add(employee);
                _unitOfWork.Complete();

                string newData = JsonSerializer.Serialize(result);

                _auditService.UseTransaction(transaction);
                _auditService.LogAudit((int)ActionType.Add, result.Id, DateTime.Now, null, newData);

                transaction.Commit();

                retValue = result.Id;

            }
            catch
            {
                transaction.Rollback();
            }
            return retValue;
        }

        public int Update(EmployeeDetailsDTO entity)
        {
            var transaction = _unitOfWork.BeginTransaction();
            int retValue = 0;
            try
            {
                var employee = _unitOfWork.Employee.GetByIdIncludeProject(entity.Id);
                string oldData = JsonSerializer.Serialize(employee);

                employee.Name = entity.Name;
                employee.Email = entity.Email;

                //update projects
                foreach (var project in entity.Projects)
                {
                    var oldProject = employee.Projects.FirstOrDefault(x => x.Id == project.Id);

                    if (oldProject != null)
                    {
                        oldProject.Name = project.Name;
                        oldProject.Description = project.Description;
                        oldProject.StartDate = DateTime.Parse(project.StartDate);
                        oldProject.EndDate = string.IsNullOrEmpty(project.EndDate) ? null : DateTime.Parse(project.EndDate);
                    }
                    else
                    {
                        Project newProject = new Project()
                        {
                            Name = project.Name,
                            Description = project.Description,
                            StartDate = DateTime.Parse(project.StartDate),
                            EndDate = string.IsNullOrEmpty(project.EndDate) ? null : DateTime.Parse(project.EndDate),
                        };

                        employee.Projects.Add(newProject);
                    }
                }

                string newData = JsonSerializer.Serialize(employee);

                //delete projects 
                foreach (var project in employee.Projects)
                {
                    if (!entity.Projects.Any(x => x.Id == project.Id))
                    {
                        _unitOfWork.Project.Delete(project);
                    }
                }

                var result = _unitOfWork.Employee.Update(employee);
                _unitOfWork.Complete();

                retValue = result.Id;

                _auditService.UseTransaction(transaction);
                _auditService.LogAudit((int)ActionType.Update, result.Id, DateTime.Now, oldData, newData);


                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            return retValue;
        }

        public void Delete(int id)
        {
            var transaction = _unitOfWork.BeginTransaction();
            try
            {
                Employee employee = _unitOfWork.Employee.GetByIdIncludeProject(id);
                foreach (var project in employee.Projects)
                {
                    _unitOfWork.Project.Delete(project);
                }
                _unitOfWork.Employee.Delete(employee);
                _unitOfWork.Complete();

                _auditService.UseTransaction(transaction);
                _auditService.LogAudit((int)ActionType.Delete, id, DateTime.Now, null, null);


                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
        }

        public EmployeeDetailsDTO GetById(int id)
        {
            Employee employee = _unitOfWork.Employee.GetByIdIncludeProject(id);
            EmployeeDetailsDTO employeeDetailsDTO = new EmployeeDetailsDTO()
            {
                Id = employee.Id,
                Email = employee.Email,
                Name = employee.Name,
                Projects = employee.Projects.Select(emp => new ProjectDTO
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Description = emp.Description,
                    StartDate = emp.StartDate.ToShortDateString(),
                    EndDate = emp.EndDate?.ToShortDateString()
                }).ToList(),

            };
            return employeeDetailsDTO;
        }

        public List<EmployeeViewDTO> List(int start, int size)
        {
            List<EmployeeViewDTO> employeeList = new List<EmployeeViewDTO>();

            var employees = _unitOfWork.Employee.ListEmployee(start, size).
                Select(emp => new EmployeeViewDTO()
                {

                    Id = emp.Id,
                    Name = emp.Name,
                    Email = emp.Email,

                }).ToList();

            if (employees != null)
            {

                employeeList = employees;
            }

            return employeeList;
        }

        public bool CheckUniqueEmail(string email,int? id)
        {
            return _unitOfWork.Employee.CheckUniqueEmail(email,id);
        }

        public List<EmployeeModel> GetEmployees()
        {
            return _unitOfWork.Employee.GetEmployees().Select(Employee => new EmployeeModel()
            {
                Id = Employee.Id,
                Name = Employee.Name,
            }).ToList();
        }
    }
}
