using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;

namespace ReportExtractorTest
{
    [TestClass]
    public class RegexOpTest
    {
        [TestMethod]
        public void レベル1()
        {
            RegexOp.IsHeader1("#１２３４").Is(true);
            RegexOp.IsHeader1("##１２３４").Is(false);
        }

        [TestMethod]
        public void レベル2()
        {
            RegexOp.IsHeader2("##aa").Is(true);
            RegexOp.IsHeader2("###aa").Is(false);
            RegexOp.IsHeader2("## 漢字").Is(true);
        }

        [TestMethod]
        public void レベル3()
        {
            RegexOp.IsHeader3("###  aa").Is(true);
            RegexOp.IsHeader3("###aa").Is(true);
            RegexOp.IsHeader3("#aa").Is(false);
            RegexOp.IsHeader3("##aa").Is(false);
        }

        [TestMethod]
        public void 空行()
        {
            RegexOp.IsBlankRow("").Is(true);
            RegexOp.IsBlankRow(" ").Is(true);
            RegexOp.IsBlankRow(" ").Is(true);
            RegexOp.IsBlankRow("     ").Is(true);
            RegexOp.IsBlankRow(" a").Is(false);
        }
    }
}
