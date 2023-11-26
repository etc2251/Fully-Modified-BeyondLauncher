using Beyond_Launcher.pages;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beyond_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public void pooper()
        {
            Background = Globals.imageBrush;
        }

        public MainWindow()
        {
            InitializeComponent();
            //      Globals.snackbar = gooberfans;
            Background = new ImageBrush(ImageSourceFromBitmap(Properties.Resource1.Fortnite_season_6_floating_island));
    
            _NavigationFrame.Navigate(new Main());

            Globals.navframe = _NavigationFrame;

        }

        

        private void NavigationItem_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Main());
        }
        public void WaitForProcessToClose(string processName)
        {
            while (Process.GetProcessesByName(processName).Length != 0)
            {
                Thread.Sleep(100);
            }
        }
        private void NavigationItem_Click_1(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Settings());
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


        private void NavigationItem_Click_2(object sender, RoutedEventArgs e)
        {
            /*  Download down = new Download();
              down.Show();*/
            _NavigationFrame.Navigate(new Mods());
        }

    }
}
