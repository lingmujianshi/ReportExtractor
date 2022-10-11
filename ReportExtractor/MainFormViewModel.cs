using ReportExtractor.Domain;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractor
{
    internal sealed class MainFormViewModel
    {
        ReportDecorator _rd;
        public MainFormViewModel()
        {
            Info.ReadSetting();
            _rd = new ReportDecorator();
        }

        internal void MakeHtml()
        {
            Info.ReadSetting();

            _rd.Execute();

            var mail = new OutlookMail();
            mail.Subject = _rd.ReportTitle;
            mail.Body = _rd.ReportHtmlForMail;
            mail.To = @"";
            mail.CC = @"";

            mail.CreateMail();
        }
    }
}
