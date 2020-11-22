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

            Action act = () => new Comic("Titel", new Series("Series"), 1, authors, publisher);

            act.Should().Throw<DomainException>().WithMessage("Een strip kan niet twee keer dezelfde autheur hebben.");

            authors = new List<Author>() { author1, author2};

            act = () => new Comic("Titel", new Series("Series"), 1, authors, publisher);

            act.Should().NotThrow<DomainException>();
        }
        [TestMethod]
        public void AddAuthor()
        {
            Comic comic = new Comic();
            Author author1 = new Author("author1");
            Author author2 = new Author("author1");
            // Action act1 = () => comic.AddAuthor(author1);
            // act1.Should().NotThrow<Exception>();
            // Action act2 = () => comic.AddAuthor(author2);
            // act2.Should().Throw<Exception>();
            comic.AddAuthor(author1);
            comic.AddAuthor(author2);
        }
        [TestMethod]
        public void TitleNullShouldThrowException()
        {            
          Action act = () => new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            act.Should().NotThrow<DomainException>();
          Action act2 = () => new Comic("", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            act2.Should().Throw<DomainException>().WithMessage("Titel mag niet leeg zijn.");
        }
        [TestMethod]
        public void SeriesNullShouldThrowException()
        {
            Action act = () => new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            act.Should().NotThrow<DomainException>();
            Action act2 = () => new Comic("De legende van het Westen", new Series(""), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            act2.Should().Throw<DomainException>().WithMessage("Series mag niet leeg zijn.");
        }
        [TestMethod]
        public void PublisherNullShouldThrowException()
        {
            Action act = () => new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher("Dupuis"));
            act.Should().NotThrow<DomainException>();
            Action act2 = () => new Comic("De legende van het Westen", new Series("Lucky Luke"), 73, new List<Author>() { new Author("Morris"), new Author("Nordmann Patrick") }, new Publisher(""));
            act2.Should().Throw<DomainException>().WithMessage("Publisher mag niet leeg zijn.");
        }
        [TestMethod]
        public void RemoveAuthorDoestExistShouldThrowException()
        {
            Comic comic = new Comic();
            Author author1 = new Author("author1");
            Author author2 = new Author("author2");
            comic.AddAuthor(author1);
            Action act1 = () => comic.RemoveAuthor(author1);
            act1.Should().NotThrow<DomainException>();
            Action act2 = () => comic.RemoveAuthor(author2);
            act2.Should().Throw<DomainException>().WithMessage("Author bestaat niet.");
        }
        [TestMethod]
        public void UpdateAuthorIndexShouldThrowException()
        {
            Comic comic = new Comic();
            Author author1 = new Author("author1");
            Author author2 = new Author("author2");
            comic.AddAuthor(author1);
            Action act1 = () => comic.UpdateAuthor(0, author2);
            act1.Should().NotThrow<DomainException>();
            Action act2 = () => comic.UpdateAuthor(1, author2);
            act2.Should().Throw<DomainException>().WithMessage("Index is te groot.");
            Action act3 = () => comic.UpdateAuthor(-1, author2);
            act3.Should().Throw<DomainException>().WithMessage("Index is te klein.");
        }
    
        [TestMethod]
        public void SetAuthorDupliacteShouldThrowException()
        {
            Comic comic = new Comic();
            Author author1 = new Author("author1");
            Author author2 = new Author("author2");
            Author author3 = new Author("author1");
            List<Author> authorsList1 = new List<Author>();
            authorsList1.Add(author1);
            authorsList1.Add(author2);
            Action act1 = () => comic.SetAuthors(authorsList1);
            act1.Should().NotThrow<DomainException>();
            List<Author> authorsList2 = new List<Author>();
            authorsList2.Add(author1);
            authorsList2.Add(author2);
            authorsList2.Add(author3);
            Action act2 = () => comic.SetAuthors(authorsList2);
            act2.Should().Throw<DomainException>().WithMessage("Een strip kan niet twee keer dezelfde autheur hebben.");
        }
    }
}
