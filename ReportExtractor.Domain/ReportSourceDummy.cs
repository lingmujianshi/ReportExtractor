using System;
using System.Collections.Generic;
using System.IO;

namespace ReportExtractor.Domain
{
    public sealed class ReportSourceDummy : IGetSource
    {
        /// <summary>
        /// 格納フォルダアドレス
        /// </summary>
        string Folder { get; }

        /// <summary>
        /// Git Diffの取得データを納めたテキストファイル名
        /// </summary>
        string GitDiffFilename { get; }
        /// <summary>
        /// 報告書のテキストファイル名
        /// </summary>
        string ReportFilename { get; }

        /// <summary>
        /// ダミーデータ
        /// </summary>
        /// <param name="folder">格納フォルダアドレス</param>
        /// <param name="gitDiffFilename">Git Diffの取得データを納めたテキストファイル名</param>
        /// <param name="reportFilename">報告書のテキストファイル名</param>
        public ReportSourceDummy(string folder, string gitDiffFilename, string reportFilename)
        {
            Folder = folder;
            GitDiffFilename = gitDiffFilename;
            ReportFilename = reportFilename;
        }

        /// <summary>
        /// 報告書に対するGit Diff 差分テキストデータを取得
        /// テスト用ダミーデータ
        /// </summary>
        /// <returns>Git Diff 差分テキストデータ</returns>
        public string GetGitDiff()
        {
            return File.ReadAllText(Path.Combine(Folder, GitDiffFilename));
        }

        /// <summary>
        /// 報告書データを一行ごとリストに格納
        /// テスト用ダミーデータ
        /// </summary>
        /// <returns>報告書リストデータ</returns>
        public List<string> GetReportData()
        {
            List<string> text = new List<string>();
            try
            {
                using (var sr = new StreamReader(Path.Combine(Folder, ReportFilename)))
                {
                    while (sr.Peek() >= 0)
                    {
                        text.Add(sr.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return text;
        }
    }
}
