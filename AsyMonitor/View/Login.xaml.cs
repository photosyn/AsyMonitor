using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace AsyMonitor
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        private string operCode;

        public string OperCode { get => operCode; set => operCode = value; }

        public Login()
        {
            SqlHelper.Init();   
            InitializeComponent();
            this.operGrid.DataContext = this;
            ShowOperCode();
        }

        private void ShowOperCode()
        {
            if(SqlHelper.Connected)
            {
                List<Group> list = new List<Group>();
                string sql = "SELECT Base_OperCode FROM General_Oper_User ORDER BY Base_OperCode";
                SqlParameter[] paras = null;
                DataTable dataTable = null;
                dataTable = SqlHelper.ExecuteDataTable(sql, paras);
                foreach (DataRow dt in dataTable.Rows)
                {
                    Group group = new Group { AuthId = dt["Base_OperCode"].ToString(), GroupName = dt["Base_OperCode"].ToString() };
                    list.Add(group);
                }
                this.operCodeCombBox.ItemsSource = list;
            }
        }

        private bool CheckUser(string user, string pwd)
        {
            SqlParameter[] paras = null;
            paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@user", user);
            paras[1] = new SqlParameter("@pwd", pwd);
            string sql = "SELECT * FROM General_Oper_User WHERE Base_OperCode = @user AND Base_Password = @pwd";
            DataTable dataTable = null;
            dataTable = SqlHelper.ExecuteDataTable(sql, paras);
            if(dataTable.Rows.Count > 0)
            {
                string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
                string stopDate = dataTable.Rows[0]["Base_StopDate"].ToString();
                int result = DateTime.Compare(DateTime.Now, Convert.ToDateTime(stopDate));
                if (result >= 0)
                {
                    MessageBox.Show("此用户使用期限已过,禁止登陆!", "温馨提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("登陆失败，请确认用户名密码是否正确", "温馨提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void IconButton_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IconButton_Click_Enter(object sender, RoutedEventArgs e)
        {
            if (SqlHelper.Connected)
            {
                if (CheckUser(OperCode, this.passwordBox.Password))
                {
                    MainWindow monitor = new MainWindow();
                    monitor.Show();
                    this.Close();
                }
                else
                {
                    this.operCodeCombBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("数据库连接失败，请先设置数据库连接", "温馨提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void IconButton_Click_DbCheck(object sender, RoutedEventArgs e)
        {
            DatabaseSetting databaseSetting = new DatabaseSetting();
            databaseSetting.ShowDialog();
        }
    }
}
