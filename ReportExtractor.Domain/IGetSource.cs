using System.Collections.Generic;

namespace ReportExtractor.Domain
{
    public interface IGetSource
    {
        /// <summary>
        /// 報告書に対するGit Diff 差分テキストデータを取得
        /// </summary>
        /// <returns>Git Diff 差分テキストデータ</returns>
        string GetGitDiff();

        /// <summary>
        /// 報告書データを一行ごとリストに格納
        /// </summary>
        /// <returns>報告書リストデータ</returns>
        List<string> GetReportData();
    }
}
