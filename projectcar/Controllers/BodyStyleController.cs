using Microsoft.AspNetCore.Mvc;
using System.IO;
using projectcar.Models;
using projectcar.ViewModel;

namespace projectcar.Controllers
{
    public class BodyStyleController : Controller
    {
        CarContext carContext;
        public BodyStyleController(CarContext carContext)
        {
            this.carContext = carContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ViewData["link"] = "BodyStyle";
                ViewData["action"] = "Display";
                ViewData["controller"] = "BodyStyle";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Create(BodyStyleModel bodyStyle, IFormFile Img)
        {
            string path = "wwwroot/images/Home/Car/" + Img.FileName;
            string dirpath = "wwwroot/images/Car/" + bodyStyle.Name;
            ViewData["action"] = "Create";
            ViewData["controller"] = "BodyStyle";
            try
            {
                BodyStyleModel bodystyle = new BodyStyleModel()
                {
                    Name = bodyStyle.Name,
                    Img = "/images/Home/Car/" + Img.FileName
                };
                FileInfo fileInfo = new FileInfo(path);
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                if (fileInfo.Exists)
                {
                    throw new Exception("Image Exist");
                }
                carContext.BodyStyles.Add(bodystyle);
                carContext.SaveChanges();
                FileStream fileStream = new FileStream(path, FileMode.Create);
                Img.CopyTo(fileStream);
                fileStream.Close();
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                return View("myError", new ErrorModel() { MessageError = ex.Message });
            }
        }
        public IActionResult Display()
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ViewData["link"] = "BodyStyle";
                List<BodyStyleModel> bodyStyles = carContext.BodyStyles.ToList();
                return View(bodyStyles);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(string name)
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                try
                {
                    BodyStyleModel? bodyStyle = carContext.BodyStyles.SingleOrDefault(m => m.Name == name);
                    if (bodyStyle != null)
                    {
                        string dirpath = "wwwroot/images/Car/" + bodyStyle.Name;
                        FileInfo fileInfo = new FileInfo($"wwwroot{bodyStyle.Img}");
                        if (Directory.Exists(dirpath))
                        {
                            Directory.Delete(dirpath);
                        }
                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }
                        carContext.BodyStyles.Remove(bodyStyle);
                        carContext.SaveChanges();
                        return RedirectToAction("Display");
                    }
                    throw new Exception("Not Found BodyStyle");
                }
                catch (Exception ex)
                {
                    return View("myError", new ErrorModel() { MessageError = ex.Message });
                }
            }
            return RedirectToAction("Index", "Home");
        }
        //instead of updata use delete and create

    }
}
