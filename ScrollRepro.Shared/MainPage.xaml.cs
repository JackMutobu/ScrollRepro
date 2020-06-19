using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ScrollRepro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
         ScrollViewer _gridScrollviewer;
        List<int> _numbers;
        int _max = 20;
        public MainPage()
        {
            this.InitializeComponent();
            _numbers = Enumerable.Range(0, _max).ToList();
            gridView.ItemsSource = _numbers;
            gridView.PointerEntered += GridView_PointerEntered;
            gridView.PointerExited += GridView_PointerExited;
            BtnLoadMore.Click += BtnLoadMore_Click;
        }

        private void BtnLoadMore_Click(object sender, RoutedEventArgs e)
        {
            _numbers.AddRange(Enumerable.Range(_max, _max + 20));
            _max += 20;
            gridView.ItemsSource = _numbers.ToArray();
          
        }

        private void GridView_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _gridScrollviewer = Scrolling(gridView);
        }

        private void GridView_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _gridScrollviewer = Scrolling(gridView);
        }

        private ScrollViewer Scrolling(DependencyObject depObj)
        {
            ScrollViewer sv = GetScrollViewer(depObj);
            if (sv != null)
            {
                TxtExtendHeight.Text = sv.ExtentHeight.ToString("0.##");
                TxtScrollableHeight.Text = sv.ScrollableHeight.ToString("0.##");
                TxtVerticalOffset.Text = sv.VerticalOffset.ToString("0.##");
            }
            return sv;
        }

        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer) return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

        private void ScrollViewFound_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
        }
    }
}
