using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace AsyMonitor
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window
    {
        private ObservableCollection<Role> roleList1 = new ObservableCollection<Role>();//门禁列表源数据
        private ObservableCollection<Role> roleList2 = new ObservableCollection<Role>();//门禁列表源数据

        MyGroupList screen1Group;
        MyGroupList screen2Group;
        MyScreenParam screen1Param;
        MyScreenParam screen2Param;

        public Settings(MyGroupList screen1Group, MyGroupList screen2Group, MyScreenParam screen1Param, MyScreenParam screen2Param)
        {
            InitializeComponent();
            this.screen1Group = screen1Group;
            this.screen2Group = screen2Group;
            this.screen1Param = screen1Param;
            this.screen2Param = screen2Param;
            InitializeRoleGroupTab();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.screen1AreaTab.Content = screen1Group;
            this.screen2AreaTab.Content = screen2Group;
            this.screen1ParamTab.Content = screen1Param;
            this.screen2ParamTab.Content = screen2Param;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Save();
            screen1Group.Save();
            screen2Group.Save();
            screen1Param.Save();
            screen2Param.Save();
            MessageBox.Show("保存成功", "保存", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Save()
        {
            string roleGroups = "";
            string roleDefault = "";
            foreach (Role role in roleList1)
            {
                if (role.Ischeck)
                {
                    if (roleGroups.Length == 0)
                    {
                        roleGroups = role.RoleId.ToString();
                    }
                    else
                    {
                        roleGroups += "," + role.RoleId.ToString();
                    }
                }
                if (role.IsDefault)
                {
                    roleDefault = role.RoleId;
                }
            }
            string group_id = IniFile.INIGetStringValue(System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini", "IOMonitor", screen1Param.ScreenParam.Reader.GroupId, "");
            if (!screen1Param.ScreenParam.RoleGroup.Equals(roleGroups)
                || !screen1Param.ScreenParam.GroupId.Equals(group_id)
                || !screen1Param.ScreenParam.RoleDefault.Equals(roleDefault))
            {
                screen1Param.ScreenParam.RoleGroup = roleGroups;
                screen1Param.ScreenParam.RoleDefault = roleDefault;
                screen1Param.ScreenParam.LoadRoleGroup();
            }

            roleGroups = "";
            roleDefault = "";
            foreach (Role role in roleList2)
            {
                if (role.Ischeck)
                {
                    if (roleGroups.Length == 0)
                    {
                        roleGroups = role.RoleId.ToString();
                    }
                    else
                    {
                        roleGroups += "," + role.RoleId.ToString();
                    }
                }
                if (role.IsDefault)
                {
                    roleDefault = role.RoleId;
                }
            }
            group_id = IniFile.INIGetStringValue(System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini", "IOMonitor", screen2Param.ScreenParam.Reader.GroupId, "");
            if (!screen2Param.ScreenParam.RoleGroup.Equals(roleGroups)
                || !screen2Param.ScreenParam.GroupId.Equals(group_id)
                || !screen2Param.ScreenParam.RoleDefault.Equals(roleDefault))
            {
                screen2Param.ScreenParam.RoleGroup = roleGroups;
                screen2Param.ScreenParam.RoleDefault = roleDefault;
                screen2Param.ScreenParam.LoadRoleGroup();
            }
        }

        private void InitializeRoleGroupTab()
        {
            string[] roles1 = screen1Param.ScreenParam.RoleGroup.Trim().Split(',');
            string[] roles2 = screen2Param.ScreenParam.RoleGroup.Trim().Split(',');

            SqlParameter[] paras = null;
            DataTable dataTable = SqlHelper.ExecuteDataTable("select * from General_Role where Base_IsDel='0'", paras);
            foreach (DataRow dt in dataTable.Rows)
            {
                bool isCheck1 = false;
                bool isCheck2 = false;
                bool isDefault1 = false;
                bool isDefault2 = false;
                foreach (string r in roles1)
                {
                    if (dt["Base_RoleID"].ToString().Equals(r))
                    {
                        isCheck1 = true;
                    }
                    if (dt["Base_RoleID"].ToString().Equals(screen1Param.ScreenParam.RoleDefault))
                    {
                        isDefault1 = true;
                    }
                }
                foreach (string r in roles2)
                {
                    if (dt["Base_RoleID"].ToString().Equals(r))
                    {
                        isCheck2 = true;
                    }
                    if (dt["Base_RoleID"].ToString().Equals(screen2Param.ScreenParam.RoleDefault))
                    {
                        isDefault2 = true;
                    }
                }
                Role role1 = new Role()
                {
                    Ischeck = isCheck1,
                    RoleId = dt["Base_RoleID"].ToString(),
                    RoleName = dt["Base_RoleName"].ToString(),
                    IsDefault = isDefault1
                };
                Role role2 = new Role()
                {
                    Ischeck = isCheck2,
                    RoleId = dt["Base_RoleID"].ToString(),
                    RoleName = dt["Base_RoleName"].ToString(),
                    IsDefault = isDefault2
                };
                roleList1.Add(role1);
                roleList2.Add(role2);
            }
            this.screen1RoleListView.ItemsSource = roleList1;
            this.screen2RoleListView.ItemsSource = roleList2;
        }
    }
}
