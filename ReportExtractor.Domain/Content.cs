using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ReportExtractorTest")]

namespace ReportExtractor.Domain
{
    internal sealed class Content
    {
        public Content()
        {

        }
        public Content(string item)
        {
            Item = item;
        }
        public LevelEnum Level { get; set; } = LevelEnum.None;
        public string Item { get; set; } = "";
        public bool IsWrite { get; set; } = false;
        public int EmphasisLevel { get; set; }
    }

    public enum LevelEnum
    {
        None = 0,
        Header1 = 1,
        Header2 = 2,
        Header3 = 3
    }
}
