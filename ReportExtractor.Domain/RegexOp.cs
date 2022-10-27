using System.Text.RegularExpressions;

namespace ReportExtractor.Domain
{
    /// <summary>
    /// 正規表現実行クラス
    /// </summary>
    public static class RegexOp
    {
        /// <summary>
        /// ヘッダ１文字「#」にマッチ
        /// </summary>
        /// <param name="str">入力文字</param>
        /// <returns>判定</returns>
        public static bool IsHeader1(string str)
        {
            string pattern = @"^#[^#]";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ヘッダ２文字「##」にマッチ
        /// </summary>
        /// <param name="str">入力文字</param>
        /// <returns>判定</returns>
        public static bool IsHeader2(string str)
        {
            string pattern = @"^##[^#]";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ヘッダ３文字「###」にマッチ
        /// </summary>
        /// <param name="str">入力文字</param>
        /// <returns>判定</returns>
        public static bool IsHeader3(string str)
        {
            string pattern = @"^(\s*|\t*)###[^#]";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 先頭から文字までスペースまたはタブ文字にマッチ
        /// </summary>
        /// <param name="str">入力文字</param>
        /// <returns>判定</returns>
        public static bool IsBlankRow(string str)
        {
            string pattern = @"^(\s*|\t*)$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 先頭の識別文字や空白文字を削除
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimTops(string str)
        {
            string pattern = @"^[!#+\s*\t*]+";
            var res = Regex.Replace(str, pattern, "");
            return res;
        }

        public static string TrimAll(string str)
        {
            string pattern = @"([\t\n\r]|\s{2,})";
            var res = Regex.Replace(str, pattern, "");
            return res;
        }

        public static bool IsPulse(string str)
        {
            string pattern = @"^(\s*|\t*)\+";
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsMinus(string str)
        {
            string pattern = @"^(\s*|\t*)-";
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsExclamation(string str)
        {
            string pattern = @"^(\s*|\t*)\!";
            return Regex.IsMatch(str, pattern);
        }
    } 
}
