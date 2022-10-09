namespace ReportExtractor.Domain
{
    internal static class GetSourceFactory
    {
        public static IGetSource Create(bool isDummy)
        {
            if (isDummy)
            {
                return new ReportSourceDummy(Info.GitFolder, Info.GitDiffFilename, Info.ReportFilename);
            }
            else
            {
                return new ReportSource(Info.GitFolder, Info.ReportFilename);
            }
        }
    }
}
