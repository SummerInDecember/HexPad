using System.Threading.Tasks;
using Avalonia.Controls;
using HexPad.ViewModels;
using System;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using HexPad.Views;
using ReactiveUI;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;

namespace HexPad.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }

    private async void OpenFolderButton_Click(object sender, RoutedEventArgs e)
    {
        await OpenFolder();
    }
    private async Task OpenFolder()
    {
        MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
        var folder = await this.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select A Folder",
            AllowMultiple = false
        });

        if (folder.Any())
            viewModel.SelectedFolder = folder.First().Path.LocalPath;
        else
            viewModel.SelectedFolder = String.Empty;


        Console.WriteLine(viewModel.SelectedFolder);
    }

    private async void OpenFile_Click(object sender, RoutedEventArgs e)
    {
        await OpenFile();
    }

    private async Task OpenFile()
    {
        MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
        var file = await this.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select a File",
            AllowMultiple = false
        });

        if(file.Any()) 
            viewModel.SelectedFile = file.First().Name; 
        else    
            viewModel.SelectedFile = String.Empty;

        Console.WriteLine(viewModel.SelectedFile);
    }
}