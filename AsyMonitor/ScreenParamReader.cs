using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyMonitor
{
    public class ScreenParamReader
    {
        private string roleGroup;
        private string ipAddr;
        private string port;
        private string id;
        private string width;
        private string high;
        private string type;
        private string polarity;
        private string validType;
        private string dutyNo;
        private string updateTime;
        private string notifyTime;
        private string leftTitle;
        private string rightTitle;
        private string groupId;
        private string roleDefault;
        private string noteText;
        private string monitorText;

        private string leftTitleFontName;
        private string leftTitleFontSize;
        private string leftTitleFontColor;
        private string rightTitleFontName;
        private string rightTitleFontSize;
        private string rightTitleFontColor;
        private string leftContextFontName;
        private string leftContextFontSize;
        private string leftContextFontColor;
        private string rightContextFontName;
        private string rightContextFontSize;
        private string rightContextFontColor;

        public string IpAddr { get => ipAddr; set => ipAddr = value; }
        public string Port { get => port; set => port = value; }
        public string Id { get => id; set => id = value; }
        public string Width { get => width; set => width = value; }
        public string High { get => high; set => high = value; }
        public string Type { get => type; set => type = value; }
        public string Polarity { get => polarity; set => polarity = value; }
        public string ValidType { get => validType; set => validType = value; }
        public string DutyNo { get => dutyNo; set => dutyNo = value; }
        public string UpdateTime { get => updateTime; set => updateTime = value; }
        public string NotifyTime { get => notifyTime; set => notifyTime = value; }
        public string LeftTitle { get => leftTitle; set => leftTitle = value; }
        public string RightTitle { get => rightTitle; set => rightTitle = value; }
        public string GroupId { get => groupId; set => groupId = value; }
        public string NoteText { get => noteText; set => noteText = value; }
        public string RoleGroup { get => roleGroup; set => roleGroup = value; }
        public string MonitorText { get => monitorText; set => monitorText = value; }
        public string RoleDefault { get => roleDefault; set => roleDefault = value; }
        public string LeftTitleFontName { get => leftTitleFontName; set => leftTitleFontName = value; }
        public string LeftTitleFontSize { get => leftTitleFontSize; set => leftTitleFontSize = value; }
        public string LeftTitleFontColor { get => leftTitleFontColor; set => leftTitleFontColor = value; }
        public string RightTitleFontName { get => rightTitleFontName; set => rightTitleFontName = value; }
        public string RightTitleFontSize { get => rightTitleFontSize; set => rightTitleFontSize = value; }
        public string RightTitleFontColor { get => rightTitleFontColor; set => rightTitleFontColor = value; }
        public string LeftContextFontName { get => leftContextFontName; set => leftContextFontName = value; }
        public string LeftContextFontSize { get => leftContextFontSize; set => leftContextFontSize = value; }
        public string LeftContextFontColor { get => leftContextFontColor; set => leftContextFontColor = value; }
        public string RightContextFontName { get => rightContextFontName; set => rightContextFontName = value; }
        public string RightContextFontSize { get => rightContextFontSize; set => rightContextFontSize = value; }
        public string RightContextFontColor { get => rightContextFontColor; set => rightContextFontColor = value; }
    }
}
