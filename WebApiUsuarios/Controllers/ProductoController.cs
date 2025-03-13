using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsuarios.Custom;
using WebApiUsuarios.Models;
using WebApiUsuarios.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly BdpruebaContext _bdpruebaContext;

        public ProductoController(BdpruebaContext bdpruebaContext) {
            _bdpruebaContext = bdpruebaContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista() 
        {
            var lista = await _bdpruebaContext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }
    }
}
