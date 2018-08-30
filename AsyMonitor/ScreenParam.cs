using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace AsyMonitor
{
    public class ScreenParam : INotifyPropertyChanged
    {
        public static string personRecordPrefix = "当前刷卡:";
        private static string roleTotalPrefix = "人员统计:";
        private static string roleTotalSuffix = "人";
        public static string dutyPrefix = "值班:";
        private static string formatPersonSyntax = "{0, -2:D2}";

        ScreenParamReader _reader;

        private string roleGroup;
        private string roleDefault;
        private string ipAddr;
        private string port;
        private int id;
        private int width;
        private int high;
        private int type;
        private int polarity;
        private int validType;
        private string dutyNo;
        private string updateTime;
        private string notifyTime;
        private string leftTitle;
        private string rightTitle;
        private string groupId;
        private string noteText;
        private string leftTitleFontName;
        private int leftTitleFontSize;
        private string leftTitleFontColor;
        private string rightTitleFontName;
        private int rightTitleFontSize;
        private string rightTitleFontColor;
        private string leftContextFontName;
        private int leftContextFontSize;
        private string leftContextFontColor;
        private string rightContextFontName;
        private int rightContextFontSize;
        private string rightContextFontColor;

        private string noticeFile;
        private string dutyNoFormat = dutyPrefix;
        private string dutyDate = "";
        private string nowTime = "";

        private string roleId1 = "";
        private string roleId2 = "";
        private string roleId3 = "";
        private string roleId4 = "";
        private string roleId5 = "";
        private string roleId6 = "";
        private string roleId7 = "";
        private string roleId8 = "";
        private string roleName1 = "";
        private string roleName2 = "";
        private string roleName3 = "";
        private string roleName4 = "";
        private string roleName5 = "";
        private string roleName6 = "";
        private string roleName7 = "";
        private string roleName8 = "";
        private int roleTotal1 = 0;
        private int roleTotal2 = 0;
        private int roleTotal3 = 0;
        private int roleTotal4 = 0;
        private int roleTotal5 = 0;
        private int roleTotal6 = 0;
        private int roleTotal7 = 0;
        private int roleTotal8 = 0;
        private string roleTotal1Format = "";
        private string roleTotal2Format = "";
        private string roleTotal3Format = "";
        private string roleTotal4Format = "";
        private string roleTotal5Format = "";
        private string roleTotal6Format = "";
        private string roleTotal7Format = "";
        private string roleTotal8Format = "";


        private int allRoleTotal = 0;
        private string allRoleTotalFormat = roleTotalPrefix;
        private string personRecord = personRecordPrefix;
        public event PropertyChangedEventHandler PropertyChanged;

        public string RoleId1
        {
            get => roleId1; set
            {
                if (roleId1 == value)
                {
                    return;
                }

                roleId1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId1)));
            }
        }
        public string RoleName1
        {
            get => roleName1; set
            {
                if (roleName1 == value)
                {
                    return;
                }

                roleName1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName1)));
            }
        }
        public string RoleId2
        {
            get => roleId2; set
            {
                if (roleId2 == value)
                {
                    return;
                }

                roleId2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId2)));
            }
        }
        public string RoleName2
        {
            get => roleName2; set
            {
                if (roleName2 == value)
                {
                    return;
                }

                roleName2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName2)));
            }
        }
        public string RoleId3
        {
            get => roleId3; set
            {
                if (roleId3 == value)
                {
                    return;
                }

                roleId3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId3)));
            }
        }
        public string RoleName3
        {
            get => roleName3; set
            {
                if (roleName3 == value)
                {
                    return;
                }

                roleName3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName3)));
            }
        }
        public string RoleId4
        {
            get => roleId4; set
            {
                if (roleId4 == value)
                {
                    return;
                }

                roleId4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId4)));
            }
        }
        public string RoleName4
        {
            get => roleName4; set
            {
                if (roleName4 == value)
                {
                    return;
                }

                roleName4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName4)));
            }
        }
        public string RoleId5
        {
            get => roleId5; set
            {
                if (roleId5 == value)
                {
                    return;
                }

                roleId5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId5)));
            }
        }
        public string RoleName5
        {
            get => roleName5; set
            {
                if (roleName5 == value)
                {
                    return;
                }

                roleName5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName5)));
            }
        }
        public string RoleId6
        {
            get => roleId6; set
            {
                if (roleId6 == value)
                {
                    return;
                }

                roleId6 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId6)));
            }
        }
        public string RoleName6
        {
            get => roleName6; set
            {
                if (roleName6 == value)
                {
                    return;
                }

                roleName6 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName6)));
            }
        }
        public string RoleId7
        {
            get => roleId7; set
            {
                if (roleId7 == value)
                {
                    return;
                }

                roleId7 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId7)));
            }
        }
        public string RoleName7
        {
            get => roleName7; set
            {
                if (roleName7 == value)
                {
                    return;
                }

                roleName7 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName7)));
            }
        }
        public string RoleId8
        {
            get => roleId8; set
            {
                if (roleId8 == value)
                {
                    return;
                }

                roleId8 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleId8)));
            }
        }
        public string RoleName8
        {
            get => roleName8; set
            {
                if (roleName8 == value)
                {
                    return;
                }

                roleName8 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleName8)));
            }
        }

        public int RoleTotal1
        {
            get => roleTotal1; set
            {
                if (roleTotal1 == value)
                {
                    return;
                }

                roleTotal1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal1)));
            }
        }
        public int RoleTotal2
        {
            get => roleTotal2; set
            {
                if (roleTotal2 == value)
                {
                    return;
                }

                roleTotal2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal2)));
            }
        }
        public int RoleTotal3
        {
            get => roleTotal3; set
            {
                if (roleTotal3 == value)
                {
                    return;
                }

                roleTotal3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal3)));
            }
        }
        public int RoleTotal4
        {
            get => roleTotal4; set
            {
                if (roleTotal4 == value)
                {
                    return;
                }

                roleTotal4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal4)));
            }
        }
        public int RoleTotal5
        {
            get => roleTotal5; set
            {
                if (roleTotal5 == value)
                {
                    return;
                }

                roleTotal5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal5)));
            }
        }
        public int RoleTotal6
        {
            get => roleTotal6; set
            {
                if (roleTotal6 == value)
                {
                    return;
                }

                roleTotal6 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal6)));
            }
        }
        public int RoleTotal7
        {
            get => roleTotal7; set
            {
                if (roleTotal7 == value)
                {
                    return;
                }

                roleTotal7 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal7)));
            }
        }
        public int RoleTotal8
        {
            get => roleTotal8; set
            {
                if (roleTotal8 == value)
                {
                    return;
                }

                roleTotal8 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal8)));
            }
        }

        public int AllRoleTotal
        {
            get => allRoleTotal; set
            {
                if (allRoleTotal == value)
                {
                    return;
                }

                allRoleTotal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllRoleTotal)));
            }
        }

        public string AllRoleTotalFormat
        {
            get => allRoleTotalFormat; set
            {
                if (allRoleTotalFormat == value)
                {
                    return;
                }

                allRoleTotalFormat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllRoleTotalFormat)));
            }
        }

        public string RoleTotal1Format
        {
            get => roleTotal1Format; set
            {
                if (roleTotal1Format == value)
                {
                    return;
                }

                roleTotal1Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal1Format)));
            }
        }
        public string RoleTotal2Format
        {
            get => roleTotal2Format; set
            {
                if (roleTotal2Format == value)
                {
                    return;
                }

                roleTotal2Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal2Format)));
            }
        }
        public string RoleTotal3Format
        {
            get => roleTotal3Format; set
            {
                if (roleTotal3Format == value)
                {
                    return;
                }

                roleTotal3Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal3Format)));
            }
        }
        public string RoleTotal4Format
        {
            get => roleTotal4Format; set
            {
                if (roleTotal4Format == value)
                {
                    return;
                }

                roleTotal4Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal4Format)));
            }
        }
        public string RoleTotal5Format
        {
            get => roleTotal5Format; set
            {
                if (roleTotal5Format == value)
                {
                    return;
                }

                roleTotal5Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal5Format)));
            }
        }
        public string RoleTotal6Format
        {
            get => roleTotal6Format; set
            {
                if (roleTotal6Format == value)
                {
                    return;
                }

                roleTotal6Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal6Format)));
            }
        }
        public string RoleTotal7Format
        {
            get => roleTotal7Format; set
            {
                if (roleTotal7Format == value)
                {
                    return;
                }

                roleTotal7Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal7Format)));
            }
        }
        public string RoleTotal8Format
        {
            get => roleTotal8Format; set
            {
                if (roleTotal8Format == value)
                {
                    return;
                }

                roleTotal8Format = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTotal8Format)));
            }
        }

        public string PersonRecord
        {
            get => personRecord; set
            {
                if (personRecord == value)
                {
                    return;
                }

                personRecord = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PersonRecord)));
            }
        }

        public string IpAddr
        {
            get => ipAddr; set
            {
                if (ipAddr == value)
                {
                    return;
                }

                ipAddr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IpAddr)));
            }
        }
        public string Port
        {
            get => port; set
            {
                if (port == value)
                {
                    return;
                }

                port = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Port)));
            }
        }
        //屏号
        public int Id
        {
            get => id; set
            {
                if (id == value)
                {
                    return;
                }

                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        public int Width
        {
            get => width; set
            {
                if (width == value)
                {
                    return;
                }

                width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Width)));
            }
        }
        public int High
        {
            get => high; set
            {
                if (high == value)
                {
                    return;
                }

                high = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(High)));
            }
        }
        //屏类型
        public int Type
        {
            get => type; set
            {
                if (type == value)
                {
                    return;
                }

                type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
            }
        }
        public int Polarity
        {
            get => polarity; set
            {
                if (polarity == value)
                {
                    return;
                }

                polarity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Polarity)));
            }
        }
        public int ValidType
        {
            get => validType; set
            {
                if (validType == value)
                {
                    return;
                }

                validType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValidType)));
            }
        }
        public string DutyNo
        {
            get => dutyNo; set
            {
                if (dutyNo == value)
                {
                    return;
                }

                dutyNo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DutyNo)));
            }
        }

        public string NotifyTime
        {
            get => notifyTime; set
            {
                if (notifyTime == value)
                {
                    return;
                }

                notifyTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotifyTime)));
            }
        }
        public string LeftTitle
        {
            get => leftTitle; set
            {
                if (leftTitle == value)
                {
                    return;
                }

                leftTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftTitle)));
            }
        }
        public string RightTitle
        {
            get => rightTitle; set
            {
                if (rightTitle == value)
                {
                    return;
                }

                rightTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightTitle)));
            }
        }
        public string NoteText
        {
            get => noteText; set
            {
                if (noteText == value)
                {
                    return;
                }

                noteText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoteText)));
            }
        }

        public string GroupId
        {
            get => groupId; set
            {
                if (groupId == value)
                {
                    return;
                }

                groupId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroupId)));
            }
        }

        public string UpdateTime
        {
            get => updateTime; set
            {
                if (updateTime == value)
                {
                    return;
                }

                updateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UpdateTime)));
            }
        }

        public string RoleGroup { get => roleGroup; set => roleGroup = value; }
        public string NoticeFile { get => noticeFile; set => noticeFile = value; }
        public ScreenParamReader Reader { get => _reader; set => _reader = value; }
        public string DutyNoFormat
        {
            get => dutyNoFormat; set
            {
                if (dutyNoFormat == value)
                {
                    return;
                }

                dutyNoFormat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DutyNoFormat)));
            }
        }

        public string NowTime
        {
            get => nowTime; set
            {
                if (nowTime == value)
                {
                    return;
                }

                nowTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NowTime)));
            }
        }

        public string DutyDate
        {
            get => dutyDate; set
            {
                if (dutyDate == value)
                {
                    return;
                }

                dutyDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DutyDate)));
            }
        }

        public string RoleDefault { get => roleDefault; set => roleDefault = value; }
        public string LeftTitleFontName
        {
            get => leftTitleFontName; set
            {
                if (leftTitleFontName == value)
                {
                    return;
                }

                leftTitleFontName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftTitleFontName)));
            }
        }
        public int LeftTitleFontSize
        {
            get => leftTitleFontSize; set
            {
                if (leftTitleFontSize == value)
                {
                    return;
                }

                leftTitleFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftTitleFontSize)));
            }
        }
        public string LeftTitleFontColor
        {
            get => leftTitleFontColor; set
            {
                if (leftTitleFontColor == value)
                {
                    return;
                }

                leftTitleFontColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftTitleFontColor)));
            }
        }

        public string RightTitleFontName
        {
            get => rightTitleFontName; set
            {
                if (rightTitleFontName == value)
                {
                    return;
                }

                rightTitleFontName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightTitleFontName)));
            }
        }
        public int RightTitleFontSize
        {
            get => rightTitleFontSize; set
            {
                if (rightTitleFontSize == value)
                {
                    return;
                }

                rightTitleFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightTitleFontSize)));
            }
        }
        public string RightTitleFontColor
        {
            get => rightTitleFontColor; set
            {
                if (rightTitleFontColor == value)
                {
                    return;
                }

                rightTitleFontColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightTitleFontColor)));
            }
        }
        public string LeftContextFontName
        {
            get => leftContextFontName; set
            {
                if (leftContextFontName == value)
                {
                    return;
                }

                leftContextFontName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftContextFontName)));
            }
        }
        public int LeftContextFontSize
        {
            get => leftContextFontSize; set
            {
                if (leftContextFontSize == value)
                {
                    return;
                }

                leftContextFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftContextFontSize)));
            }
        }
        public string LeftContextFontColor
        {
            get => leftContextFontColor; set
            {
                if (leftContextFontColor == value)
                {
                    return;
                }

                leftContextFontColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftContextFontColor)));
            }
        }
        public string RightContextFontName
        {
            get => rightContextFontName; set
            {
                if (rightContextFontName == value)
                {
                    return;
                }

                rightContextFontName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightContextFontName)));
            }
        }
        public int RightContextFontSize
        {
            get => rightContextFontSize; set
            {
                if (rightContextFontSize == value)
                {
                    return;
                }

                rightContextFontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightContextFontSize)));
            }
        }
        public string RightContextFontColor
        {
            get => rightContextFontColor; set
            {
                if (rightContextFontColor == value)
                {
                    return;
                }

                rightContextFontColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightContextFontColor)));
            }
        }

        public ScreenParam(ScreenParamReader reader)
        {
            Reader = reader;
            Load();
            LoadRoleGroup();
        }

        public void LoadRoleGroup()
        {
            string sql = "select * from General_Role where Base_IsDel='0'";
            SqlParameter[] param = null;
            DataTable dataTable = SqlHelper.ExecuteDataTable(sql, param);

            int realUsedRoleCount = 0;
            int realSkipRoleCount = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int realDisplayOrder = 0;
                string role_id = dataTable.Rows[i]["Base_RoleID"].ToString();
                string role_name = dataTable.Rows[i]["Base_RoleName"].ToString() + ":";
                int role_total = 0;
                string role_total_format = "   0" + roleTotalSuffix;
                if (!RoleGroup.Contains(role_id))
                {
                    role_id = "";
                    role_name = "";
                    role_total_format = "";
                    realSkipRoleCount++;
                    realDisplayOrder = dataTable.Rows.Count - realSkipRoleCount + 1;
                }
                else
                {
                    realUsedRoleCount++;
                    realDisplayOrder = realUsedRoleCount;
                }
                switch (realDisplayOrder)
                {
                    case 1:
                        {
                            RoleName1 = role_name;
                            RoleId1 = role_id;
                            RoleTotal1Format = role_total_format;
                            RoleTotal1 = role_total;
                            break;
                        }
                    case 2:
                        {
                            RoleName2 = role_name;
                            RoleId2 = role_id;
                            RoleTotal2Format = role_total_format;
                            RoleTotal2 = role_total;
                            break;
                        }
                    case 3:
                        {
                            RoleName3 = role_name;
                            RoleId3 = role_id;
                            RoleTotal3Format = role_total_format;
                            RoleTotal3 = role_total;
                            break;
                        }
                    case 4:
                        {
                            RoleName4 = role_name;
                            RoleId4 = role_id;
                            RoleTotal4Format = role_total_format;
                            RoleTotal4 = role_total;
                            break;
                        }
                    case 5:
                        {
                            RoleName5 = role_name;
                            RoleId5 = role_id;
                            RoleTotal5Format = role_total_format;
                            RoleTotal5 = role_total;
                            break;
                        }
                    case 6:
                        {
                            RoleName6 = role_name;
                            RoleId6 = role_id;
                            RoleTotal6Format = role_total_format;
                            RoleTotal6 = role_total;
                            break;
                        }
                    case 7:
                        {
                            RoleName7 = role_name;
                            RoleId7 = role_id;
                            RoleTotal7Format = role_total_format;
                            RoleTotal7 = role_total;
                            break;
                        }
                    case 8:
                        {
                            RoleName8 = role_name;
                            RoleId8 = role_id;
                            RoleTotal8Format = role_total_format;
                            RoleTotal8 = role_total;
                            break;
                        }
                }
            }
        }

        public void CalcAllRoleTotal()
        {
            AllRoleTotal = RoleTotal1 + RoleTotal2 + RoleTotal3 + RoleTotal4 + RoleTotal5 + RoleTotal6 + RoleTotal7 + RoleTotal8;
            AllRoleTotalFormat = roleTotalPrefix + String.Format("{0, 4:D3}", AllRoleTotal) + roleTotalSuffix;
        }

        public void ResetRoleDefaultTotal()
        {
            RoleTotal1 = 0;
            RoleTotal2 = 0;
            RoleTotal3 = 0;
            RoleTotal4 = 0;
            RoleTotal5 = 0;
            RoleTotal6 = 0;
            RoleTotal7 = 0;
            RoleTotal8 = 0;
        }

        public void SetRoleTotal(string roleId, int total)
        {
            if (RoleGroup.Contains(roleId))
            {
                if (roleId.Equals(RoleId1))
                {
                    RoleTotal1 += total;
                    RoleTotal1Format = String.Format(formatPersonSyntax, RoleTotal1) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId2))
                {
                    RoleTotal2 += total;
                    RoleTotal2Format = String.Format(formatPersonSyntax, RoleTotal2) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId3))
                {
                    RoleTotal3 += total;
                    RoleTotal3Format = String.Format(formatPersonSyntax, RoleTotal3) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId4))
                {
                    RoleTotal4 += total;
                    RoleTotal4Format = String.Format(formatPersonSyntax, RoleTotal4) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId5))
                {
                    RoleTotal5 += total;
                    RoleTotal5Format = String.Format(formatPersonSyntax, RoleTotal5) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId6))
                {
                    RoleTotal6 += total;
                    RoleTotal6Format = String.Format(formatPersonSyntax, RoleTotal6) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId7))
                {
                    RoleTotal7 += total;
                    RoleTotal7Format = String.Format(formatPersonSyntax, RoleTotal7) + roleTotalSuffix;
                }
                else if (roleId.Equals(RoleId8))
                {
                    RoleTotal8 += total;
                    RoleTotal8Format = String.Format(formatPersonSyntax, RoleTotal8) + roleTotalSuffix;
                }
            }
            else
            {
                //不属于当前屏幕的卡类别数量直接归入默认卡类别中
                if (RoleDefault.Equals(RoleId1))
                {
                    RoleTotal1 += total;
                    RoleTotal1Format = String.Format(formatPersonSyntax, RoleTotal1) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId2))
                {
                    RoleTotal2 += total;
                    RoleTotal2Format = String.Format(formatPersonSyntax, RoleTotal2) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId3))
                {
                    RoleTotal3 += total;
                    RoleTotal3Format = String.Format(formatPersonSyntax, RoleTotal3) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId4))
                {
                    RoleTotal4 += total;
                    RoleTotal4Format = String.Format(formatPersonSyntax, RoleTotal4) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId5))
                {
                    RoleTotal5 += total;
                    RoleTotal5Format = String.Format(formatPersonSyntax, RoleTotal5) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId6))
                {
                    RoleTotal6 += total;
                    RoleTotal6Format = String.Format(formatPersonSyntax, RoleTotal6) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId7))
                {
                    RoleTotal7 += total;
                    RoleTotal7Format = String.Format(formatPersonSyntax, RoleTotal7) + roleTotalSuffix;
                }
                else if (RoleDefault.Equals(RoleId8))
                {
                    RoleTotal8 += total;
                    RoleTotal8Format = String.Format(formatPersonSyntax, RoleTotal8) + roleTotalSuffix;
                }
            }
        }

        public void Load()
        {
            string iniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini";
            RoleGroup = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RoleGroup, "");
            IpAddr = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.IpAddr, "");
            Port = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.Port, "");
            Id = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.Id, ""));
            Width = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.Width, ""));
            High = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.High, ""));
            Type = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.Type, ""));
            Polarity = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.Polarity, ""));
            ValidType = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.ValidType, ""));
            DutyNo = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.DutyNo, "");
            UpdateTime = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.UpdateTime, "");
            NotifyTime = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.NotifyTime, "");
            LeftTitle = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftTitle, "");
            RightTitle = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightTitle, "");
            GroupId = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.GroupId, "");
            RoleDefault = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RoleDefault, "");
            LeftTitleFontName = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftTitleFontName, "宋体");
            LeftTitleFontSize = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftTitleFontSize, "36"));
            LeftTitleFontColor = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftTitleFontColor, "-65535");
            RightTitleFontName = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightTitleFontName, "宋体");
            RightTitleFontSize = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightTitleFontSize, "36"));
            RightTitleFontColor = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightTitleFontColor, "-65535");
            LeftContextFontName = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftContextFontName, "宋体");
            LeftContextFontSize = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftContextFontSize, "12"));
            LeftContextFontColor = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.LeftContextFontColor, "-65535");
            RightContextFontName = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightContextFontName, "宋体");
            RightContextFontSize = Convert.ToInt32(IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightContextFontSize, "12"));
            RightContextFontColor = IniFile.INIGetStringValue(iniFileName, "IOMonitor", Reader.RightContextFontColor, "-65535");

            System.IO.StreamReader st;
            st = new System.IO.StreamReader(Reader.NoteText, System.Text.Encoding.UTF8);
            //UTF-8通用编码
            NoteText = st.ReadToEnd();
            st.Close();
        }

        public void Save()
        {
            string iniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini";
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RoleGroup, RoleGroup);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.IpAddr, IpAddr);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.Port, Port);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.Id, Id.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.Width, Width.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.High, High.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.Type, Type.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.Polarity, Polarity.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.ValidType, ValidType.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.DutyNo, DutyNo);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.UpdateTime, UpdateTime);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.NotifyTime, NotifyTime);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftTitle, LeftTitle);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightTitle, RightTitle);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.GroupId, GroupId);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RoleDefault, RoleDefault);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(Reader.NoteText);
            sw.WriteLine(NoteText);
            sw.Flush();//文件流
            sw.Close();//最后要关闭写入状态
        }

        public void SaveFont()
        {
            string iniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini";
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftTitleFontName, LeftTitleFontName);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftTitleFontSize, LeftTitleFontSize.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftTitleFontColor, LeftTitleFontColor);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightTitleFontName, RightTitleFontName);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightTitleFontSize, RightTitleFontSize.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightTitleFontColor, RightTitleFontColor);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftContextFontName, LeftContextFontName);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftContextFontSize, LeftContextFontSize.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.LeftContextFontColor, LeftContextFontColor);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightContextFontName, RightContextFontName);
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightContextFontSize, RightContextFontSize.ToString());
            IniFile.INIWriteValue(iniFileName, "IOMonitor", Reader.RightContextFontColor, RightContextFontColor);
        }

        private string AddSpace(int totalLen, string source, string endFlag)
        {
            while (source.Length < totalLen)
            {
                source += " ";
            }
            return source + "*";
        }

        public void GenerateFile()
        {
            StreamWriter sw = new StreamWriter(_reader.MonitorText, false);
            string lineText = RoleName1 + RoleTotal1Format + "  " + RoleName2 + RoleTotal2Format;
            lineText = AddSpace(15, lineText, "*");
            sw.WriteLine(lineText);
            if (RoleName3.Length > 0)
            {
                lineText = RoleName3 + RoleTotal3Format + "  " + RoleName4 + RoleTotal4Format;
                lineText = AddSpace(15, lineText, "*");
                sw.WriteLine(lineText);
            }
            if (RoleName5.Length > 0)
            {
                lineText = RoleName5 + RoleTotal5Format + "  " + RoleName6 + RoleTotal6Format;
                lineText = AddSpace(15, lineText, "*");
                sw.WriteLine(lineText);
            }
            if (RoleName7.Length > 0)
            {
                lineText = RoleName7 + RoleTotal7Format + "  " + RoleName8 + RoleTotal8Format;
                lineText = AddSpace(15, lineText, "*");
                sw.WriteLine(lineText);
            }
            //人员统计
            lineText = AddSpace(18, AllRoleTotalFormat, "*");
            sw.WriteLine(lineText);

            lineText = AddSpace(20, DutyNoFormat, "*");
            sw.WriteLine(lineText);

            lineText = AddSpace(20, DateTime.Now.ToString(), "*");
            sw.WriteLine(lineText);
            sw.Close();
        }
    }
}
