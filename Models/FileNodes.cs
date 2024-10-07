using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

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

        public FileNodes(string folder, char a) // This char is here just so i can overload
        {                                       // this contructor (I beg forgiveness from my professors)

            foreach(string file in Directory.GetFiles(folder))
            {
                Console.WriteLine(file);
                new FileNodes(file);
            }
        }
    }
}