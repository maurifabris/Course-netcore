using Adrian.core.Config;
using Adrian.core.DbContexts;
using Adrian.core.Models.CONF;
using Adrian.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Adrian.mvc.Controllers
{

    public class ClientesController : Controller
    {
        private readonly WebDBContext db = new WebDBContext();

        //Devuelve la pagina
        public async Task<IActionResult> Index()
        {
            try
            {
                return await Task.Run<ActionResult>(() =>
                {
                    return View();
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { mensaje = "Error al ingresar a clientes. Intente mas tarde." });
            }
        }

        //Devuelve la lista de objetos en JSON
        public async Task<JsonResult> GetInfo()
        {
            var Clientes = await db.Clientes
                            .OrderBy(x => x.Nombre)
                            .Select(y => new {
                                Numeral = y.ID,
                                Nombre = y.Nombre,
                                Cuit = y.Cuit,
                                Telefono = y.Telefono,
                                ProvinciaID = y.ProvinciaID,
                                Localidad = y.Localidad,
                                Direccion = y.Direccion,
                                Observaciones
                            = y.Observaciones
                            })
                            .ToListAsync();
            return Json(Clientes);
        }

        //Actualiza la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveInfo([Bind("Nombre,Cuit,Telefono,ProvinciaID,Localidad,Direccion,Observaciones")] ClientesViewModel Clientes, int Numeral, string Mode)
        {
            ResultadoViewModel resultadoView = new ResultadoViewModel();

            if (ModelState.IsValid)
            {
                //Si estoy editando 
                if (Mode == "E")
                {
                    try
                    {
                        //Obtengo el registro de la tabla correspondiente al objeto que se esta editando
                        var ClientesDB = await db.Clientes.FirstOrDefaultAsync(m => m.ID == Numeral);

                        if (ClientesDB == null)
                        {
                            resultadoView.returnValue = ConfigAppSetting.NotFoundMessage;
                            return Json(resultadoView);
                        }

                        ClientesDB.Nombre = Clientes.Nombre;
                        ClientesDB.Cuit = Clientes.Cuit;
                        ClientesDB.Telefono = Clientes.Telefono;
                        ClientesDB.ProvinciaID = Clientes.ProvinciaID;
                        ClientesDB.Localidad = Clientes.Localidad;
                        ClientesDB.Direccion = Clientes.Direccion;
                        ClientesDB.Observaciones = Clientes.Observaciones;
                        db.Update(ClientesDB);
                        await db.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {
                        resultadoView.returnValue = ConfigAppSetting.DBErrorMessage;
                    }
                }

                //Si estoy borrando 
                if (Mode == "D")
                {
                    try
                    {
                        // Obtener el registro de la tabla correspondiente al objeto que quiero borrar
                        var ClientesDB = await db.Clientes.FirstOrDefaultAsync(m => m.ID == Numeral);

                        if (ClientesDB == null)
                        {
                            resultadoView.returnValue = ConfigAppSetting.NotFoundMessage;
                            return Json(resultadoView);
                        }

                        db.Remove(ClientesDB);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        resultadoView.returnValue = ConfigAppSetting.DBErrorMessage;
                    }
                }

                //Si estoy dando de alta
                if (Mode == "A")
                {
                    //Agrego el registro 
                    try
                    {
                        Clientes ClientesDB = new Clientes
                        {
                            Nombre = Clientes.Nombre,
                            Cuit = Clientes.Cuit,
                            Telefono = Clientes.Telefono,
                            ProvinciaID = Clientes.ProvinciaID,
                            Localidad = Clientes.Localidad,
                            Direccion = Clientes.Direccion,
                            Observaciones = Clientes.Observaciones,
                        };
                        db.Clientes.Add(ClientesDB);
                        await db.SaveChangesAsync();
                        resultadoView.NumeralID = ClientesDB.ID.ToString();
                    }
                    catch (Exception ex)
                    {
                        resultadoView.returnValue = ConfigAppSetting.DBErrorMessage;
                        return Json(resultadoView);
                    }
                }
                
            }
            return Json(resultadoView);
        }
    }
}


