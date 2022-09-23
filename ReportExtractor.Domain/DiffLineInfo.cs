namespace ReportExtractor.Domain
{
    /// <summary>
    /// gitdiff結果解析用構造体
    /// one-based
    /// </summary>
    public struct DiffLineInfo
    {
        public DiffLineInfo(int pos, int start, int end)
        {
            Pos = pos;
            Start = start;
            End = end;
        }

        /// <summary>
        /// テキスト位置
        /// </summary>
        public int Pos;

        /// <summary>
        ///  記載開始行
        /// </summary>
        public int Start;
        
        /// <summary>
        /// 記載終了行
        /// </summary>
        public int End;
    }
}
