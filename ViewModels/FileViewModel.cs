using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HexPad.Models;
using ReactiveUI;

namespace HexPad.ViewModels
{
    public class FileViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment {get;} = "FileViewModel";
        public IScreen HostScreen {get;}

        public ObservableCollection<FileNodes> FileNode { get; set;}
        public ObservableCollection<FileNodes> SelectedNodes { get; set;}

        public FileViewModel(IScreen screen, MainWindowViewModel mainWin)
        { 
            HostScreen = screen;
            InitTreeView(mainWin.SelectedFolder);
        }

        void InitTreeView(string folder)
        {
            SelectedNodes = new ObservableCollection<FileNodes>();
            FileNode = new ObservableCollection<FileNodes>();

            foreach(string file in Directory.GetFiles(folder))
            {
                FileNode.Add(new FileNodes(file));
            }

            var moth = FileNode.Last().SubNodes?.Last();
            if (moth!=null) SelectedNodes.Add(moth);  
        }

        /**
        * TODO: Keep working 
        */
        void AddSubFoldersAndFilesRecursive(string folder)
        {
            
        }
    }
}