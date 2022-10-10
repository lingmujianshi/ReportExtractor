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
            mail.Subject = "電気制御二課状況報告";
            mail.Body = _rd.ReportHtmlShort;
            mail.To = @"";
            mail.CC = @"";

            mail.CreateMail();
        }
    }
}
