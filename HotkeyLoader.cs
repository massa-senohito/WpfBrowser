using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ke = System.Windows.Input.Key;
using MKe = System.Windows.Input.ModifierKeys;
using System.Windows;

namespace WpfBrowser
{
    class HotkeyLoader
    {
        class HotKey
        {
            public MKe ModKey { get; set; }
            public Ke Key { get; set; }
            public string Url { get; set; }
        }
        List<HotKey> HotKeys = new List<HotKey>();
        public string OnHotKey( MKe mkey , Ke key )
        {
            var hot = HotKeys.FirstOrDefault( h => h.ModKey == mkey && h.Key == key );
            if ( hot != null )
            {
                return hot.Url;
            }
            else return null;
        }
        public HotkeyLoader( string path )
        {
            var csv = File.ReadAllLines( path );
            foreach ( var line in csv )
            {
                var els = line.Split( ',' );
                var mkey = MKe.None;
                var key = Ke.None;
                var url = els.Last();
                for ( int i = 0 ; i < els.Length - 1 ; i++ )
                {
                    var tol = els[i].ToUpper();
                    if ( tol == "ALT" )
                    {
                        mkey |= MKe.Alt;
                        continue;
                    }
                    if ( tol == "SHIFT" )
                    {
                        mkey |= MKe.Shift;
                        continue;
                    }
                    if ( tol == "CTRL" )
                    {
                        mkey |= MKe.Control;
                        continue;
                    }
                    var res = Enum.TryParse<Ke>( tol ,out key );
                    if ( !res )
                    {
                        MessageBox.Show( tol + " is not a key" );
                    }
                }
                if ( mkey == MKe.None )
                {
                    MessageBox.Show( url + " is invalid key" );
                }
                else
                {
                    HotKeys.Add( new HotKey { ModKey = mkey , Key = key , Url = url } );
                }
            }
        }
    }
}
