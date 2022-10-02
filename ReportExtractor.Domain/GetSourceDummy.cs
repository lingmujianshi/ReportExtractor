using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReportExtractor.Domain
{
    public sealed class GetSourceDummy : IGetSource
    {
        string Folder { get; }
        string GitDiffFilename { get; }
        string ReportFilename { get; }

        public GetSourceDummy(string folder, string gitDiffFilename, string reportFilename)
        {
            Folder = folder;
            GitDiffFilename = gitDiffFilename;
            ReportFilename = reportFilename;
        }

        public string GetGitDiff()
        {
            return File.ReadAllText(Path.Combine(Folder, GitDiffFilename));
        }

        public List<string> GetReportData()
        {
            List<string> text = new List<string>();
            try
            {
                using (var sr = new StreamReader(Path.Combine(Folder, ReportFilename)))
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
