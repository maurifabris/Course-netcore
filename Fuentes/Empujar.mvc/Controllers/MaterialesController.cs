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
    public class MaterialesController : Controller
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
                return RedirectToAction("Error", "Home", new { mensaje = "Error al ingresar al modulo de Tipos de materiales. Intente mas tarde." });
            }
        }

        //Devuelve la lista de objetos en JSON
        public async Task<JsonResult> GetInfo()
        {
            var materiales = await db.Materiales
                            .OrderBy(x => x.Nombre)
                            .Select(y => new { Numeral = y.ID, Nombre = y.Nombre, PrecioCompra = y.PrecioCompra, PrecioVenta = y.PrecioVenta })
                            .ToListAsync();
            return Json(materiales);
        }

        //Actualiza la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveInfo([Bind("Nombre,PrecioCompra,PrecioVenta")] MaterialesViewModel material, int Numeral, string Mode)
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
                        var materialDB = await db.Materiales.FirstOrDefaultAsync(m => m.ID == Numeral);

                        if (materialDB == null)
                        {
                            resultadoView.returnValue = ConfigAppSetting.NotFoundMessage;
                            return Json(resultadoView);
                        }

                        materialDB.Nombre = material.Nombre;
                        materialDB.PrecioCompra = material.PrecioCompra;
                        materialDB.PrecioVenta = material.PrecioVenta;

                        db.Update(materialDB);
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
        var materialDB = await db.Materiales.FirstOrDefaultAsync(m => m.ID == Numeral);

        if (materialDB == null)
        {
            resultadoView.returnValue = ConfigAppSetting.NotFoundMessage;
            return Json(resultadoView);
        }

        db.Remove(materialDB);
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
                        Materiales materialDB = new Materiales
                        {
                            Nombre = material.Nombre,
                            PrecioCompra = material.PrecioCompra,
                            PrecioVenta = material.PrecioVenta
                        };

                        db.Materiales.Add(materialDB);
                        await db.SaveChangesAsync();
                        resultadoView.NumeralID = materialDB.ID.ToString();
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


