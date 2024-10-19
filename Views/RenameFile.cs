using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Avalonia.Controls;
using DialogHostAvalonia;

namespace HexPad.Views
{
    public delegate void NewName(RenameFile sender, string? newName);
    public partial class RenameFile : UserControl
    {
        public event NewName GiveNewName;
        public DialogOpenedEventArgs Args { get; set; }
        public RenameFile()
        {
            InitializeComponent();
        }

        private void Rename(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            GiveNewName.Invoke(this, NameGiven.Text);
        }


    }
}