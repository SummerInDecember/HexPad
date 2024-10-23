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
        
        RenameFile _renameFileObj = new RenameFile();
        DeleteFile _deleteFileObj = new DeleteFile();
        public ObservableCollection<FileNodes>? SubNodes {get;}
        public string Title { get; set;}

        public ReactiveCommand<Unit, Unit> NodeClicked { get; }

        public ReactiveCommand<Unit, Unit> DeleteFileCommand { get; }

        public ReactiveCommand<Unit,Unit> RenameFileCommand { get; }


        public FileNodes(string title, FileViewModel model)
        {
            _fileViewModel = model;

            Title = title;
            NodeClicked = ReactiveCommand.Create(WhenNodeClicked);
            DeleteFileCommand = ReactiveCommand.Create(DeleteFileMethod);
            RenameFileCommand = ReactiveCommand.CreateFromTask(RenameFile);
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
                _deleteFileObj.Args = args;
                _deleteFileObj.Result += Result;
            });
            
        }

        private void Result(DeleteFile sender, bool result)
        {
            if(!result)
            {
                sender.Args.Session.Close();
                return;
            }

            Console.WriteLine(Title);
            if(File.Exists(Title))
            {
                File.Delete(Title);
                _fileViewModel.ClearTreeAndReload(); 
            }
            else if(Directory.Exists(Title))
            {
                DirectoryDeleter(Title);
                _fileViewModel.ClearTreeAndReload();
            }
            else
            {
                Console.WriteLine("Unexpected error (I honestly dont know how this happened)");
                return;
            }
            
            sender.Args.Session.Close();
            sender.Result -= Result;
                
            
        }

        /**
         * Deletes all the contents in the folder and itself
         * Firstly it deletes every file in the folder then calls the dunction on the subfolders
         * and when the current folder (either it be a "root" folder or subfolder) is empty
         * it gets deleted
         */
        private void DirectoryDeleter(string directory)
        {
            foreach(string file in Directory.GetFiles(directory))
                File.Delete(file);
            
            foreach(string subFolder in Directory.GetDirectories(directory))
                DirectoryDeleter(subFolder);

            if(Directory.GetFiles(directory).Length == 0 &&
               Directory.GetDirectories(directory).Length == 0)
                Directory.Delete(directory);

            
        }


        private async Task RenameFile()
        {
            Console.WriteLine("huh");
            await DialogHost.Show(_renameFileObj, delegate(object sender, DialogOpenedEventArgs args)
            {
                // TODO: Make this work for directories. This funciton acts all weird with directories
                // ! THis is making me go crazy I must commit and push
                Console.WriteLine("Worked??LKLKLK");
                _renameFileObj.Args = args;
                _renameFileObj.GiveNewName += Renamer;
            });
        }

        private async void Renamer(RenameFile sender, string? newName)
        {
            Console.WriteLine("Worked??");
            if(newName == null)
            {
                sender.Args.Session.Close();
                return;
            }
            int lastSlash = Path.GetFullPath(Title).LastIndexOf("/");
            string path = Title.Substring(0, lastSlash);
            Console.WriteLine(path);
            Console.WriteLine($"{path}/{newName}");
            if(File.Exists(Title))
            {
                File.Move(Title, $"{path}/{newName}");
            }
            else if(Directory.Exists(Title))
            {
                Directory.Move(Title, $"{path}/{newName}");
            }
            else
            {
                Console.WriteLine("What happened?");
                return;
            }

            
            _fileViewModel.ClearTreeAndReload();
            sender.Args.Session.Close();
            sender.GiveNewName -= Renamer;
        }


        private void WhenNodeClicked()
        {
            //Console.WriteLine(Title);
            
            // TODO: Implement what to do when a file is clicked
        }

    }
}