using DataLayer;
using DataLayer.DataBaseClasses;
using DataLayer.Extension_Methods;
using DomainLibrary.DomainLayer;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Repositories
{
    public class ComicRepository : IComicRepository
    {
        private AdoNetContext context;

        public ComicRepository(AdoNetContext context)
        {
            this.context = context;
        }

        private IEnumerable<Comic> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<Comic> items = new List<Comic>();
                while (reader.Read())
                {
                    var item = new DComic();
                    Map(reader, item);
                    items.Add(Mapper.ToComic(item));
                }

                return items;
            }
        }

        private void Map(IDataRecord record, DComic dComic)
        {
            dComic.Id = (int)record["ID"];
            dComic.Title = (string)record["Title"];
            dComic.SeriesNumber = (int)record["SeriesNr"];

        }

        public void AddComic(Comic comic)
        {
            DComic toAdd = Mapper.ToDComic(comic);
            AddDPublisher(toAdd);
            Console.WriteLine(toAdd.Publisher.Id);
            AddDComic(toAdd);
            Console.WriteLine(toAdd.Id);
            AddDSeries(toAdd);
            Console.WriteLine(toAdd.Id);
            AddDAuthors(toAdd);
            foreach (var auth in toAdd.Authors)
            {

                Console.WriteLine(auth.Id);
            }
            LinkAuthorComic(toAdd);

        }

        private void AddDPublisher(DComic dComic)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Publishers Where Publishers.name = @name";
                command.AddParameter("name", dComic.Publisher.Name);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    dComic.Publisher.Id = (int)id;

                }
                else
                {
                    command.CommandText = @"Insert into Publishers (Name) " +
                                           "values (@name)" +
                                           "SELECT CAST(scope_identity() AS int);";
                    dComic.Publisher.Id = (int)command.ExecuteScalar();
                }

            }
        }

        private void AddDComic(DComic dComic)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Comics Where Comics.Title = @title AND Comics.SeriesNr = @seriesNR AND Comics.Publisher_ID = @publisherId;";
                command.AddParameter("title", dComic.Title);
                command.AddParameter("seriesNR", dComic.SeriesNumber);
                command.AddParameter("publisherId", dComic.Publisher.Id);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    //update incatalouge to true
                    dComic.Id = (int)id;

                }
                else
                {
                    command.CommandText = @"Insert into Comics (Title, SeriesNr, Publisher_ID) " +
                                           "values (@title, @seriesNR, @publisherId) " +
                                           "SELECT CAST(scope_identity() AS int);";
                    dComic.Id = (int)command.ExecuteScalar();
                }
            }
        }

        private void AddDSeries(DComic dComic)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = @"Select * From Series Where Series.name = @name AND Series.Comic_ID = @comicId";
                command.AddParameter("name", dComic.Series.Name);
                command.AddParameter("comicId", dComic.Id);
                int? id = (int?)command.ExecuteScalar();

                if (id != null)
                {
                    dComic.Series.Id = (int)id;

                }
                else
                {
                    command.CommandText = @"Insert into Series (Name ,Comic_ID) " +
                                           "values (@name, @comicId)" +
                                           "SELECT CAST(scope_identity() AS int);";
                    dComic.Series.Id = (int)command.ExecuteScalar();
                }

            }
        }

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
            throw new NotImplementedException();
        }

        public IEnumerable<Comic> GetComics()
        {
            using (var command = context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "get all querry";
                ///temp return
                return null;
            }
        }


        public void RemoveComic(Comic comic)
        {
            using (var command = context.CreateCommand())
            {
                //need getcomic

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "up_Comics_Delete";
                //command.Parameters.Add(command.CreateParameter("@pId", comic.Id));
            }

        }
    }
}
