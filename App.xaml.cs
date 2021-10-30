using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] CommandLineArgs { get; private set; }
        private void Application_Startup( object sender , StartupEventArgs e )
        {
            if ( e.Args.Length == 0 )
                return;
            CommandLineArgs = e.Args;
        }
    public App()
    {
    
    }

        private void App_DispatcherUnhandledException( object sender , DispatcherUnhandledExceptionEventArgs e )
        {
        }
    }
}
