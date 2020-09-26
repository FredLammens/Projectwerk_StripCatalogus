using DomainLibrary;
using DomainLibrary.ComicClasses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class StripTests
    {
        [TestMethod]
        public void CheckDuplicateAuthors()
        {
            Author author1 = new Author("Voornaam1", "Achternaam1");
            Author author2 = new Author("Voornaam2", "Achternaam2");
            List<Author> authors = new List<Author>() { author1, author1 };

            List<Publisher> publishers = new List<Publisher>() { new Publisher("Uitgevrij") };

            Action act = () => new Comic("Titel", "Series", 1, authors, publishers);

            act.Should().Throw<DomainException>().WithMessage("Een strip kan niet twee keer dezelfde autheur hebben.");

            authors = new List<Author>() { author1, author2};

            act = () => new Comic("Titel", "Series", 1, authors, publishers);

            act.Should().NotThrow<DomainException>();
        }

                [TestMethod]
        public void CheckDuplicatePublishers()
        {
            List<Author> authors = new List<Author>() { new Author("Voornaam", "Achternaam") };

            Publisher publisher1 = new Publisher("Uitgevrij1");
            Publisher publisher2 = new Publisher("Uitgevrij2");
            List<Publisher> publishers = new List<Publisher>() {publisher1, publisher1};

            Action act = () => new Comic("Titel", "Series", 1, authors, publishers);

            act.Should().Throw<DomainException>().WithMessage("Een strip kan niet twee keer dezelfde uitgeverij hebben.");

            publishers = new List<Publisher>() { publisher1, publisher2 };

            act = () => new Comic("Titel", "Series", 1, authors, publishers);

            act.Should().NotThrow<DomainException>();
        }
    }
}
