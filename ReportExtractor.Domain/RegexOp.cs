using System.Text.RegularExpressions;

namespace ReportExtractor.Domain
{
    /// <summary>
    /// 階層を判定するクラス
    /// </summary>
    public static class RegexOp
    {
        public static bool IsHeader1(string str)
        {
            string pattern = @"^#[^#]";
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsHeader2(string str)
        {
            string pattern = @"^##[^#]";
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsHeader3(string str)
        {
            string pattern = @"^###[^#]";
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsBlankRow(string str)
        {
            string pattern = @"^(\s*|\t*)$";
            return Regex.IsMatch(str, pattern);
        }
    } 
}
