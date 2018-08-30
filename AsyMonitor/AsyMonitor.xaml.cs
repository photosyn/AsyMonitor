using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace AsyMonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MyScreenParam myScreen1Param;
        MyScreenParam myScreen2Param;
        ScreenParam screenParamUse;
        MyGroupList screen1Group;
        MyGroupList screen2Group;
        MyGroupList screenGroupUse;
        private DispatcherTimer mDataTimer = null;
        private bool bScreenDllInit = false;

        public MainWindow()
        {
            SqlHelper.Init();
            InitializeScreenParam();
            InitializeScreenGroup();
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;

            InitTimer();
            InitScreen();


        }

        private void AreaMonitor(ScreenParam screenParam, MyGroupList myGroupList)
        {
            string sql = "";
            DataTable dataTable = null;
            string time = DateTime.Now.ToString("HH:mm:ss");
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            bool updateFlag = false;

            if (screenParam.DutyNoFormat.Equals(ScreenParam.dutyPrefix))
            {
                updateFlag = true;
            }
            else
            {
                int result = DateTime.Compare(Convert.ToDateTime(time), Convert.ToDateTime(screenParam.UpdateTime));
                if (result >= 0 && !date.Equals(screenParam.DutyDate))
                {
                    updateFlag = true;
                }
            }
            if (updateFlag)
            {
                string year = "'" + DateTime.Now.ToString("yyyy-MM") + "'";
                sql = String.Format("select * from IO_LeaderDuty where M_Month={0:G} and M_Date={1:D}", year, Convert.ToInt32(DateTime.Now.ToString("dd")));
                string name = "";
                SqlParameter[] paras = null;

                dataTable = SqlHelper.ExecuteDataTable(sql, paras);
                foreach (DataRow dt in dataTable.Rows)
                {
                    name = dt["M_Name"].ToString();
                }
                screenParam.DutyNoFormat = ScreenParam.dutyPrefix + name + "  " + screenParam.DutyNo;
                if (name.Length > 0)
                {
                    screenParam.DutyDate = date;
                }
            }

            SqlParameter[] param = null;
            //查询当前刷卡
            sql = "SELECT top 1 A.*,B.Base_PerID,B.Base_PerNo,B.Base_PerName,C.Base_RoleName FROM AcvB_AccessLog A,"
                + " General_Personnel B,General_Role C,General_Group D "
                + " where A.Base_PerID=B.Base_PerID and B.Base_RoleID=C.Base_RoleID and B.Base_GroupID=D.Base_GroupID"
                + " and A.Device_ID in(" + myGroupList.DevicesFormat + ")"
                + " order by A.Access_DateTime desc";
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start(); //  开始监视代码
            dataTable = SqlHelper.ExecuteDataTable(sql, param);
            string inOutRecord = "";
            foreach (DataRow dt in dataTable.Rows)
            {

                if (dt["InOutFlag"].ToString().Equals("0"))
                {
                    inOutRecord = "进闸";
                }
                else
                {
                    inOutRecord = "出闸";
                }
                inOutRecord = "当前刷卡： " + dt["Base_PerName"].ToString() + "  工号" + dt["Base_PerNo"].ToString();
            }
            //screenParam.PersonRecord = inOutRecord;
            //stopwatch.Stop(); //  停止监视
            //TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
            //Console.WriteLine("seconds:" + timeSpan.TotalSeconds);

            param = new SqlParameter[3];//参数化有问题，暂时用字符串连接代替
            param[0] = new SqlParameter("@deviceList", myGroupList.DevicesFormat);
            param[1] = new SqlParameter("@groupIdLen", SqlDbType.Int, 4)
            {
                Value = 5
            };
            param[2] = new SqlParameter("@groupId", screenParam.GroupId);

            sql = "select F.Base_RoleID,Max(G.Base_RoleName) as Base_RoleName,count(E.Base_PerID) as PerCount from"
                + " (select top 500000  a.* from AcvB_AccessLog a where ISNULL(Base_PerID,'''')<>'''' and Device_ID in(" + myGroupList.DevicesFormat + ")"
                + " and a.Access_DateTime=(select MAX(Access_DateTime) from AcvB_AccessLog"
                + " where Base_PerID=a.Base_PerID and isnull(Reserved,'''')='''' )"
                + " order by Base_PerID) E, General_Personnel F,General_Role G,General_Group H"
                + " where E.Base_PerID=F.Base_PerID and F.Base_RoleID=G.Base_RoleID and F.Base_GroupID=H.Base_GroupID"
                //+ " and SUBSTRING(H.Base_AuthID,1,@groupIdLen) = @groupId"
                + " and E.InOutFlag='0' and E.Card_Status='1'"
                + " group by F.Base_RoleID";

            dataTable = SqlHelper.ExecuteDataTable(sql, param);
            screenParam.ResetRoleDefaultTotal();
            foreach (DataRow dt in dataTable.Rows)
            {

                string role_id = dt["Base_RoleID"].ToString();
                int count = dt.Field<int>("PerCount");
                screenParam.SetRoleTotal(role_id, count);
            }
            screenParam.CalcAllRoleTotal();

            screenParam.GenerateFile();
        }

        public void ThreadMethod(object sender)
        {
            while (true)
            {
                AreaMonitor(myScreen1Param.ScreenParam, screen1Group);
                AreaMonitor(myScreen2Param.ScreenParam, screen2Group);
            }
        }
        public void ThreadMethod2(object sender)
        {
            while (true)
            {
                string strResult = "";
                BxDriver.ReflashDynamicArea(myScreen1Param.ScreenParam, 0, out strResult);
                Thread.Sleep(500);
                BxDriver.ReflashDynamicArea(myScreen2Param.ScreenParam, 0, out strResult);
                Thread.Sleep(500);
            }
        }

        private void InitializeScreenGroup()
        {
            screen1Group = new MyGroupList("Devs");
            screen2Group = new MyGroupList("Devs2");
        }

        private void InitializeScreenParam()
        {
            ScreenParamReader reader = new ScreenParamReader()
            {
                RoleGroup = "Roles",
                IpAddr = "ScrIP",
                Port = "ScrPort",
                Id = "ScreenNo",
                Width = "ScreenW",
                High = "ScreenH",
                Type = "ScreenType",
                Polarity = "DataP",
                ValidType = "OEP",
                DutyNo = "DutyTelNo",
                UpdateTime = "UpTime",
                NotifyTime = "ChgeTime",
                LeftTitle = "Title",
                RightTitle = "Title2",
                GroupId = "MGroupID",
                RoleDefault = "RoleDefault1",
                LeftTitleFontName = "LTFontName1",
                LeftTitleFontSize = "LTFontSize1",
                LeftTitleFontColor = "LTFontColor1",
                RightTitleFontName = "RTFontName1",
                RightTitleFontSize = "RTFontSize1",
                RightTitleFontColor = "RTFontColor1",
                LeftContextFontName = "LCFontName1",
                LeftContextFontSize = "LCFontSize1",
                LeftContextFontColor = "LCFontColor1",
                RightContextFontName = "RCFontName1",
                RightContextFontSize = "RCFontSize1",
                RightContextFontColor = "RCFontColor1",
                NoteText = System.AppDomain.CurrentDomain.BaseDirectory + "Notice.txt",
                MonitorText = System.AppDomain.CurrentDomain.BaseDirectory + "ShowSrcInfo.txt"

            };
            myScreen1Param = new MyScreenParam(reader);

            ScreenParamReader reader2 = new ScreenParamReader()
            {
                RoleGroup = "Roles2",
                IpAddr = "ScrIP2",
                Port = "ScrPort2",
                Id = "ScreenNo2",
                Width = "ScreenW2",
                High = "ScreenH2",
                Type = "ScreenType2",
                Polarity = "DataP2",
                ValidType = "OEP2",
                DutyNo = "DutyTelNo2",
                UpdateTime = "UpTime2",
                NotifyTime = "ChgeTime2",
                LeftTitle = "LTitle2",
                RightTitle = "RTitle2",
                GroupId = "MGroupID2",
                RoleDefault = "RoleDefault2",
                LeftTitleFontName = "LTFontName2",
                LeftTitleFontSize = "LTFontSize2",
                LeftTitleFontColor = "LTFontColor2",
                RightTitleFontName = "RTFontName2",
                RightTitleFontSize = "RTFontSize2",
                RightTitleFontColor = "RTFontColor2",
                LeftContextFontName = "LCFontName2",
                LeftContextFontSize = "LCFontSize2",
                LeftContextFontColor = "LCFontColor2",
                RightContextFontName = "RCFontName2",
                RightContextFontSize = "RCFontSize2",
                RightContextFontColor = "RCFontColor2",
                NoteText = System.AppDomain.CurrentDomain.BaseDirectory + "Notice2.txt",
                MonitorText = System.AppDomain.CurrentDomain.BaseDirectory + "ShowSrcInfo2.txt"
            };
            myScreen2Param = new MyScreenParam(reader2);
        }

        private void InitTimer()
        {
            if (mDataTimer == null)
            {
                mDataTimer = new DispatcherTimer();
                mDataTimer.Tick += new EventHandler(DataTimer_Tick);
                mDataTimer.Interval = TimeSpan.FromSeconds(1);
                mDataTimer.Start();
            }

        }

        private void InitScreen()
        {
            int nResult = BxDriver.Initialize();
            if (0 != nResult)
            {
                BxDriver.GetErrorMessage("Initialize", nResult);
                return;
            }
            else
            {
                bScreenDllInit = true;
            }

            Thread thread = new Thread(ThreadMethod);
            thread.IsBackground = true;
            thread.Name = "AsyMonitor";
            thread.Start();
            Thread thread2 = new Thread(ThreadMethod2);
            thread2.IsBackground = true;
            thread2.Name = "ReflashScreen";
            thread2.Start();

            //Console.WriteLine("主线程:" + Thread.CurrentThread.ManagedThreadId + "运行");
            //timer = new System.Threading.Timer(
            //    new System.Threading.TimerCallback(ThreadMethod), null, 5, 5000);

            //timer2 = new System.Threading.Timer(
            //    new System.Threading.TimerCallback(ThreadMethod2), null, 5, 10000);
        }

        private void DataTimer_Tick(object sender, EventArgs e)
        {
            string nowTime = DateTime.Now.ToString();
            screenParamUse.NowTime = nowTime;
        }

        private void Window_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("确定要退出本程序？", "温馨提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (message == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (bScreenDllInit)
                {
                    int nResult = BxDriver.Uninitialize();
                    if (0 != nResult)
                    {
                        BxDriver.GetErrorMessage("Uninitialize", nResult);
                    }
                    else
                    {
                        bScreenDllInit = true;
                    }
                }
            }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(screen1Group, screen2Group, myScreen1Param, myScreen2Param);
            settingsWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string setResult = "";
            double leftRatio = this.leftGrid.ActualWidth / this.mainGrid.ActualWidth;
            double titleRatio = this.rightTitle.ActualHeight / this.mainGrid.ActualHeight;
            if (!BxDriver.SetScreenPara(myScreen1Param.ScreenParam, leftRatio, titleRatio, out setResult))
            {
                MessageBox.Show(setResult, "下载屏幕参数", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (!BxDriver.SetScreenPara(myScreen2Param.ScreenParam, leftRatio, titleRatio, out setResult))
            {
                MessageBox.Show(setResult, "下载屏幕参数", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void screen1Radio_Checked(object sender, RoutedEventArgs e)
        {
            this.mainGrid.DataContext = myScreen1Param.ScreenParam;
            screenParamUse = myScreen1Param.ScreenParam;
            screenGroupUse = screen1Group;
        }

        private void screen2Radio_Checked(object sender, RoutedEventArgs e)
        {
            this.mainGrid.DataContext = myScreen2Param.ScreenParam;
            screenParamUse = myScreen2Param.ScreenParam;
            screenGroupUse = screen2Group;
        }

        private void Button_Download_Click(object sender, RoutedEventArgs e)
        {
            string setResult = "";
            double leftRatio = this.leftGrid.ActualWidth / this.mainGrid.ActualWidth;
            double titleRatio = this.rightTitle.ActualHeight / this.mainGrid.ActualHeight;
            if (!BxDriver.SetScreenPara(screenParamUse, leftRatio, titleRatio, out setResult))
            {
                MessageBox.Show(setResult, "下载屏幕参数", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Font_Click(object sender, RoutedEventArgs e)
        {
            //this.leftTitle.FontFamily = new FontFamily("隶书");
            //this.leftTitle.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Blue"));
            FontSettings fontWindow = new FontSettings(myScreen1Param.ScreenParam, myScreen2Param.ScreenParam);
            fontWindow.ShowDialog();
        }
    }
}
