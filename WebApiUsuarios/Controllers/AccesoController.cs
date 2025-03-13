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
        private readonly BdpruebaContext _bdpruebaContext;
        private readonly Utilidades _utilidades;
        public AccesoController(BdpruebaContext bdpruebaContext, Utilidades utilidades)
        {
            _bdpruebaContext = bdpruebaContext;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(usuarioDTO objeto)
        {

            var modeloUsuario = new Usuario
            {
                Nombre = objeto.Nombre,
                Correo = objeto.Correo,
                Clave = _utilidades.encriptarSHA256(objeto.Clave),
            };

            await _bdpruebaContext.AddAsync(modeloUsuario);
            await _bdpruebaContext.SaveChangesAsync();

            if (modeloUsuario.IdUsuario != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _bdpruebaContext.Usuarios.Where(u =>
            u.Correo == objeto.Correo &&
            u.Clave == _utilidades.encriptarSHA256(objeto.Clave)).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
            }

        }
    }
}
