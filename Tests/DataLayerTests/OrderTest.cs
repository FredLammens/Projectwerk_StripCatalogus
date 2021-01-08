using DataLayer;
using DataLayer.Repositories;
using DomainLibrary;
using DomainLibrary.DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.DataLayerTests
{
    [TestClass]
    public class OrderTest
    {
        AdoNetContext context = new AdoNetContext(true, "Test");
        [TestMethod]
        public void TestAddOrder()
        {
            OrderRepository orderRepo = new OrderRepository(context);
            DateTime date = new DateTime(2020, 12, 01, 10, 00, 00);
            Dictionary<Comic, int> orderComics = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 1);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"),5);
            orderComics.Add(comic1, 1);
            orderComics.Add(comic2, 1);
            Controller controller = new Controller(new UnitOfWork());
           //controller.AddComic(comic1);
          // controller.AddComic(comic2);
            Order order = new Order(1,date,orderComics);
            orderRepo.AddOrder(order);
            
            context.Commit();
         
        }
    }
}
