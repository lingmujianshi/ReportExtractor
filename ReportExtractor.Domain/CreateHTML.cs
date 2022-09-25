using System.Collections.Generic;
using System.Text;

namespace ReportExtractor.Domain
{
    /// <summary>
    /// コンテンツリストからHTMLを作成するクラス
    /// </summary>
    public class CreateHTML
    {
        /// <summary>
        /// 元となるとなるContentリスト
        /// </summary>
        List<Content> _items;

        /// <summary>
        /// HTML body部分を格納
        /// </summary>
        StringBuilder _body;

        /// <summary>
        /// Sectionエリアの文字を一時格納
        /// </summary>
        string _section;

        /// <summary>
        /// Pエリアの文字を一時格納
        /// </summary>
        string _paragraph;

        /// <summary>
        /// セクション内であるかどうか
        /// </summary>
        bool _isSectionStart;

        /// <summary>
        /// Pタグプロセス中か
        /// </summary>
        bool _isPStart;

        /// <summary>
        /// 連続空白行カウンタ
        /// </summary>
        int _blankLineCount;

        /// <summary>
        /// 更新情報のみのHTMLを格納
        /// </summary>
        string _htmlShort;

        /// <summary>
        /// 全体のHTMLを格納
        /// </summary>
        string _html;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="items">コンテンツリスト</param>
        public CreateHTML(List<Content> items)
        {
            _items = items;
            TrimTopSignes();
            
            //更新情報のみのHTMLを作成して格納
            MakeBody(true);
            _htmlShort = CombineHtml();
            _htmlShort=RegexOp.TrimAll(_htmlShort);

            //全体のHTMLを作成して格納
            MakeBody(false);
            _html = CombineHtml();
        }

        /// <summary>
        /// 更新情報のみのHTMLを取得
        /// </summary>
        /// <returns>HTMLテキスト</returns>
        public string GetHtmlShort()
        {
            return _htmlShort;
        }

        /// <summary>
        /// 全体のHTMLを取得
        /// </summary>
        /// <returns>HTMLテキスト</returns>
        public string GetHtml()
        {
            return _html;
        }

        /// <summary>
        /// 先頭の`#,!,Space,Tabなど余計な文字を削除
        /// </summary>
        private void TrimTopSignes()
        {
            foreach (Content i in _items)
            {
                i.Item = RegexOp.TrimTops(i.Item);
            }
        }

        /// <summary>
        /// 作成したHTLMとヘッダ・フッタを結合
        /// </summary>
        /// <returns>HTMLテキスト</returns>
        private string CombineHtml()
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append(_htmeHeader).Append(_body).Append(_htmlFooter).ToString();
        }

        /// <summary>
        /// HTML Body部分を作成
        /// </summary>
        /// <param name="updatedOnly">更新分のみ表示</param>
        private void MakeBody(bool updatedOnly)
        {
            _body = new StringBuilder();
            StartSectionProcess();

            foreach (Content item in _items)
            {
                if (updatedOnly)
                {
                    // 記載しない行の場合は次の行へ
                    if (!item.IsWrite) continue;
                }

                // 空白行判定用
                var isBlank = RegexOp.IsBlankRow(item.Item);

                // 入力行
                var line = "";

                // 空白行ではない場合
                if (!isBlank)
                {
                    //空白行ではないので、空白行カウントをリセット
                    _blankLineCount = 0;

                    // 判定して必要な場合emタグかstrongタグを付加
                    line = EmphasisProcess(item);
                }

                // レベルによって処理を変更
                switch (item.Level)
                {
                    // H1タグ
                    case LevelEnum.Header1:
                        EndParagraphProcess();

                        //sectionに何かしらの文字が入っているとき
                        if (_section != "")
                        {
                            //ヘッダ１ごとにsectionを区切る
                            EndSectionProcess();
                            StartSectionProcess();
                        }

                        //前後にタグ追加
                        _section += _tagH1.AddTags(line);
                        //HTMLをテキストで見やすくするため改行
                        AddNewLine(ref _section);
                        break;

                    // H2タグ
                    case LevelEnum.Header2:
                        EndParagraphProcess();
                        //前後にタグ追加
                        _section += _tagH2.AddTags(line);
                        //HTMLをテキストで見やすくするため改行
                        AddNewLine(ref _section);
                        break;

                    // H3タグ
                    case LevelEnum.Header3:
                        EndParagraphProcess();
                        //前後にタグ追加
                        _section += _tagH3.AddTags(line);
                        //HTMLをテキストで見やすくするため改行
                        AddNewLine(ref _section);
                        break;

                    // Pタグ
                    case LevelEnum.None:
                        if (isBlank)
                        {
                            //空行がある場合Pタグで区切る
                            EndParagraphProcess();
                        }
                        else
                        {
                            //ヘッダタグや空行でない場合はPタグ開始
                            StartParagraphProcess();
                            _paragraph += AddBr(line, isBlank);
                        }
                        break;
                }
            }

            //一通り読み終わった後のタグ付加
            EndParagraphProcess();
            EndSectionProcess();
        }

