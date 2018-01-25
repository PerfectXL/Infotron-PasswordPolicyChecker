using System.Web;
using System.Web.Mvc;

namespace Infotron.PasswordPolicy.MvcSample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
