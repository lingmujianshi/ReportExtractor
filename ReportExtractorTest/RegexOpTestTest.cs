using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;

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

        [TestMethod]
        public void 先頭余計な文字を削除()
        {
            RegexOp.TrimTops("#文字列").Is("文字列");
            RegexOp.TrimTops("# 文字列").Is("文字列");
            RegexOp.TrimTops(" #文字列").Is("文字列");
            RegexOp.TrimTops("##文字列").Is("文字列");
            RegexOp.TrimTops("###文字列").Is("文字列");
            RegexOp.TrimTops(" 文字列").Is("文字列");
            RegexOp.TrimTops("!文字列").Is("文字列");
            RegexOp.TrimTops(" #    文字列").Is("文字列");

            RegexOp.TrimTops(" ! 文字列!").Is("文字列!");
        }

        [TestMethod]
        public void 改行スペースなどを削除()
        {
            RegexOp.TrimAll("\t#文字列\r\n").Is("#文字列");
            RegexOp.TrimAll("\t  文字列\r\n").Is("文字列");
            RegexOp.TrimAll("\t 文字列\r\n").Is(" 文字列");
            RegexOp.TrimAll(
                @"<HTML lang=""ja"">
<meta charset=""UTF-8"">
").Is(@"<HTML lang=""ja""><meta charset=""UTF-8"">");

        }

    }
}
