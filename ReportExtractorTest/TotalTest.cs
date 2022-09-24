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
            var gitdiff = MyFile.ReadTextFile(@"gitdiff1.txt");
            List<string> report = MyFile.ReadTextLines(@"Report1.txt");


            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();
            analysis.GetLines(gitdiff);
            infoList = analysis.GetLineInfoList();

            List<int> list = new List<int>();
            list = analysis.GetChangedNumbersList(infoList);

            ContentsEntity contents = new ContentsEntity(report);
            contents.SetStrong(list);

            CreateHTML cl = new CreateHTML(contents.Items);

            cl.MakeBody();
            cl.OutputText();

            MyFile.WriteTexts(@"TotalTest1.html", cl.OutputText());

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

            CreateHTML cl = new CreateHTML(contents.Items);

            cl.MakeBody();
            cl.OutputText();

            MyFile.WriteTexts(@"TotalTest2.html", cl.OutputText());

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

            CreateHTML cl = new CreateHTML(contents.Items);

            cl.MakeBody();
            cl.OutputText();

            MyFile.WriteTexts(@"TotalTest3.html", cl.OutputText());

        }
    }
}
