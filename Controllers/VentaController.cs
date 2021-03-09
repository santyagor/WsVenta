using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsVenta.Models;
using WsVenta.Models.Request;
using WsVenta.Models.Response;

namespace WsVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Add(VentaRequest request)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var venta = new Ventum();
                    venta.Total = request.Total;
                    venta.Fecha = DateTime.Now;
                    venta.IdCliente = request.IdCliente;
                    db.Venta.Add(venta);
                    db.SaveChanges();

                    foreach (var modelConcepto in request.Conceptos)
                    {
                        var concepto = new Models.Concepto();
                        concepto.Cantidad = modelConcepto.Cantidad;
                        concepto.IdProducto = modelConcepto.IdProducto;
                        concepto.PrecioUnitario = modelConcepto.PrecioUnitario;
                        concepto.Importe = modelConcepto.Importe;
                        concepto.IdVenta = venta.Id;
                        db.Conceptos.Add(concepto);
                        db.SaveChanges();
                    }

                    respuesta.Exito =1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;                
            }

            return Ok(respuesta);
        }
    }
}
