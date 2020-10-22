using Data.Repositories;
using DataLayer;
using DataLayer.DataBaseClasses;
using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Testin
{
    class Program
    {
        static void Main(string[] args)
        {
            AdoNetContext anc = new AdoNetContext(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Projectwerk-Stripcatalogus;Integrated Security=True;Pooling=False", true);
            ComicRepository cr = new ComicRepository(anc);
            Comic comic = new Comic("Title 16", "Series 9", 10, new List<Author>() { new Author("Name of author 9"), new Author("Name of author 02"), new Author("Name of author 55") }, new Publisher("Publisher 45"));
            cr.AddComic(comic);
            anc.SaveChanges();
            List<Comic> comics = cr.GetComics().ToList();
            cr.RemoveComic(comic);
            comics = cr.GetComics().ToList();
            cr.AddComic(comic);
            comics = cr.GetComics().ToList();
            Console.WriteLine();
        }
    }
}
