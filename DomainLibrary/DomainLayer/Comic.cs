
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLibrary.DomainLayer

{
    /// <summary>
    /// This class represents a comic.
    /// </summary>
    public class Comic
    {
        #region Properties
        private string _title;
        /// <summary>
        /// The title of the comic
        /// </summary>
        public string Title { get => _title; set { if (string.IsNullOrEmpty(value)) throw new DomainException("Titel mag niet leeg zijn."); _title = value; } }

        private Series _series;
        /// <summary>
        /// The series the comic belongs to.
        /// </summary>
        public Series Series { get => _series; set { if (value.Equals(null)) throw new DomainException("Series mag niet leeg zijn."); _series = value; } }
        /// <summary>
        /// The number the comic is in the series.
        /// </summary>
        public int? SeriesNumber { get; set; }
        /// <summary>
        /// The autor(s) that wrote this comic
        /// </summary>
        private List<Author> _authors = new List<Author>();

        public IReadOnlyList<Author> Authors { get => _authors.AsReadOnly(); }

        private Publisher _publisher;
        /// <summary>
        /// The publisher that published the comic.
        /// </summary>
        public Publisher Publisher { get => _publisher; set { if (value.Equals(null)) throw new DomainException("Publisher mag niet leeg zijn."); _publisher = value; } }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Comic() { }
        /// <summary>
        /// A constuctor that makes a Comic object.
        /// </summary>
        /// <param name="title">The comics title.</param>
        /// <param name="series">The series the comic belongs to.</param>
        /// <param name="seriesNumber">The number the comic is in the series.</param>
        /// <param name="authors">The autor(s) that wrote this comic</param>
        /// <param name="publisher">The publisher that published the comic.</param>
        public Comic(string title, Series series, int? seriesNumber, List<Author> authors, Publisher publisher)
        {
            Title = title;
            Series = series;
            SeriesNumber = seriesNumber;
            SetAuthors(authors);
            Publisher = publisher;
        }
        #endregion

        #region Functionality

        /// <summary>
        /// Check whether a given list of authors has a duplicate.
        /// </summary>
        /// <param name="authors">List of authors to check</param>
        /// <returns></returns>
        private bool DuplicateAuthors(List<Author> authors)
        {
            if (authors.GroupBy(a => a.GetHashCode()).Any(g => g.Count() > 1))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Add author object 
        /// </summary>
        /// <param name="author">auhtor object to add</param>
        public void AddAuthor(Author author)
        {
            if (!_authors.Any(a => a.Name == author.Name))
            {
                _authors.Add(author);
            }
            else
            {
                throw new DomainException("Auteur zit al in de strip.");
            }
        }
        /// <summary>
        /// removes author object
        /// </summary>
        /// <param name="author">author object to remove</param>
        public void RemoveAuthor(Author author)
        {
            if (!_authors.Contains(author))
                throw new DomainException("Author bestaat niet.");
            _authors.Remove(author);
        }
        /// <summary>
        /// updates the author in authors
        /// </summary>
        /// <param name="index">index of author in authors</param>
        /// <param name="author">author object to update</param>
        public void UpdateAuthor(int index, Author author) 
        {
            if (index >= _authors.Count)
                throw new DomainException("Index is te groot.");
            if (index < 0)
                throw new DomainException("Index is te klein.");
            _authors[index] = author;
        } 
        /// <summary>
        /// sets the author list to the list given.
        /// </summary>
        /// <param name="authors">list of authors to set</param>
        public void SetAuthors(List<Author> authors) 
        {
            if (DuplicateAuthors(authors))
                throw new DomainException("Een strip kan niet twee keer dezelfde autheur hebben.");
            _authors = authors;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is Comic comic &&
                   _title == comic._title &&
                   EqualityComparer<Series>.Default.Equals(_series, comic._series) &&
                   SeriesNumber == comic.SeriesNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_title, _series, SeriesNumber);
        }
        #endregion
    }
}
