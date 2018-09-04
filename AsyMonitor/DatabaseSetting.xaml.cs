using System;
using System.Collections.Generic;
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
    /// DatabaseSetting.xaml 的交互逻辑
    /// </summary>
    public partial class DatabaseSetting : Window
    {
        private string dbServer;
        private string dbName;
        private string dbUser;

        public string DbServer { get => dbServer; set => dbServer = value; }
        public string DbName { get => dbName; set => dbName = value; }
        public string DbUser { get => dbUser; set => dbUser = value; }

        public DatabaseSetting()
        {
            InitializeComponent();
            this.dbGrid.DataContext = this;
            LoadDbInfo();
        }

        private void LoadDbInfo()
        {
            string iniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini";
            DbServer = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLServer", "");
            DbName = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLDataBase", "");
            DbUser = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLSysUser", "");
            this.passwordBox.Password = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLSysPassword", "");
        }

        private void IconButton_Click_Save(object sender, RoutedEventArgs e)
        {
            string iniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini";
            if(DbServer.Length > 0 && DbName.Length > 0 && DbUser.Length > 0)
            {
                IniFile.INIWriteValue(iniFileName, "SYSCONFIG", "SQLServer", DbServer);
                IniFile.INIWriteValue(iniFileName, "SYSCONFIG", "SQLDataBase", DbName);
                IniFile.INIWriteValue(iniFileName, "SYSCONFIG", "SQLSysUser", DbUser);
                IniFile.INIWriteValue(iniFileName, "SYSCONFIG", "SQLSysPassword", this.passwordBox.Password);
                if(SqlHelper.Connect(DbName, DbServer, DbUser, this.passwordBox.Password))
                {
                    MessageBox.Show("保存成功.", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("数据库连接失败.", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("输入的信息不能为空,请重新输入!", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void IconButton_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IconButton_Click_DbTest(object sender, RoutedEventArgs e)
        {
            if (SqlHelper.Connect(DbName, DbServer, DbUser, this.passwordBox.Password))
            {
                MessageBox.Show("测试连接成功.", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("测试连接失败.", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
