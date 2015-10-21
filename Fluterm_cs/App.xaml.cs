using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fluterm_cs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartupCmd(object sender, StartupEventArgs e)
        {
            Console.WriteLine("ApplicationStartupCmd");
        }

        private void ApplicationExitCmd(object sender, ExitEventArgs e)
        {
            Console.WriteLine("ApplicationExitCmd");
        }
    }
}
