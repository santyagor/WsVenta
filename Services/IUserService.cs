using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsVenta.Models.Request;
using WsVenta.Models.Response;

namespace WsVenta.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model); 
    }
}
