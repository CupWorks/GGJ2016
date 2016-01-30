using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Game.Client.UWA
{
    public sealed partial class HistoryView : UserControl
    {
        private bool IsListVisible { get; set; } = false;

        public HistoryView()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (IsListVisible)
            {
                VisibleButton.Content = "show";
                RootGrid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Pixel);
  
            }
            else
            {
                VisibleButton.Content = "hide";
                RootGrid.ColumnDefinitions[1].Width = new GridLength(200, GridUnitType.Pixel);
            }
            IsListVisible = !IsListVisible;
        }
    }
}
