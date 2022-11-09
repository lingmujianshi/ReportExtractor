using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static string htmlMailFilename { get; private set; } = @"ReportMail.html";

        public static string CssStyle { get; private set; } = @"";

        public static void ReadSetting()
        {
            var Items = File.ReadAllLines(@"Info.txt");
            IsDummy = bool.Parse(Items[0]);
            GitFolder = Items[1];
            GitDiffFilename = Items[2];
            ReportFilename = Items[3];
            HtmlShortFilename = Items[4];
            htmlFilename = Items[5];
            CssStyle = File.ReadAllText(@"style.css");
        }

    }
}
