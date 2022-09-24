using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;

namespace ReportExtractorTest
{
    [TestClass]
    public class LevelCheckTest
    {
        [TestMethod]
        public void レベル1()
        {
            var levelCheck = new LevelCheck();
            levelCheck.IsHeader1("□１２３４").Is(true);
            levelCheck.IsHeader1(" □１２３４").Is(false);
        }

        [TestMethod]
        public void レベル2()
        {
            var levelCheck = new LevelCheck();
            levelCheck.IsHeader2("aa").Is(true);
            levelCheck.IsHeader2("  aa").Is(false);
            levelCheck.IsHeader2("漢字").Is(true);
        }

        [TestMethod]
        public void レベル3()
        {
            var levelCheck = new LevelCheck();
            levelCheck.IsHeader3("  aa").Is(true);
            levelCheck.IsHeader3("   aa").Is(true);
        }

        [TestMethod]
        public void 空行()
        {
            var levelCheck = new LevelCheck();
            levelCheck.IsBlankRow("").Is(true);
            levelCheck.IsBlankRow(" ").Is(true);
            levelCheck.IsBlankRow(" ").Is(true);
            levelCheck.IsBlankRow("     ").Is(true);
            levelCheck.IsBlankRow(" a").Is(false);

        }
    }
}
