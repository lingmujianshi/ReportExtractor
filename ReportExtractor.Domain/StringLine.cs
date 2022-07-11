using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractor.Domain
{
    public static class StringLine
    {
        public static int GetTopPos(this string str, int v)
        {
            int pos = str.LastIndexOf('\n', v);
            int result;

            if (pos < 0)
            {
                result = 0;
            }
            else
            {
                result = pos + 1;
            }

            return result;
        }

        public static int GetEndPos(this string str, int v)
        {
            int pos = str.IndexOf('\n', v);

            if (pos < 0)
            {
                pos = str.Length - 1;
            }

            return pos;
        }

        public static int GetWidth(this string str, int AreaTop, int AreaEnd)
        {
            int topPos = str.GetTopPos(AreaTop);
            int endPos = str.GetEndPos(AreaEnd);
            return endPos - topPos;
        }
    }
}
