using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        // public ReportingStructure GetReportingStructureById(string id)
        // {
        //     ReportingStructure empReporting = new ReportingStructure();
        //     var empList = _employeeContext.Employees.ToList();
        //     empReporting.employee = empList.SingleOrDefault(e => e.EmployeeId == id);
        //     if (empReporting.employee.DirectReports != null)
        //     {
        //         empReporting.numberOfReports = empReporting.employee.DirectReports.Count;
        //         foreach (var item in empReporting.employee.DirectReports)
        //         {
        //             if (item.DirectReports != null && item.DirectReports.Count > 0)
        //             {
        //                 empReporting.numberOfReports = empReporting.numberOfReports + item.DirectReports.Count;
        //                 continue;
        //             }
        //         }
        //     }
        //     return empReporting;
        // }


        public ReportingStructure GetReportingStructureById(string id)
        {
            ReportingStructure empReporting = new ReportingStructure();
            var empList = _employeeContext.Employees.ToList();
            empReporting.employee = empList.SingleOrDefault(e => e.EmployeeId == id);
            empReporting.numberOfReports = callDepth(empReporting.employee.DirectReports);
            return empReporting;
        }


        public int callDepth(List<Employee> directReports)
        {
            if (directReports == null)
            {
                return 0;
            }
            int depth = directReports.Count;
            foreach (var item in directReports)
            {
                //if (item.DirectReports != null)
                //{
                depth += callDepth(item.DirectReports);
                //}

            }
            return depth;
        }
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
