namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var res = 2 + 3;
            Assert.AreEqual(res, 5);

        }
    }
}