using System.ComponentModel.DataAnnotations;
using Infotron.PasswordPolicyChecker;

namespace Infotron.PasswordPolicy.MvcSample.Models
{
    public class PasswordModel
    {
        public string UserName { get; set; }

        [PasswordPolicyCheckRegularExpression(ErrorMessage =
             "Password must conform to password policy: at least seven characters, of which at least two digits and no spaces."), DataType(DataType.Password),
         Required]
        public string Password { get; set; }
    }
}
