using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;

namespace HexPad.ViewModels
{
    public class FileViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment {get;} = "FileViewModel";
        public IScreen HostScreen {get;}

        public FileViewModel(IScreen screen) => HostScreen = screen;
    }
}