using FortClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Beyond_Launcher.pages
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Page
    {
        public Loading()
        {
            InitializeComponent();
        }

        public void WaitForProcessToClose(string processName)
        {
            while (Process.GetProcessesByName(processName).Length != 0)
            {
                Thread.Sleep(100);
            }
        }
        public void SafeKillProcess(string processName)
        {
            try
            {
                Process[] processesByName = Process.GetProcessesByName(processName);
                for (int i = 0; i < processesByName.Length; i++)
                {
                    processesByName[i].Kill();
                }
            }
            catch
            {
            }
        }

        public bool ProcessIsOpen(string processName)
        {

            Process[] processesByName = Process.GetProcessesByName(processName);
            if (processesByName.Length > 0)
                return true;
            else
                return false;


        }

        public void ShowCustomMessageBox(string title, string content, RoutedEventHandler Event = null, string leftButtonContent = "")
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content
            };

            //   uiMessageBox.IsPrimaryButtonEnabled

            if (leftButtonContent != "")
                uiMessageBox.ButtonLeftName = leftButtonContent;

            if (Event != null)
                uiMessageBox.ButtonRightClick += Event;
            uiMessageBox.ButtonLeftClick += Event;

            var result = uiMessageBox.ShowDialog();
        }
        public void uragoofergamer()
        {
            Dispatcher.Invoke(new Action(() => {
                Globals.navframe.Navigate(new Main());
            }));
        }
        private Thread sillygoofer;
        private void Page_Initialized(object sender, EventArgs e)
        {
            bool bStarted = false;
        

            //  Globals.navframe.Navigate(new Main());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (; ; )
            {
                if (ProcessIsOpen("FortniteClient-Win64-Shipping") || ProcessIsOpen("Beyond"))
                {
                    SafeKillProcess("EpicGamesLauncher");
                    SafeKillProcess("EpicWebHelper");
                    SafeKillProcess("CrashReportClient");
                    SafeKillProcess("FortniteLauncher");
                    SafeKillProcess("FortniteClient-Win64-Shipping");
                    SafeKillProcess("Beyond");
                    SafeKillProcess("FortniteClient-Win64-Shipping_BE");
                    SafeKillProcess("BeyondClient-Win64-Shipping");
                    Globals.navframe.Navigate(new Main());
                    break;
                }

            }
           
        }
    }
}
