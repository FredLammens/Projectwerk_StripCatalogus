using DomainLibrary;
using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.DomainLayerTests
{
    [TestClass]
    public class CatalogueTests
    {
        /// <summary>
        /// checks if adding comics does not return any exception and has been added ot the comics list.
        /// </summary>
        [TestMethod]
        public void ComicsNoDuplicateShouldNotThrow() 
        {
            Catalogue catalogue = new Catalogue();
            String title = "title";
            Series series = new Series("Series");
            List<Author> authors = new List<Author>() { new Author("Author1"), new Author("Author2") };
            Publisher publisher = new Publisher("Uitgevrij");
            Comic comic = new Comic(title, series, 1, authors, publisher);
            catalogue.AddComic(comic);
            String title2 = "title2";
            Comic comic2 = new Comic(title2, series, 1, authors, publisher);
            Action act = () => catalogue.AddComic(comic2);
            act.Should().NotThrow<DomainException>();
            catalogue.Comics.Count.Should().Be(2);
        }
        [TestMethod]
        public void AddComicExistsShouldReturnFalse()
        {
            Catalogue catalogue = new Catalogue();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            Comic comic3 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            catalogue.AddComic(comic1).Should().BeTrue();
            catalogue.AddComic(comic2).Should().BeTrue();
            catalogue.AddComic(comic3).Should().BeFalse();
        }
        [TestMethod]
        public void RemoveComicDoesntExistShouldThrowException()
        {
            Catalogue catalogue = new Catalogue();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            catalogue.AddComic(comic1);
            Action act = () => catalogue.RemoveComic(comic1);
            act.Should().NotThrow<DomainException>();
            Action act2 = () => catalogue.RemoveComic(comic2);
            act2.Should().Throw<DomainException>().WithMessage("Comic bestaat niet.");

        }
        [TestMethod]
        public void UpdateComicCheckIndexShouldThrowException()
        {
            Catalogue catalogue = new Catalogue();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            catalogue.AddComic(comic1);
            Action act1 = () => catalogue.UpdateComic(0, comic2);
            act1.Should().NotThrow<DomainException>();
            Action act2 = () => catalogue.UpdateComic(5, comic2);
            act2.Should().Throw<DomainException>().WithMessage("Index is te groot.");
            Action act3 = () => catalogue.UpdateComic(-1, comic2);
            act3.Should().Throw<DomainException>().WithMessage("Index is te klein.");
        }
        [TestMethod]
        public void SetComicDupliacteShouldThrowException()
        {
            Catalogue catalogue = new Catalogue();
            Comic comic1 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            Comic comic2 = new Comic("Oklahoma Jim", new Series("Lucky Luke"), 69, new List<Author>() { new Author("Léturgie Jean"), new Author("Morris"), new Author("Conrad Didier"), new Author("Pearce") }, new Publisher("Dupuis"));
            Comic comic3 = new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            List<Comic> comicsList1 = new List<Comic>();
            comicsList1.Add(comic1);
            comicsList1.Add(comic2);
            Action act1 = () => catalogue.SetComics(comicsList1);
            act1.Should().NotThrow<DomainException>();
            List<Comic> comicsList2 = new List<Comic>();
            comicsList2.Add(comic1);
            comicsList2.Add(comic2);
            comicsList2.Add(comic3);
            Action act2 = () => catalogue.SetComics(comicsList2);
            act2.Should().Throw<DomainException>().WithMessage("Een strip kan niet twee keer voorkomen.");
        }
    }
}
