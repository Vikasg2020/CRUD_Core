using CRUD_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using CRUD_Core.Db_Context;

namespace CRUD_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            TemplateContext tc = new TemplateContext();

            var re = tc.UseTemps.ToList();

           List< DataModel> dm = new List<DataModel>();



            foreach (var item in re)
            {

                dm.Add(new DataModel
                {
                    Id= item.Id,
                    Name = item.Name,
                    Moblie = item.Moblie,
                    Depart = item.Depart,
                    Email = item.Email
                });

            }

            return View(dm);
        }
        
        [HttpGet]
        public IActionResult AddData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddData(DataModel dm)
        {
            TemplateContext tc = new TemplateContext();

            UseTemp ut = new UseTemp();

            ut.Id = dm.Id;
            ut.Name = dm.Name;
            ut.Moblie = dm.Moblie;
            ut.Depart = dm.Depart;
            ut.Email = dm.Email;

            if (dm.Id == 0)
            {
                tc.UseTemps.Add(ut);
                tc.SaveChanges();
            }
            else
            {
                tc.Entry(ut).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                tc.SaveChanges();
            }

           

            return RedirectToAction("Index", "Home");
        }


        public IActionResult EditData(int id)
        {
            TemplateContext tc = new TemplateContext();

            var re = tc.UseTemps.Where(m => m.Id == id).First();

            DataModel dm =new  DataModel();

            dm.Id = re.Id;
            dm.Name = re.Name;
            dm.Moblie = re.Moblie;
            dm.Depart = re.Depart;
            dm.Email = re.Email;


            return View("AddData" ,dm);
        }
        public IActionResult delete(int id)
        {
            TemplateContext context = new TemplateContext();
            var del = context.UseTemps.Where(n => n.Id == id).First();
            context.UseTemps.Remove(del);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
