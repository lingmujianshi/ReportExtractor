namespace ReportExtractor.Domain
{
    /// <summary>
    /// ipconfigを実行して情報を受信するクラス
    /// </summary>
    public class GetGitDiff : OuterCommand
    {
        /// <summary>
        /// コマンド名 "ipconfig"
        /// </summary>
        const string COMMAND = "git";

        /// <summary>
        /// 引数なし
        /// </summary>
        const string ARGUE = @"-C C:\\Users\tsuzuki\Documents\作業・他\浦安工場作業報告 diff";

        public GetGitDiff()
        : base()
        {
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        /// <returns>コマンド結果</returns>
        public string Run()
        {
            return ExeCommand(COMMAND, ARGUE);
        }
    }
}
