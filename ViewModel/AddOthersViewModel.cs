using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel
{
    public class AddOthersViewModel : ViewModelBase
    {
        /// <summary>
        /// controller
        /// </summary>
        private Controller controller;
        /// <summary>
        /// Input for UI serie
        /// </summary>
        public string InputSeries{get;set;}
        /// <summary>
        /// Input for UI author
        /// </summary>
        public string InputAuthor { get; set; }
        /// <summary>
        /// Input for UI publisher
        /// </summary>
        public string InputPublisher { get; set; }
        /// <summary>
        /// Command for binding insert serie
        /// </summary>
        public ICommand InsertSeriesCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Command for binding insert author
        /// </summary>
        public ICommand InsertAuthorCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Command for binding insert publisher
        /// </summary>
        public ICommand InsertPublisherCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Constructor that initializes controller and commands
        /// </summary>
        public AddOthersViewModel()
        {
            CreateCommand();
            controller = new Controller(new UnitOfWork());
        }
        /// <summary>
        /// method for insert series command
        /// </summary>
        public void InsertSeriesExecute() 
        {
            if (InputSeries == null)
                throw new PresentationException("Gelieve series in te vullen a.u.b.");
            controller.AddSeries(new Series(InputSeries));
        }
        /// <summary>
        /// method for insert author command
        /// </summary>
        public void InsertAuthorExecute()
        {
            if (InputAuthor == null)
                throw new PresentationException("Gelieve auteur in te vullen a.u.b.");
            controller.AddAuthor(new Author(InputAuthor));
        }
        /// <summary>
        /// method for insert publisher command
        /// </summary>
        public void InsertPublisherExecute()
        {
            if (InputPublisher == null)
                throw new PresentationException("Gelieve uitgeverij in te vullen a.u.b.");
            controller.AddPublisher(new Publisher(InputPublisher));
        }
        /// <summary>
        /// Method for linking the methods to the commands
        /// </summary>
        private void CreateCommand()
        {
            InsertSeriesCommand = new RelayCommand(InsertSeriesExecute);
            InsertPublisherCommand = new RelayCommand(InsertPublisherExecute);
            InsertAuthorCommand = new RelayCommand(InsertAuthorExecute);
        }
    }
}
