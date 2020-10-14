using DataLayer;
using DataLayer.DataBaseClasses;
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
            //needs db
        }

        public void AddComic(Comic comic)
        {
            DComic toAdd = Mapper.ToDComic(comic);
            using (var command = context.CreateCommand())
            {

                command.CommandText = @"Add querry";
                //add parameters
                //command.Parameters.Add("companyId", );
                //command.Parameters.Add("firstName", );
                command.ExecuteNonQuery();
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
