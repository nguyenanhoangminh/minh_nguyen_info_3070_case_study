//author : Minh Nguyen
//date : 20/10/19
//description: handle request of user send to the server
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelpdeskViewModels;

namespace Helpdesk_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private readonly ILogger _logger;
        public employeeController(ILogger<employeeController> logger)
        {
            _logger = logger;
        }

        //[HttpPut]
        //public IActionResult Put(EmployeeViewModel viewmodel)

        //[Route("/api/employees")]
        [HttpPut]
        public IActionResult PUT(EmployeeViewModel viewmodel)
        {
            try
            {
                int retVal = viewmodel.Update();
                switch (retVal)
                {
                    case 1: return Ok(new { msg = "Employee " + viewmodel.LastName + " updated!" });
                    case -1: return Ok(new { msg = "Employee " + viewmodel.LastName + " not updated!"});
                    case -2: return Ok(new { msg = "Data is stale for " + viewmodel.LastName + ", Employee not updated!" });
                    default: return Ok(new { msg = "DEFAULT Employee " + viewmodel.LastName + " not updated! Ret = " + retVal });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something wrong
            }
        }
        //[Route("/api/employees/[action]")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                EmployeeViewModel vmodel = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployee = vmodel.GetAll();
                return Ok(allEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something wrong
            }
        }
        [HttpPost]
        public IActionResult POST(EmployeeViewModel viewmodel)
        {
            try
            {
                viewmodel.Add();
                return viewmodel.Id > 1 ? Ok(new { msg = "Employee " + viewmodel.LastName + " added!" })
                    : Ok(new { msg = "Employee " + viewmodel.LastName + "not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something wrong
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel viewModel = new EmployeeViewModel();
                viewModel.Id = id;
                return viewModel.Delete() == 1 
                    ? Ok(new { msg = "Employee " + viewModel.Id + " deleted!" })
                    : Ok(new { msg = "Employee " + viewModel.Id + "not deleted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something wrong
            }
        }


        //[Route("/api/employees/[action]/{email}")]
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                EmployeeViewModel vm = new EmployeeViewModel();
                vm.Email = email;
                vm.GetByEmail();
                return Ok(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something wrong
            }
        }
    }
}