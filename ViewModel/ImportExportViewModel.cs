using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class ImportExportViewModel : ViewModelBase
    {

        public ImportExportViewModel()
        {
            CreateCommand();
        }

        Controller controller = new Controller(new UnitOfWork());




        public ICommand ImportCommand
        {
            get;
            internal set;
        }
        public ICommand ExportCommand
        {
            get;
            internal set;
        }

        private void CreateCommand()
        {
            ImportCommand = new RelayCommand(ImportExecute);
            ExportCommand = new RelayCommand(ExportExecute);
        }

        public void ImportExecute()
        {
            // var openFileDialog = new OpenFileDialog();
            //(openFileDialog.ShowDialog() == true);
            // txtEditor.Text = File.ReadAllText(openFileDialog.FileName);


            string comicsFilePath = @"C:\Users\niels\Downloads\dump.json";

            List<ViewComic> comics = Parser.DeSerializeComics(comicsFilePath).ToList();
            List<Comic> comicsToImport = new List<Comic>();
            foreach (ViewComic comic in comics)
            {
                comicsToImport.Add(Mapper.ViewComicMapper(comic));
            }

            //controller.AddComics(comicsToImport);
        }

        public void ExportExecute()
        {

        }
    }
}
