using System;
using System.Linq;
using System.Windows;

namespace AsyMonitor
{
    /// <summary>
    /// FontSettings.xaml 的交互逻辑
    /// </summary>
    public partial class FontSettings : Window
    {
        private ScreenParam screen1Param;
        private ScreenParam screen2Param;

        public FontSettings(ScreenParam screen1Param, ScreenParam screen2Param)
        {
            InitializeComponent();
            this.screen1Param = screen1Param;
            this.screen2Param = screen2Param;
        }

        private void Button_Click_Screen1LetfTitle(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen1Param.LeftTitleFontSize = (int)fontDialog.Font.Size;
            screen1Param.LeftTitleFontName = fontDialog.Font.FontFamily.Name;
            screen1Param.LeftTitleFontColor = fontDialog.Color.ToArgb().ToString();

        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            screen1Param.SaveFont();
            screen2Param.SaveFont();
            MessageBox.Show("保存成功", "保存", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_Screen1RightTitle(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen1Param.RightTitleFontSize = (int)fontDialog.Font.Size;
            screen1Param.RightTitleFontName = fontDialog.Font.FontFamily.Name;
            screen1Param.RightTitleFontColor = fontDialog.Color.ToArgb().ToString();
        }

        private void Button_Click_Screen1LeftContext(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen1Param.LeftContextFontSize = (int)fontDialog.Font.Size;
            screen1Param.LeftContextFontName = fontDialog.Font.FontFamily.Name;
            screen1Param.LeftContextFontColor = fontDialog.Color.ToArgb().ToString();
        }

        private void Button_Click_Screen1RightContext(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen1Param.RightContextFontSize = (int)fontDialog.Font.Size;
            screen1Param.RightContextFontName = fontDialog.Font.FontFamily.Name;
            screen1Param.RightContextFontColor = fontDialog.Color.ToArgb().ToString();
        }

        private void Button_Click_Screen2LeftTitle(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen2Param.LeftTitleFontSize = (int)fontDialog.Font.Size;
            screen2Param.LeftTitleFontName = fontDialog.Font.FontFamily.Name;
            screen2Param.LeftTitleFontColor = fontDialog.Color.ToArgb().ToString();
        }

        private void Button_Click_Screen2RightTitle(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen2Param.RightTitleFontSize = (int)fontDialog.Font.Size;
            screen2Param.RightTitleFontName = fontDialog.Font.FontFamily.Name;
            screen2Param.RightTitleFontColor = fontDialog.Color.ToArgb().ToString();
        }

        private void Button_Click_Screen2LeftContext(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen2Param.LeftContextFontSize = (int)fontDialog.Font.Size;
            screen2Param.LeftContextFontName = fontDialog.Font.FontFamily.Name;
            screen2Param.LeftContextFontColor = fontDialog.Color.ToArgb().ToString();
        }

        private void Button_Click_Screen2RightContext(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            screen2Param.RightContextFontSize = (int)fontDialog.Font.Size;
            screen2Param.RightContextFontName = fontDialog.Font.FontFamily.Name;
            screen2Param.RightContextFontColor = fontDialog.Color.ToArgb().ToString();
        }
    }
}
