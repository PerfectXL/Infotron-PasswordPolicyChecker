using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infotron.PasswordPolicyChecker
{
    public static class PasswordPolicyCheckBuilder
    {
        /// <summary>
        ///     Gets the regular expression to validate a password. Uses default <see cref="PasswordPolicyConfiguration" />.
        /// </summary>
        public static string GetRegularExpression()
        {
            return GetRegularExpression(null, new PasswordPolicyConfiguration());
        }

        /// <summary>
        ///     Gets the regular expression to validate a password. Uses default <see cref="PasswordPolicyConfiguration" />.
        /// </summary>
        /// <param name="userName">Name of the user. This name must not occur in the password.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     Cannot enforce \"MinNumberOfSymbols\" when \"SelectedSymbols\" is
        ///     empty.
        /// </exception>
        public static string GetRegularExpression(string userName)
        {
            return GetRegularExpression(userName, new PasswordPolicyConfiguration());
        }

        /// <summary>
        ///     Gets the regular expression to validate a password.
        /// </summary>
        /// <param name="p">The password policy check parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     Cannot enforce \"MinNumberOfSymbols\" when \"SelectedSymbols\" is
        ///     empty.
        /// </exception>
        public static string GetRegularExpression(PasswordPolicyConfiguration p)
        {
            return GetRegularExpression(null, p);
        }

        /// <summary>
        ///     Gets the regular expression to validate a password.
        /// </summary>
        /// <param name="userName">Name of the user. This name must not occur in the password.</param>
        /// <param name="p">The password policy check parameters.</param>
        /// <returns>A regular expression string that can be used in .NET and Javascript.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     Cannot enforce \"MinNumberOfSymbols\" when \"SelectedSymbols\" is
        ///     empty.
        /// </exception>
        public static string GetRegularExpression(string userName, PasswordPolicyConfiguration p)
        {
            if (p.MinNumberOfSymbols != null && string.IsNullOrEmpty(p.SelectedSymbols))
            {
                throw new ArgumentOutOfRangeException(null, "Cannot enforce \"MinNumberOfSymbols\" when \"SelectedSymbols\" is empty.");
            }

            var sb = new StringBuilder("^");
            if (p.MaxNumberOfAllowedRepetitions.GetValueOrDefault() > 0)
            {
                sb.Append($"(?=^(?:(.)(?!(?:.*\\1){{{p.MaxNumberOfAllowedRepetitions},}}))+$)");
            }

            p.SelectedSymbols = p.SelectedSymbols ?? "";

            IEnumerable<KeyValuePair<string, int?>> lookaheadInfo = new Dictionary<string, int?>
            {
                {"A-Z\\u00C0-\\u00DE", p.MinNumberOfUpperCaseChars}, /* Javascript does not support Unicode character classes */
                {"a-z\\u00DF-\\u00FF", p.MinNumberOfLowerCaseChars},
                {"0-9", p.MinNumberOfDigits},
                {RegexEscape(p.SelectedSymbols), p.MinNumberOfSymbols}
            }.Where(x => x.Value.HasValue);

            foreach (KeyValuePair<string, int?> kvp in lookaheadInfo)
            {
                sb.Append($"(?=(?:.*[{kvp.Key})]){{{kvp.Value},}})");
            }

            if (!string.IsNullOrEmpty(userName))
            {
                sb.Append($"(?!.*{RegexEscape(userName)})");
            }

            string matchCharClass = !string.IsNullOrEmpty(p.DisallowedChars) ? $"[^{RegexEscape(p.DisallowedChars)}]" : ".";
            sb.Append($"{matchCharClass}{{{p.MinPasswordLength},}}$");

            return sb.ToString();
        }

        private static string RegexEscape(string s)
        {
            return Regex.Escape(s).Replace("-", "\\-");
        }
    }
}
