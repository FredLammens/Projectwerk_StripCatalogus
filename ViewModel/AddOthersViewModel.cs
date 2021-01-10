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
        private Controller controller;
        public string InputSeries{get;set;}
        public string InputAuthor { get; set; }
        public string InputPublisher { get; set; }

        public ICommand InsertSeriesCommand
        {
            get;
            internal set;
        }
        public ICommand InsertAuthorCommand
        {
            get;
            internal set;
        }
        public ICommand InsertPublisherCommand
        {
            get;
            internal set;
        }
        public AddOthersViewModel()
        {
            CreateCommand();
            controller = new Controller(new UnitOfWork());
        }
        public void InsertSeriesExecute() 
        {
            if (InputSeries == null)
                throw new PresentationException("Gelieve series in te vullen a.u.b.");
            controller.AddSeries(new Series(InputSeries));
        }
        public void InsertAuthorExecute()
        {
            if (InputAuthor == null)
                throw new PresentationException("Gelieve auteur in te vullen a.u.b.");
            controller.AddAuthor(new Author(InputAuthor));
        }
        public void InsertPublisherExecute()
        {
            if (InputPublisher == null)
                throw new PresentationException("Gelieve uitgeverij in te vullen a.u.b.");
            controller.AddPublisher(new Publisher(InputPublisher));
        }
        private void CreateCommand()
        {
            InsertSeriesCommand = new RelayCommand(InsertSeriesExecute);
            InsertPublisherCommand = new RelayCommand(InsertPublisherExecute);
            InsertAuthorCommand = new RelayCommand(InsertAuthorExecute);
        }
    }
}
