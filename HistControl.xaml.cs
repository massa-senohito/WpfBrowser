using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfBrowser
{
    /// <summary>
    /// HistControl.xaml の相互作用ロジック
    /// </summary>
    public partial class URLListControl : UserControl
    {

        public URLListType ListType { get; set; }
        public URLListControl( )
        {
            InitializeComponent( );
            Mouse.AddMouseDownHandler( URLList , URLList_Click );
            URLList.SelectionChanged += URLList_SelectionChanged;
        }

        private void URLList_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            var item = e.AddedItems[0] as ListViewItem;
            Main.Navigate( item.Content as string);
            Main.Focus( );
        }

        // スクロールを考慮していない
        string ClickedIndex( MouseButtonEventArgs e )
        {
            var pos = e.GetPosition( URLList);
            var height = 24;
            var index = (int)pos.Y / height ;
            var border = (Border)VisualTreeHelper.GetChild( URLList , 0 );
            var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild( border, 0 );
            index += (int)scrollViewer.VerticalOffset;
            if ( index <= URLList.Items.Count )
            {
                var item = URLList.Items[ index ] as ListViewItem;
                item.Foreground = Brushes.Green;
                return item.Content as string;
            }
            return null;
        }
        private void URLList_Click( object sender , MouseButtonEventArgs e )
        {
            var url = ClickedIndex(e);
            if ( url == null )
            {
                return;
            }
            if ( e.MiddleButton == MouseButtonState.Pressed )
            {
                if ( ListType == URLListType.Hist )
                {
                    FavHist.AddFav( "" , url );
                }
            }
            Main.Focus( );
        }
        MainWindow Main;
        FavHistControl FavHist;
        public void SetParent( FavHistControl favHist , MainWindow main )
        {
            Main = main;
            FavHist = favHist;
        }

        public void Load( IEnumerable<UrlModel> urls)
        {
            foreach ( var item in urls )
            {
                AddURL( item.Url );
            }
        }
        public void AddURL(string url)
        {
            var item = new ListViewItem();
            item.Content = url;
            URLList.Items.Add( item );
        }
    }
}
