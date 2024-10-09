using Avalonia.Controls;
using Avalonia.VisualTree;
using System;

namespace HexPad
{
    public partial class DeleteFile : UserControl
    {
        public event Action<bool> Result;

        public DeleteFile()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Result?.Invoke(true);

        }

        private void No_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Result?.Invoke(false);
        }
    }
}