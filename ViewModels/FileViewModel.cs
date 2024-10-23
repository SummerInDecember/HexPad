using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using HexPad.Models;
using System.Threading;
using ReactiveUI;

namespace HexPad.ViewModels
{
    public class FileViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableCollection<FileNodes> _FileNode;
        private ObservableCollection<FileNodes> _SelectedNodes;
        public string? UrlPathSegment {get;} = "FileViewModel";
        public IScreen HostScreen {get;}

        public ObservableCollection<FileNodes> FileNode { get; set;}
        public ObservableCollection<FileNodes> SelectedNodes { get; set;}

        public string Folder { get; set; }

        public FileViewModel(IScreen screen, MainWindowViewModel mainWin)
        { 
            HostScreen = screen;
            Folder = mainWin.SelectedFolder;
            PreInitTreeView();
        }

        /**
        * !Only call this function from the constructor
        * Initializes some variables
        */
        private void PreInitTreeView()
        {
            SelectedNodes = new ObservableCollection<FileNodes>();
            FileNode = new ObservableCollection<FileNodes>();
            _FileNode = FileNode;
            _SelectedNodes = SelectedNodes;

            InitTreeView(Folder, FileNode, SelectedNodes);
        }
        private void InitTreeView(string folder, ObservableCollection<FileNodes> FileNode,
                                                    ObservableCollection<FileNodes> SelectedNodes)
        {
            try
            {
                AddSubFoldersRecursive(folder, FileNode);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("PANICKING");
                return;
            }

            try
            {
                var moth = FileNode.Last().SubNodes?.Last();
                if (moth!=null) SelectedNodes.Add(moth); 
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public void ClearTreeAndReload()
        {
            _FileNode.Clear();
            _SelectedNodes.Clear();
            InitTreeView(Folder, _FileNode, _SelectedNodes);

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
                FileNodes subFolderNode = new FileNodes(subFol, new ObservableCollection<FileNodes>(), this);
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
                parentNode.Add(new FileNodes(file, this));
            }
        }
    }
}