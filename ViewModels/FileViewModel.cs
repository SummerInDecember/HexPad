using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<FileNodes> FileNode { get;}
        public ObservableCollection<FileNodes> SelectedNodes { get;}

        public FileViewModel(IScreen screen)
        { 
            HostScreen = screen;


            SelectedNodes = new ObservableCollection<FileNodes>();
            FileNode = new ObservableCollection<FileNodes>
            {
                new FileNodes("TestOne", new ObservableCollection<FileNodes>
                {
                    new FileNodes("TestUnder")
                }),
                new FileNodes("ThisShould Be enough", new ObservableCollection<FileNodes>
                {
                    new FileNodes("Enough")
                })
            };

            var moth = FileNode.Last().SubNodes?.Last();
            if (moth!=null) SelectedNodes.Add(moth);    
        }
    }
}