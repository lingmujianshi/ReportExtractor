using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;
using System.Collections.Generic;

namespace ReportExtractorTest
{
    [TestClass]
    public class TotalTest
    {
        [TestMethod]
        public void TotalTest1()
        {
            //var gitdiff = MyFile.ReadTextFile(@"gitdiff1.txt");
            //List<string> report = MyFile.ReadTextLines(@"Report1.txt");

            IGetSource source = new GetSourceDummy(".", @"gitdiff1.txt", @"Report1.txt");
            var gitdiff = source.GetGitDiff();
            List<string> report = source.GetReportData(); 

            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();
            analysis.GetLines(gitdiff);
            infoList = analysis.GetLineInfoList();

            List<int> list = new List<int>();
            list = analysis.GetChangedNumbersList(infoList);

            ContentsEntity contents = new ContentsEntity(report);
            contents.SetStrong(list);
            contents.SetWrite();

            CreateHTML cl = new CreateHTML(contents.Items);

            MyFile.WriteTexts(@"TotalTest1.html", cl.GetHtml());
            MyFile.WriteTexts(@"TotalTest1_s.html", cl.GetHtmlShort());

        }

        [TestMethod]
        public void TotalTest2()
        {
            var gitdiff = MyFile.ReadTextFile(@"gitdiff2.txt");
            List<string> report = MyFile.ReadTextLines(@"Report2.txt");


            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();
            analysis.GetLines(gitdiff);
            infoList = analysis.GetLineInfoList();

            List<int> list = new List<int>();
            list = analysis.GetChangedNumbersList(infoList);

            ContentsEntity contents = new ContentsEntity(report);
            contents.SetStrong(list);
            contents.SetWrite();

            CreateHTML cl = new CreateHTML(contents.Items);

            MyFile.WriteTexts(@"TotalTest2.html", cl.GetHtml());
            MyFile.WriteTexts(@"TotalTest2_s.html", cl.GetHtmlShort());

        }

        [TestMethod]
        public void TotalTest3()
        {
            var gitdiff = MyFile.ReadTextFile(@"gitdiff3.txt");
            List<string> report = MyFile.ReadTextLines(@"Report3.txt");


            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();
            analysis.GetLines(gitdiff);
            infoList = analysis.GetLineInfoList();

            List<int> list = new List<int>();
            list = analysis.GetChangedNumbersList(infoList);

            ContentsEntity contents = new ContentsEntity(report);
            contents.SetStrong(list);
            contents.SetWrite();

            CreateHTML cl = new CreateHTML(contents.Items);

            MyFile.WriteTexts(@"TotalTest3.html", cl.GetHtml());
            MyFile.WriteTexts(@"TotalTest3_s.html", cl.GetHtmlShort());

        }
    }
}
