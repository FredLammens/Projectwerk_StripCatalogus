using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.DomainLayerTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void OrderCheckAmountTest()
        {

            DateTime date = new DateTime(2021, 12, 01, 10, 00, 00);
            Dictionary<Comic, int> orderComics1 = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 5);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 1);
            orderComics1.Add(comic1, 2);
            Action a = () => new Order(1, orderComics1);
            a.Should().NotThrow<ArgumentException>();
            Dictionary<Comic, int> orderComics2 = new Dictionary<Comic, int>();
            orderComics2.Add(comic2, 4);
            Action b = () => new Order(2, orderComics2);
            b.Should().Throw<ArgumentException>().WithMessage($"hoeveelheid: {orderComics2.First().Value} overschrijdt hoeveelheid van {orderComics2.First().Key.Title}: {orderComics2.First().Key.AmountAvailable}.");

        }
        [TestMethod]
        public void OrderCheckAddToAmounts()
        {
            DateTime date = new DateTime(2021, 12, 01, 10, 00, 00);
            Dictionary<Comic, int> orderComics1 = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 15);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 11);
            orderComics1.Add(comic1, 2);
            Order order = new Order(1, orderComics1);
            order.OrderComics.Count.Should().Be(1);
            order.OrderComics.First().Key.Title.Should().Be("De legende van het Westen");
            order.OrderComics.First().Key.AmountAvailable.Should().Be(13);
            orderComics1.Add(comic2, 4);
            order = new Order(2, orderComics1);
            order.OrderComics.Count.Should().Be(2);
            order.OrderComics.ElementAt(1).Key.Title.Should().Be("Oklahoma Jim");
            order.OrderComics.ElementAt(1).Key.AmountAvailable.Should().Be(7);
        }
        // order => id?? 
        [TestMethod]
        public void CheckAmountNotNegative()
        {
            DateTime date = new DateTime(2021, 12, 01, 10, 00, 00);
            Dictionary<Comic, int> orderComics1 = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 5);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 1);
            orderComics1.Add(comic1, 2);
            Action a = () => new Order(1, orderComics1);
            a.Should().NotThrow<ArgumentException>();
            Dictionary<Comic, int> orderComics2 = new Dictionary<Comic, int>();
            orderComics2.Add(comic2, -1);
            Action b = () => new Order(2, orderComics2);
            b.Should().Throw<ArgumentException>().WithMessage($"hoeveelheid kan niet negatief zijn.");
        }
        [TestMethod]
        public void CheckDuplicateInDictionaryTest()
        {
            DateTime date = new DateTime(2020, 12, 01, 10, 00, 00);
            Dictionary<Comic, int> orderComics1 = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 5);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 1);
            orderComics1.Add(comic1, 1);
            Action a = () => orderComics1.Add(comic2, 1);
            a.Should().NotThrow<ArgumentException>();
            Dictionary<Comic, int> orderComics2 = new Dictionary<Comic, int>();
            orderComics2.Add(comic1, 1);
            Action b = () => orderComics2.Add(comic1, 1);
            b.Should().Throw<ArgumentException>().WithMessage("An item with the same key has already been added. Key: DomainLibrary.DomainLayer.Comic");

        }
    }
}