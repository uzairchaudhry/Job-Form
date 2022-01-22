using Job_Form_BSEF18M538.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Job_Form_BSEF18M538.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult user()
        {
            return View();
        }

        public ActionResult admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminM(string un, string pass)
        {
            if (un == "Uzair" && pass == "6702")
            {
                HttpContext.Session.SetString("user", "Yes");

                return RedirectToAction("adminMain", "home");
            } else if(un != "Uzair")            
            {
                ViewBag.msg = "Invalid Admin";
                return View("admin");
            }
            else
            {
                ViewBag.msg = "Invalid Password";
                return View("admin");
            }
        }
        public ActionResult adminMain()
        {
            if (HttpContext.Session.GetString("user") == "Yes")
            {
                return View();
            }
            else if (HttpContext.Session.GetString("user") == "null")
            {
                return RedirectToAction("admin", "home");
                
            }
            else
            {
                return RedirectToAction("admin", "home");
            }
        }
        [HttpPost]
        public ActionResult adduser(Application temp)
        {
            IFormFile NewPhoto = Request.Form.Files[0];
            if (NewPhoto != null)
            {
                    string[] file = NewPhoto.FileName.Split('.'); //To get extension of file
                
                    string fileName = string.Format(@"{0}.{1}", Guid.NewGuid(), file[file.Length-1]); //Guid used to generate unique filename
                    string folderName = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                    string filePath = Path.Combine(folderName, fileName);
                    NewPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    temp.Image = fileName;
            }
            try
            {
                  using (Job_ApplicationContext obj=new Job_ApplicationContext())
                {
                    obj.Applications.Add(temp);
                    obj.SaveChanges();

                }
                return RedirectToAction("Thanks","home");
            }
            catch (Exception ex)
            {
                return View("Index");
            }

        }
        public ActionResult Thanks()
        {
            return View();
        }
        public ActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("admin", "home");
        }
        [HttpPost]
        public ActionResult getForm(int id)
        {
            Application obj = new Application();
            using (Job_ApplicationContext temp = new Job_ApplicationContext())
            {
                obj = temp.Applications.Find(id);
                if(obj==null)
                {
                    return View("adminMain", "home");
                }
                else
                {
                    return PartialView("_myForm", obj);
                }
            }
            
        }
        public ActionResult viewForm()
        {
            List<Application> list = new List<Application>();
            using (Job_ApplicationContext obj = new Job_ApplicationContext())
            {
                list = obj.Applications.ToList();
            }
            return PartialView("_viewForm", list);
        }
        public ActionResult delForm(int id)
        {
            List<Application> list = new List<Application>();

            using (Job_ApplicationContext obj = new Job_ApplicationContext())
            {
                Application temp = obj.Applications.Find(id);
                if (temp != null)
                {
                    obj.Applications.Remove(temp);
                    obj.SaveChanges();
                }
                list = obj.Applications.ToList();
            }
            return PartialView("_viewForm", list);
        }
        [HttpPost]
        public ActionResult edit(Application obj,string oldimage)
        {
            List<Application> list = new List<Application>();
            if(Request.Form.Files.Count>0)
            { 
            IFormFile NewPhoto = Request.Form.Files[0];
                if (NewPhoto != null)
                {
                        string[] file = NewPhoto.FileName.Split('.'); //To get extension of file
                        string fileName = string.Format(@"{0}.{1}", Guid.NewGuid(), file[1]); //Guid used to generate unique filename
                        string folderName = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                        string filePath = Path.Combine(folderName, fileName);
                        NewPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                        obj.Image = fileName;                    
                }
            }
            else
            {
                obj.Image = oldimage;
            }
                try
                {
                    using (Job_ApplicationContext temp = new Job_ApplicationContext())
                    {
                        temp.Applications.Update(obj);
                        temp.SaveChanges();
                    }
                    
                    return RedirectToAction("adminMain", "Home"); 
                }
                catch (Exception ex)
                {
                    return RedirectToAction("adminMain", "Home");
                }
            }
        [HttpPost]
        public ActionResult editForm(int id)
        {
            Application obj = new Application();
            using (Job_ApplicationContext temp = new Job_ApplicationContext())
            {
                obj = temp.Applications.Find(id);
                if(obj==null)
                {
                    return View("adminMain", "home");
                }
                else
                {
                    return PartialView("_editForm", obj);
                }
            }
            
        }
    }
}
