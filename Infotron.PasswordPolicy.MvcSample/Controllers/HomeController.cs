using System.Text.RegularExpressions;
using System.Web.Mvc;
using Infotron.PasswordPolicy.MvcSample.Models;
using Infotron.PasswordPolicyChecker;

namespace Infotron.PasswordPolicy.MvcSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            return View(new PasswordModel());
        }

        [HttpPost]
        public ActionResult Index(PasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Example of a check against the UserName. For a simpler implementation we can of course just use
            // `if (model.Password.Contains(model.UserName)) { /* ... */ }`
            string expression =
                PasswordPolicyCheckBuilder.GetRegularExpression(model.UserName, PasswordPolicyCheckRegularExpressionAttribute.PasswordPolicyConfiguration);
            if (!new Regex(expression).IsMatch(model.Password))
            {
                ModelState.AddModelError("Password", "Password must not contain user name.");
                return View(model);
            }

            return RedirectToAction("Details", model);
        }

        public ActionResult Details(PasswordModel model)
        {
            return View(model);
        }
    }
}
