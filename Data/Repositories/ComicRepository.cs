﻿using DataLayer;
using DataLayer.DataBaseClasses;
using DataLayer.Extension_Methods;
using DomainLibrary.DomainLayer;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Data.Repositories
{
    /// <summary>
    /// A collection of comics in the database.
    /// </summary>
    public class ComicRepository : IComicRepository
    {
        /// <summary>
        /// Connection with the datebase.
        /// </summary>
        private AdoNetContext context;

        /// <summary>
        /// Constructor to make a ComicRepository.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public ComicRepository(AdoNetContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all comic objects from the database.
        /// </summary>
        /// <param name="command">Command containing the gett all query.</param>
        /// <returns></returns>
        private IEnumerable<Comic> ToComicList(IDbCommand command)
        {
            List<DComic> items = new List<DComic>();
            //gets all comic objects without 
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

            List<Comic> toReturn = new List<Comic>();
            foreach (var item in items)
            {
                toReturn.Add(Mapper.ToComic(item));
            }
            return toReturn;
        }
        /// <summary>
        /// Maps a record to a DComic object.
        /// </summary>
        /// <param name="record">Record to bind.</param>
        /// <param name="dComic">Comic to bind to.</param>
        private void Map(IDataRecord record, DComic dComic)
        {
            dComic.Id = (int)record["Comic_Id"];
            dComic.Title = (string)record["Title"];
            dComic.SeriesNumber = (int?)record["SeriesNr"];
            dComic.Publisher = new DPublisher();
            dComic.Publisher.Id = (int)record["Publisher_Id"];
            dComic.Publisher.Name = (string)record["Publisher_Name"];
            dComic.Series = new DSeries();
            dComic.Series.Id = (int)record["Series_ID"];
            dComic.Series.Name = (string)record["Series_Name"];
        }

        /// <summary>
        /// Gets all comic objects from the database.
        /// </summary>
        /// <param name="command">Command containing the gett all query.</param>
        /// <returns></returns>
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

        public void AddComic(Comic comic)
        {
            DComic toAdd = Mapper.ToDComic(comic);
            AddDPublisher(toAdd);
            AddDComic(toAdd);
            AddDSeries(toAdd);
            AddDAuthors(toAdd);
            foreach (var auth in toAdd.Authors)
            {

                Console.WriteLine(auth.Id);
            }
            LinkAuthorComic(toAdd);

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
                command.CommandText = @"Select * From Comics Where Comics.Title = @title AND Comics.SeriesNr = @seriesNR AND Comics.Publisher_ID = @publiserId AND Comics.Series_Id = @series_Id;";
                command.AddParameter("title", dComic.Title);
                command.AddParameter("seriesNR", dComic.SeriesNumber);
                command.AddParameter("publiserId", dComic.Publisher.Id);
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
                    command.CommandText = @"Insert into Comics (Title, SeriesNr, Publisher_ID) " +
                                           "values (@title, @seriesNR, @publiserId) " +
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


        public void AddComics(IEnumerable<Comic> comics)
        {
            var comicsList = comics.ToList();
            for (int i = 0; i < comicsList.Count; i++)
            {
                this.AddComic(comicsList[i]);
            }
        }


        public IEnumerable<Comic> GetComics()
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = "SELECT Comics.ID as Comic_Id, Title, SeriesNr, " +
                                      "Publishers.ID as Publisher_Id, Publishers.Name as Publisher_Name, " +
                                      "Series.ID as Series_ID, Series.Name as Series_Name " +
                                      "FROM Comics " +
                                      "INNER Join Publishers ON Publishers.ID = Comics.Publisher_ID " +
                                      "INNER Join Series ON Series.Comic_ID = Comics.ID " +
                                      "Where Comics.IsInCatalogue = 1;";

                return ToComicList(command);
            }

        }


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
    }
}
