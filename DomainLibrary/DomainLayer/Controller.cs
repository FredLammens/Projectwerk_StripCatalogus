﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// Controlls aal actions in the domain layer.
    /// </summary>
    class Controller
    {
        #region Properties
        private IUnitOfWork uow;

        #endregion

        #region Constuctors
        /// <summary>
        /// A constructor that makes a Controller object.
        /// </summary>
        /// <param name="uow">An object that implements the IUnitOfWork interface.</param>
        public Controller(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        #endregion
        #region Operations
        /// <summary>
        /// Adds a comic to the database.
        /// </summary>
        /// <param name="comic">Comic to add.</param>
        public void AddComic(Comic comic)
        {
            uow.Comics.AddComic(comic);
        }
        /// <summary>
        /// Retrieves all comics from the database.
        /// </summary>
        /// <returns>A catalogue of comics.</returns>
        public Catalogue GetComics()
        {
            return new Catalogue( uow.Comics.GetComics().ToList());
        }
        /// <summary>
        /// Import all comics from a json file.
        /// </summary>
        public void ImportComics()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Export all comics in the catalogue to a json file.
        /// </summary>
        public void ExportComics()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
