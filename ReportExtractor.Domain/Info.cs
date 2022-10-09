using System;
using System.IO;

namespace ReportExtractor.Domain
{
    public static class Info
    {
        public static bool IsDummy = true;
        public static string GitFolder { get; private set; } = @"../../../\ReportExtractorTest\bin\Debug\TestData";
        public static string GitDiffFilename { get; private set; } = @"gitdiff3.txt";
        public static string ReportFilename { get; private set; } = @"Report3.txt";
        public static string HtmlShortFilename { get; private set; } = @"Report_s.html";
        public static string htmlFilename { get; private set; } = @"Report.html";

        /// <summary>
        /// ファイルなど設定データの読み込み
        /// </summary>
        /// <exception cref="InputException"></exception>
        public static void ReadSetting()
        {
            try
            {
                var Items = File.ReadAllLines(@"Info.txt");
                IsDummy = bool.Parse(Items[0]);
                GitFolder = Items[1];
                GitDiffFilename = Items[2];
                ReportFilename = Items[3];
                HtmlShortFilename = Items[4];
                htmlFilename = Items[5];
            }
            catch (Exception ex)
            {
                throw new InputException(ex.Message);
            }
            
        }
    }
}
