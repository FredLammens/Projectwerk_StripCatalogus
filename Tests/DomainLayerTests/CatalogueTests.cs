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
    public class CatalogueTests
    {
        /// <summary>
        /// checks if multiple of same comic are added to catalogue the catalogue throws an error. 
        /// </summary>
        [TestMethod]
        public void DuplicateComicsShouldThrow() 
        {
            Catalogue catalogue = new Catalogue();
            String title = "title";
            Series series = new Series("Series");
            List<Author> authors = new List<Author>(){ new Author("Author1"), new Author("Author2") };
            Publisher publisher = new Publisher("Uitgevrij");
            Comic comic = new Comic(title, series, 1, authors, publisher);
            catalogue.AddComic(comic);

            Action act = () => catalogue.AddComic(comic);
            act.Should().Throw<DomainException>().WithMessage("De strip title zit al in de catalogus.");
        }
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
            comic.Title = "title2";
            Action act = () => catalogue.AddComic(comic);
            act.Should().NotThrow<DomainException>();
            catalogue.Comics.Count.Should().Be(2);
        }
    }
}
