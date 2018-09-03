using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AsyMonitor
{
    public class PortValidationRule : ValidationRule
    {
        public int MinValue
        {
            get;
            set;
        }
        public int MaxValue
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int port = 1;
            try
            {
                if ((value as string).Length > 0)
                    port = System.Convert.ToInt32(value as string);
            }
            catch (System.Exception e)
            {
                return new ValidationResult(false, "非法输入 " + e.Message);
            }

            if ((port < MinValue) || (port > MaxValue))
            {
                return new ValidationResult(false,
                  "请输入端口范围: " + MinValue + " - " + MaxValue + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class IPAddressRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string IPAddress = value as string;

            if (!string.IsNullOrWhiteSpace(IPAddress))
            {
                string IPAddressFormartRegex = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

                // 检查输入的字符串是否符合IP地址格式
                if (!Regex.IsMatch(IPAddress, IPAddressFormartRegex))
                {
                    return new ValidationResult(false, "IP地址格式不正确");
                }
            }
            return new ValidationResult(true, null);
        }
    }
}
