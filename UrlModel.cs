using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace WpfBrowser
{
    // 最適化
    //https://iyemon018.hatenablog.com/entry/2016/03/04/150330
    public enum URLListType
    {
        Hist,
        Fav,
    }

    public class UrlModel
    {
    // todo 
        //public virtual Icon Favicon { get; set; }
        public string Title{ get; set; }
        public string Url{ get; set; }
        public string Category{ get; set; }
        public URLListType URLType{ get; set; }
    }

    //public class MyContext : DbContext
    //{
    //    public DbSet<UrlModel> FavList{ get; set; }
    //    public DbSet<UrlModel> HistList{ get; set; }
    //    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    //    {
    //        optionsBuilder.UseSqlite(
    //            @"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True" );
    //    }
    //}
    public class DBLoader : IDisposable
    {
        private bool disposedValue;
        //ElasticClient Client;
        public DBLoader( )
        {
        }
        public static UrlModel MakeUrl( string title , string url , URLListType type)
        {
            var urlm = new UrlModel( );
            urlm.Title = title;
            urlm.Url = url;
            urlm.URLType = type;
            return urlm;
        }
        protected virtual void Dispose( bool disposing )
        {
            if ( !disposedValue )
            {
                if ( disposing )
                {
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        public void Dispose( )
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose( disposing: true );
            GC.SuppressFinalize( this );
        }
    }
}
