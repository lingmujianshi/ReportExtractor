using ReportExtractor.Domain;
using System;


namespace ReportExtractorConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Info.ReadSetting();

            var rd = new ReportDecorator();
            rd.Execute();

            var mail = new OutlookMail();
            mail.Subject = rd.ReportTitle;
            mail.Body = rd.ReportHtmlForMail;
            mail.To = @"";
            mail.CC = @"";

            mail.CreateMail();

        }

        static void GetGitDiff()
        {
            //String str = ExeCommandSync("git", @"-C C:\\Users\tsuzuki\Documents\作業・他\浦安工場作業報告 diff");
            var cmd = new OuterCommand();
            //String str = cmd.ExeCommand(@"C:\Program Files\Git\cmd\git.exe", @"--version");
            String str = cmd.ExeCommand("git", @"--version");
            Console.WriteLine(str);
        }

        static void MailDemo()
        {
            var mail = new OutlookMail();
            mail.Subject = @"メールのタイトル";
            mail.Body = @"<body>ここに<em>文面</em>を書きます。</body>";
            mail.To = @"aaa@xxx.com";
            mail.CC = @"bbb@yyy.com";

            mail.CreateMail();
        }
    }
}
