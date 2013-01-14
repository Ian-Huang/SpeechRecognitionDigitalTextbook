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
using Microsoft.Win32;

namespace SpeechRecognitionDigitalTextbook
{
    /// <summary>
    /// Setting.xaml 的互動邏輯
    /// </summary>
    public partial class SettingPage : Page
    {

        #region Window Event

        public SettingPage()
        {
            InitializeComponent();
        }

        //當畫面載入，執行此函式
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //載入Navigation Page的寬高
            this.Width = Application.Current.Windows[0].ActualWidth;
            this.Height = Application.Current.Windows[0].ActualHeight;

            //全域物件初始化            
            this.currentClassIndex = -1;
            this.classNum = 1;
            this.classList = new List<ClassData>();
            this.openFileDialog = new OpenFileDialog();
        }

        //當畫面Unload，執行此函式
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        //當畫面解析度改變，將UI位置改變
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 1280x720是版面標準比例，當版面尺寸改變，去get新的Scale，後續定UI會用到
            Point newScale = new Point(e.NewSize.Width / 1280, e.NewSize.Height / 720);

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

            this.ClassList.Width = 200;
            this.ClassList.Height = Math.Abs(Canvas.GetTop(this.ClassList) - Canvas.GetTop(this.CreateButton)) - 10;

            //ListText position & Width Height            
            Canvas.SetLeft(this.ListText, (this.ClassList.Width - this.ListText.ActualWidth) / 2);
            Canvas.SetTop(this.ListText, this.TopRectangle.Height);

        }

        //當"BackImage"被觸發，會回到上一頁
        private void BackImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("HomeMenu.xaml", UriKind.Relative));
        }


        //當"CreateButton"被觸發，新增新的課程
        private List<ClassData> classList { get; set; }
        private int classNum { get; set; }        
        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClassData data = new ClassData("未命名課程" + classNum.ToString());

            this.classList.Add(data);
            this.classNum++;
            this.ClassList.ItemsSource = null;
            this.ClassList.ItemsSource = classList;

            this.ClassNameTextbox.IsEnabled = true;         //開啟ClassNameTextbox 輸入功能
        }


        //當"DeleteImage"被觸發，刪除清單上被選取的課程
        private void DeleteImage_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (this.classList.Count == 1)
            //{
            //    MessageBox.Show("不可刪除全部課程");
            //    return;
            //}
            if (ClassList.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("是否刪除 <" + this.classList[ClassList.SelectedIndex].ClassName + "> 課程", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.classList.RemoveAt(this.ClassList.SelectedIndex);
                    this.ClassList.ItemsSource = null;
                    this.ClassList.ItemsSource = this.classList;
                }

                //當刪光全部課程，版面清空
                if (this.classList.Count == 0)
                {
                    this.ClassNameTextbox.Text = "";
                    this.ClassNameTextbox.IsEnabled = false;
                }
            }
        }

        //處理選取背景圖片拖放到Canvas上的事件
        private void GetBackgroundCanvas_Drop(object sender, DragEventArgs e)
        {
            //  獲得拖放圖片
            String[] DropFiles = (String[])(e.Data.GetData(DataFormats.FileDrop));
            if (DropFiles != null)
            {
                // 設置圖像
                try
                {
                    BitmapImage DropImage = new BitmapImage(new Uri(DropFiles[0]));
                    //DropImage.BeginInit();
                    //DropImage.UriSource = new Uri(DropFiles[0]);
                    //DropImage.EndInit();
                    this.ChooseBackgroundImage.Source = DropImage;
                }
                catch
                {
                    MessageBox.Show("圖片格式錯誤");
                }
            }
        }

        //當"ChooseBackgroundPicture" Button被觸發，選擇課程的背景圖
        private OpenFileDialog openFileDialog { get; set; }
        private void ChooseBackgroundPicture_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            this.openFileDialog.FileName = "";                                          // Default file name
            this.openFileDialog.DefaultExt = ".txt";                                    // Default file extension
            this.openFileDialog.Filter = "JPEG|*.JPG;*.JPEG;*.JPE; | PNG|*.PNG;*.PNS";  // Filter files by extension
            this.openFileDialog.Multiselect = true;                                     //是否可以選擇多檔案
            this.openFileDialog.Title = "選擇背景圖片";

            // Show open file dialog box
            Nullable<bool> result = this.openFileDialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                //將選取的圖片匯入背景image元件
                BitmapImage DropImage = new BitmapImage(new Uri(this.openFileDialog.FileName));
                this.ChooseBackgroundImage.Source = DropImage;
            }
        }

        //當ClassList的子項目被選取或改變，觸發此函式
        private int currentClassIndex { get; set; }             //紀錄上次選擇子項目的index
        private void ClassList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ClassList.ReleaseMouseCapture();           //釋放滑鼠
            if (ClassList.SelectedIndex != -1)
            {
                this.currentClassIndex = this.ClassList.SelectedIndex;
                
                //Load Class Data
                this.ClassNameTextbox.Text = this.classList[this.ClassList.SelectedIndex].ClassName;
            }
            else
            {
                if (this.currentClassIndex < this.classList.Count)
                    this.ClassList.SelectedIndex = this.currentClassIndex;
                else
                    this.ClassList.SelectedIndex = this.classList.Count - 1;
            }
            
        }

        //當"SaveClassData" Button被觸發，儲存課程資訊
        private void SaveClassDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassList.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("是否儲存 <" + this.classList[ClassList.SelectedIndex].ClassName + "> 課程", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //儲存課程資訊
                    this.classList[this.ClassList.SelectedIndex].ClassName = this.ClassNameTextbox.Text;
                    this.ClassList.ItemsSource = null;
                    this.ClassList.ItemsSource = this.classList;
                }
            }
        }

        #endregion

        
        public class ClassData
        {
            public string ClassName { set; get; }

            public ClassData()
            {
            }
            public ClassData(string name)
            {
                this.ClassName = name;
            }
        }

        
    }
}