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
    /// Setting.xaml 的互動邏輯
    /// </summary>
    public partial class ClassListPage : Page
    {

        #region Window Event

        public ClassListPage()
        {
            InitializeComponent();
        }

        //當畫面載入，執行此函式
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //載入Navigation Page的寬高
            double width = Application.Current.Windows[0].ActualWidth;
            double height = Application.Current.Windows[0].ActualHeight;
            this.Width = width;
            this.Height = height;
        }

        //當畫面Unload，執行此函式
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        //當畫面解析度改變，將UI位置改變
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        { 
            //TopRectangle width
            this.TopRectangle.Width = this.Width;

            //GameSettingText position
            Canvas.SetLeft(this.GameSettingText, (e.NewSize.Width - this.GameSettingText.ActualWidth) / 2);
            Canvas.SetTop(this.GameSettingText, (this.TopRectangle.Height - this.GameSettingText.ActualHeight) / 2);        

            //BackImage width & height
            this.BackImage.Width = this.BackImage.Height = this.TopRectangle.ActualHeight;           
        }

        private void BackImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("HomeMenu.xaml", UriKind.Relative));
        }

        #endregion       

        

        
 
    }
}
