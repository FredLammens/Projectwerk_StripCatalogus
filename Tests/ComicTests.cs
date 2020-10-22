using DomainLibrary;
using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class ComicTests
    {
        /// <summary>
        /// Tests the DuplicateAuthor check in the Comic class constructor.
        /// </summary>
        [TestMethod]
        public void CheckDuplicateAuthors()
        {
            Author author1 = new Author("Naam1");
            Author author2 = new Author("Naam2");
            List<Author> authors = new List<Author>() { author1, author1 };

            Publisher publisher = new Publisher("Uitgevrij");

            Action act = () => new Comic("Titel", "Series", 1, authors, publisher);

            act.Should().Throw<DomainException>().WithMessage("Een strip kan niet twee keer dezelfde autheur hebben.");

            authors = new List<Author>() { author1, author2};

            act = () => new Comic("Titel", "Series", 1, authors, publisher);

            act.Should().NotThrow<DomainException>();
        }
    }
}
