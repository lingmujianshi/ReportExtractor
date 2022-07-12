using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractor.Domain
{
    public static class StringLine
    {
        /// <summary>
        /// 文字列から行の先頭位置を調べる関数
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">探し始める位置</param>
        /// <returns>行先頭の位置</returns>
        public static int GetTopPos(this string str, int start)
        {
            //行先頭にカーソルがある場合の対応
            int pos = start - 1;
            if (pos < 0) pos = 0;

            //改行コード\nの次の文字が行の先頭
            return str.LastIndexOf('\n', pos) + 1;
        }

        /// <summary>
        /// 文字列から行の終了位置を調べる関数
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">探し始める位置</param>
        /// <returns>行終了位置</returns>
        public static int GetEndPos(this string str, int start)
        {
            //改行コードの位置
            int pos = str.IndexOf('\n', start);

            //最後の行は改行コードが無いので、文字列の終了位置とする。
            if (pos < 0)
            {
                pos = str.Length;
            }

            return pos;
        }

        /// <summary>
        /// 選択範囲の行の文字数を返す
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="AreaTop">範囲先頭</param>
        /// <param name="AreaEnd">範囲終了</param>
        /// <returns></returns>
        public static int GetWidth(this string str, int AreaTop, int AreaEnd)
        {
            int topPos = str.GetTopPos(AreaTop);
            int endPos = str.GetEndPos(AreaEnd);
            return endPos - topPos;
        }
    }
}
