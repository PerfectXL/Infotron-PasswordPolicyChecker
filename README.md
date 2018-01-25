Infotron PasswordPolicyChecker
==============================

Construct a regular expression that can be used to check the complexity of a password. The regular expression can be configured with different parameters, see `PasswordPolicyConfiguration`.

There is also an MVC Data Annotation Attribute that you can use to decorate the password field in your view model. This attribute uses a static configuration that you can configure on application startup. Look at `Global.asax.cs` in the MVC sample project to find out how this is done.

Example usage:

```cs
    PasswordPolicyConfiguration configuration = new PasswordPolicyConfiguration
    {
        MinNumberOfDigits = 1,
        MinNumberOfLowerCaseChars = 1,
        MinNumberOfUpperCaseChars = 1,
        MinPasswordLength = 6
    };
    string pattern = PasswordPolicyCheckBuilder.GetRegularExpression("user@example.com", configuration);
    bool isConforming = new Regex(pattern).IsMatch("abcdefG1"); // true
    isConforming = new Regex(pattern).IsMatch("abcdefgh"); // false
```
