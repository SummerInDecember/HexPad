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

            AddSubFoldersRecursive(folder, FileNode);

            var moth = FileNode.Last().SubNodes?.Last();
            if (moth!=null) SelectedNodes.Add(moth);  
        }

        /**
        * Function adds the subfolders to the treeview and calls the "AddFilesToSubFolder" method
        * to add the files to those subfolders
        */
        void AddSubFoldersRecursive(string folder, ObservableCollection<FileNodes> parentNode)
        {
            AddFilesToSubFolder(folder, parentNode);

            string[] subFolders = Directory.GetDirectories(folder);
            if(subFolders.Length == 0)
                return;
            foreach(string subFol in subFolders)
            {
                FileNodes subFolderNode = new FileNodes(subFol, new ObservableCollection<FileNodes>());
                parentNode.Add(subFolderNode);
                AddSubFoldersRecursive(subFol, subFolderNode.SubNodes);
            }
        }

        /**
        * Function adds the files in the folder to the treeview
        */
        void AddFilesToSubFolder(string folder, ObservableCollection<FileNodes> parentNode)
        {
            foreach(string file in Directory.GetFiles(folder))
            {
                parentNode.Add(new FileNodes(file));
            }
        }
    }
}