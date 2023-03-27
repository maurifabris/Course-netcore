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
    public class CentrosDeCostoController : Controller
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
            var CentrosDeCosto = await db.CentrosDeCosto
                            .OrderBy(x => x.Nombre)
                            .Select(y => new { Numeral = y.ID, Nombre = y.Nombre, Identidicacion = y.Identidicacion })
                            .ToListAsync();
            return Json(CentrosDeCosto);
        }

        //Actualiza la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveInfo([Bind("Nombre,Identidicacion")] CentrosDeCostoViewModel material, int Numeral, string Mode)
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
                        var CentrosDeCostoDB  = await db.CentrosDeCosto.FirstOrDefaultAsync(m => m.ID == Numeral);

                        if (CentrosDeCostoDB  == null)
                        {
                            resultadoView.returnValue = ConfigAppSetting.NotFoundMessage;
                            return Json(resultadoView);
                        }

                        CentrosDeCostoDB .Nombre = CentrosDeCosto.Nombre;
                        CentrosDeCostoDB .Identificacion = CentrosDeCosto.Identifiacion;

                        db.Update(CentrosDeCostoDB );
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
        var CentrosDeCostoDB  = await db.CentrosDeCosto.FirstOrDefaultAsync(m => m.ID == Numeral);

        if (CentrosDeCostoDB  == null)
        {
            resultadoView.returnValue = ConfigAppSetting.NotFoundMessage;
            return Json(resultadoView);
        }

        db.Remove(CentrosDeCostoDB );
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
                        CentrosDeCosto CentrosDeCostoDB  = new CentrosDeCosto
                        {
                            Nombre = CentrosDeCosto.Nombre,
                            PrecioCompra = CentrosDeCosto.Identificacion,
                        };

                        db.CentrosDeCosto.Add(CentrosDeCostoDB);
                        await db.SaveChangesAsync();
                        resultadoView.NumeralID = CentrosDeCostoDB.ID.ToString();
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


