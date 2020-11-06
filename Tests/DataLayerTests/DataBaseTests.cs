using Data.Repositories;
using DataLayer;
using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class DataBaseTests
    {
        AdoNetContext context = new AdoNetContext(@"Data Source=PC-KDM\SQLEXPRESS;Initial Catalog=StripsTest;Integrated Security=True", true);
        [TestMethod]
        public void TestAddComic()
        {
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            ComicRepository cr = new ComicRepository(context);
            cr.AddComic(comic1);
            cr.AddComic(comic2);
            var result = cr.GetComics();

            result.Should().HaveCount(2);
            result.First().Title.Should().Be("De legende van het Westen");
            result.First().Series.Name.Should().Be("Lucky Luke");
            result.First().SeriesNumber.Should().Be(73);
            result.First().Publisher.Name.Should().Be("Dupuis");
            result.First().Authors.Should().HaveCount(2);
            result.First().Authors.First().Name.Should().Be("Morris");
            result.First().Authors.ElementAt(1).Name.Should().Be("Nordmann Patrick");

            result.ElementAt(1).Title.Should().Be("Oklahoma Jim");
            result.ElementAt(1).Series.Name.Should().Be("Lucky Luke");
            result.ElementAt(1).SeriesNumber.Should().Be(69);
            result.ElementAt(1).Publisher.Name.Should().Be("Dupuis");
            result.ElementAt(1).Authors.Should().HaveCount(4);
            result.ElementAt(1).Authors.First().Name.Should().Be("Léturgie Jean");
            result.ElementAt(1).Authors.ElementAt(3).Name.Should().Be("Pearce");
        }
        [TestMethod]
        public void TestAddComics()
        {
            ComicRepository cr = new ComicRepository(context);
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            Comic comic3 = new Comic("Chasse aux fantômes", new Series("Lucky Luke"), 61, new List<Author>() { new Author("Morris"), new Author("Lo Hartog Van Banda") }, new Publisher("Lucky Productions"));
            var comics = new List<Comic>();
            var result = cr.GetComics();
            result.Should().HaveCount(0);

            comics.Add(comic1);
            comics.Add(comic2);
            comics.Add(comic3);

            cr.AddComics(comics);

            result = cr.GetComics();
            result.Should().HaveCount(3);
            result.First().Title.Should().Be("De legende van het Westen");
            result.First().Series.Name.Should().Be("Lucky Luke");
            result.First().SeriesNumber.Should().Be(73);
            result.First().Publisher.Name.Should().Be("Dupuis");
            result.First().Authors.Should().HaveCount(2);
            result.First().Authors.First().Name.Should().Be("Morris");
            result.First().Authors.ElementAt(1).Name.Should().Be("Nordmann Patrick");

            result.ElementAt(1).Title.Should().Be("Oklahoma Jim");
            result.ElementAt(1).Series.Name.Should().Be("Lucky Luke");
            result.ElementAt(1).SeriesNumber.Should().Be(69);
            result.ElementAt(1).Publisher.Name.Should().Be("Dupuis");
            result.ElementAt(1).Authors.Should().HaveCount(4);
            result.ElementAt(1).Authors.First().Name.Should().Be("Léturgie Jean");
            result.ElementAt(1).Authors.ElementAt(3).Name.Should().Be("Pearce");

            result.ElementAt(2).Title.Should().Be("Chasse aux fantômes");
            result.ElementAt(2).Series.Name.Should().Be("Lucky Luke");
            result.ElementAt(2).SeriesNumber.Should().Be(61);
            result.ElementAt(2).Publisher.Name.Should().Be("Lucky Productions");
            result.ElementAt(2).Authors.Should().HaveCount(2);
            result.ElementAt(2).Authors.First().Name.Should().Be("Morris");
            result.ElementAt(2).Authors.ElementAt(1).Name.Should().Be("Lo Hartog Van Banda");
        }
        [TestMethod]
        public void CheckDuplicateComic()
        {
            ComicRepository cr = new ComicRepository(context);
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));

            cr.AddComic(comic1);
            cr.AddComic(comic2);
            var result = cr.GetComics();
            result.Should().HaveCount(1);
            result.First().Title.Should().Be("De legende van het Westen");
            result.First().Series.Name.Should().Be("Lucky Luke");
            result.First().SeriesNumber.Should().Be(73);
            result.First().Publisher.Name.Should().Be("Dupuis");
            result.First().Authors.Should().HaveCount(2);
            result.First().Authors.First().Name.Should().Be("Morris");
            result.First().Authors.ElementAt(1).Name.Should().Be("Nordmann Patrick");

            Comic comic3 = new Comic("De legende van het Westen Part 2", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            cr.AddComic(comic3);
            result = cr.GetComics();
            result.Should().HaveCount(2);
        }
        [TestMethod]
        public void TestGetComics()
        {
            ComicRepository cr = new ComicRepository(context);
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));

            var result = cr.GetComics();
            result.Should().BeEmpty();

            cr.AddComic(comic1);
            cr.AddComic(comic2);

            result = cr.GetComics();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.First().Title.Should().Be("De legende van het Westen");
            result.ElementAt(1).Title.Should().Be("Oklahoma Jim");
        }

        [TestMethod]
        public void TestRemoveComic()
        {
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            ComicRepository cr = new ComicRepository(context);
            cr.AddComic(comic1);
            cr.AddComic(comic2);
            var result1 = cr.GetComics();
            result1.Should().HaveCount(2);
            result1.First().Title.Should().Be("De legende van het Westen");
            result1.First().SeriesNumber.Should().Be(73);

            cr.RemoveComic(comic1);
            result1 = cr.GetComics();
            result1.Should().HaveCount(1);
            result1.First().Title.Should().Be("Oklahoma Jim");
            result1.First().SeriesNumber.Should().Be(69);
        }
    }
}
