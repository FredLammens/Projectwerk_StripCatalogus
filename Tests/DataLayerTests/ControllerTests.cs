using DataLayer;
using DomainLibrary;
using DomainLibrary.DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void TestImportComics()
        {
            IUnitOfWork uow = new UnitOfWork();
            Controller controller = new Controller(uow);
            string path = "C:/Users/User/Desktop/DATAPW/dump (1).json";
            controller.ImportComics(path);

        }
        [TestMethod]
        public void TestExportComics()
        {
            IUnitOfWork uow = new UnitOfWork();
            Controller controller = new Controller(uow);
            string path = "C:/Users/User/Desktop/DATAPW";
            List<Comic> comics = new List<Comic>();
            Catalogue catalogue = new Catalogue();
            catalogue = controller.GetCatalogue();
            comics = catalogue.Comics.ToList();
            controller.ExportComics(path);
        }
    }
}