        /// <summary>
        /// 判断し最後にBrタグを付加
        /// </summary>
        /// <param name="line">入力文字列</param>
        /// <returns>処理後の文字列</returns>
        private string AddBr(string line, bool isBlank)
        {
            string res = "";
            if (isBlank)
            {
                //空白行が2行以上が続いたらBrタグは追加しない。
                if (_blankLineCount < 1)
                {
                    //空白行をカウント
                    _blankLineCount++;
                    ////brタグ追加
                    //res = HtmlTag.Br;
                    ////HTMLをテキストで見やすくするため改行
                    //AddNewLine(ref res);

                }
            }
            //else
            //{
                //brタグ追加
                res = line + HtmlTag.Br;
                //HTMLをテキストで見やすくするため改行
                AddNewLine(ref res);
            //}
            return res;

        }

        /// <summary>
        /// 判定して必要な場合emタグかstrongタグを付加
        /// </summary>
        /// <param name="c">Content行構造体</param>
        /// <returns></returns>
        private string EmphasisProcess(Content c)
        {
            string line = "";
            if (c.EmphasisLevel == 1)
            {
                // emタグ付加
                line = _tagEm.AddTags(c.Item);
            }
            else if (c.EmphasisLevel == 2)
            {
                // strongタグ付加
                line = _tagStrong.AddTags(c.Item);
            }
            else
            {
                // 付加しないでそのまま
                line = c.Item;
            }
            return line;
        }

        /// <summary>
        /// セクション開始時の処理
        /// </summary>
        private void StartSectionProcess()
        {
            if (!_isSectionStart)
            {
                _isSectionStart = true;
                _section = "";
            }
        }

        /// <summary>
        /// セクション終了時の処理
        /// </summary>
        private void EndSectionProcess()
        {
            if (_isSectionStart)
            {
                _section = _tagSection.AddTagsNewLine(_section);
                _isSectionStart = false;
                _body.Append(_section);
            }
        }

        /// <summary>
        /// Pタグ開始時の処理
        /// </summary>
        private void StartParagraphProcess()
        {
            if (!_isPStart)
            {
                _paragraph = _tagP.Top;
                AddNewLine(ref _paragraph);
                _isPStart = true;
            }
        }

        /// <summary>
        /// Pタグ終了時の処理
        /// </summary>
        private void EndParagraphProcess()
        {
            if (_isPStart)
            {
                _paragraph += _tagP.End;
                AddNewLine(ref _paragraph);
                _section += _paragraph;
                _isPStart = false;
            }
        }

        /// <summary>
        /// 改行コードを追加（見やすくするため）
        /// </summary>
        /// <param name="str"></param>
        private void AddNewLine(ref string str)
        {
            str += HtmlTag.Cr;
        }

        // タグ

        HtmlTag _tagH1 = new HtmlTag("h1");
        HtmlTag _tagH2 = new HtmlTag("h2");
        HtmlTag _tagH3 = new HtmlTag("h3");
        HtmlTag _tagSection = new HtmlTag("section");
        HtmlTag _tagP = new HtmlTag("p");
        HtmlTag _tagEm = new HtmlTag("em");
        HtmlTag _tagStrong = new HtmlTag("strong");

        /// <summary>
        /// HTMLヘッダ部分（固定）
        /// </summary>
        StringBuilder _htmeHeader = new StringBuilder(@"<HTML lang=""ja"">
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

        /// <summary>
        /// HTMLフッタ部分（固定）
        /// </summary>
        StringBuilder _htmlFooter = new StringBuilder(@"</BODY>
</HTML>
");
    }
}
