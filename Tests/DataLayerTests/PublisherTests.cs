using Data.Repositories;
using DataLayer;
using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Tests.DataLayerTests
{
    [TestClass]
    public class PublisherTests
    {
        AdoNetContext context = new AdoNetContext(true,"Test");
        [TestMethod]
        public void TestAddPublisher()
        {
            ComicRepository cr = new ComicRepository(context);
            Publisher publisher1 = new Publisher("publisher1");
            Publisher publisher2 = new Publisher("publisher2");
            var result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(0);
            cr.AddPublisher(publisher1);
            result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("publisher1");
            cr.AddPublisher(publisher2);
            result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(2);
            result1.ElementAt(1).Name.Should().Be("publisher2");
            context.Dispose();
        }
        [TestMethod]
        public void TestUpdatePubliher()
        {
            ComicRepository cr = new ComicRepository(context);
            Publisher publishertoUpdate = new Publisher("publisher1");
            Publisher updated = new Publisher("publisher2");
            cr.AddPublisher(publishertoUpdate);

            var result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("publisher1");
            cr.UpdatePublisher(publishertoUpdate, updated);
            result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("publisher2");
            context.Dispose();
        }
        [TestMethod]
        public void TestGetAllPublishers()
        {
            ComicRepository cr = new ComicRepository(context);
            Publisher publisher1 = new Publisher("publisher1");
            Publisher publisher2 = new Publisher("publisher2");
            Publisher publisher3 = new Publisher("publisher3");
            cr.AddPublisher(publisher1);

            var result1 = cr.GetAllPublishers();

            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("publisher1");

            cr.AddPublisher(publisher2);
            cr.AddPublisher(publisher3);

            result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(3);
            result1.First().Name.Should().Be("publisher1");
            result1.ElementAt(1).Name.Should().Be("publisher2");
            result1.ElementAt(2).Name.Should().Be("publisher3");
            context.Dispose();
        }
        [TestMethod]
        public void CheckDuplicatePublisher()
        {
            ComicRepository cr = new ComicRepository(context);
            Publisher publisher1 = new Publisher("publisher1");
            Publisher publisher2 = new Publisher("publisher1");
            cr.AddPublisher(publisher1);
            cr.AddPublisher(publisher2);
            var result1 = cr.GetAllPublishers();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("publisher1");            
            Publisher publisher3 = new Publisher("publisher2");
            cr.AddPublisher(publisher3);
            var result2 = cr.GetAllPublishers();
            result2.Should().HaveCount(2);
            result2.First().Name.Should().Be("publisher1");
            result2.ElementAt(1).Name.Should().Be("publisher2");
            context.Dispose();
        }
    }
}
