using DataLayer;
using DomainLibrary.DomainLayer;
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
            Dictionary<Comic, int> orderComics = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van le fred", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 1);
            Comic comic2 = new Comic("Oklahoma jiha", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 5);
            orderComics.Add(comic1, 1);
            orderComics.Add(comic2, 1);
            Controller controller = new Controller(new UnitOfWork());
            controller.AddComic(comic1);
            controller.AddComic(comic2);
            Delivery delivery = new Delivery(DateTime.Now.AddDays(10), orderComics);
            controller.AddDelivery(delivery);
            context.Dispose();
        }
    }
}
