using System.Text.RegularExpressions;

namespace ReportExtractor.Domain
{
    /// <summary>
    /// 階層を判定するクラス
    /// </summary>
    public class LevelCheck
    {
        public bool IsHeader1(string str)
        {
            string pattern = @"^□";
            return Regex.IsMatch(str, pattern);
        }

        public bool IsHeader2(string str)
        {
            string pattern = @"^\w";
            return Regex.IsMatch(str, pattern);
        }

        public bool IsHeader3(string str)
        {
            string pattern = @"^(\s\s|\t)";
            return Regex.IsMatch(str, pattern);
        }

        public bool IsBlankRow(string str)
        {
            string pattern = @"^(\s*|\t*)$";
            return Regex.IsMatch(str, pattern);
        }
    }

}
