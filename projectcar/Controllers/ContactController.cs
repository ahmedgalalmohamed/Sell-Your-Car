using Microsoft.AspNetCore.Mvc;
using projectcar.Models;

namespace projectcar.Controllers
{
    public class ContactController : Controller
    {
        CarContext carContext;
        public ContactController(CarContext carContext)
        {
            this.carContext = carContext;
        }

        public IActionResult Create()
        {
            ViewData["link"] = "Contact";
            ViewData["action"] = "Contact";
            ViewData["controller"] = "Home";
            return View();
        }

        [HttpPost]
        public string Create(ContactModel contact)
        {
            try
            {
                carContext.contacts.Add(contact);
                carContext.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IActionResult ContactAdmin(bool x)
        {
            string role = HttpContext.Session.GetString("role");
            if (role == "system admin")
            {
                if (x)
                {
                    List<ContactModel> contacts = carContext.contacts.ToList();
                    return View(contacts);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public string Delete(int id)
        {
            try
            {
                ContactModel contact = carContext.contacts.SingleOrDefault(c => c.Id == id);
                carContext.contacts.Remove(contact);
                carContext.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost]
        public string DeleteAll()
        {
            try
            {
                carContext.contacts.RemoveRange(carContext.contacts.ToList());
                carContext.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
