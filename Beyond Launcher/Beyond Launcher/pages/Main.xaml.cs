using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net;
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
using FortClass;
using System.Runtime.CompilerServices;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using Wpf.Ui.Common;
using static Beyond_Launcher.MainWindow;
using System.Windows.Media.Animation;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Beyond_Launcher.Properties;
using System.Windows.Media.Media3D;

namespace Beyond_Launcher.pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
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

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);


        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }


        public Main()
        {
            InitializeComponent();

            if (ProcessIsOpen("FortniteClient-Win64-Shipping"))
            {
                LaunchButton.Content = "Close";
            }

            Email.Text = Settings1.Default.Email;
            Password.Password = Settings1.Default.Password;


            //  Globals.snackbar.Show("silly");


            using (WebClient client = new WebClient())
            {
                string version = client.DownloadString("http://135.148.86.165:3551/lversion");

                if (version != "1.9")
                {
                    /*MessageBox.Show("You are on the incorrect launcher version! Please update it in #downloads");
                    Environment.Exit(0);*/
                    Update update = new Update();
                    update.ShowDialog();
                }
                else
                {
                //    Footer.Content = $"Version v{version} || Launcher by Twin1dev";
                }

            }


        //  kevin.Source = ImageSourceFromBitmap(Resource1.fortnite_s6_jpg);
          beynd.Source = ImageSourceFromBitmap(Resource1.Beyond_B_purple_logo);

            /*            if (Properties.Settings1.Default.Email != "" || Properties.Settings1.Default.Password != "")
                        {
                            Email.Text = Properties.Settings1.Default.Email;
                            Password.Password = Properties.Settings1.Default.Password;
                        }*/

        }
        private void setlaunchtext(string text)
        {
            LaunchButton.Content = text;
        }
        private ISnackbarService snackbarService;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)LaunchButton.Content == "Close")
            {
                SafeKillProcess("EpicGamesLauncher");
                SafeKillProcess("EpicWebHelper");
                SafeKillProcess("CrashReportClient");
                SafeKillProcess("FortniteLauncher");
                SafeKillProcess("FortniteClient-Win64-Shipping");
                SafeKillProcess("Beyond");
                SafeKillProcess("EasyAntiCheat_EOS");
                LaunchButton.Content = "Launch";
                Thread.Sleep(1500);
                return;


            }
            Properties.Settings1.Default.Email = Email.Text;
            Properties.Settings1.Default.Password = Password.Password;
            Properties.Settings1.Default.Save();

            LaunchButton.Content = "Checking for any bans";

       

            //  Globals.imageBrush = new SolidColorBrush(Color.FromRgb(15, 15, 15));
            bool hwidCheck = Anticheat.hasHwid(Properties.Settings1.Default.Email);

            if (hwidCheck)
            {
                SafeKillProcess("EpicGamesLauncher");
                SafeKillProcess("EpicWebHelper");
                SafeKillProcess("CrashReportClient");
                SafeKillProcess("FortniteLauncher");
                SafeKillProcess("FortniteClient-Win64-Shipping");
                SafeKillProcess("Beyond");
                SafeKillProcess("EasyAntiCheat_EOS");
                ShowCustomMessageBox("Banned.", "You are banned from beyond.", MsgBoxClose, "lzzz");
                Environment.Exit(0);
            }

           // LaunchButton.Content = "Launching";


            Thread thr = new Thread(() =>
            {

                if (Properties.Settings1.Default.Path == "")
                {
                    MessageBox.Show("Path is Empty!");
                    return;
                }




             //  Mods.DownloadRequiredPaks();

    
                if (Anticheat.Scan())
                {
                    SafeKillProcess("EpicGamesLauncher");
                    SafeKillProcess("EpicWebHelper");
                    SafeKillProcess("CrashReportClient");
                    SafeKillProcess("FortniteLauncher");
                    SafeKillProcess("FortniteClient-Win64-Shipping");
                    SafeKillProcess("Beyond");
                    SafeKillProcess("EasyAntiCheat_EOS");
                    // ShowCustomMessageBox("Cheater Found!", "Cheating Paks have been found!");
                    // Sometimes it doesnt close.
                    SafeKillProcess("EpicGamesLauncher");
                    SafeKillProcess("EpicWebHelper");
                    SafeKillProcess("CrashReportClient");
                    SafeKillProcess("FortniteLauncher");
                    SafeKillProcess("FortniteClient-Win64-Shipping");
                    SafeKillProcess("Beyond");
                    SafeKillProcess("EasyAntiCheat_EOS");
                    Environment.Exit(0);
                }








                SafeKillProcess("EpicGamesLauncher");
                SafeKillProcess("EpicWebHelper");
                SafeKillProcess("CrashReportClient");
                SafeKillProcess("FortniteLauncher");
                SafeKillProcess("FortniteClient-Win64-Shipping");
                SafeKillProcess("Beyond");
                SafeKillProcess("FortniteClient-Win64-Shipping_BE");
                SafeKillProcess("BeyondClient-Win64-Shipping");
                WaitForProcessToClose("FortniteClient-Win64-Shipping_BE");
                if (Directory.Exists(Properties.Settings1.Default.Path + "\\EasyAntiCheat"))
                {
                    Directory.Delete(Properties.Settings1.Default.Path + "\\EasyAntiCheat", true);
                }
                if (File.Exists(Properties.Settings1.Default.Path + "\\Beyond.exe"))
                {
                    File.Delete(Properties.Settings1.Default.Path + "\\Beyond.exe");
                }
                if (File.Exists(Properties.Settings1.Default.Path + "\\Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64\\GFSDK_Aftermath_Lib.x64.dll"))
                {
                    File.Delete(Properties.Settings1.Default.Path + "\\Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64\\GFSDK_Aftermath_Lib.x64.dll");
                }

                Fortnite.DownloadFile("http://135.148.86.165:3551/eac", Properties.Settings1.Default.Path + "\\EAC.zip");
                ZipFile.ExtractToDirectory(Properties.Settings1.Default.Path + "\\EAC.zip", Properties.Settings1.Default.Path);
                Fortnite.DownloadFile("http://135.148.86.165:3551/clientdll", Properties.Settings1.Default.Path + "\\Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64\\GFSDK_Aftermath_Lib.x64.dll");
                Thread.Sleep(2000);

             

                //  LaunchButton.IsEnabled = false;

                Fortnite.LaunchGame(Properties.Settings1.Default.Email, Properties.Settings1.Default.Password, Properties.Settings1.Default.Path, Directory.GetCurrentDirectory(), "", redirectdll: Directory.GetCurrentDirectory() + "\\Redirect.dll");
        //        uragoofergamer();
            });
            setlaunchtext("Launching");
            //});
            // thr.SetApartmentState(ApartmentState.STA);
            thr.Start();

            setlaunchtext("Close");
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings1.Default.Email = Email.Text;
            Properties.Settings1.Default.Save();
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

            Properties.Settings1.Default.Password = Password.Password;
            Properties.Settings1.Default.Save();
        }
        
        public void ShowCustomMessageBox(string title, string content, RoutedEventHandler Event, string leftButtonContent = "")
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content
            };

         //   uiMessageBox.IsPrimaryButtonEnabled

            if (leftButtonContent != "")
                uiMessageBox.ButtonLeftName = leftButtonContent;

            uiMessageBox.ButtonRightClick += MsgBoxClose;
            uiMessageBox.ButtonLeftClick += Event;

            var result = uiMessageBox.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowCustomMessageBox("ok", "ok", MsgBoxClose, "ok");




        }

        public void MsgBoxClose(object sender, EventArgs e)
        {
            var msgbox = (Wpf.Ui.Controls.MessageBox)sender;

            msgbox.Close();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string goon = wc.DownloadString("https://pastebin.com/raw/8ApEX5hr");

             //   newstext.Content = goon;

                if (Settings1.Default.Email != "")
                {
                   string mail = wc.DownloadString($"http://135.148.86.165:3551/getusernamebyemail/{Settings1.Default.Email}");

                   if (mail != "invalid")
                    {
                        helloplayer.Content = $"Hey {mail}!";
                    }
                }
            }

            
       
            

           
            // Create and configure opacity animation
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            // Create and configure slide down animation
            DoubleAnimation slideDownAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };

            // Create a translate transform for slide down animation
            TranslateTransform translateTransform = new TranslateTransform();
            MainGrid.RenderTransform = translateTransform;

            // Apply animations to grid
            MainGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideDownAnimation);
        }

        private void LaunchButton_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

    }
}
