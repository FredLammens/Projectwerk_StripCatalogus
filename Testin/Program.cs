using Data.Repositories;
using DataLayer;
using DataLayer.DataBaseClasses;
using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;

namespace Testin
{
    class Program
    {
        static void Main(string[] args)
        {
            AdoNetContext anc = new AdoNetContext(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Projectwerk-Stripcatalogus;Integrated Security=True;Pooling=False", true);
            ComicRepository cr = new ComicRepository(anc);
            Comic comic = new Comic("Title 17", "Series 45", 9, new List<Author>() { new Author("Name of author 2"), new Author("Name of author "), new Author("Name of author 3") }, new Publisher("Publisher 8"));
            cr.AddComic(comic);
            anc.SaveChanges();
        }
    }
}
