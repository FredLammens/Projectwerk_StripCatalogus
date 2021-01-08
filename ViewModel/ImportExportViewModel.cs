using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class ImportExportViewModel : ViewModelBase
    {
        private Controller controller;
        private List<ViewComic> comics;
        private List<ViewComic> doubles;
        private List<Comic> comicsToImport;
        public ImportExportViewModel()
        {
            controller = new Controller(new UnitOfWork());
        }

        public bool Import(string comicsFilePath)
        {

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
            foreach (var item in doubles)
            {
                comics.Remove(item);
            }
            foreach (ViewComic comic in comics)
            {
                comicsToImport.Add(Mapper.ViewComicMapper(comic));
            }
            controller.AddComics(comicsToImport);
            return true;
        }

        public void Export()
        {

        }
    }
}
