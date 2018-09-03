using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace AsyMonitor
{
    /// <summary>
    /// MyScreenParam.xaml 的交互逻辑
    /// </summary>
    public partial class MyScreenParam : UserControl
    {
        ScreenParam screenParam;

        public ScreenParam ScreenParam { get => screenParam; set => screenParam = value; }

        public MyScreenParam(ScreenParamReader reader)
        {
            InitializeComponent();
            ScreenParam = new ScreenParam(reader);
            this.topGrid.DataContext = ScreenParam;
            InitializeGroupCombo();

        }

        private void InitializeGroupCombo()
        {
            List<Group> list = new List<Group>();
            SqlParameter[] paras = null;
            DataTable dataTable = SqlHelper.ExecuteDataTable("SELECT Base_GroupID, Base_GroupName, Base_AuthID, Base_IsDel,Base_Level," +
                " Base_Tel,Base_Addr,Base_Memo FROM General_Group WHERE Base_GroupID <> '-1' AND Base_IsDel <> '1' and Base_Level=0", paras);
            foreach (DataRow dt in dataTable.Rows)
            {
                Group group = new Group { AuthId = dt["Base_AuthID"].ToString(), GroupName = dt["Base_GroupName"].ToString() };
                list.Add(group);
            }
            this.groupComboBox.ItemsSource = list;
        }

        public void Save()
        {
            ScreenParam.Save();
        }

        private void ButtonSpinner_Spin_Id(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            if (e.Direction == SpinDirection.Increase)
                ScreenParam.Id++;
            else
                ScreenParam.Id--;
            ScreenParam.Id = Math.Min(ScreenParam.Id, 65535);
            ScreenParam.Id = Math.Max(ScreenParam.Id, 1);
        }

        private void ButtonSpinner_Spin_Width(object sender, SpinEventArgs e)
        {
            if (e.Direction == SpinDirection.Increase)
                ScreenParam.Width += 16;
            else
                ScreenParam.Width -= 16;
            ScreenParam.Width = Math.Min(ScreenParam.Width, 2048);
            ScreenParam.Width = Math.Max(ScreenParam.Width, 64);
        }

        private void ButtonSpinner_Spin_High(object sender, SpinEventArgs e)
        {
            if (e.Direction == SpinDirection.Increase)
                ScreenParam.High += 16;
            else
                ScreenParam.High -= 16;
            ScreenParam.High = Math.Min(ScreenParam.High, 512);
            ScreenParam.High = Math.Max(ScreenParam.High, 16);
        }

        private void timeCtrl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ScreenParam.UpdateTime = this.timeCtrl.Text;
        }

        private void ipAddr_KeyDown(object sender, KeyEventArgs e)
        {
            bool shiftKey = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;//判断shifu键是否按下
            if (shiftKey == true)                  //当按下shift
            {
                e.Handled = true;//不可输入
            }
            else                           //未按shift
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Decimal))
                {
                    e.Handled = true;//不可输入
                }
            }
        }

        private void ipPort_KeyDown(object sender, KeyEventArgs e)
        {
            bool shiftKey = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;//判断shifu键是否按下
            if (shiftKey == true)                  //当按下shift
            {
                e.Handled = true;//不可输入
            }
            else                           //未按shift
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back))
                {
                    e.Handled = true;//不可输入
                }
            }
        }

        private void convertTime_KeyDown(object sender, KeyEventArgs e)
        {
            bool shiftKey = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;//判断shifu键是否按下
            if (shiftKey == true)                  //当按下shift
            {
                e.Handled = true;//不可输入
            }
            else                           //未按shift
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back))
                {
                    e.Handled = true;//不可输入
                }
            }
        }
    }
}
