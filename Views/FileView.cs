using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using HexPad.ViewModels;
using ReactiveUI;

namespace HexPad.Views
{
    public class FileView : ReactiveUserControl<FileViewModel>
    {
        public FileView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}