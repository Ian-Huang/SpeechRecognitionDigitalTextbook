using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpeechRecognitionDigitalTextbook
{
    /// <summary>
    /// HomeMenu.xaml 的互動邏輯
    /// </summary>
    public partial class HomeMenu : Page
    {
        public HomeMenu()
        {
            InitializeComponent();
        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double width = Application.Current.Windows[0].ActualWidth;
            double height = Application.Current.Windows[0].ActualHeight;
            this.Width = width;
            this.Height = height;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(this.StartGameButton, (e.NewSize.Width - this.StartGameButton.Width) / 2);
            Canvas.SetTop(this.StartGameButton, e.NewSize.Height * 0.35f);

            Canvas.SetLeft(this.GameSettingButton, (e.NewSize.Width - this.GameSettingButton.Width) / 2);
            Canvas.SetTop(this.GameSettingButton, e.NewSize.Height * 0.5f);

            Canvas.SetLeft(this.ExitGameButton, (e.NewSize.Width - this.ExitGameButton.Width) / 2);
            Canvas.SetTop(this.ExitGameButton, e.NewSize.Height * 0.65f);
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("ClassListPage.xaml", UriKind.Relative));
        }

        private void GameSettingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SettingPage.xaml", UriKind.Relative));
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
