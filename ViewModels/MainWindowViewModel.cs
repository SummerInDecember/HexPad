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

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
#pragma warning disable CA1822 // Mark members as static

    public RoutingState Router { get; } = new RoutingState();

    public string SelectedFolder { get; set; } = String.Empty;
    public string SelectedFile { get; set; } = String.Empty;  

    public ReactiveCommand<Unit, IRoutableViewModel> GoFileExp {get;}
    public MainWindowViewModel()
    {
        GoFileExp = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new FileViewModel(this)));
    }


    
#pragma warning restore CA1822 // Mark members as static

}
