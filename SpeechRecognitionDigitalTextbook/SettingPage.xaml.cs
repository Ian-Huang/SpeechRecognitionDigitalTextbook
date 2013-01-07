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
    public partial class SettingPage : Page
    {
        public class ClassListData
        {
            public string ClassListName { set; get; }

            public ClassListData()
            {
            }
            public ClassListData(string name)
            {
                this.ClassListName = name;
            }
        }


        #region Window Event

        public SettingPage()
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

            //CreateButton position
            Canvas.SetLeft(this.CreateButton, 0);
            Canvas.SetTop(this.CreateButton, e.NewSize.Height - this.CreateButton.ActualHeight - 10);

            //DeleteImage position
            Canvas.SetLeft(this.DeleteImage, this.CreateButton.ActualWidth + 5);
            Canvas.SetTop(this.DeleteImage, e.NewSize.Height - this.DeleteImage.ActualHeight - 10);

            //ListText position & Width Height
            Canvas.SetLeft(this.ListText, (this.ClassList.Width - this.ListText.ActualWidth) / 2);
            Canvas.SetTop(this.ListText, this.TopRectangle.Height);
            this.ClassList.Width = 200;
            this.ClassList.Height = Math.Abs(Canvas.GetTop(this.ClassList) - Canvas.GetTop(this.CreateButton)) - 10;
        }

        private void BackImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("HomeMenu.xaml", UriKind.Relative));
        }

        List<ClassListData> dataList = new List<ClassListData>();
        int num = 1;
        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: 在此新增事件處理常式執行項目。
            ClassListData data = new ClassListData("class" + num.ToString());
            
            dataList.Add(data);
            num++;
            this.ClassList.ItemsSource = null;
            this.ClassList.ItemsSource = dataList;
        }

        #endregion       

        

        
 
    }
}
