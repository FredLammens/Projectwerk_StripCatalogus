using DataLayer;
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
    public class DeliveryTests
    {
        // delivery => id??
        [TestMethod]
        public void CheckAmountNotNegative()
        {
            DateTime date = new DateTime(2021, 12, 01, 10, 00, 00);
            DateTime dateDelivery = new DateTime(2021, 12, 10, 10, 00, 00);
            Dictionary<Comic, int> orderComics1 = new Dictionary<Comic, int>();
            Dictionary<Comic, int> orderComics2 = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 5);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 1);
            orderComics1.Add(comic1, 1);
            orderComics2.Add(comic2, -2);
            Action a = () => new Delivery(dateDelivery, orderComics1);
            a.Should().NotThrow<ArgumentException>();
            Action b = () => new Delivery(dateDelivery, orderComics2);
            b.Should().Throw<ArgumentException>().WithMessage($"hoeveelheid kan niet negatief zijn.");
        }
        [TestMethod]
        public void CheckDateDeliveryNotGreaterThenDateTest()
        {
            DateTime dateDelivery = new DateTime(2021, 12, 10, 10, 00, 00);
            DateTime dateDeliveryInPast = new DateTime(2020, 12, 10, 10, 00, 00);
            Dictionary<Comic, int> orderComics1 = new Dictionary<Comic, int>();
            Dictionary<Comic, int> orderComics2 = new Dictionary<Comic, int>();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"), 5);
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"), 1);
            orderComics1.Add(comic1, 1);
            orderComics2.Add(comic2, 1);
            Action a = () => new Delivery( dateDelivery, orderComics1);
            a.Should().NotThrow<ArgumentException>();
            Action b = () => new Delivery(dateDeliveryInPast, orderComics2);
            b.Should().Throw<ArgumentException>().WithMessage($"levering kan niet voor in het verleden zijn.");
        }
    }
}
