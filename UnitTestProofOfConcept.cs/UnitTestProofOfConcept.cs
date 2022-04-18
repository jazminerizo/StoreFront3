using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreFront3.UI.MVC.Utilities;

namespace StoreFront3.TESTS
{
    [TestClass]
    public class UnitTestProofOfConcept
    {
        [TestMethod]
        public void Can_Add_Two_Numbers()
        {
            decimal result = MathUtility.AddNumbers(10, 32);
            Assert.IsTrue(result == 42, "Addition failed - positive numbers didn't add up.");

            decimal result2 = MathUtility.AddNumbers(1.99m, 8);
            Assert.AreEqual(9.99m, result2, "Addition failed - values didn't add up.");

            Assert.IsNotNull(result2);
        }
        
        
    }
}
