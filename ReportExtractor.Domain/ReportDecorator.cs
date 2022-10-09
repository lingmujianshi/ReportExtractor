using System.Collections.Generic;
using System.IO;

namespace ReportExtractor.Domain
{
    public class ReportDecorator
    {
        public string ReportHtml { get; private set; }
        public string ReportHtmlShort { get; private set; }
        IGetSource _source;
        
        public ReportDecorator()
        {
            _source = GetSourceFactory.Create(Info.IsDummy);

        }

        public void Execute()
        {    
            var gitdiff = _source.GetGitDiff();
            List<string> report = _source.GetReportData();

            AnalysisDiff analysis = new AnalysisDiff();
            List<DiffLineInfo> infoList = new List<DiffLineInfo>();
            analysis.GetLines(gitdiff);
            infoList = analysis.GetLineInfoList();

            List<int> list = new List<int>();
            list = analysis.GetChangedNumbersList(infoList);

            ContentsEntity contents = new ContentsEntity(report);
            contents.SetStrong(list);
            contents.SetWrite();

            CreateHTML cl = new CreateHTML(contents.Items);
            
            ReportHtml = cl.GetHtml();
            ReportHtmlShort = cl.GetHtmlShort();

            File.WriteAllText(Info.htmlFilename, ReportHtml);
            File.WriteAllText(Info.HtmlShortFilename, ReportHtmlShort);
        }
    }
}
