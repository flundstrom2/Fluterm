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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetSerialPortNames()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console.
            foreach(string port in ports)
            {
                Console.WriteLine(port);
            }
        }

        private void GetUSB()
        {
            Console.WriteLine("GetUSB Devices:");
            var usbDevices = GetUSBDevices();

            foreach (var usbDevice in usbDevices)
            {
                Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}, Caption: {3}, SystemName: {4}, SystemCreationClassName: {5}, CreationClassName: {6}, Status: {7}, Name: {8}, ErrorDescription: {9}, ClassCode: {10}, SubclassCode: {11}",
                    usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description,
                    usbDevice.Caption,
                    usbDevice.SystemName,
                    usbDevice.SystemCreationClassName,
                    usbDevice.CreationClassName,
                    usbDevice.Status,
                    usbDevice.Name,
                    usbDevice.ErrorDescription,
                    usbDevice.ClassCode,
                    usbDevice.SubclassCode);
            }
            Console.WriteLine("GetUSB Devices finished");

            Console.WriteLine("GetUSB Controllers:");
            var usbControllers = GetUSBControllers();

            foreach (var usbController in usbControllers)
            {
                Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}, Caption: {3}, SystemName: {4}, SystemCreationClassName: {5}, CreationClassName: {6}, Status: {7}, Name: {8}, ErrorDescription: {9}, Manufacturer: {10}",
                    usbController.DeviceID, usbController.PnpDeviceID, usbController.Description,
                    usbController.Caption,
                    usbController.SystemName,
                    usbController.SystemCreationClassName,
                    usbController.CreationClassName,
                    usbController.Status,
                    usbController.Name,
                    usbController.ErrorDescription,
                    usbController.Manufacturer);
            }
            Console.WriteLine("GetUSB finished");

            /*
GetUSB (Win32_USBHub):
Device ID: USB\VID_2109&PID_0812\7&9AC155&0&3, PNP Device ID: USB\VID_2109&PID_0812\7&9AC155&0&3, Description: Generiskt SuperSpeed USB-nav
Device ID: USB\ROOT_HUB30\4&37AF89EA&0&0, PNP Device ID: USB\ROOT_HUB30\4&37AF89EA&0&0, Description: USB-rotnav (xHCI)
Device ID: USB\VID_2109&PID_2812\6&148744D9&0&1, PNP Device ID: USB\VID_2109&PID_2812\6&148744D9&0&1, Description: Allmänt USB-nav
Device ID: USB\VID_2109&PID_2812\5&DC584B&0&1, PNP Device ID: USB\VID_2109&PID_2812\5&DC584B&0&1, Description: Allmänt USB-nav
Device ID: USB\VID_08BB&PID_2902\8&1035D58&0&2, PNP Device ID: USB\VID_08BB&PID_2902\8&1035D58&0&2, Description: BEHRINGER USB AUDIO 2.8.40
Device ID: USB\VID_05E3&PID_0608\7&230D7013&0&1, PNP Device ID: USB\VID_05E3&PID_0608\7&230D7013&0&1, Description: Allmänt USB-nav
Device ID: USB\ROOT_HUB20\4&194F9256&0, PNP Device ID: USB\ROOT_HUB20\4&194F9256&0, Description: USB-rotnav (hub)
Device ID: USB\VID_1D03&PID_001A\09101, PNP Device ID: USB\VID_1D03&PID_001A\09101, Description: USB-enhet (sammansatt)
Device ID: USB\VID_8087&PID_0024\5&1022DFB2&1&1, PNP Device ID: USB\VID_8087&PID_0024\5&1022DFB2&1&1, Description: Generic USB Hub
Device ID: USB\VID_1235&PID_0061\6&148744D9&0&2, PNP Device ID: USB\VID_1235&PID_0061\6&148744D9&0&2, Description: USB-enhet (sammansatt)
Device ID: USB\VID_046D&PID_C537\7&3889683&1&4, PNP Device ID: USB\VID_046D&PID_C537\7&3889683&1&4, Description: USB-enhet (sammansatt)
Device ID: USB\VID_2109&PID_2812\7&2A07D096&0&3, PNP Device ID: USB\VID_2109&PID_2812\7&2A07D096&0&3, Description: Allmänt USB-nav
Device ID: USB\VID_04F2&PID_B337\6&7C19C13&0&3, PNP Device ID: USB\VID_04F2&PID_B337\6&7C19C13&0&3, Description: USB-enhet (sammansatt)
Device ID: USB\VID_1A40&PID_0101\7&230D7013&0&3, PNP Device ID: USB\VID_1A40&PID_0101\7&230D7013&0&3, Description: Allmänt USB-nav
Device ID: USB\VID_1C75&PID_0206\8&1035D58&0&1, PNP Device ID: USB\VID_1C75&PID_0206\8&1035D58&0&1, Description: USB-enhet (sammansatt)
Device ID: USB\VID_2109&PID_0812\5&DC584B&0&5, PNP Device ID: USB\VID_2109&PID_0812\5&DC584B&0&5, Description: Generiskt SuperSpeed USB-nav
Device ID: USB\VID_05E3&PID_0610\6&148744D9&0&3, PNP Device ID: USB\VID_05E3&PID_0610\6&148744D9&0&3, Description: Allmänt USB-nav
Device ID: USB\VID_2109&PID_0812\6&348EE156&0&1, PNP Device ID: USB\VID_2109&PID_0812\6&348EE156&0&1, Description: Generiskt SuperSpeed USB-nav
Device ID: USB\VID_05E3&PID_0616\6&348EE156&0&3, PNP Device ID: USB\VID_05E3&PID_0616\6&348EE156&0&3, Description: Generiskt SuperSpeed USB-nav
Device ID: USB\VID_0781&PID_5580\AA010501150437250249, PNP Device ID: USB\VID_0781&PID_5580\AA010501150437250249, Description: USB-masslagringsenhet
Device ID: USB\VID_8087&PID_0024\5&1338FCC5&1&1, PNP Device ID: USB\VID_8087&PID_0024\5&1338FCC5&1&1, Description: Generic USB Hub
Device ID: USB\VID_1A40&PID_0101\6&148744D9&0&4, PNP Device ID: USB\VID_1A40&PID_0101\6&148744D9&0&4, Description: Allmänt USB-nav
Device ID: USB\VID_046D&PID_C22D\7&3889683&1&2, PNP Device ID: USB\VID_046D&PID_C22D\7&3889683&1&2, Description: 'Fluterm_cs.vshost.exe'
USB-enhet (sammansatt)
Device ID: USB\ROOT_HUB20\4&2ED3924F&0, PNP Device ID: USB\ROOT_HUB20\4&2ED3924F&0, Description: USB-rotnav (hub)
Device ID: USB\VID_1A40&PID_0101\8&2F26081&0&4, PNP Device ID: USB\VID_1A40&PID_0101\8&2F26081&0&4, Description: Allmänt USB-nav
GetUSB finished

Device ID: USB\VID_2109&PID_0812\7&9AC155&0&3, 
PNP Device ID: USB\VID_2109&PID_0812\7&9AC155&0&3, 
Description: Generiskt SuperSpeed USB-nav
Caption: Generiskt SuperSpeed USB-nav
SystemName: SNOWDOG
SystemCreationClassName: Win32_ComputerSystem 
CreationClassName: Win32_USBHub 
Status: OK 
Name: Generiskt SuperSpeed USB-nav 
ErrorDescription:  ClassCode:  SubclassCode: 

Device ID: USB\ROOT_HUB30\4&37AF89EA&0&0, 
PNP Device ID: USB\ROOT_HUB30\4&37AF89EA&0&0, 
Description: USB-rotnav (xHCI) 
Caption: USB-rotnav (xHCI) 
SystemName: SNOWDOG 
SystemCreationClassName: Win32_ComputerSystem 
CreationClassName: Win32_USBHub 
Status: OK 
Name: USB-rotnav (xHCI)
ErrorDescription:  ClassCode:  SubclassCode: 
...



GetUSB Controllers:
Device ID: PCI\VEN_8086&DEV_1E2D&SUBSYS_06471025&REV_04\3&11583659&0&D0, PNP Device ID: PCI\VEN_8086&DEV_1E2D&SUBSYS_06471025&REV_04\3&11583659&0&D0, Description: Intel(R) 7 Series/C216 Chipset Family USB Enhanced Host Controller - 1E2D, Caption: Intel(R) 7 Series/C216 Chipset Family USB Enhanced Host Controller - 1E2D, SystemName: SNOWDOG, SystemCreationClassName: Win32_ComputerSystem, CreationClassName: Win32_USBController, Status: OK, Name: Intel(R) 7 Series/C216 Chipset Family USB Enhanced Host Controller - 1E2D, ErrorDescription: , Manufacturer: Intel
Device ID: PCI\VEN_8086&DEV_1E26&SUBSYS_06471025&REV_04\3&11583659&0&E8, PNP Device ID: PCI\VEN_8086&DEV_1E26&SUBSYS_06471025&REV_04\3&11583659&0&E8, Description: Intel(R) 7 Series/C216 Chipset Family USB Enhanced Host Controller - 1E26, Caption: Intel(R) 7 Series/C216 Chipset Family USB Enhanced Host Controller - 1E26, SystemName: SNOWDOG, SystemCreationClassName: Win32_ComputerSystem, CreationClassName: Win32_USBController, Status: OK, Name: Intel(R) 7 Series/C216 Chipset Family USB Enhanced Host Controller - 1E26, ErrorDescription: , Manufacturer: Intel
Device ID: PCI\VEN_8086&DEV_1E31&SUBSYS_06471025&REV_04\3&11583659&0&A0, PNP Device ID: PCI\VEN_8086&DEV_1E31&SUBSYS_06471025&REV_04\3&11583659&0&A0, Description: USB xHCI-kompatibel värdstyrenhet, Caption: Intel(R) USB 3.0 eXtensible Host Controller - 0100 (Microsoft), SystemName: SNOWDOG, SystemCreationClassName: Win32_ComputerSystem, CreationClassName: Win32_USBController, Status: OK, Name: Intel(R) USB 3.0 eXtensible Host Controller - 0100 (Microsoft), ErrorDescription: , Manufacturer: Generic USB xHCI-värdstyrenhet
Device ID: BTHENUM\{8855C1D2-9BFE-4B96-BCBF-CBB9682C76BD}_LOCALMFG&0000\8&1BCA791E&0&000000000000_00000000, PNP Device ID: BTHENUM\{8855C1D2-9BFE-4B96-BCBF-CBB9682C76BD}_LOCALMFG&0000\8&1BCA791E&0&000000000000_00000000, Description: Bluetooth Hard Copy Cable Replacement Server, Caption: Bluetooth Hard Copy Cable Replacement Server, SystemName: SNOWDOG, SystemCreationClassName: Win32_ComputerSystem, CreationClassName: Win32_USBController, Status: OK, Name: Bluetooth Hard Copy Cable Replacement Server, ErrorDescription: , Manufacturer: Qualcomm Atheros Communications
GetUSB finished

*/

        }

        static List<USBControllerInfo> GetUSBControllers()
        {
            List<USBControllerInfo> controllers = new List<USBControllerInfo>();

            ManagementObjectCollection collection;

            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBController"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                controllers.Add(createUSBControllerInfo(device));
            }

            collection.Dispose();

            return controllers;
        }

        static List<USBHubInfo> GetUSBDevices()
        {
          List<USBHubInfo> devices = new List<USBHubInfo>();
          List<USBControllerInfo> controllers = new List<USBControllerInfo>();

          ManagementObjectCollection collection;

            // it should be queried "Win32_USBControllerDevice" and not "Win32_USBHub" in order to receive list of all usb devices. Then use "Dependent" property to get the device address string

          /*
class Win32_USBHub : CIM_USBHub
{
    uint16   Availability;
    string   Caption;
    uint8    ClassCode;
    uint32   ConfigManagerErrorCode;
    boolean  ConfigManagerUserConfig;
    string   CreationClassName;
    uint8    CurrentAlternateSettings;
    uint8    CurrentConfigValue;
    string   Description;
    string   DeviceID;
    boolean  ErrorCleared;
    string   ErrorDescription;
    boolean  GangSwitched;
    datetime InstallDate;
    uint32   LastErrorCode;
    string   Name;
    uint8    NumberOfConfigs;
    uint8    NumberOfPorts;
    string   PNPDeviceID;
    uint16   PowerManagementCapabilities[];
    boolean  PowerManagementSupported;
    uint8    ProtocolCode;
    string   Status;
    uint16   StatusInfo;
    uint8    SubclassCode;
    string   SystemCreationClassName;
    string   SystemName;
    uint16   USBVersion;
};

class Win32_USBControllerDevice : CIM_ControlledBy
{
    uint16                AccessState;
    CIM_USBController REF Antecedent;
    CIM_LogicalDevice REF Dependent;
    uint32                NegotiatedDataWidth;
    uint64                NegotiatedSpeed;
    uint32                NumberOfHardResets;
    uint32                NumberOfSoftResets;
};

class Win32_USBController : CIM_USBController
{
  uint16   Availability;
  string   Caption;
  uint32   ConfigManagerErrorCode;
  boolean  ConfigManagerUserConfig;
  string   CreationClassName;
  string   Description;
  string   DeviceID;
  boolean  ErrorCleared;
  string   ErrorDescription;
  datetime InstallDate;
  uint32   LastErrorCode;
  string   Manufacturer;
  uint32   MaxNumberControlled;
  string   Name;
  string   PNPDeviceID;
  uint16   PowerManagementCapabilities[];
  boolean  PowerManagementSupported;
  uint16   ProtocolSupported;
  string   Status;
  uint16   StatusInfo;
  string   SystemCreationClassName;
  string   SystemName;
  datetime TimeOfLastReset;
};

           
class Win32_PnPEntity : CIM_LogicalDevice
{
  uint16   Availability;
  string   Caption;
  string   ClassGuid;
  string   CompatibleID[];
  uint32   ConfigManagerErrorCode;
  boolean  ConfigManagerUserConfig;
  string   CreationClassName;
  string   Description;
  string   DeviceID;
  boolean  ErrorCleared;
  string   ErrorDescription;
  string   HardwareID[];
  datetime InstallDate;
  uint32   LastErrorCode;
  string   Manufacturer;
  string   Name;
  string   PNPClass;
  string   PNPDeviceID;
  uint16   PowerManagementCapabilities[];
  boolean  PowerManagementSupported;
  boolean  Present;
  string   Service;
  string   Status;
  uint16   StatusInfo;
  string   SystemCreationClassName;
  string   SystemName;
};          
           
           */
          using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
              collection = searcher.Get();

          foreach (var device in collection)
          {
              devices.Add(createUSBHubInfo(device));
          }

          collection.Dispose();

          return devices;
        }

        private static USBHubInfo createUSBHubInfo(ManagementBaseObject device)
        {
            if (device == null) {
                return null;
            }
            return new USBHubInfo(
                        (string)device.GetPropertyValue("DeviceID"),
                        (string)device.GetPropertyValue("PNPDeviceID"),
                        (string)device.GetPropertyValue("Description"),
                        (string)device.GetPropertyValue("Caption"),
                        (string)device.GetPropertyValue("SystemName"),
                        (string)device.GetPropertyValue("SystemCreationClassName"),
                        (string)device.GetPropertyValue("CreationClassName"),
                        (string)device.GetPropertyValue("Status"),
                        (string)device.GetPropertyValue("Name"),
                        (string)device.GetPropertyValue("ErrorDescription"),

                        (string)device.GetPropertyValue("ClassCode"),
                        (string)device.GetPropertyValue("SubclassCode")
                        );
        }

        private static USBControllerInfo createUSBControllerInfo(ManagementBaseObject device)
        {
            if (device == null)
            {
                return null;
            }
            return new USBControllerInfo(
                        (string)device.GetPropertyValue("DeviceID"),
                        (string)device.GetPropertyValue("PNPDeviceID"),
                        (string)device.GetPropertyValue("Description"),
                        (string)device.GetPropertyValue("Caption"),
                        (string)device.GetPropertyValue("SystemName"),
                        (string)device.GetPropertyValue("SystemCreationClassName"),
                        (string)device.GetPropertyValue("CreationClassName"),
                        (string)device.GetPropertyValue("Status"),
                        (string)device.GetPropertyValue("Name"),
                        (string)device.GetPropertyValue("ErrorDescription"),

                        (string)device.GetPropertyValue("Manufacturer")
                        );
        }

        class USBHubInfo
        {
            public USBHubInfo(string deviceID, string pnpDeviceID, string description, string caption,
                string systemName, string systemCreationClassName, string creationClassName, string status, string name, string errorDescription,
                string classCode, string subclassCode)
            {
                this.DeviceID = deviceID;
                this.PnpDeviceID = pnpDeviceID;
                this.Description = description;
                this.Caption = caption;
                this.SystemName = systemName;
                this.SystemCreationClassName = systemCreationClassName;
                this.CreationClassName = creationClassName;
                this.Status = status;
                this.Name = name;
                this.ErrorDescription = errorDescription;
                this.ClassCode = classCode;
                this.SubclassCode = subclassCode;
            }
            public string DeviceID { get; private set; }
            public string PnpDeviceID { get; private set; }
            public string Description { get; private set; }
            public string Caption { get; private set; }
            public string SystemName { get; private set; }
            public string SystemCreationClassName { get; private set; }
            public string CreationClassName { get; private set; }
            public string Status { get; private set; }
            public string Name { get; private set; }
            public string ErrorDescription { get; private set; }
            public string ClassCode { get; private set; }
            public string SubclassCode { get; private set; }
        }

        class USBControllerInfo
        {
            public USBControllerInfo(string deviceID, string pnpDeviceID, string description, string caption,
                string systemName, string systemCreationClassName, string creationClassName, string status, string name, string errorDescription,
                string manufacturer)
            {
                this.DeviceID = deviceID;
                this.PnpDeviceID = pnpDeviceID;
                this.Description = description;
                this.Caption = caption;
                this.SystemName = systemName;
                this.SystemCreationClassName = systemCreationClassName;
                this.CreationClassName = creationClassName;
                this.Status = status;
                this.Name = name;
                this.ErrorDescription = errorDescription;
                this.Manufacturer = manufacturer;
            }
            public string DeviceID { get; private set; }
            public string PnpDeviceID { get; private set; }
            public string Description { get; private set; }
            public string Caption { get; private set; }
            public string SystemName { get; private set; }
            public string SystemCreationClassName { get; private set; }
            public string CreationClassName { get; private set; }
            public string Status { get; private set; }
            public string Name { get; private set; }
            public string ErrorDescription { get; private set; }
            public string Manufacturer { get; private set; }
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


            GetSerialPortNames();

            GetUSB();

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

            s += " RichTextBox_LogArea.MinHeight: " + RichTextBox_LogArea.MinHeight + " RichTextBox_LogArea.Height: ";
            if (RichTextBox_LogArea.MinHeight <= RichTextBox_LogArea.Height + winDeltaH)
            {
                RichTextBox_LogArea.Height += winDeltaH;
            }

            s += "\r";
            //RichTextBox_LogArea.AppendText(s);
            Console.Write(s);
        }

        private void RichTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            string s;

            double rtbDeltaW = e.NewSize.Width - e.PreviousSize.Width;
            double rtbDeltaH = e.NewSize.Height - e.PreviousSize.Height;

            s = "RichTextBox OnSizeChanged: ";
            s += e.PreviousSize;
            s += " -> ";
            s += e.NewSize;
            s += " (" + rtbDeltaW + "," + rtbDeltaH + ")";
            s += "\r";

            //RichTextBox_LogArea.AppendText(s);
            Console.Write(s);
        }

    }
}
