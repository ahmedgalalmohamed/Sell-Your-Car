using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectcar.Models;
using projectcar.ViewModel;

namespace projectcar.Controllers
{
    public class MakerController : Controller
    {
        CarContext carContext;
        public MakerController(CarContext carContext)
        {
            this.carContext = carContext;
        }
        [HttpGet]
        public IActionResult Create()
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ViewData["link"] = "Maker";
                ViewData["action"] = "Display";
                ViewData["controller"] = "Maker";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Create(MakerModel maker)
        {
            ViewData["action"] = "Create";
            ViewData["controller"] = "Maker";

            try
            {
                MakerModel makerModel = new MakerModel()
                {
                    Name = maker.Name,
                    Location = maker.Location
                };
                carContext.Makers.Add(makerModel);
                carContext.SaveChanges();
                return RedirectToAction("Create");
            }
            catch
            {
                return View("myError", new ErrorModel() { MessageError = "This Maker Not Found" });
            }
        }
        public IActionResult Display()
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ViewData["link"] = "Maker";
                List<MakerModel> makerModels = carContext.Makers.ToList();
                ListMakerMakerVM listMakerMaker = new ListMakerMakerVM();
                listMakerMaker.makers = makerModels;
                return View(listMakerMaker);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(string name)
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                MakerModel? maker = carContext.Makers.SingleOrDefault(m => m.Name == name);
                if (maker != null)
                {
                    List<ModelModel> modes = carContext.Models.Where(m => m.MakerId == maker.Id).Include(m=>m.BodyStyle).ToList();
                    foreach(var mode in modes)
                    {
                        string path = $"wwwroot/images/Car/{mode.BodyStyle.Name}/{maker.Name}";
                        if (Directory.Exists(path))
                        {
                            Directory.Delete(path, true);
                        }
                    }
                    carContext.Makers.Remove(maker);
                    carContext.SaveChanges();
                    return RedirectToAction("Display");
                }
                return View("myError", new ErrorModel() { MessageError = "This Maker Not Found" });
            }
            return RedirectToAction("Index", "Home");
        }

        
    }
}
