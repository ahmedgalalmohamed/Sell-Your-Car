using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectcar.Models;
using projectcar.ViewModel;

namespace projectcar.Controllers
{
    public class ModelController : Controller
    {
        CarContext carContext;
        public ModelController(CarContext carContext)
        {
            this.carContext = carContext;
        }
        [HttpGet]
        public IActionResult Create()
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ViewData["link"] = "Model";
                ViewData["action"] = "Display";
                ViewData["controller"] = "Model";
                ModelBodyMake modelBodyMake = new ModelBodyMake()
                {
                    BodyStyles = carContext.BodyStyles.ToList(),
                    Makers = carContext.Makers.ToList()
                };
                return View(modelBodyMake);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Create(ModelModel model)
        {
            ViewData["action"] = "Create";
            ViewData["controller"] = "Model";

            try
            {
                ModelModel modelModel = new ModelModel()
                {
                    Name = model.Name,
                    Yearmodel = model.Yearmodel,
                    MakerId = model.MakerId,
                    BodyStyleId = model.BodyStyleId
                };
                carContext.Models.Add(modelModel);
                carContext.SaveChanges();
                BodyStyleModel bodyStyleModel = carContext.BodyStyles.SingleOrDefault(bs => bs.Id == model.BodyStyleId);
                MakerModel makerModel = carContext.Makers.SingleOrDefault(m => m.Id == model.MakerId);
                string dirpath = $"wwwroot/images/Car/{bodyStyleModel.Name}/{makerModel.Name}";
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                dirpath = $"{dirpath}/{model.Yearmodel}-{model.Name}";
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View("myError", new ErrorModel() { MessageError = "This Model Found" });
            }
        }
        public IActionResult Display()
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ViewData["link"] = "Model";
                ModelBodyMake modelBodyMake = new ModelBodyMake()
                {
                    Models = carContext.Models.Include(m => m.BodyStyle).Include(m => m.Maker).ToList(),
                    BodyStyles = carContext.BodyStyles.ToList(),
                    Makers = carContext.Makers.ToList()
                };
                return View(modelBodyMake);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            string role = HttpContext.Session.GetString("role");
            if (role != null && role != "user")
            {
                ModelModel? model = carContext.Models.SingleOrDefault(m => m.Id == id);
                if (model != null)
                {
                    BodyStyleModel bodyStyleModel = carContext.BodyStyles.SingleOrDefault(bs => bs.Id == model.BodyStyleId);
                    MakerModel makerModel = carContext.Makers.SingleOrDefault(m => m.Id == model.MakerId);
                    string dirpath = $"wwwroot/images/Car/{bodyStyleModel.Name}/{makerModel.Name}/{model.Yearmodel}-{model.Name}";
                    if (Directory.Exists(dirpath))
                    {
                        Directory.Delete(dirpath);
                    }
                    carContext.Models.Remove(model);
                    carContext.SaveChanges();
                    return RedirectToAction("Display");

                }
                return View("myError", new ErrorModel() { MessageError = "This Model Not Found" });
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public List<ModelModel> Modelfilter(int makerid, int bodystyleid, int yearmodelid)
        {
            List<ModelModel> model = carContext.Models.Where(m => m.MakerId == makerid && m.BodyStyleId == bodystyleid && m.Yearmodel == yearmodelid).ToList();
            return model;
        }

    }
}
