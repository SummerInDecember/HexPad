using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using HexPad.Views;
using ReactiveUI;

namespace HexPad.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static

    public string SelectedFolder { get; set; } = String.Empty;
    public string SelectedFile { get; set; } = String.Empty;  
    public MainWindowViewModel()
    {
    }


    
#pragma warning restore CA1822 // Mark members as static

}
