using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReportExtractor.Domain
{
    public class AnalysisDiff
    {
        private string[] _lines;

        /// <summary>
        /// git diff情報を正規表現で判定
        /// @@ -12,34 +56,78 @@　の形
        /// </summary>
        /// <param name="str">判定する文字列</param>
        /// <returns>git diff情報が含まれていればtrue</returns>
        public bool IsDiffInfo(string str)
        {
            // @@ -12,34 +56,78 @@　のパターン
            string gitDiffpattern = @"^@@\s*\-\s*\d+\s*,\s*\d+\s*\+\s*\d+\s*,\s*\d+\s*@@";
            var isMatch = Regex.IsMatch(str, gitDiffpattern);
            return isMatch;
        }

        public int GetPosition(string str)
        {
            // +12 のパターン
            string LineInfopattern = @"\+\s*\d+";
            var match = Regex.Match(str, LineInfopattern);

            // 抜き出した文字列からスペースと+を削除
            string ansStr = match.Value.Replace(" ", "").Replace("+", "");

            return int.Parse(ansStr);
        }

        /// <summary>
        /// 行切り出ししてローカル配列へ格納
        /// </summary>
        /// <param name="txt">入力テキスト</param>
        public void GetLines(string txt)
        {
            // git diff出力1行ごとに分ける
            _lines = StringLine.GetStringArray(txt);
        }

        /// <summary>
        /// git diff情報解析リストを作成
        /// </summary>
        /// <returns>gitdiff結果解析用構造体リスト</returns>
        public List<DiffLineInfo> GetLineInfoList()
        {
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();

            int start = 0, end = 0, pos = 0;
            bool isDetected = false;
            for (int i = 0; i < _lines.Length; i++)
            {
                if (IsDiffInfo(_lines[i]))
                {
                    if (isDetected)
                    {
                        infoList.Add(new DiffLineInfo(pos, start, end));
                    }
                    pos = this.GetPosition(_lines[i]);
                    start = i + 2;
                    isDetected = true;
                }

                end = i + 1;
            }
            infoList.Add(new DiffLineInfo(pos, start, end));
            return infoList;
        }

        /// <summary>
        /// 変更行番号リストを取得
        /// </summary>
        /// <param name="infoList">git diff情報解析リスト</param>
        /// <returns>変更行番号リスト</returns>
        public List<int> GetChangedNumbersList(List<DiffLineInfo> infoList)
        {

            List<int> lines = new List<int>();

            foreach (var info in infoList)
            {
                int count = info.Pos;
                int start = info.Start - 1;
                int end = info.End - 1;
                for (int i = start; i < end; i++)
                {
                    string line = _lines[i];
                    if (Regex.IsMatch(line, @"^\+"))
                    {
                        lines.Add(count);
                    }
                    if (!Regex.IsMatch(line, @"^\-"))
                    {
                        count++;
                    }
                }
            }

            return lines;
        }
    }

}
