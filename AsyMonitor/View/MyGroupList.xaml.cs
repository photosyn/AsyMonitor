﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AsyMonitor
{
    /// <summary>
    /// MyGroupList.xaml 的交互逻辑
    /// </summary>
    public partial class MyGroupList : UserControl
    {
        private ObservableCollection<Device> deviceList = new ObservableCollection<Device>();//门禁列表源数据
        private List<TreeNode> areaList = new List<TreeNode>();//区域源数据
        private string _devicesKey = "";
        private string _devices = "";
        private string _devicesFormat = "";

        public string DevicesFormat { get => _devicesFormat; set => _devicesFormat = value; }

        public MyGroupList(string deviceKey)
        {
            _devicesKey = deviceKey;
            string sql = "SELECT * FROM AcvB_AsyMonitorInfo";
            SqlParameter[] param = null;

            DataTable dataTable = null;
            dataTable = SqlHelper.ExecuteDataTable(sql, param);
            if (_devicesKey.Equals("Devs") && dataTable.Rows.Count >= 1)
            {
                _devices = dataTable.Rows[0]["Monitor_Devs"].ToString();
            }
            if (_devicesKey.Equals("Devs2") && dataTable.Rows.Count >= 2)
            {
                _devices = dataTable.Rows[1]["Monitor_Devs"].ToString();
            }
            DevicesFormat = formatDevices(_devices);
            InitializeComponent();
            InitializeTreeView();
            InitializeListView();
        }

        private string formatDevices(string source)
        {
            string formatStr = "";
            string[] deviceList = source.Split(',');

            foreach (string device in deviceList)
            {
                if (formatStr.Length > 0)
                {
                    formatStr += ",'" + device + "'";
                }
                else
                {
                    formatStr = "'" + device + "'";
                }
            }
            return formatStr;
        }

        public void Save()
        {
            string didGroups = "";
            foreach (Device device in deviceList)
            {
                if (device.Ischeck)
                {
                    if (didGroups.Length == 0)
                    {
                        didGroups = device.Did.ToString();
                    }
                    else
                    {
                        didGroups += "," + device.Did.ToString();
                    }
                }
            }
            _devices = didGroups;
            DevicesFormat = formatDevices(_devices);
            string sql = "UPDATE AcvB_AsyMonitorInfo SET Monitor_Devs = '" + _devices + "'"; ;
            SqlParameter[] paras = null;

            if (_devicesKey.Equals("Devs"))
            {
                sql += " WHERE Screen_ID = 1";
            }
            if (_devicesKey.Equals("Devs2"))
            {
                sql += " WHERE Screen_ID = 2";
            }
            SqlHelper.ExecuteSql(sql, paras);
        }

        private void InitializeTreeView()
        {
            areaList.Add(new TreeNode() { Key = "", ParentKey = "ROOT", NodeName = "全部" });
            SqlParameter[] paras = null;
            DataTable dataTable = SqlHelper.ExecuteDataTable("select len(Group_No) as LenNO, * from AcvB_Group order by LenNO, Group_Name", paras);
            foreach (DataRow dt in dataTable.Rows)
            {
                string tempGroupNo = dt["Group_No"].ToString();
                string tempGroupName = dt["Group_Name"].ToString();
                string paraentkey = tempGroupNo.Substring(0, tempGroupNo.Length - 2);
                //string key = tempGroupNo.Substring(tempGroupNo.Length - 2);
                areaList.Add(new TreeNode() { Key = tempGroupNo, ParentKey = paraentkey, NodeName = tempGroupName });
            }
            List<TreeNode> rootNode = GetTreeData("ROOT", areaList);
            this.treeView.ItemsSource = rootNode;
        }

        private List<TreeNode> GetTreeData(string parentKey, List<TreeNode> nodes)
        {
            List<TreeNode> mainNodes = nodes.Where(x => x.ParentKey == parentKey).ToList<TreeNode>();
            List<TreeNode> otherNodes = nodes.Where(x => x.ParentKey != parentKey).ToList<TreeNode>();
            foreach (TreeNode grp in mainNodes)
            {
                grp.Nodes = GetTreeData(grp.Key, otherNodes);
            }
            return mainNodes;
        }

        private void InitializeListView()
        {
            string[] devList = _devices.Trim().Split(',');

            SqlParameter[] paras = null;
            DataTable dataTable = SqlHelper.ExecuteDataTable("select Device_ID as Did, Device_Name as Name, Device_Location as Location from AcvB_Device where Device_Status <> '1' " +
                "and Board_ID not in (select Board_ID from AcvB_DevForbidden where Base_OperCode = 'SYSTEM') Order by Device_Name", paras);
            foreach (DataRow dt in dataTable.Rows)
            {
                bool isCheck = false;
                foreach (string dev in devList)
                {
                    if (dt["Did"].ToString().Equals(dev))
                    {
                        isCheck = true;
                    }
                }
                Device device = new Device()
                {
                    Ischeck = isCheck,
                    Did = Convert.ToInt32(dt["Did"]),
                    Name = dt["Name"].ToString(),
                    Location = dt["Location"].ToString()
                };
                deviceList.Add(device);
            }
            this.deviceListView.ItemsSource = deviceList;
        }

        private void UpdateListView(string groupNo)
        {
            string[] devList = _devices.Trim().Split(',');

            SqlParameter[] paras = null;
            string sql = "select Device_ID as Did, Device_Name as Name, Device_Location as Location from AcvB_Device where Device_Status <> '1' " +
                "and Board_ID not in (select Board_ID from AcvB_DevForbidden where Base_OperCode = 'SYSTEM')";
            if (!groupNo.Equals("ROOT"))
            {
                sql += " and Group_No like " + "\'" + groupNo + "%" + "\'";
            }
            sql += " Order by Device_Name";

            DataTable dataTable = SqlHelper.ExecuteDataTable(sql, paras);
            deviceList.Clear();
            //this.listView.ItemsSource = null;
            foreach (DataRow dt in dataTable.Rows)
            {
                bool isCheck = false;
                foreach (string dev in devList)
                {
                    if (dt["Did"].ToString().Equals(dev))
                    {
                        isCheck = true;
                    }
                }
                Device device = new Device()
                {
                    Ischeck = isCheck,
                    Did = Convert.ToInt32(dt["Did"]),
                    Name = dt["Name"].ToString(),
                    Location = dt["Location"].ToString()
                };
                deviceList.Add(device);
            }
            this.deviceListView.ItemsSource = deviceList;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeNode treeNode = (TreeNode)this.treeView.SelectedItem;
            if (null != treeNode)
            {
                UpdateListView(treeNode.Key);
            }
        }
    }
}
