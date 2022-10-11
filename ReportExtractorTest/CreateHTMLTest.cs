using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace ReportExtractorTest
{
    [TestClass]
    public class CreateHTMLTest
    {
        [TestMethod]
        public void ReadContentsTest()
        {
            var lines = MyFile.ReadTextLines(@"ContentsTestDataEntity.txt");
            List<Content> items = new List<Content>();
            foreach (var line in lines)
            {
                var dat = line.Split(',');
                items.Add(new Content()
                {
                    Item = dat[0],
                    Level = (LevelEnum)int.Parse(dat[1]),
                    IsWrite = bool.Parse(dat[2]),
                    EmphasisLevel = int.Parse(dat[3])
                });
            }
            CreateHTML cl = new CreateHTML(items);

            MyFile.WriteTexts(@"test.html", cl.GetHtml());
            MyFile.WriteTexts(@"test_s.html", cl.GetHtmlShort());
            MyFile.WriteTexts(@"testMail.html", cl.GetHtmlForMail());

        }
    }
}
