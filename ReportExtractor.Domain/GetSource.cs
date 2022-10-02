using System;
using System.Collections.Generic;
using System.IO;

namespace ReportExtractor.Domain
{
    /// <summary>
    /// git diffを実行して情報を受信するクラス
    /// </summary>
    public sealed class GetSource : IGetSource
    {
        //public string _command = @"C:\Program Files\Git\cmd\git.exe";
        string _command = @"git";
        public string GitDiffArg { get; }
        public string Folder { get; }
        public string Filename { get; }
        OuterCommand oc;

        public GetSource(string folder, string filename)
        {
            Folder = folder;
            Filename = filename;
            GitDiffArg = $"-C {Folder} diff";
            oc = new OuterCommand();
        }

        string IGetSource.GetGitDiff()
        {
            return oc.ExeCommandSync(_command, GitDiffArg);
        }

        public List<string> GetReportData()
        {
            List<string> text = new List<string>();
            try
            {
                using (var sr = new StreamReader(Path.Combine(Folder, Filename)))
                {
                    while (sr.Peek() >= 0)
                    {
                        text.Add(sr.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return text;
        }
    }
}
