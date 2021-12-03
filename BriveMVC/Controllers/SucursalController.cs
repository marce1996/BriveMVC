using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.Http.Headers;
using System.Threading.Tasks;
using BriveMVC.Models;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace BriveMVC.Controllers
{
    public class SucursalController : Controller
    {
        string Baseurl = "";
        private readonly IConfiguration _configuration;

        public SucursalController(IConfiguration configuration)
        {
            _configuration = configuration;
            Baseurl = _configuration.GetValue<string>("webAPI");
        }

        // GET: SucursalController
        public async Task<ActionResult> ObtenerSucursal()
        {
            List<SucursalModel> SucursalObtener = new List<SucursalModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Sucursals/");

                if(Res.IsSuccessStatusCode)
                {
                    var _SucursalResponse = Res.Content.ReadAsStringAsync().Result;
                    SucursalObtener = JsonConvert.DeserializeObject<List<SucursalModel>>(_SucursalResponse);
                }    
            }
            return View(SucursalObtener);
        }

        // GET: SucursalController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SucursalModel Result = DetailsID(Convert.ToInt32(id));

            return View(Result);
        }

        // GET: SucursalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SucursalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SucursalModel sucursal)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HttpResponseMessage response = await client.PostAsJsonAsync("api/command", command);
                var postTask = client.PostAsJsonAsync<SucursalModel>("Sucursals/", sucursal);
                postTask.Wait();
                var result = postTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ObtenerSucursal");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View(sucursal);
        }
        public ActionResult Edit()
        {
            return View();
        }
        // GET: SucursalController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            SucursalModel Result = DetailsID(Convert.ToInt32(id));
            if(Result == null)
            {
                return NotFound();
            }
            return View(Result);
        }

        // POST: SucursalController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id)
        //{
        //    //try
        //    //{
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    //catch
        //    //{
        //    //    return View();
        //    //}
        //    return View();
        //}

        // GET: SucursalController/Delete/5
        //public ActionResult Delete(int id)
        //{
          //  return View();
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            SucursalModel Result = DetailsID(Convert.ToInt32(id));
            if(Result == null)
            {
                return NotFound();
            }
            return View(Result);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP DELETE
                var deleteTask = client.DeleteAsync($"Sucursals//" + id);
                deleteTask.Wait();

                var resut = deleteTask.Result;

                if (resut.IsSuccessStatusCode)
                {
                    return RedirectToAction("ObtenerSucursal");
                }
            }

            ModelState.AddModelError(string.Empty, "Error, contactar al administrador.");
            return View();
        }
        // POST: SucursalController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        private SucursalModel DetailsID(int id)
        {
            SucursalModel SucurGetID = new SucursalModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET (id)
                var responseTasK = client.GetAsync("Sucursals/" + id);
                responseTasK.Wait();

                var Res = responseTasK.Result;
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res=True entra y asigna los datos
                    var _ClienteResponse = Res.Content.ReadAsAsync<SucursalModel>();
                    _ClienteResponse.Wait();
                    SucurGetID = _ClienteResponse.Result;
                }
            }
            return SucurGetID;
        }
    }
}
