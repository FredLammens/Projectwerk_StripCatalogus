using DataLayer;
using DataLayer.DataBaseClasses;
using DataLayer.Extension_Methods;
using DomainLibrary.DomainLayer;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repositories
{
    /// <summary>
    /// A collection of comics in the database.
    /// </summary>
    public class ComicRepository : IComicRepository
    {
        #region Properties
        /// <summary>
        /// Connection with the datebase.
        /// </summary>
        private AdoNetContext context;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to make a ComicRepository.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public ComicRepository(AdoNetContext context)
        {
            this.context = context;
        }
        #endregion

        #region Mappers
        /// <summary>
        /// Maps a record to a DComic object.
        /// </summary>
        /// <param name="record">Record to bind.</param>
        /// <param name="dComic">Comic to bind to.</param>
        private void Map(IDataRecord record, DComic dComic)
        {
            dComic.Id = (int)record["Comic_Id"];
            dComic.Title = (string)record["Title"];
            dComic.SeriesNumber = !Convert.IsDBNull(record["SeriesNr"]) ? (int?)record["SeriesNr"] : null;
            dComic.Publisher = new DPublisher();
            dComic.Publisher.Id = (int)record["Publisher_Id"];
            dComic.Publisher.Name = (string)record["Publisher_Name"];
            dComic.Series = new DSeries();
            dComic.Series.Id = (int)record["Series_ID"];
            dComic.Series.Name = (string)record["Series_Name"];
            dComic.AmountAvailable = (int)record["Amount_Available"];
        }

        /// <summary>
        /// Maps a record to a DSeries object.
        /// </summary>
        /// <param name="record">Record to bind.</param>
        /// <param name="dSeries">Comic to bind to.</param>
        private void Map(IDataRecord record, DSeries dSeries)
        {
            dSeries.Id = (int)record["ID"];
            dSeries.Name = (string)record["Name"];
        }

        /// <summary>
        /// Maps a record to a DPublisher object.
        /// </summary>
        /// <param name="record">Record to bind.</param>
        /// <param name="dPublisher">DPublisher to bind to.</param>
        private void Map(IDataRecord record, DPublisher dPublisher)
        {
            dPublisher.Id = (int)record["ID"];
            dPublisher.Name = (string)record["Name"];
        }

        /// <summary>
        /// Maps a record to a DAuthor object.
        /// </summary>
        /// <param name="record">Record to bind.</param>
        /// <param name="dAuthor">Author to bind to.</param>
        private void Map(IDataRecord record, DAuthor dAuthor)
        {
            dAuthor.Id = (int)record["ID"];
            dAuthor.Name = (string)record["Name"];
        }
        #endregion

        #region GetDataObject
        /// <summary>
        /// Gets all DComic objects from the database.
        /// </summary>
        /// <param name="command">Command containing the get all query.</param>
        /// <returns>All dcomics from the database.</returns>
        private IEnumerable<DComic> ToDComicList(IDbCommand command)
        {
            List<DComic> items = new List<DComic>();
            //gets all comic objects without authors
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = new DComic();
                    Map(reader, item);
                    items.Add(item);
                }
            }

            //gets authors for each comic object
            for (int i = 0; i < items.Count; i++)
            {
                command.CommandText = "SELECT Authors.ID, Authors.Name " +
                                      "from Authors " +
                                      "INNER Join Comics_Authors ON Authors.ID = Authors_ID " +
                                      $"Where Comics_ID = @id{i};";
                command.AddParameter($"id{i}", items[i].Id);
                items[i].Authors = ToDAuthorList(command).ToList();
            }

            return items;
        }

        /// <summary>
        /// Gets all DSeries objects from the database.
        /// </summary>
        /// <param name="command">Command containing the get all query.</param>
        /// <returns>All dseries from the database.</returns>
        private IEnumerable<DSeries> ToDSeriesList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<DSeries> items = new List<DSeries>();
                while (reader.Read())
                {
                    var item = new DSeries();
                    Map(reader, item);
                    items.Add(item);
                }

                return items;
            }
        }

        /// <summary>
        /// Gets all DPublisher objects from the database.
        /// </summary>
        /// <param name="command">Command containing the get all query.</param>
        /// <returns>All dpublishers from the database.</returns>
        private IEnumerable<DPublisher> ToDPublisherList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<DPublisher> items = new List<DPublisher>();
                while (reader.Read())
                {
                    var item = new DPublisher();
                    Map(reader, item);
                    items.Add(item);
                }

                return items;
            }
        }

        /// <summary>
        /// Gets all DAuthors objects from the database.
        /// </summary>
        /// <param name="command">Command containing the gett all query.</param>
        /// <returns>All dauthors from the database.</returns>
        private IEnumerable<DAuthor> ToDAuthorList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<DAuthor> items = new List<DAuthor>();
                while (reader.Read())
                {
                    var item = new DAuthor();
                    Map(reader, item);
                    items.Add(item);
                }

                return items;
            }
        }
        #endregion

        #region AddDomainObject
        public void AddComic(Comic comic)
        {
            DComic toAdd = Mapper.ToDComic(comic);
            AddDPublisher(toAdd.Publisher);
            AddDSeries(toAdd.Series);
            AddDComic(toAdd);
            AddDAuthors(toAdd);
            LinkAuthorComic(toAdd);
            AddStock(toAdd);

        }

        public void AddAuthor(Author author)
        {
            var dAuthor = Mapper.ToDAuthor(author);

            AddDAuthor(dAuthor);

        }

        public void AddSeries(Series series)
        {
            var dSeries = Mapper.ToDSeries(series);
            AddDSeries(dSeries);
        }

        public void AddPublisher(Publisher publisher)
        {
            var dPunlisher = Mapper.ToDPublisher(publisher);
            AddDPublisher(dPunlisher);
        }

        public void AddComics(IEnumerable<Comic> comics)
        {
            var comicsList = comics.ToList();
            for (int i = 0; i < comicsList.Count; i++)
            {
                this.AddComic(comicsList[i]);
            }
        }

        #endregion

        #region AddDataObject
        /// <summary>
        ///  Adds a DAuthor to the database.
        /// </summary>
        /// <param name="dAuthor">author to add</param>
        private void AddDAuthor(DAuthor dAuthor)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @$"Select * From Authors Where Authors.name = @name";
                command.AddParameter($"name", dAuthor.Name);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    dAuthor.Id = (int)id;
                }
                else
                {
                    command.CommandText = @"Insert into Authors (Name) " +
                                           $"values (@name)" +
                                           "SELECT CAST(scope_identity() AS int);";
                    dAuthor.Id = (int)command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Adds a DComic to the database
        /// </summary>
        /// <param name="dComic">DComic to add</param>
        private void AddDComic(DComic dComic)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Comics Where Comics.Title = @title AND Comics.SeriesNr = @series_Nr OR @series_Nr IS NULL AND Comics.Publisher_ID = @publiser_Id AND Comics.Series_ID = @series_Id;";
                command.AddParameter("title", dComic.Title);
                command.AddParameter("series_Nr", dComic.SeriesNumber);
                command.AddParameter("publiser_Id", dComic.Publisher.Id);
                command.AddParameter("series_Id", dComic.Series.Id);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    command.CommandText = @"Update Comics SET IsInCatalogue = 1 WHERE Id = @id;";
                    command.AddParameter("id", id);
                    command.ExecuteNonQuery();
                    dComic.Id = (int)id;

                }
                else
                {
                    command.CommandText = @"Insert into Comics (Title, SeriesNr, Publisher_ID, Series_ID) " +
                                           "values (@title, @series_Nr, @publiser_Id, @series_Id) " +
                                           "SELECT CAST(scope_identity() AS int);";
                    dComic.Id = (int)command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Adds a DPublisher to the database.
        /// </summary>
        /// <param name="dComic">DComic with publisher to add.</param>
        private void AddDPublisher(DPublisher dPublisher)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Publishers Where Publishers.name = @name";
                command.AddParameter("name", dPublisher.Name);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    dPublisher.Id = (int)id;

                }
                else
                {
                    command.CommandText = @"Insert into Publishers (Name) " +
                                           "values (@name)" +
                                           "SELECT CAST(scope_identity() AS int);";
                    dPublisher.Id = (int)command.ExecuteScalar();
                }

            }
        }

        /// <summary>
        /// Adds a DSeries to the database.
        /// </summary>
        /// <param name="dComic">DComic with series to add.</param>
        private void AddDSeries(DSeries dSeries)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Series Where Series.name = @name";
                command.AddParameter("name", dSeries.Name);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    dSeries.Id = (int)id;

                }
                else
                {
                    command.CommandText = @"Insert into Series (Name) " +
                                           "values (@name)" +
                                           "SELECT CAST(scope_identity() AS int);";
                    dSeries.Id = (int)command.ExecuteScalar();
                }

            }
        }

        /// <summary>
        /// Adds a collection DAuthors to the database.
        /// </summary>
        /// <param name="dComic">DComic with authors to add.</param>
        private void AddDAuthors(DComic dComic)
        {

            using (var command = context.CreateCommand())
            {
                for (int i = 0; i < dComic.Authors.Count; i++)
                {
                    command.CommandText = @$"Select * From Authors Where Authors.name = @name{i}";
                    command.AddParameter($"name{i}", dComic.Authors[i].Name);
                    int? id = (int?)command.ExecuteScalar();

                    if (id != null)
                    {
                        dComic.Authors[i].Id = (int)id;
                    }
                    else
                    {
                        command.CommandText = @"Insert into Authors (Name) " +
                                               $"values (@name{i})" +
                                               "SELECT CAST(scope_identity() AS int);";
                        dComic.Authors[i].Id = (int)command.ExecuteScalar();
                    }

                }
            }
        }

        /// <summary>
        /// Adds a link between a comic and a author in the database.
        /// </summary>
        /// <param name="dComic">DComic to use.</param>
        private void LinkAuthorComic(DComic dComic)
        {
            using (var command = context.CreateCommand())
            {
                command.AddParameter($"ComicId", dComic.Id);
                for (int i = 0; i < dComic.Authors.Count; i++)
                {
                    command.CommandText = @$"Select * From Comics_Authors Where Authors_ID = @AuthorId{i} And Comics_Id = @ComicId";
                    command.AddParameter($"AuthorId{i}", dComic.Authors[i].Id);
                    int? id = (int?)command.ExecuteScalar();

                    if (id == null)
                    {
                        command.CommandText = @"Insert into Comics_Authors (Authors_ID, Comics_Id) " +
                                               $"values (@AuthorId{i}, @ComicId);";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void AddStock(DComic toAdd)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Stock Where Stock.ComicID = @comic_Id";
                command.AddParameter("comic_Id", toAdd.Id);
                int? id = (int?)command.ExecuteScalar();

                if (id == null)
                {
                    command.CommandText = @"Insert into Stock (ComicID, Stock) " +
                       "values (@comic_Id, @stock)" +
                       "SELECT CAST(scope_identity() AS int);";
                    command.AddParameter("stock", toAdd.AmountAvailable);

                    command.ExecuteNonQuery();

                }

            }
        }
        #endregion

        #region GetDomainObject

        public IEnumerable<Comic> GetComics()
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = "SELECT Comics.ID as Comic_Id, Title, SeriesNr, " +
                                      "Publishers.ID as Publisher_Id, Publishers.Name as Publisher_Name, " +
                                      "Series.ID as Series_ID, Series.Name as Series_Name, " +
                                      "Stock.Stock as Amount_Available " +
                                      "FROM Comics " +
                                      "INNER Join Publishers ON Publishers.ID = Comics.Publisher_ID " +
                                      "INNER Join Series ON Series.ID = Comics.Series_ID " +
                                      "INNER JOIN Stock ON Comics.ID = Stock.ComicID " +
                                      "Where Comics.IsInCatalogue = 1;";



                List<Comic> toReturn = new List<Comic>();
                foreach (var item in ToDComicList(command))
                {
                    toReturn.Add(Mapper.ToComic(item));
                }
                return toReturn;
            }

        }

        public IEnumerable<Series> GetAllSeries()
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = "SELECT Series.ID, Series.Name " +
                                      "FROM Series;";

                var toReturn = new List<Series>();
                foreach (var item in ToDSeriesList(command))
                {
                    toReturn.Add(Mapper.ToSeries(item));
                }
                return toReturn;
            }

        }

        public IEnumerable<Author> GetAllAuthors()
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = "SELECT Authors.ID, Authors.Name " +
                                      "FROM Authors;";

                var toReturn = new List<Author>();
                foreach (var item in ToDAuthorList(command))
                {
                    toReturn.Add(Mapper.ToAuthor(item));
                }
                return toReturn;
            }
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {

            using (var command = context.CreateCommand())
            {
                command.CommandText = "SELECT Publishers.ID, Publishers.Name " +
                                      "FROM Publishers;";

                var toReturn = new List<Publisher>();
                foreach (var item in ToDPublisherList(command))
                {
                    toReturn.Add(Mapper.ToPublisher(item));
                }
                return toReturn;
            }
        }
        #endregion

        #region RemoveDomainObject
        public void RemoveComic(Comic comic)
        {
            var dComic = Mapper.ToDComic(comic);


            using (var command = context.CreateCommand())
            {
                command.CommandText = "UPDATE Comics " +
                                      "SET IsInCatalogue = 0 " +
                                      "WHERE Comics.Title = @Title AND Comics.SeriesNr = @SeriesNr; ";
                command.AddParameter("Title", dComic.Title);
                command.AddParameter("SeriesNr", dComic.SeriesNumber);
                command.ExecuteNonQuery();
            }

        }
        #endregion

        #region UpdateDomainObject
        public void UpdateComic(Comic toUpdate, Comic updated)
        {
            var oldComic = Mapper.ToDComic(toUpdate);
            var newComic = Mapper.ToDComic(updated);
            AddDSeries(newComic.Series);
            AddDPublisher(newComic.Publisher);
            AddDComic(oldComic);
            newComic.Id = oldComic.Id;
            AddDAuthors(newComic);
            AddDAuthors(oldComic);
            DeleteComicAuthorsLinks(oldComic);
            LinkAuthorComic(newComic);
            using (var command = context.CreateCommand())
            {
                command.CommandText = "UPDATE Comics " +
                                      "SET Title =  @newTitle, SeriesNr = @newSeriesNr, Series_ID = @newSeriesId, Publisher_ID = @newPublisherId " +
                                      "WHERE ID = @Id;";

                command.AddParameter("Id", oldComic.Id);
                //-----------------------------------------------------------------
                command.AddParameter("newPublisherId", newComic.Publisher.Id);
                command.AddParameter("newTitle", newComic.Title);
                command.AddParameter("newSeriesNr", newComic.SeriesNumber);
                command.AddParameter("newSeriesId", newComic.Series.Id);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Deletes the link between comic and authors
        /// </summary>
        /// <param name="oldComic">comic to delete links from</param>
        private void DeleteComicAuthorsLinks(DComic oldComic) 
        {
            using (var command = context.CreateCommand())
            {
                command.AddParameter($"ComicId", oldComic.Id);
                for (int i = 0; i < oldComic.Authors.Count; i++)
                {
                    command.CommandText = @$"DELETE From Comics_Authors Where Authors_ID = @AuthorId{i} And Comics_Id = @ComicId";
                    command.AddParameter($"AuthorId{i}", oldComic.Authors[i].Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateSeries(Series toUpdate, Series updated)
        {
            var oldSeries = Mapper.ToDSeries(toUpdate);
            var newSeries = Mapper.ToDSeries(updated);
            using (var command = context.CreateCommand())
            {
                command.CommandText = "UPDATE Series " +
                                      "SET Name =  @newName " +
                                      "WHERE Name = @name;";

                command.AddParameter("name", oldSeries.Name);
                command.AddParameter("newName", newSeries.Name);
                command.ExecuteNonQuery();
            }
        }
        public void UpdatePublisher(Publisher toUpdate, Publisher updated)
        {
            var oldPublisher = Mapper.ToDPublisher(toUpdate);
            var newPublisher = Mapper.ToDPublisher(updated);
            using (var command = context.CreateCommand())
            {
                command.CommandText = "UPDATE Publishers " +
                                      "SET Name =  @newName " +
                                      "WHERE Name = @name;";

                command.AddParameter("name", oldPublisher.Name);
                command.AddParameter("newName", newPublisher.Name);
                command.ExecuteNonQuery();
            }

        }
        public void UpdateAuthor(Author toUpdate, Author updated)
        {
            var oldComic = Mapper.ToDAuthor(toUpdate);
            var newComic = Mapper.ToDAuthor(updated);
            using (var command = context.CreateCommand())
            {
                command.CommandText = "UPDATE Authors " +
                                      "SET Name =  @newName " +
                                      "WHERE Name = @name;";

                command.AddParameter("name", oldComic.Name);
                command.AddParameter("newName", newComic.Name);
                command.ExecuteNonQuery();
            }

        }
        #endregion
    }
}
