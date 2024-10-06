using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexPad.ViewModels;
using HexPad.Views;
using ReactiveUI;

namespace HexPad
{
    public class AppViewLocator : ReactiveUI.IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
        {
            FileViewModel context => new FileView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };

    }
}