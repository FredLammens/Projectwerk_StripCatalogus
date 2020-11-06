using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// Controlls all actions in the domain layer.
    /// </summary>
    public class Controller
    {
        #region Properties
        private IUnitOfWork uow;
        private Catalogue catalogue;

        #endregion

        #region Constuctors
        /// <summary>
        /// A constructor that makes a Controller object and initializes the catalogue with all comics from the database.
        /// </summary>
        /// <param name="uow">An object that implements the IUnitOfWork interface.</param>
        public Controller(IUnitOfWork uow)
        {
            this.uow = uow;
            //laden van catalogue
            catalogue = new Catalogue(uow.Comics.GetComics().ToHashSet());
        }
        #endregion

        #region Operations
        /// <summary>
        /// Adds a comic to the database and catalogue.
        /// </summary>
        /// <param name="comic">Comic to add.</param>
        public void AddComic(Comic comic)
        {
            catalogue.AddComic(comic);
            uow.Comics.AddComic(comic);
            uow.SaveChanges();
        }
        /// <summary>
        /// Returns the catalogue
        /// </summary>
        /// <returns>A catalogue of comics.</returns>
        public Catalogue GetCatalogue()
        {        
            return catalogue;
        }
        /// <summary>
        /// Import all comics from a json file.
        /// </summary>
        public void ImportComics(string path)
        {
           uow.Comics.AddComics(Parser.DeSerializeComics(path));
           uow.SaveChanges();
        }
        /// <summary>
        /// Export all comics in the catalogue to a json file.
        /// </summary>
        public void ExportComics( string path )
        {       
            Parser.SerializeComics(catalogue.Comics.ToList(), path);
            uow.SaveChanges();
        }
        #endregion

    }
}
