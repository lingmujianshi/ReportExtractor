using ReportExtractor.Domain;
using System;


namespace ReportExtractorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Console.WriteLine("出力");

            Info.ReadSetting();

            var rd = new ReportDecorator();
            rd.Execute();

            var mail = new OutlookMail();
            mail.Subject = rd.ReportTitle;
            mail.Body = rd.ReportHtmlForMail;
            mail.To = @"";
            mail.CC = @"";

            mail.CreateMail();

            //Console.ReadKey();

       }

        static void GetGitDiff()
        {
            //String str = ExeCommandSync("git", @"-C C:\\Users\tsuzuki\Documents\作業・他\浦安工場作業報告 diff");
            var cmd = new OuterCommand();
            //String str = cmd.ExeCommand(@"C:\Program Files\Git\cmd\git.exe", @"--version");
            String str = cmd.ExeCommand("git", @"--version");
            Console.WriteLine(str);
        }
    }
}
