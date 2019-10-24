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
    public class departmentController : ControllerBase
    {
        private readonly ILogger _logger;
        public departmentController(ILogger<departmentController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                DepartmentViewModel vmodel = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartment = vmodel.GetAll();
                return Ok(allDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something wrong
            }
        }
    }
}