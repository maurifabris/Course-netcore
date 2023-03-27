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
    public class MaterialPrecioController : Controller
    {
        private readonly WebDBContext db = new WebDBContext();

        // Método que devuelve la vista de edición
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                // Obtiene el objeto padre
                var material = await db.Materiales.FindAsync(id);
                if (material == null)
                {
                    return NotFound();
                }

                // Crea un nuevo objeto hijo con los datos del padre
                var materialPrecio = new MaterialPrecioViewModel
                {
                    MaterialID = material.ID,
                    PrecioCompra = material.PrecioCompra,
                    PrecioVenta = material.PrecioVenta
                };

                return View(materialPrecio);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { mensaje = "Error al ingresar al modulo de precios de materiales. Intente mas tarde." });
            }
        }

        // Método que guarda los datos en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaterialID,PrecioCompra,PrecioVenta")] MaterialPrecioViewModel materialPrecio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Crea un nuevo objeto hijo y lo agrega a la base de datos
                    var materialPrecioDB = new PreciosHistoricosDeMateriales
                    {
                        MaterialID = materialPrecio.MaterialID,
                        PrecioCompra = materialPrecio.PrecioCompra,
                        PrecioVenta = materialPrecio.PrecioVenta
                    };

                    db.MaterialPrecios.Add(materialPrecioDB);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index", "Materiales");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ConfigAppSetting.DBErrorMessage);
            }

            // Si hay algún error en la validación, devuelve la vista con los datos ingresados
            return View(materialPrecio);
        }
    }

    internal class MaterialPrecio
    {
        public int MaterialID { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
