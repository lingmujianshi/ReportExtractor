using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportExtractorTest
{
    [TestClass]
    public class AnalysisDiffTest
    {

        [TestMethod]
        public void diff情報正規表現()
        {
            AnalysisDiff analysis = new AnalysisDiff();
            string str;
            str = @"@@ -12,34 +56,78 @@";
            analysis.IsDiffInfo(str).Is(true);
            str = @"@@ -34,33 + 34,18 @@";
            analysis.IsDiffInfo(str).Is(true);
            str = @"@@ -34, 33 +34, 18 @@";
            analysis.IsDiffInfo(str).Is(true);
            str = @"@@ -114,6 +99,7 @@ MTC-ST#13450-1P Hefei → 田中";
            analysis.IsDiffInfo(str).Is(true);
        }

        [TestMethod]
        public void 開始位置()
        {
            AnalysisDiff analysis = new AnalysisDiff();
            string str;

            str = @"@@ -12,34 +56,78 @@";
            analysis.GetPosition(str).Is(56);

            str = @"@@ -34,33 + 34,18 @@";
            analysis.GetPosition(str).Is(34);

            str = @"@@ -34, 33 + 34, 18 @@";
            analysis.GetPosition(str).Is(34);

            str = @"@@ -114,6 +99,7 @@ MTC-ST#13450-1P Hefei → 田中";
            analysis.GetPosition(str).Is(99);
        }

        //string _folder = @".\TestData\";

        //string File.ReadTextFile(string filename)
        //{
        //    string text = "";
        //    try
        //    {
        //        using (var sr = new StreamReader(_folder + filename, Encoding.GetEncoding("UTF-8")))
        //        {
        //            text = sr.ReadToEnd();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }
        //    return text;
        //}

        [TestMethod]
        public void ポジション開始終了()
        {
            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();

            analysis.GetLines(MyFile.ReadTextFile(@"gitdiff1.txt"));
            infoList = analysis.GetLineInfoList();
            infoList[0].Pos.Is(1);
            infoList[0].Start.Is(6);
            infoList[0].End.Is(11);

            analysis.GetLines(MyFile.ReadTextFile(@"gitdiff2.txt"));
            infoList = analysis.GetLineInfoList();
            infoList.Count.Is(10);

            infoList[0].Pos.Is(1);
            infoList[0].Start.Is(6);
            infoList[0].End.Is(22);

            infoList[1].Pos.Is(26);
            infoList[1].Start.Is(24);
            infoList[1].End.Is(31);

            infoList[2].Pos.Is(34);
            infoList[2].Start.Is(33);
            infoList[2].End.Is(72);

            infoList[3].Pos.Is(99);
            infoList[3].Start.Is(74);
            infoList[3].End.Is(80);

            infoList[4].Pos.Is(110);
            infoList[4].Start.Is(82);
            infoList[4].End.Is(88);

            infoList[5].Pos.Is(134);
            infoList[5].Start.Is(90);
            infoList[5].End.Is(99);

            infoList[6].Pos.Is(254);
            infoList[6].Start.Is(101);
            infoList[6].End.Is(109);

            infoList[7].Pos.Is(331);
            infoList[7].Start.Is(111);
            infoList[7].End.Is(122);

            infoList[8].Pos.Is(399);
            infoList[8].Start.Is(124);
            infoList[8].End.Is(131);

            infoList[9].Pos.Is(470);
            infoList[9].Start.Is(133);
            infoList[9].End.Is(153);

            analysis.GetLines(MyFile.ReadTextFile(@"gitdiff3.txt"));
            infoList = analysis.GetLineInfoList();
            infoList.Count.Is(4);
            infoList[0].Pos.Is(1);
            infoList[0].Start.Is(6);
            infoList[0].End.Is(24);

            infoList[1].Pos.Is(24);
            infoList[1].Start.Is(26);
            infoList[1].End.Is(32);

            infoList[2].Pos.Is(36);
            infoList[2].Start.Is(34);
            infoList[2].End.Is(50);

            infoList[3].Pos.Is(121);
            infoList[3].Start.Is(52);
            infoList[3].End.Is(60);
        }

        [TestMethod]
        public void 変更行配列取得()
        {
            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();
            List<int> list = new List<int>();
            string result,ans;

            analysis.GetLines(MyFile.ReadTextFile(@"gitdiff1.txt"));
            infoList = analysis.GetLineInfoList();
            list = analysis.GetChangedNumbersList(infoList);
            result = string.Join(",", list.ToArray());
            list.Count.Is(1);
            ans = MyFile.ReadTextFile(@"gitdiff1Array.txt");
            result.Is(ans);


            analysis.GetLines(MyFile.ReadTextFile(@"gitdiff2.txt"));
            infoList = analysis.GetLineInfoList();
            list = analysis.GetChangedNumbersList(infoList);
            list.Count.Is(43);
            result = string.Join(",", list.ToArray());
            ans = MyFile.ReadTextFile(@"gitdiff2Array.txt");
            result.Is(ans);


            analysis.GetLines(MyFile.ReadTextFile(@"gitdiff3.txt"));
            infoList = analysis.GetLineInfoList();
            list = analysis.GetChangedNumbersList(infoList);
            result = string.Join(",", list.ToArray());
            list.Count.Is(11);
            ans = MyFile.ReadTextFile(@"gitdiff3Array.txt");
            result.Is(ans);
        }
    }
}
