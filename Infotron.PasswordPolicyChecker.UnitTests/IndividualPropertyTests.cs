using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Infotron.PasswordPolicyChecker.UnitTests
{
    [TestFixture]
    public class IndividualPropertyTests
    {
        [Test]
        public void DisallowedCharsTest1()
        {
            var p = new PasswordPolicyConfiguration {DisallowedChars = "abcdefghijklmnopqrstuvwxyz"};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456a"));
            Assert.IsTrue(regex.IsMatch("1234567"));
        }

        [Test]
        public void DisallowedCharsTest2()
        {
            var p = new PasswordPolicyConfiguration {DisallowedChars = @"\"};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch(@"1\23456"));
            Assert.IsTrue(regex.IsMatch("1234567"));
        }

        [Test]
        public void MaxNumberOfAllowedRepetitionsTest1()
        {
            var p = new PasswordPolicyConfiguration {MaxNumberOfAllowedRepetitions = 1};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("12A3456A"));
            Assert.IsFalse(regex.IsMatch("A23456A"));
            Assert.IsFalse(regex.IsMatch("A23A456"));

            Assert.IsTrue(regex.IsMatch("1234567"));
            Assert.IsTrue(regex.IsMatch("1234567A"));
            Assert.IsTrue(regex.IsMatch("A1234567"));
        }

        [Test]
        public void MaxNumberOfAllowedRepetitionsTest2()
        {
            var p = new PasswordPolicyConfiguration {MaxNumberOfAllowedRepetitions = 2};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123AA4567A"));
            Assert.IsFalse(regex.IsMatch("A12A3456A"));
            Assert.IsFalse(regex.IsMatch("A234A56A"));

            Assert.IsTrue(regex.IsMatch("1234567"));
            Assert.IsTrue(regex.IsMatch("A1234567"));
            Assert.IsTrue(regex.IsMatch("A23A456"));
        }

        [Test]
        public void MaxNumberOfAllowedRepetitionsTest3()
        {
            var p = new PasswordPolicyConfiguration {MaxNumberOfAllowedRepetitions = 3};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("1A23AA4567A"));
            Assert.IsFalse(regex.IsMatch("A1A2A3456A"));
            Assert.IsFalse(regex.IsMatch("AA234A56A"));

            Assert.IsTrue(regex.IsMatch("1234567"));
            Assert.IsTrue(regex.IsMatch("A12A34567"));
            Assert.IsTrue(regex.IsMatch("A23A45A6"));
        }

        [Test]
        public void MinNumberOfLowerCaseCharsTest()
        {
            var p = new PasswordPolicyConfiguration {MinNumberOfLowerCaseChars = 3};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456"));
            Assert.IsFalse(regex.IsMatch("12a3456"));
            Assert.IsFalse(regex.IsMatch("12ab3456"));
            Assert.IsFalse(regex.IsMatch("ABC1234567"));
            Assert.IsTrue(regex.IsMatch("123a4b5c67"));
            Assert.IsTrue(regex.IsMatch("1234567abc"));
            Assert.IsTrue(regex.IsMatch("abc1234567"));
            Assert.IsTrue(regex.IsMatch("óöô1234567"));
        }

        [Test]
        public void MinNumberOfSymbolsTest1()
        {
            var p = new PasswordPolicyConfiguration {MinNumberOfSymbols = 3};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456"));
            Assert.IsFalse(regex.IsMatch("12A3456$"));
            Assert.IsFalse(regex.IsMatch("12AB34##56"));
            Assert.IsTrue(regex.IsMatch("123!4@5#67"));
            Assert.IsTrue(regex.IsMatch("1234567!@#"));
            Assert.IsTrue(regex.IsMatch("!@#1234567"));
        }

        [Test]
        public void MinNumberOfSymbolsTest2()
        {
            var p = new PasswordPolicyConfiguration {MinNumberOfSymbols = 3, SelectedSymbols = "^&*"};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456"));
            Assert.IsFalse(regex.IsMatch("12A3456$"));
            Assert.IsFalse(regex.IsMatch("12AB34##56"));
            Assert.IsFalse(regex.IsMatch("12AB34#^&56"));
            Assert.IsTrue(regex.IsMatch("123^4&5*67"));
            Assert.IsTrue(regex.IsMatch("1234567^&*"));
            Assert.IsTrue(regex.IsMatch("&*^1234567"));
        }

        [Test]
        public void MinNumberOfUpperCaseCharsTest()
        {
            var p = new PasswordPolicyConfiguration {MinNumberOfUpperCaseChars = 3};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456"));
            Assert.IsFalse(regex.IsMatch("12A3456"));
            Assert.IsFalse(regex.IsMatch("12AB3456"));
            Assert.IsFalse(regex.IsMatch("abc1234567"));
            Assert.IsTrue(regex.IsMatch("123A4B5C67"));
            Assert.IsTrue(regex.IsMatch("1234567ABC"));
            Assert.IsTrue(regex.IsMatch("ABC1234567"));
            Assert.IsTrue(regex.IsMatch("ÓÖÔ1234567"));
        }

        [Test]
        public void MinPasswordLengthTest()
        {
            var p = new PasswordPolicyConfiguration {MinPasswordLength = 7};
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression(p));

            Assert.IsFalse(regex.IsMatch("123456"));
            Assert.IsTrue(regex.IsMatch("1234567"));
        }

        [Test]
        public void UserNameTest()
        {
            var regex = new Regex(PasswordPolicyCheckBuilder.GetRegularExpression("myUsername"));

            Assert.IsTrue(regex.IsMatch("123456"));
            Assert.IsTrue(regex.IsMatch("1234567myusername"));
            Assert.IsFalse(regex.IsMatch("1234567myUsername"));
        }
    }
}
