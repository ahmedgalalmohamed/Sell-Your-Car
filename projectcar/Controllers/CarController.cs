using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectcar.Models;
using projectcar.ViewModel;

namespace projectcar.Controllers
{
    public class CarController : Controller
    {
        CarContext carContext;
        public CarController(CarContext carContext)
        {
            this.carContext = carContext;
        }

        [HttpGet]
        public IActionResult Create(bool x)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (!x || HttpContext.Session.GetString("role") != "user")
            {
                return RedirectToAction("Index", "Home");
            }
            CarModel car_ = new CarModel();
            CarUserVM carUser = new CarUserVM()
            {
                car = car_,
                models = carContext.Models.ToList(),
                BodyStyles = carContext.BodyStyles.ToList(),
                Makers = carContext.Makers.ToList()
            };
            return View(carUser);
        }
        [HttpPost]
        public IActionResult Create(CarModel car, IFormFile fileinp)
        {
            try
            {
                UserModel? user = carContext.Users.SingleOrDefault(u => u.Username == HttpContext.Session.GetString("username"));
                ModelModel? model = carContext.Models.Where(m => m.Id == car.ModelId).Include(m => m.BodyStyle).Include(m => m.Maker).SingleOrDefault();
                car.UserId = user.Id;
                if (fileinp == null)
                {
                    car.Img = "/images/Car/def_car.jpg";
                }
                else
                {
                    string exten = fileinp.FileName.Substring(fileinp.FileName.IndexOf('.') + 1);
                    string path = $"/images/Car/{model.BodyStyle.Name}/{model.Maker.Name}/{model.Yearmodel}-{model.Name}/{car.VIN}.{exten}";
                    FileStream fileStream = new FileStream("wwwroot" + path, FileMode.Create);
                    fileinp.CopyTo(fileStream);
                    fileStream.Close();
                    car.Img = path;
                }
                carContext.Cars.Add(car);
                carContext.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                return View("myError", new ErrorModel() { MessageError = ex.Message });
            }

        }


        public IActionResult AllCarForUser(bool x)
        {
            string username = HttpContext.Session.GetString("username");
            if ((username != null && HttpContext.Session.GetString("role") == "user") && x)
            {
                return RedirectToAction("AllCarForUserHome", new { username = username, flag = true });
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult AllCarForUserHome(string username, bool? flag)
        {
            ViewData["flag"] = false;
            if (flag != null)
            {
                ViewData["flag"] = true;
            }
            List<CarModel> cars = carContext.Cars.Where(c => c.UserId == carContext.Users.SingleOrDefault(u => u.Username == username).Id).Include(c => c.Model).ThenInclude(m => m.Maker).Include(m => m.Model).ThenInclude(m => m.BodyStyle).ToList();
            return View("AllCarForUser", cars);
        }

        public IActionResult Detials(string VIN)
        {
            if (VIN == null)
            {
                return RedirectToAction("Detials", "Home");
            }
            CarModel car = carContext.Cars.Where(c => c.VIN == VIN).Include(c => c.User).Include(c => c.Model).SingleOrDefault();
            return View(car);
        }


        public string DeleteCar(string VIN)
        {
            if (VIN != null)
            {
                try
                {
                    CarModel car = carContext.Cars.Where(c => c.VIN == VIN).SingleOrDefault();
                    FileInfo file = new FileInfo($"wwwroot{car.Img}");
                    if (file.Exists&& !car.Img.Contains("def_car"))
                    {
                        file.Delete();
                    }
                    carContext.Cars.Remove(car);
                    carContext.SaveChanges();
                    return "y";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "n";
        }


        //function help me
        [HttpPost]
        public bool checkVIN(string VIN)
        {
            CarModel car = carContext.Cars.SingleOrDefault(c => c.VIN == VIN);
            if (car == null)
            {
                return false;
            }
            return true;
        }
    }
}
