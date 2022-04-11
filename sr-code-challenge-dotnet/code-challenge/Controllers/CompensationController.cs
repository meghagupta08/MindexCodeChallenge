using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompenationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger,
        ICompenationService compenationService)
        {
            _logger = logger;
            _compensationService = compenationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for ' {compensation.employeeId}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.compensationID }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        // [HttpPut("{id}")]
        // public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        // {
        //     _logger.LogDebug($"Recieved employee update request for '{id}'");

        //     var existingEmployee = _employeeService.GetById(id);
        //     if (existingEmployee == null)
        //         return NotFound();

        //     _employeeService.Replace(existingEmployee, newEmployee);

        //     return Ok(newEmployee);
        // }
    }
}
