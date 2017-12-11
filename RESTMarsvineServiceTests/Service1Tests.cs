using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTMarsvineService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTMarsvineService.Tests
{
    [TestClass()]
    public class Service1Tests
    {
        [TestMethod()]
        public void DeleteMeasurementsTest()
        {
            //arrange
            string id = "1";
            var delete = new Service1();
            //act
            int rowaffacted = delete.DeleteMeasurements(id);
            //assert
            Assert.AreEqual(1,rowaffacted);
            //hvis 1 række er ændret så virker den. det betyder at 1 række er blevet slettet.
        }
    }
}