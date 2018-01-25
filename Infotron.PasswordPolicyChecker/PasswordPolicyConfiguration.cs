namespace Infotron.PasswordPolicyChecker
{
    /// <summary>
    ///     Configure the password policy.
    /// </summary>
    public class PasswordPolicyConfiguration
    {
        /// <summary>
        ///     Characters that are forbidden in the password.
        /// </summary>
        public string DisallowedChars { get; set; }

        /// <summary>
        ///     The maximum number a character may occur in total in the password. If null or 0, this policy is not enforced.
        /// </summary>
        public int? MaxNumberOfAllowedRepetitions { get; set; }

        /// <summary>
        ///     The minimum number of digits. If null, this policy is not enforced.
        /// </summary>
        public int? MinNumberOfDigits { get; set; }

        /// <summary>
        ///     The minimum number of lower case characters. If null, this policy is not enforced.
        /// </summary>
        public int? MinNumberOfLowerCaseChars { get; set; }

        /// <summary>
        ///     The minimum number of <see cref="SelectedSymbols" />. If null, this policy is not enforced.
        /// </summary>
        public int? MinNumberOfSymbols { get; set; }

        /// <summary>
        ///     The minimum number of upper case characters. If null, this policy is not enforced.
        /// </summary>
        public int? MinNumberOfUpperCaseChars { get; set; }

        /// <summary>
        ///     The minimum length of the password. Default: 6 characters.
        /// </summary>
        public int MinPasswordLength { get; set; } = 6;

        /// <summary>
        ///     The selected symbols that must occur <see cref="MinNumberOfSymbols" /> times. Default: "~ ! @ # $ % ^ & * _ - + = ;
        ///     : , . ? /".
        /// </summary>
        public string SelectedSymbols { get; set; } = "~!@#$%^&*_-+=;:,.?/";
    }
}
