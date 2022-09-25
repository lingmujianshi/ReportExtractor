using System.Collections.Generic;

namespace ReportExtractor.Domain
{
    public interface IGetSource
    {
        string GetGitDiff();
        List<string> GetReportData();
    }
}
