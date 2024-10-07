using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace HexPad.Models
{
    public class FileNodes
    {
        public ObservableCollection<FileNodes>? SubNodes {get;}
        public string Title { get;}

        public FileNodes(string title)
        {
            Title = title;
        }

        public FileNodes(string title, ObservableCollection<FileNodes> subNodes)
        {
            Title = title;
            SubNodes = subNodes;
        }
    }
}