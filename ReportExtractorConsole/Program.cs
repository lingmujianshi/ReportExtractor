using ReportExtractor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            OuterCommand cmd = new OuterCommand();
            Console.WriteLine("出力");

            String str = cmd.ExeCommandSync("git", @"-C C:\\Users\tsuzuki\Documents\作業・他\浦安工場作業報告 diff");
            //String str = cmd.ExeCommand("gitdiff.bat", @"");
            Console.WriteLine(str);
            Console.ReadKey();

       }
    }
}
