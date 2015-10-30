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
using System.Diagnostics;
using System.IO.Ports;
using System.Management; // need to add System.Management to your project references.

namespace Fluterm_cs
{

	enum ft_hex_encoding_e
    {
        FT_HEX_ENCODING_NONE,
        FT_HEX_ENCODING_0x,
        FT_HEX_ENCODING_BACKSLASH
    };

    public class Fluterm
    {
        private ft_hex_encoding_e m_hex_encoding = ft_hex_encoding_e.FT_HEX_ENCODING_NONE;

        public Fluterm()
        {
         //   m_hex_encoding = FT_HEX_ENCODING_NONE;
        }

        void SetHexEncoding (ft_hex_encoding_e encoding) {
            m_hex_encoding = encoding;
        }

        ft_hex_encoding_e GetHexEncoding() {
            return m_hex_encoding;
        }
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Fluterm_cs.USBUtil mUsbUtil = new Fluterm_cs.USBUtil();
        private Fluterm_cs.COMUtil mComUtil = new Fluterm_cs.COMUtil();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            Debug.Print("OnInitialized");
            // ComboBox_ComPort.Items().Add("OnInitialized"); ' Adderas efter att ComboBoxed populerats.

            // 'ProgressBar1.Visible = True;
            // ' Set Minimum to 1 to represent the first file being copied.
            //ProgressBar1.Minimum = 1;
            // ' Set Maximum to the total number of files to copy.
            //ProgressBar1.Maximum = 100;
            // ' Set the initial value of the ProgressBar.
            //ProgressBar1.Value = 25;
            // ' Set the Step property to a value of 1 to represent each file being copied.
            // ' ProgressBar1.Step = 1;


            //mComUtil.GetSerialPortNames();

            //mUsbUtil.GetUSB();

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

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            string s;

            double winDeltaW = e.NewSize.Width - e.PreviousSize.Width;
            double winDeltaH = e.NewSize.Height - e.PreviousSize.Height;

            s = "Window OnSizeChanged: ";
            s += e.PreviousSize;
            s += " -> ";
            s += e.NewSize;
            s += " (" + winDeltaW + "," + winDeltaH + ")";

            s += LogArea_GetSizeString();
            if (e.PreviousSize.Width != 0 && e.PreviousSize.Height != 0)
            {
                if (LogArea.MinHeight > LogArea.ActualHeight + winDeltaH)
                {
                    double requestedHeight = LogArea.ActualHeight + winDeltaH;
                    double winDeltaHOld = winDeltaH;
                    double winDeltaHNew = winDeltaHOld + LogArea.MinHeight - requestedHeight;
                    winDeltaH = winDeltaHNew;
                    double winNewHeight = e.PreviousSize.Height + winDeltaHNew;
                    Height = winNewHeight;
                    s += " (Delta " + winDeltaHOld + " would be less than LogArea's minHeight, adjusting delta to " + winDeltaHNew + " and adjusting size of window to " + Height + ")";
                    LogArea.Height = LogArea.MinHeight;
                    s += " (Updating LogArea height to " + LogArea.Height + ") ";
                }
                else
                {
                    double newHeight = LogArea.ActualHeight + winDeltaH;
                    s += " (Updating LogArea height to " + newHeight + ") ";
                    LogArea.Height = newHeight; // This triggers LogArea_SizeChanged
                }
            }
            else
            {
                s += " (Initial configuration of size; ignoring it) ";
            }

            s += "\r";
            //Console.Write(s);
        }

        private string LogArea_GetSizeString()
        {
            string s = "";
            // Height and Width returns NaN.
            s += "LogArea.MinHeight: " + LogArea.MinHeight + " LogArea.ActualHeight: " + LogArea.ActualHeight;
            s += " LogArea.MinWidth: " + LogArea.MinWidth + " LogArea.ActualWidth: " + LogArea.ActualWidth;
            return s;
        }

        private void LogArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            string s;

            double rtbDeltaW = e.NewSize.Width - e.PreviousSize.Width;
            double rtbDeltaH = e.NewSize.Height - e.PreviousSize.Height;

            s = "LogArea OnSizeChanged: ";
            s += e.PreviousSize;
            s += " -> ";
            s += e.NewSize;
            s += " (" + rtbDeltaW + "," + rtbDeltaH + ") ";
            s += LogArea_GetSizeString();

            if (e.PreviousSize.Width == 0 && e.PreviousSize.Height == 0)
            {
                double winNewHeight = (ActualHeight - e.NewSize.Height) + LogArea.MinHeight;
                double winNewWidth = (ActualWidth - e.NewSize.Width) + LogArea.MinWidth;
                s += " (Initial configuration of size; setting Windows MinHeight, Minwidth to (" + winNewHeight + ", " + winNewWidth + ")) ";
                MinHeight = winNewHeight;
                MinWidth = winNewWidth;

                LogArea.Text =
                    " 1: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 2: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 3: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 4: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 5: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 6: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 7: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 8: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    " 9: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "10: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "11: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "12: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "13: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "14: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "15: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "16: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "17: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "18: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "19: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "20: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "21: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "22: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "23: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "24: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "25: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "26: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "27: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "28: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "29: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "30: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "31: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n" +
                    "32: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n";
            }

            s += "\r";

            //Console.Write(s);
        }

        private void TextInput_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Console.WriteLine("TextInput_TargetUpdated");
        }

        private void TextInput_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Console.WriteLine("TextInput_SourceUpdated");
        }

        private void TextInput_TextInput(object sender, TextCompositionEventArgs e)
        {
            Console.WriteLine("TextInput_TextInput");
        }

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // For each character
            Console.WriteLine("TextInput_TextChanged");
        }

        private void Baudrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When selection changed. Only occationally for manual text input (!)
            Console.WriteLine("Baudrate_SelectionChanged");
        }

        private void Baudrate_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Console.WriteLine("Baudrate_SourceUpdated");
        }

        private void Baudrate_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Console.WriteLine("Baudrate_TargetUpdated");
        }

        private void Baudrate_TextInput(object sender, TextCompositionEventArgs e)
        {
            Console.WriteLine("Baudrate_TextInput");
        }
    }
}
