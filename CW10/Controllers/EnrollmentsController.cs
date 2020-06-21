using cw10.DTOs;
using cw10.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        public IDbService _service;

        public EnrollmentsController(IDbService service)
        {
            _service = service;
        }
        [Route("api/promotions")]
        [HttpPost]
        public IActionResult PromoteStudent(PromoteStudentRequest psr)
        {
            var res = _service.PromoteStudent(psr);
            return Ok(res);
        }
    }
}