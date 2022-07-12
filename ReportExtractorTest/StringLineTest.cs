using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;

namespace ReportExtractorTest
{
    [TestClass]
    public class StringLineTest
    {
        [TestMethod]
        public void 行の先頭位置()
        {
            string str = "01234\n6789\nabcdef";
            
            str.GetTopPos(3).Is(0);
            str.GetTopPos(6).Is(6);
            str.GetTopPos(13).Is(11);
        }

        [TestMethod]
        public void 行の終了位置()
        {
            string str = "01234\n6789\nabcdef";

            str.GetEndPos(0).Is(5);
            str.GetEndPos(6).Is(10);
            str.GetEndPos(13).Is(17);
        }

        [TestMethod]
        public void 行の幅()
        {
            string str = "01234\n6789\nabcdef";

            str.GetWidth(1,1).Is(5);
            str.GetWidth(6,7).Is(4);
            str.GetWidth(1,7).Is(10);
        }
    }
}
