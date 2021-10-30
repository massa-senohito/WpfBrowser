using System;
using System.Collections.Generic;
using System.Text;
using PlayingBlue.Web;
using System.Net;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;

namespace WpfBrowser
{
    class FaviconDownload
    {
        Favicon Icon;
        WebClient Client = new WebClient();
    public FaviconDownload(string url)
    {
            Icon = new Favicon(new Uri(url));
    }
    void Download()
    {
            Client.DownloadDataCompleted += Client_DownloadDataCompleted;
            Client.DownloadDataAsync( Icon.ImageUri );
    }

        private void Client_DownloadDataCompleted( object sender , DownloadDataCompletedEventArgs e )
        {
            var byt = e.Result;
            using ( var ms = new MemoryStream( byt) )
            {
                var bmp = new Bitmap( ms );
            }
        }
    }
}
