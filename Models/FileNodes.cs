using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using ReactiveUI;
using System.Reactive;
using DialogHostAvalonia;
using HexPad.Views;
using HexPad.ViewModels;

namespace HexPad.Models
{
    public class FileNodes
    {   
        FileViewModel _fileViewModel;
        DeleteFile _deleteFileObj = new DeleteFile();
        public ObservableCollection<FileNodes>? SubNodes {get;}
        public string Title { get; set;}

        public ReactiveCommand<Unit, Unit> NodeClicked { get; }

        public ReactiveCommand<Unit, Unit> DeleteFileCommand { get; }

        public FileNodes(string title, FileViewModel model)
        {
            _fileViewModel = model;

            Title = title;
            NodeClicked = ReactiveCommand.Create(WhenNodeClicked);
            DeleteFileCommand = ReactiveCommand.Create(DeleteFileMethod);
        }

        public FileNodes(string title, ObservableCollection<FileNodes> subNodes, FileViewModel model)
        {
            _fileViewModel = model;

            Title = title;
            SubNodes = subNodes;
            NodeClicked = ReactiveCommand.Create(WhenNodeClicked);
            DeleteFileCommand = ReactiveCommand.Create(DeleteFileMethod);
            
        }

        private void DeleteFileMethod()
        {
            DialogHost.Show(_deleteFileObj, delegate(object sender, DialogOpenedEventArgs args)
            {
                _deleteFileObj.Result += result => {
                    if(result)
                    {
                        Console.WriteLine(Title);
                        if(File.Exists(Title))
                        {
                            File.Delete(Title);
                            _fileViewModel.ClearTreeAndReload(); 
                        }
                        else
                            Console.WriteLine("Unexpected error (I honestly dont know how this happened)");
                        args.Session.Close();
                    }
                    else
                        args.Session.Close();
                };
            });
            
        }


        private void WhenNodeClicked()
        {
            //Console.WriteLine(Title);
            
            // TODO: Implement what to do when a file is clicked
        }

    }
}