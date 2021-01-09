using DataLayer;
using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class ImportExportViewModel : ViewModelBase
    {
        #region private members
        private readonly Controller controller;
        private List<ViewComic> comics;
        private List<ViewComic> doubles;
        private List<Comic> comicsToImport;
        private double _percentage;
        private bool _showLoadingPanel = false;
        private bool _showButtonPanel = true;
        private bool _showLoadingBar = true; //Todo:
        private string _loadingText;
        #endregion
        #region Properties
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
        public string LoadingText 
        {
            get => _loadingText;
            private set 
            {
                _loadingText = value;
                OnPropertyChanged();
            }
        }
        public bool ShowLoadingBar 
        {
            get => _showLoadingBar;
            set 
            {
                _showLoadingBar = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public ImportExportViewModel()
        {
            controller = new Controller(new UnitOfWork());
        }

        public bool Import(string comicsFilePath)
        {
            LoadingText = "Checken op dubbele strips.";
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
        public void ResetPanels() 
        {
            ShowLoadingPanel = false;
            ShowButtonPanel = true;
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
            LoadingText = "Strips converteren.";
            double oneLoopPercentage = 1.0 / comics.Count * 100;
            foreach (ViewComic comic in comics)
            {
                comicsToImport.Add(Mapper.ViewComicMapper(comic));
                Percentage += oneLoopPercentage;
            }
            Math.Ceiling(Percentage);
            LoadingText = "Strips opslaan.";
            controller.AddComics(comicsToImport);
            ShowLoadingPanel = false;
            ShowButtonPanel = true; 
            return true;
        }

        public void Export(string exportPath)
        {
            ShowLoadingBar = false;
            ShowButtonPanel = false;
            LoadingText = "Bezig met exporteren.";
            ShowLoadingPanel = true;
            Parser.SerializeComics(Mapper.ComicsMapper(controller.GetCatalogue().Comics), exportPath);
            ShowLoadingPanel = false;
            ShowButtonPanel = true;
            ShowLoadingBar = true;
        }
    }
}
