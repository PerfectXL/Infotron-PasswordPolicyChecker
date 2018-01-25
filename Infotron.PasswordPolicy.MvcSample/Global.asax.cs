using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Infotron.PasswordPolicyChecker;

namespace Infotron.PasswordPolicy.MvcSample
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            PasswordPolicyCheckRegularExpressionAttribute.PasswordPolicyConfiguration.MinNumberOfDigits = 2;
            PasswordPolicyCheckRegularExpressionAttribute.PasswordPolicyConfiguration.MinPasswordLength = 7;
            PasswordPolicyCheckRegularExpressionAttribute.PasswordPolicyConfiguration.DisallowedChars = " "; // Do not allow space in password.
        }
    }
}
