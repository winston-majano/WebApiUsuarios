using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsuarios.Custom;
using WebApiUsuarios.Models;
using WebApiUsuarios.Models.DTOS;
using Microsoft.AspNetCore.Authorization;

namespace WebApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]  //con esto decimos que el usuario ingresara sin estar autorizado 
    public class AccesoController : ControllerBase
    {
       // private redonly
        public AccesoController()
        {
            
        }

    }
}
