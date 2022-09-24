using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractor.Domain
{
    public class HtmlTag
    {
        public string Top { get; }
        public string End { get; }

        public HtmlTag(string tag)
        {
            Top = $"<{tag}>";
            End = $"</{tag}>";
        }

        public string AddTags(string str)
        {
            return Top + str + End;
        }
    }
}
