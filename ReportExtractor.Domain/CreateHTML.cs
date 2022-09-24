using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractor.Domain
{
    public class CreateHTML
    {
        List<Content> _items;
        StringBuilder _body;
        string _section;
        string _paragraph;
        bool _isH1 = false;
        bool _isP = false;
        int _brCount;

        public CreateHTML(List<Content> items)
        {
            _items = items;
        }


        public string OutputText()
        {
            //return _htmeHeader + @"<h1>テストHTML</h1>\n" + _htmlFooter;
            StringBuilder sb = new StringBuilder();
            return sb.Append(_htmeHeader).Append(_body).Append(_htmlFooter).ToString();
        }


        public void MakeBody()
        {
            _body = new StringBuilder();

            foreach (Content item in _items)
            {
                if (!item.IsWrite) continue;

                var line = StrongProcess(item);

                switch (item.Level)
                {
                    case LevelEnum.Header1:
                        EndParagraphProcess();
                        EndSectionProcess();
                        _isH1 = true;
                        _section = "";
                        _section += _tagH1.AddTags(line);
                        AddNewLine(ref _section);
                        break;
                    case LevelEnum.Header2:
                        EndParagraphProcess();
                        _section += _tagH2.AddTags(line);
                        AddNewLine(ref _section);
                        break;
                    case LevelEnum.Header3:
                        EndParagraphProcess();
                        _section += _tagH3.AddTags(line);
                        AddNewLine(ref _section);
                        break;
                    case LevelEnum.None:
                        if (line == "")
                        {
                            EndParagraphProcess();
                        }
                        else
                        {
                            StartParagraphProcess();
                            _paragraph += AddBr(line);
                        }
                        break;
                }
            }
            EndParagraphProcess();
            EndSectionProcess();
        }

        private string AddBr(string str)
        {
            string res = "";
            if (RegexOp.IsBlankRow(str))
            {
                if (_brCount < 1)
                {
                    _brCount++;
                    res = _Br;
                    AddNewLine(ref res);

                }
            }
            else
            {
                _brCount = 0;
                res = str + _Br;
                AddNewLine(ref res);
            }
            return res;

        }

        private string StrongProcess(Content c)
        {
            string line = "";
            if (c.IsStrong)
            {
                line = _tagEm.AddTags(c.Item);
            }
            else
            {
                line = c.Item;
            }
            return line;
        }

        private void EndSectionProcess()
        {
            if (_isH1)
            {
                _tagSection.AddTags(_section);
                _isH1 = false;
                _body.Append(_section);
            }
        }

        private void StartParagraphProcess()
        {
            if (!_isP)
            {
                _paragraph = _tagP.Top;
                AddNewLine(ref _paragraph);
                _isP = true;
            }
        }

        private void EndParagraphProcess()
        {
            if (_isP)
            {
                _paragraph += _tagP.End;
                AddNewLine(ref _paragraph);
                _section += _paragraph;
                _isP = false;
            }
        }

        private void AddNewLine(ref string str)
        {
            str += _Cr;
        }

        HtmlTag _tagH1 = new HtmlTag("h1");
        HtmlTag _tagH2 = new HtmlTag("h2");
        HtmlTag _tagH3 = new HtmlTag("h3");
        HtmlTag _tagSection = new HtmlTag("section");
        HtmlTag _tagP = new HtmlTag("p");
        HtmlTag _tagEm = new HtmlTag("em");

        string _Br = @"<br>";
        string _Cr = "\r\n";

        StringBuilder _htmeHeader = new StringBuilder(@"

<HTML lang=""ja"">
<meta charset=""UTF-8"">
<style>
    body{
        width: 40em;
        margin-left: 5px;
    }
    h1 {
        font-size: 20px;
        font-family: monospace;
        font-weight: bold;
        background-color: rgba(0, 0, 0, 0.05);
        padding: 10px 20px;
        border-radius: 5px;
 
    }
    h2 {
        font-size: 14px;
        font-family: monospace;
        border-left: solid 5px #999999;
        border-bottom: solid 1px #999999;
        padding-left: 5px;
        
    }

    h3 {
        font-family: monospace;
        font-size: 12px;
    }

    p{
        font-size: 12px;
        margin-left: 1em;
    }

    em {
        font-style: normal;
        color: blue;
    }

    strong {
        font-style: normal;
        color: red;
    }
</style>

<BODY>
");
        StringBuilder _htmlFooter = new StringBuilder(@"
</BODY>

</HTML>
");
    }
}
