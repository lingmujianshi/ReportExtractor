namespace ReportExtractor.Domain
{
    public class HtmlTag
    {
        /// <summary>
        /// brタグ 挟まないので単独で
        /// </summary>
        static public string Br { get; } = @"<br>";

        /// <summary>
        /// 改行コード\r\n（HTMLタグではないが見やすくするため）
        /// </summary>
        static public string Cr { get; } = "\r\n";

        /// <summary>
        /// 先頭タグ <tag>の形
        /// </summary>
        public string Top { get; }

        /// <summary>
        /// 終了タグ </tag>の形
        /// </summary>
        public string End { get; }

        /// <summary>
        /// コンストラクタ
        /// 先頭タグと終了タグを生成
        /// </summary>
        /// <param name="tag"></param>
        public HtmlTag(string tag)
        {
            Top = $"<{tag}>";
            End = $"</{tag}>";
        }

        /// <summary>
        /// 入力文字列を挟んで先頭タグと終了タグを追加
        /// </summary>
        /// <param name="str">入力文字列</param>
        /// <returns>タグを追加した文字列</returns>
        public string AddTags(string str)
        {
            return Top + str + End;
        }


        /// <summary>
        /// 入力文字列を挟んで先頭タグと終了タグを追加。
        /// 見やすくするために改行コードあり。
        /// </summary>
        /// <param name="str">入力文字列</param>
        /// <returns>タグを追加した文字列</returns>
        public string AddTagsNewLine(string str)
        {
            return Top + Cr + str + End + Cr;
        }
    }
}
