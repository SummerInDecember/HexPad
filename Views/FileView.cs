using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using HexPad.Models;
using HexPad.ViewModels;
using ReactiveUI;

namespace HexPad.Views
{
    public partial class FileView : ReactiveUserControl<FileViewModel>
    {
        
        public FileView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }

    }
}