using System.Collections.Generic;
using System.IO;

namespace ReportExtractor.Domain
{
    public class ReportDecorator
    {
        public string ReportHtml { get; private set; }
        public string ReportHtmlShort { get; private set; }
        public string ReportHtmlForMail { get; private set; }
        public string ReportTitle { get; private set; }

        readonly IGetSource _source;
        
        public ReportDecorator()
        {
            if (Info.IsDummy)
            {
                _source = new ReportSourceDummy(Info.GitFolder, Info.GitDiffFilename, Info.ReportFilename);
            }
            else
            {
                _source = new ReportSource(Info.GitFolder, Info.ReportFilename);
            }
        }

        public void Execute()
        {    
            var gitdiff = _source.GetGitDiff();
            List<string> report = _source.GetReportData();

            AnalysisDiff analysis = new AnalysisDiff();
            analysis.GetLines(gitdiff);
            List<DiffLineInfo> infoList = analysis.GetLineInfoList();

            List<int> list = analysis.GetChangedNumbersList(infoList);

            ContentsEntity contents = new ContentsEntity(report);
            contents.SetStrong(list);
            contents.SetWrite();

            CreateHTML cl = new CreateHTML(contents.Items);
            
            ReportHtml = cl.GetHtml();
            ReportHtmlShort = cl.GetHtmlShort();
            ReportHtmlForMail = cl.GetHtmlForMail();
            ReportTitle = cl.Title;

            File.WriteAllText(Info.htmlFilename, ReportHtml);
            File.WriteAllText(Info.HtmlShortFilename, ReportHtmlShort);
            File.WriteAllText(Info.htmlMailFilename, ReportHtmlForMail);
        }
    }
}
