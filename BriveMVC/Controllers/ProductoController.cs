using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BriveMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BriveMVC.Controllers
{
    public class ProductoController : Controller
    {
        string Baseurl = "";
        private readonly IConfiguration _configuration;

        public ProductoController (IConfiguration configuration)
        {
            _configuration = configuration;
            Baseurl = _configuration.GetValue<string>("webAPI");
        }

        // GET: ProductoController
        public async Task<IActionResult> ObtenerProductos(int IdSucursal)
        {
            List<ProductoModel> ProductoGetALL = new List<ProductoModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llamar todos los Clientes usando HttpClient
                HttpResponseMessage Res = await client.GetAsync("Productoes/");

                if (Res.IsSuccessStatusCode)
                {
                    //Si Res=True entra y asigna los datos
                    var _ProductoResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializar el api y almacena los datos
                    ProductoGetALL = JsonConvert.DeserializeObject<List<ProductoModel>>(_ProductoResponse);
                }
            }
            return View(ProductoGetALL);
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoModel Result = DetailsID(Convert.ToInt32(id));

            return View(Result);
        }

        // GET: ProductoController/Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private ProductoModel DetailsID(int id)
        {
            ProductoModel PedidoGetID = new ProductoModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET (id)
                var responseTasK = client.GetAsync("Productoes/" + id);
                responseTasK.Wait();

                var Res = responseTasK.Result;
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res=True entra y asigna los datos
                    var _ProductoResponse = Res.Content.ReadAsAsync<ProductoModel>();
                    _ProductoResponse.Wait();
                    PedidoGetID = _ProductoResponse.Result;
                }
            }
            return PedidoGetID;
        }

    }
}
