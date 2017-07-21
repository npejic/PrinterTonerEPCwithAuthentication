using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrinterTonerEPCwithAuthentication.Models;
using PrinterTonerEPCwithAuthentication.Common;

namespace PrinterTonerEPCwithAuthentication.Tests.Common
{
    [TestClass]
    public class TestControllerMethods
    {
        [TestMethod]
        public void TestSumOfAllSoldTonersInPeriod()
        {
            // arrange
            List<TonerTotal> testList = new List<TonerTotal>();
            
            testList.Add(new TonerTotal { TotalTonerModel = "model1", TonerTotalCount = 1, TonerTotalMin = 1 });
            testList.Add(new TonerTotal { TotalTonerModel = "model2", TonerTotalCount = 3, TonerTotalMin = 1 });
            testList.Add(new TonerTotal { TotalTonerModel = "model1", TonerTotalCount = 6, TonerTotalMin = 1 });

            int expected = 10;
            // act
            int realNumberOfSoldToners = ControllerMethods.SumOfAllSoldTonersInPeriod(testList);

            Assert.AreEqual(expected, realNumberOfSoldToners, 0, "Account not debited correctly");

        }
    }
}
