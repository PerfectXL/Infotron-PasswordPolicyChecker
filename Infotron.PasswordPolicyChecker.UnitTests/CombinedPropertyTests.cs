using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Infotron.PasswordPolicyChecker.UnitTests
{
    [TestFixture]
    public class CombinedPropertyTests
    {
        [Test]
        public void CombinedMinNumberOfCharsTest()
        {
            var p = new PasswordPolicyConfiguration {MinNumberOfUpperCaseChars = 6, MinNumberOfLowerCaseChars = 10};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("abcdefghABCDEFGH"));
            Assert.IsTrue(regex.IsMatch("aAbBcCdDeEfFgGhHijklmnop"));
        }

        [Test]
        public void DisallowedCharsMinNumberOfLowerCaseCharsTest()
        {
            var p = new PasswordPolicyConfiguration {DisallowedChars = "abcdefghijklmnopqrstuvwxyz", MinNumberOfLowerCaseChars = 2};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456a"));
            Assert.IsFalse(regex.IsMatch("1234567"));
            Assert.IsFalse(regex.IsMatch("123456ab"));
            Assert.IsTrue(regex.IsMatch("123456øù"));
        }

        [Test]
        public void EverythingCombinedTest1()
        {
            var p = new PasswordPolicyConfiguration
            {
                DisallowedChars = "abc",
                MaxNumberOfAllowedRepetitions = 4,
                MinNumberOfDigits = 1,
                MinNumberOfLowerCaseChars = 1,
                MinNumberOfSymbols = 1,
                MinNumberOfUpperCaseChars = 1,
                MinPasswordLength = 7,
                SelectedSymbols = "~!@"
            };
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("PjGar3gS"));
            Assert.IsFalse(regex.IsMatch("PjGzr3gS"));
            Assert.IsFalse(regex.IsMatch("pjg~zr3gs"));
            Assert.IsFalse(regex.IsMatch("Pjg~gg3gg"));

            Assert.IsTrue(regex.IsMatch("PjG~zr3gS"));
            Assert.IsTrue(regex.IsMatch("j4Jf!Lpx4"));
            Assert.IsTrue(regex.IsMatch("9LzMx@Stv"));
            Assert.IsTrue(regex.IsMatch("MB5uu!@Uwf"));
            Assert.IsTrue(regex.IsMatch("9Uv9H~~nh7"));
        }

        [Test]
        public void MinPasswordLengthMinNumberOfLowerCaseCharsTest()
        {
            var p = new PasswordPolicyConfiguration {MinPasswordLength = 6, MinNumberOfLowerCaseChars = 10};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("abcdefgh"));
            Assert.IsTrue(regex.IsMatch("abcdefghijklmnop"));
        }
    }
}
