using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.DataLayerTests
{
    [TestClass]
    public class DeliveryTest
    {
        AdoNetContext context = new AdoNetContext(true, "Test");
        [TestMethod]
        public void TestAddDelivery()
        {
            //DeliveryRepository deliveryRepository = new DeliveryRepository(context);
        }
    }
}
