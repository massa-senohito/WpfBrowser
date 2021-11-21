using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using System.Windows.Threading;
using Ke = System.Windows.Input.Key;
using MKe = System.Windows.Input.ModifierKeys;
namespace WpfBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Microsoft.Web.WebView2.Core.CoreWebView2Deferral Deferral;
        FaviconDownload FavDownLoader;
        HotkeyLoader hot = new HotkeyLoader("urlShortcut.txt");
        public MainWindow( )
        {
            InitializeComponent( );
            Browser.KeyDown += Browser_KeyDown;
            Browser.PreviewKeyDown += Browser_PreviewKeyDown;
            Browser.CoreWebView2InitializationCompleted += Browser_CoreWebView2InitializationCompleted;
            Browser.NavigationCompleted += Browser_NavigationCompleted;
            Browser.NavigationStarting += Browser_NavigationStarting;
            Closing += MainWindow_Closing;
            //var ex = Browser.CanExecuteJavascriptInMainFrame;
            //            var Alt = MKe.Alt;
            //var ctrl = MKe.Control;
            //var ht = HotkeyManager.Current;
            //ht.AddOrReplace( "google" , Ke.G , Alt , OnGoogle );
            Hist.SetParent( this );
            for ( int i = 0 ; i < hot.FavKey.Count ; i++ )
            {
                Hist.AddFav( hot.FavKey[i] , hot.FavUrl[i] );
            } 
            InitializeAsync( );
        }

        private void MainWindow_Closing( object sender , System.ComponentModel.CancelEventArgs e )
        {
        }

        private void _timer_Tick( object sender , EventArgs e )
        {
        }

        private void Browser_NavigationStarting( object sender , Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e )
        {
            string url = e.Uri;
            Hist.SetUrl( url );
        }

        private void Browser_NavigationCompleted( object sender , Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e )
        {
            string url = Browser.Source.ToString( );
            var title = Browser.CoreWebView2.DocumentTitle;

            Hist.AddHist( title , url );
            Title = title;
        }

        private void Browser_CoreWebView2InitializationCompleted( object sender , Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e )
        {
            Browser.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }

        private void CoreWebView2_NewWindowRequested( object sender , Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e )
        {
            if ( Keyboard.IsKeyDown( Ke.LeftShift ) )
            {

            }
            else
            {
                e.Handled = true;
                var info = new ProcessStartInfo( Environment.CurrentDirectory + "\\WpfBrowser.exe");
                info.Arguments = e.Uri;
                Process.Start( info );
            }
        }

        async void InitializeAsync( )
        {
            await Browser.EnsureCoreWebView2Async( null );
            string[ ] commandLineArgs = App.CommandLineArgs;
            if ( commandLineArgs?.Length > 0)
            {
                Navigate( commandLineArgs[0]);
            }
            else{
                Navigate( "https://www.google.com" );
            }
            //Browser.CoreWebView2.WebMessageReceived += UpdateAddressBar;
        }

        private void Browser_PreviewKeyDown( object sender , KeyEventArgs e )
        {
            Browser_KeyDown( sender , e );
        }
        public void Navigate( string url )
        {
            Browser.CoreWebView2.Navigate( url );
        }
        private void Browser_KeyDown( object sender , KeyEventArgs e )
        {
            var mod = e.KeyboardDevice.Modifiers;
            var url = hot.OnHotKey(mod , e.Key);
            if(url != null)
            {
                Browser.CoreWebView2.Navigate( url );
            }

//EO
//有料？
//Cef googleにログインしようとすると未対応ブラウザだとか
//webview2 edge sdkを入れる必要がある
        }
    }
}
