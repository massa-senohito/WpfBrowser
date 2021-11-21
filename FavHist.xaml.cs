using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBrowser
{
    /// <summary>
    /// FavHist.xaml の相互作用ロジック
    /// </summary>
    public partial class FavHistControl : UserControl
    {
        public FavHistControl( )
        {
            InitializeComponent( );
            IEnumerable<UrlModel> favs ; IEnumerable<UrlModel> hists;
            DB.FetchFavHistList( out favs , out hists );
            LoadFavHist( favs , hists );
            var app = (App)Application.Current;
            app.DispatcherUnhandledException += App_DispatcherUnhandledException;
            app.Exit += App_Exit;
        }

        private void App_Exit( object sender , ExitEventArgs e )
        {
            DB.Dispose( );
        }

        private void App_DispatcherUnhandledException( object sender , System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
        {
            DB.Dispose( );
        }

        MainWindow Main;
        DBLoader DB = new DBLoader();

        public void SetParent( MainWindow main )
        {
            Main = main;
            Fav.SetParent( this , main );
            Hist.SetParent( this , main );
        }

        public void AddHist( string title , string url )
        {
            DB.Add( DBLoader.MakeUrl( title , url , URLListType.Hist ) );
            HistDropDown.Content = $"History({Hist.UrlCount})";
            Hist.AddURL( url );
        }
        public void AddFav( string title , string url )
        {
            DB.Add( DBLoader.MakeUrl( title , url , URLListType.Fav ) );
            Fav.AddURL( url , title );
        }
        public void SetUrl(string url)
        {
            URLBox.Text = url;
        }
        public void LoadFavHist( IEnumerable<UrlModel> favs , IEnumerable<UrlModel> hists )
        {
            Hist.Load( hists );
            Fav.Load( favs );
        }

        private void URLBox_KeyDown( object sender , KeyEventArgs e )
        {
            if ( e.Key == Key.Enter )
            {
                Main.Navigate( URLBox.Text );
            }
        }

    }
}
