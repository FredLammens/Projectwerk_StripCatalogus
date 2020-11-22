using Data.Repositories;
using DataLayer;
using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.DataLayerTests
{
    [TestClass]
    public class AuthorTests
    {
        AdoNetContext context = new AdoNetContext(true, "Test");
        [TestMethod]
        public void TestAddAuthor()
        {
            ComicRepository cr = new ComicRepository(context);
            Author author1 = new Author("author1");
            Author author2 = new Author("author2");
            var result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(0);
            cr.AddAuthor(author1);
            result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("author1");
            cr.AddAuthor(author2);
            result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(2);
            result1.ElementAt(2).Name.Should().Be("author2");

        }
        [TestMethod]
        public void TestUpdateAuthor()
        {
            ComicRepository cr = new ComicRepository(context);
            Author authortoUpdate = new Author("author1");
            Author updated = new Author("author2");
            cr.AddAuthor(authortoUpdate);
            var result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("author1");

            cr.UpdateAuthor(authortoUpdate, updated);
            result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("author2");
        }
        [TestMethod]
        public void TestGetAllAuthors()
        {
            ComicRepository cr = new ComicRepository(context);
            Author author1 = new Author("author1");
            Author author2 = new Author("author2");
            Author author3 = new Author("author3");
            cr.AddAuthor(author1);
            var result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("author1");

            cr.AddAuthor(author2);
            cr.AddAuthor(author3);

            result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(3);

            result1.First().Name.Should().Be("author1");
            result1.ElementAt(1).Name.Should().Be("author2");
            result1.ElementAt(2).Name.Should().Be("author3");
        }
        [TestMethod]
        public void CheckDuplicateAuthor()
        {
            ComicRepository cr = new ComicRepository(context);
            Author author1 = new Author("author1");
            Author author2 = new Author("author1");
            cr.AddAuthor(author1);
            cr.AddAuthor(author2);
            var result1 = cr.GetAllAuthors();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("author1");

            Author author3 = new Author("author3");
            cr.AddAuthor(author3);
            var result2 = cr.GetAllAuthors();
            result2.Should().HaveCount(2);
        }
    }
}
