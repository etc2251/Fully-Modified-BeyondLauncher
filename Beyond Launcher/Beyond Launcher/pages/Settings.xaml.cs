using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Animation;

namespace Beyond_Launcher.pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.Multiselect = false;
                dialog.Title = "Select your Fortnite Folder!";
                dialog.IsFolderPicker = true;
                dialog.EnsurePathExists = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (!Directory.Exists(dialog.FileName + "\\FortniteGame"))
                    {
                        System.Windows.MessageBox.Show("This path does not have fortnite!");
                        return;

                    }
                    else
                    {
                        PathText.Text = dialog.FileName;
                        Properties.Settings1.Default.Path = dialog.FileName;
                        Properties.Settings1.Default.Save();
                    }
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Properties.Settings1.Default.Reset();
            Properties.Settings1.Default.Save();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
   /*         email.Text = Properties.Settings1.Default.Email;
            pass.Password = Properties.Settings1.Default.Password;*/
            PathText.Text = Properties.Settings1.Default.Path;
            
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
            translateTransform.BeginAnimation(TranslateTransform.XProperty, slideDownAnimation);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
/*            Properties.Settings1.Default.Email = email.Text;
            Properties.Settings1.Default.Save();*/
        }

        private void pass_TextChanged(object sender, TextChangedEventArgs e)
        {
       /*     Properties.Settings1.Default.Password = pass.Password;
            Properties.Settings1.Default.Save();*/
        }
    }
}
