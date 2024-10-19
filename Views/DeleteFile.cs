using Avalonia.Controls;
using Avalonia.VisualTree;
using DialogHostAvalonia;
using System;

namespace HexPad.Views
{
    public delegate void ResultEvent(DeleteFile sender, bool result);
    public partial class DeleteFile : UserControl
    {
        public event ResultEvent Result;

        public DialogOpenedEventArgs Args { get; set; }
        public DeleteFile()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Result?.Invoke(this, true);

        }

        private void No_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Result?.Invoke(this, false);
        }
    }
}