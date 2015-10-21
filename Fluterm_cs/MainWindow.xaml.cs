using System;
using System.Collections.Generic;
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

namespace Fluterm_cs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnOpenCmd(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OnOpenCmd");
        }

        private void OnPrintCmd(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OnPrintCmd");
        }

        private void OnExitCmd(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OnExitCmd(RoutedEventArgs)");
            MainWindow1.Close();            
        }

        private void OnWindowClosingCmd(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //  To prevent a window from closing, you can set the Cancel property of the CancelEventArgs argument to true.
            Console.WriteLine("OnWindowClosingCmd");
        }

        private void OnWindowClosedCmd(object sender, EventArgs e)
        {
            Console.WriteLine("OnWindowClosedCmd");
        }

    }
}
