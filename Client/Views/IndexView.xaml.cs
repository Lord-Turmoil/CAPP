using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for IndexView.xaml
    /// </summary>
    public partial class IndexView : UserControl
    {
        public IndexView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            var psi = new ProcessStartInfo {
                FileName = link.NavigateUri.AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
