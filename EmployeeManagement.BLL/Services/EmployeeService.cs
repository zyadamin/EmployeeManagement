using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BLL.Models;
using EmployeeManagement.Domian.Entity;
using EmployeeManagement.Domian.Enum;
using SimpleApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Add(EmployeeCreateDTO employeeCreateDTO)
        {
            Employee employee = new Employee()
            {

                Email = employeeCreateDTO.Email,
                Name = employeeCreateDTO.Name,
                Projects = employeeCreateDTO.Projects.Select(dto => new Project
                {
                    Name = dto.Name,
                    Description = dto.Description,
                }).ToList()
            };
            var result = _unitOfWork.Employee.Add(employee);

            AuditLog auditLog = new AuditLog()
            {
                id = 1,
                ActionType = ActionType.Add,
                EmployeeId = 1,
                NewData = JsonSerializer.Serialize(employee)
            };

            _unitOfWork.Audit.Add(auditLog);
            _unitOfWork.SaveChanges();

            return result.Id;
        }

        public void Delete(int id)
        {
            Employee employee = _unitOfWork.Employee.GetByIdIncludeProject(id);
            foreach (var project in employee.Projects)
            {

                _unitOfWork.Project.Delete(project);
            }
            AuditLog auditLog = new AuditLog()
            {
                id = employee.Id,
                ActionType = ActionType.Delete,
                EmployeeId = 1,
            };

            _unitOfWork.Employee.Delete(employee);

            _unitOfWork.Audit.Add(auditLog);

            _unitOfWork.SaveChanges();
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

        public int Update(EmployeeDetailsDTO entity)
        {
            var oldEmployee = _unitOfWork.Employee.GetByIdIncludeProject(entity.Id);

            AuditLog auditLog = new AuditLog()
            {
                ActionType = ActionType.Update,
                EmployeeId = oldEmployee.Id,
                OldData = JsonSerializer.Serialize(oldEmployee)
            };

            oldEmployee.Name = entity.Name;
            oldEmployee.Email = entity.Email;

            foreach (var project in entity.Projects)
            {
                var oldProject = oldEmployee.Projects.FirstOrDefault(x => x.Id == project.Id);

                if (oldProject != null)
                {
                    oldProject.Name = project.Name;
                    oldProject.Description = project.Description;
                }
                else
                {
                    Project newProject = new Project()
                    {
                        Name = project.Name,
                        Description = project.Description
                    };

                    oldEmployee.Projects.Add(newProject);
                }
            }

            List<Project> DeletedProjects = new List<Project>();
            foreach (var project in oldEmployee.Projects)
            {
                if (!entity.Projects.Any(x => x.Id == project.Id))
                {

                    DeletedProjects.Add(project);
                }
            }

            auditLog.NewData = JsonSerializer.Serialize(oldEmployee);

            var result = _unitOfWork.Employee.Update(oldEmployee);
            foreach (var project in DeletedProjects)
            {
                _unitOfWork.Project.Delete(project);
            }

            _unitOfWork.Audit.Add(auditLog);


            _unitOfWork.Complete();
            return result.Id;
        }
    }
}
