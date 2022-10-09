using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;

namespace ReportExtractorTest
{
    [TestClass]
    public class GetSourceTest
    {
        [TestMethod]
        public void レポートデータ()
        {
            IGetSource source = new ReportSource(@"D:\VS\AutoReport\ReportExtractor", @"ReportExtractor.sln");
            Console.Write(source.GetReportData());
        }

        //[TestMethod]
        public void GitDiffデータ()
        {
            IGetSource source = new ReportSource(@"D:\VS\AutoReport\ReportExtractor", @"ReportExtractor.sln");
            Console.Write(source.GetGitDiff());
        }
    }
}
