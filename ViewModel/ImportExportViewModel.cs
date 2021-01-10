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
        /// <summary>
        /// DomainController
        /// </summary>
        private readonly Controller controller;
        /// <summary>
        /// holds deserialized Comics
        /// </summary>
        private List<ViewComic> comics;
        /// <summary>
        /// holds a list of double viewcomics
        /// </summary>
        private List<ViewComic> doubles;
        /// <summary>
        /// Mapped comics
        /// </summary>
        private List<Comic> comicsToImport;

        private double _percentage;
        private bool _showLoadingPanel = false;
        private bool _showButtonPanel = true;
        private bool _showLoadingBar = true;
        private string _loadingText;
        #endregion
        #region Properties
        /// <summary>
        /// Databinded percentage for loadingbar
        /// </summary>
        public double Percentage
        {
            get { return _percentage; }
            private set
            {
                _percentage = value;
                OnPropertyChanged("Percentage");
            }
        }
        /// <summary>
        /// Databinded bool for visibility of loadingPanel
        /// </summary>
        public bool ShowLoadingPanel
        {
            get { return _showLoadingPanel; }
            private set
            {
                _showLoadingPanel = value;
                OnPropertyChanged("ShowLoadingPanel");
            }
        }
        /// <summary>
        /// Databinded bool for visibility of buttonPanel
        /// </summary>
        public bool ShowButtonPanel
        {
            get { return _showButtonPanel; }
            private set
            {
                _showButtonPanel = value;
                OnPropertyChanged("ShowButtonPanel");
            }
        }
        /// <summary>
        /// Databinded text for loadingPanel
        /// </summary>
        public string LoadingText 
        {
            get => _loadingText;
            private set 
            {
                _loadingText = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Databinded bool for visibility of loadingPanel loadingbar
        /// </summary>
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
        #region Constructor
        /// <summary>
        /// Controller that adds the domainController
        /// </summary>
        public ImportExportViewModel()
        {
            controller = new Controller(new UnitOfWork());
        }
        #endregion
        #region Methods
        /// <summary>
        /// imports comics from file.
        /// </summary>
        /// <param name="comicsFilePath"></param>
        public void Import(string comicsFilePath)
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
            ContinueImport();
        }
        /// <summary>
        /// Resetpannels to show buttonpanel only
        /// </summary>
        public void ResetPanels() 
        {
            ShowLoadingPanel = false;
            ShowButtonPanel = true;
        }
        /// <summary>
        /// continues import after removing any doubles
        /// </summary>
        public void ContinueImport()
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
        }
        /// <summary>
        /// Exports comics from catalogue to exportpath
        /// </summary>
        /// <param name="exportPath">path to export.</param>
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
        #endregion
    }
}
