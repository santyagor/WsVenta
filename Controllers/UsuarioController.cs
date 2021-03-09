using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsVenta.Models.Request;
using WsVenta.Models.Response;
using WsVenta.Services;

namespace WsVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();
            var userResponse = _userService.Auth(model);
            if(userResponse ==null)
            {
                respuesta.Mensaje = "usuario o contraseña incorrecta";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userResponse;
                
            return Ok(respuesta);
        }
    }
}
