using DataLayer;
using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class ImportExportViewModel : ViewModelBase
    {
        private readonly Controller controller;
        private List<ViewComic> comics;
        private List<ViewComic> doubles;
        private List<Comic> comicsToImport;

        private double _percentage;
        private bool _showLoadingPanel = false;
        private bool _showButtonPanel = true;
        public double Percentage
        {
            get { return _percentage; }
            private set
            {
                _percentage = value;
                OnPropertyChanged("Percentage");
            }
        }
        public bool ShowLoadingPanel
        {
            get { return _showLoadingPanel; }
            private set
            {
                _showLoadingPanel = value;
                OnPropertyChanged("ShowLoadingPanel");
            }
        }
        public bool ShowButtonPanel
        {
            get { return _showButtonPanel; }
            private set
            {
                _showButtonPanel = value;
                OnPropertyChanged("ShowButtonPanel");
            }
        }

        public ImportExportViewModel()
        {
            controller = new Controller(new UnitOfWork());
        }

        public bool Import(string comicsFilePath)
        {
            Percentage = 0;
            ShowButtonPanel = false;
            ShowLoadingPanel = true;
            comics = Parser.DeSerializeComics(comicsFilePath).ToList();
            comicsToImport = new List<Comic>();
            doubles = comics.GroupBy(c => c)
                                .Where(g => g.Count() > 1)
                                .Select(y => y.Key)
                                .ToList();
            if (doubles.Count > 0)
                throw new PresentationException($"Er zijn {doubles.Count} dubbele strips gevonden. wilt u dit overslaan?");
            return ContinueImport();
        }

        public bool ContinueImport()
        {
            if (doubles.Count > 0)
            {
                foreach (var item in doubles)
                {
                    comics.Remove(item);
                }
            }
            Percentage += 1;
            double oneLoopPercentage = 1.0 / comics.Count * 100; //Todo: check *100
            foreach (ViewComic comic in comics)
            {
                comicsToImport.Add(Mapper.ViewComicMapper(comic));
                Percentage += oneLoopPercentage;
            }
            //Todo: uncommenten voor commit
            controller.AddComics(comicsToImport);
            Percentage += 1;
            ShowLoadingPanel = false;
            ShowButtonPanel = true; 
            return true;
        }

        public void Export(string exportPath)
        {
            Parser.SerializeComics(Mapper.ComicsMapper(controller.GetCatalogue().Comics), exportPath);
        }
    }
}
