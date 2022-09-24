using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;
using System.Collections.Generic;

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

            contents.SetStrong(new List<int>() { 4, 8 ,19});

            contents.Items[3].IsStrong.Is(true);
            contents.Items[7].IsStrong.Is(true);
            contents.Items[8].IsStrong.Is(false);
            contents.Items[18].IsStrong.Is(true);

            contents.SetWrite();
            contents.Items[0].IsWrite.Is(true);
            contents.Items[1].IsWrite.Is(true);
            contents.Items[2].IsWrite.Is(true);
            contents.Items[3].IsWrite.Is(true);
            contents.Items[4].IsWrite.Is(false);
            contents.Items[5].IsWrite.Is(true);
            contents.Items[6].IsWrite.Is(true);
            contents.Items[7].IsWrite.Is(true);
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
            contents.Items[18].IsWrite.Is(true);

            int row = 1;
            foreach (var i in contents.Items)
            {
                Console.WriteLine($"{row}\t{i.Level}\t{i.IsWrite}\t{i.IsStrong}");
                row++;
            }
        }
    }
}
