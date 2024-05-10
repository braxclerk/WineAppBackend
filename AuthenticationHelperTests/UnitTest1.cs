//[TestClass]
//public class AuthenticationHelperTest
//{
//    [TestMethod]
//    public void EncryptTest()
//    {
//        var header = AuthenticationHelper.Encrypt("john.doe", "VerySecret!");
//        Assert.AreEqual("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", header);
//    }
//    [TestMethod]
//    public void DecryptTest()
//    {
//        AuthenticationHelper.Decrypt("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", out string
//        username, out string password);
//        Assert.AreEqual("john.doe", username);
//        Assert.AreEqual("VerySecret!", password);
//    }
//}