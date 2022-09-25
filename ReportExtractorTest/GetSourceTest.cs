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
            IGetSource source = new GetSource(@"D:\VS\AutoReport\ReportExtractor", @"ReportExtractor.sln");
            Console.Write(source.GetReportData());
        }

        //[TestMethod]
        public void gitdiffデータ()
        {
            IGetSource source = new GetSource(@"D:\VS\AutoReport\ReportExtractor", @"ReportExtractor.sln");
            Console.Write(source.GetGitDiff());
        }
    }
}
