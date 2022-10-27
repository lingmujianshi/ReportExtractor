using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ReportExtractor.Domain;

namespace ReportExtractorTest
{
    [TestClass]
    public class ContentsTest
    {
        [TestMethod]
        public void ContentsTest1()
        {
            List<string> report = MyFile.ReadTextLines((@"ContentsTestData.txt"));
            ContentsEntity contents = new ContentsEntity(report);

            contents.GetLevel("### abc").Is<LevelEnum>(LevelEnum.Header3);
            contents.GetLevel(" ### abc").Is<LevelEnum>(LevelEnum.Header3);
            contents.GetLevel(" ### abc").Is<LevelEnum>(LevelEnum.Header3);

            contents.SetStrong(new List<int>() { 27 });

            contents.Items[3].EmphasisLevel.Is(0);
            contents.Items[7].EmphasisLevel.Is(0);
            contents.Items[8].EmphasisLevel.Is(0);
            contents.Items[18].EmphasisLevel.Is(0);
            contents.Items[26].EmphasisLevel.Is(1);

            contents.SetWrite();
            contents.Items[0].IsWrite.Is(false);
            contents.Items[1].IsWrite.Is(true);
            contents.Items[2].IsWrite.Is(false);
            contents.Items[3].IsWrite.Is(false);
            contents.Items[4].IsWrite.Is(false);
            contents.Items[5].IsWrite.Is(true);
            contents.Items[6].IsWrite.Is(false);
            contents.Items[7].IsWrite.Is(false);
            contents.Items[8].IsWrite.Is(false);
            contents.Items[9].IsWrite.Is(true);
            contents.Items[10].IsWrite.Is(true);
            contents.Items[11].IsWrite.Is(true);
            contents.Items[12].IsWrite.Is(false);
            contents.Items[13].IsWrite.Is(false);
            contents.Items[14].IsWrite.Is(true);
            contents.Items[15].IsWrite.Is(true);
            contents.Items[16].IsWrite.Is(false);
            contents.Items[17].IsWrite.Is(false);
            contents.Items[18].IsWrite.Is(false);
            contents.Items[19].IsWrite.Is(false);
            contents.Items[20].IsWrite.Is(true);
            contents.Items[21].IsWrite.Is(false);
            contents.Items[22].IsWrite.Is(false);
            contents.Items[23].IsWrite.Is(true);
            contents.Items[24].IsWrite.Is(true);
            contents.Items[25].IsWrite.Is(true);
            contents.Items[26].IsWrite.Is(true);
            contents.Items[27].IsWrite.Is(true);
            contents.Items[28].IsWrite.Is(false);

            int row = 1;
            Console.WriteLine($"row\tLevel\tIsWrite\tIsStrong");
            foreach (var i in contents.Items)
            {
                Console.WriteLine($"{row}\t{i.Level}\t{i.IsWrite}\t{i.EmphasisLevel}");
                row++;
            }
        }

        [TestMethod]
        public void ContentsTest2()
        {
            List<string> report2 = new List<string>();
            report2.Add(" +1行目");
            report2.Add(" !2行目");
            report2.Add(" 3行目");
            report2.Add(" -4行目");
            report2.Add("!+5行目");

            ContentsEntity contents2 = new ContentsEntity(report2);
            contents2.SetStrong(new List<int>() { 4 });
            contents2.Items[0].EmphasisLevel.Is(0);
            contents2.Items[1].EmphasisLevel.Is(0);
            contents2.Items[2].EmphasisLevel.Is(0);
            contents2.Items[3].EmphasisLevel.Is(1);
            contents2.Items[4].EmphasisLevel.Is(0);

            contents2.ManualEmphasisSet();
            contents2.Items[0].Item.Is(" 1行目");
            contents2.Items[0].IsWrite.Is(true);
            contents2.Items[0].EmphasisLevel.Is(1);
            contents2.Items[1].Item.Is(" 2行目");
            contents2.Items[1].IsWrite.Is(true);
            contents2.Items[1].EmphasisLevel.Is(2);
            contents2.Items[2].Item.Is(" 3行目");
            contents2.Items[2].IsWrite.Is(false);
            contents2.Items[2].EmphasisLevel.Is(0);
            contents2.Items[3].Item.Is(" 4行目");
            contents2.Items[3].IsWrite.Is(false);
            contents2.Items[3].EmphasisLevel.Is(0);
            contents2.Items[4].Item.Is("+5行目");
            contents2.Items[4].IsWrite.Is(true);
            contents2.Items[4].EmphasisLevel.Is(2);


        }
    }
}
