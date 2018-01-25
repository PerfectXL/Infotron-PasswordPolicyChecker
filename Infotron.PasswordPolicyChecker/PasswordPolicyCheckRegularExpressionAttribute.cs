using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Infotron.PasswordPolicyChecker
{
    /// <inheritdoc cref="RegularExpressionAttribute" />
    /// <summary>
    ///     Configure <see cref="P:PasswordPolicyCheckRegularExpressionAttribute.PasswordPolicyConfiguration" />
    ///     in Global.asax.
    /// </summary>
    /// <seealso cref="RegularExpressionAttribute" />
    /// <seealso cref="IClientValidatable" />
    public class PasswordPolicyCheckRegularExpressionAttribute : RegularExpressionAttribute, IClientValidatable
    {
        private static readonly Lazy<PasswordPolicyConfiguration> LazyPasswordPolicyCheckParams =
            new Lazy<PasswordPolicyConfiguration>(() => new PasswordPolicyConfiguration());

        public PasswordPolicyCheckRegularExpressionAttribute() : base(PasswordPolicyCheckBuilder.GetRegularExpression(PasswordPolicyConfiguration)) { }

        /// <summary>
        ///     Gets the password policy check parameters. The different values for this configuration object must be set globally.
        /// </summary>
        public static PasswordPolicyConfiguration PasswordPolicyConfiguration => LazyPasswordPolicyCheckParams.Value;

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Pattern);
        }
    }
}
