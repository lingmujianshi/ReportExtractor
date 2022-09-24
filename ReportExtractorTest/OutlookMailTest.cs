using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportExtractor.Domain;
using System;

namespace ReportExtractorTest
{
    [TestClass]
    public class OutlookMailTest
    {
        //[TestMethod]
        public void メール作成()
        {
            var body = MyFile.ReadTextFile(@"test.html");

            var mail = new OutlookMail();
            mail.Subject = "メールのタイトル";
            mail.Body = body;
            mail.To = @"XXX@XXX.co.jp; YYY@YYY.co.jp;";
            mail.CC = @"AAA@AAA.co.jp;";

            mail.CreateMail();
        }
    }
}
