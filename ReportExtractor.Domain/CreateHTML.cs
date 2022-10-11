﻿using System.Collections.Generic;
using System.Text;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ReportExtractorTest")]

namespace ReportExtractor.Domain
{
    /// <summary>
    /// コンテンツリストからHTMLを作成するクラス
    /// </summary>
    internal sealed class CreateHTML
    {
        // Sectionエリアの文字を一時格納
        string _section;

        // Pエリアの文字を一時格納
        string _paragraph;

        // セクション内であるかどうか
        bool _isSectionStart;

        // Pタグプロセス中か
        bool _isPStart;

        // 連続空白行カウンタ
        int _blankLineCount;

        /// <summary>
        /// コンテンツリスト
        /// </summary>
        List<Content> _items;

        /// <summary>
        /// タイトル（itemsの1行目をタイトルとする）
        /// </summary>
        public string Title { get; private set; } = "HTMLタイトル";

        /// <summary>
        /// コントラクタ
        /// </summary>
        /// <param name="items">コンテンツリスト</param>
        public CreateHTML(List<Content> items)
        {
            _items = items;
        }

        /// <summary>
        /// 変更箇所だけのHTMLを取得
        /// </summary>
        /// <returns>変更箇所だけのHTML</returns>
        public string GetHtmlShort()
        {
            TrimTopSignes(ref _items);


            //更新情報のみのHTMLを作成して格納
            StringBuilder shortBody = MakeBody(_items, true);
            string html = CombineHtml(shortBody);
            //html=RegexOp.TrimAll(_htmlShort);

            return html;
        }

        /// <summary>
        /// 報告書全体のHTMLを取得
        /// </summary>
        /// <returns>報告書全体のHTML</returns>
        public string GetHtml()
        {
            TrimTopSignes(ref _items);
            StringBuilder fullBody = MakeBody(_items, false);
            string html = CombineHtml(fullBody);
            return html;
        }

        /// <summary>
        /// メール用に編集されたHTMLを取得
        /// （変更箇所＋全体）
        /// </summary>
        /// <returns>メール用に編集されたHTML</returns>
        public string GetHtmlForMail()
        {
            TrimTopSignes(ref _items);
            //更新情報のみのHTMLを作成して格納
            StringBuilder shortBody = MakeBody(_items, true);
            
            //全体のHTMLを作成して格納
            StringBuilder fullBody = MakeBody(_items, false);
            
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append(shortBody).Append(_htmlMiddle).Append(fullBody);
            string htmlForMail = CombineHtml(mailBody);
            
            return htmlForMail;
        }

        /// <summary>
        /// 先頭の`#,!,Space,Tabなど余計な文字を削除
        /// </summary>
        private void TrimTopSignes(ref List<Content> items)
        {
            foreach (Content i in items)
            {
                i.Item = RegexOp.TrimTops(i.Item);
            }
        }

        string GetHtmlTitle()
        {
            return "<title>" + Title + "</title>";
        }

        /// <summary>
        /// 作成したHTLMとヘッダ・フッタを結合
        /// </summary>
        /// <returns>HTMLテキスト</returns>
        private string CombineHtml(StringBuilder _body)
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append(_htmeHeaderStart).Append(GetHtmlTitle()).Append(_htmeCSS).Append(_htmeHeaderEnd).Append(_body).Append(_htmlFooter).ToString();
        }

        /// <summary>
        /// HTML Body部分を作成
        /// </summary>
        /// <param name="updatedOnly">更新分のみ表示</param>
        private StringBuilder MakeBody(List<Content> items,bool updatedOnly)
        {
            StringBuilder body = new StringBuilder();
            StartSectionProcess();

            foreach (Content item in items)
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
                            EndSectionProcess(ref body);
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
            EndSectionProcess(ref body);

            return body;
        }

        /// <summary>
        /// 判断し最後にBrタグを付加
        /// </summary>
        /// <param name="line">入力文字列</param>
        /// <returns>処理後の文字列</returns>
        private string AddBr(string line, bool isBlank)
        {
            string res;
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
            string line;
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
        private void EndSectionProcess(ref StringBuilder body)
        {
            if (_isSectionStart)
            {
                _section = _tagSection.AddTagsNewLine(_section);
                _isSectionStart = false;
                body.Append(_section);
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

        readonly HtmlTag _tagH1 = new HtmlTag("h1");
        readonly HtmlTag _tagH2 = new HtmlTag("h2");
        readonly HtmlTag _tagH3 = new HtmlTag("h3");
        readonly HtmlTag _tagSection = new HtmlTag("section");
        readonly HtmlTag _tagP = new HtmlTag("p");
        readonly HtmlTag _tagEm = new HtmlTag("em");
        readonly HtmlTag _tagStrong = new HtmlTag("strong");

        /// <summary>
        /// HTMLヘッダ部分（固定）
        /// </summary>
        readonly string _htmeHeaderStart = @"<HTML lang=""ja"">
<meta charset=""UTF-8"">";

readonly string _htmeHeaderEnd = @"<BODY>
";

        readonly string _htmeCSS = @"<style>
    body{
        width: 40em;
        margin-left: 5px;
    }
    h1 {
        font-size: 20px;
        font-weight: bold;
        padding: 10px 20px;
 
    }
    h2 {
        font-size: 14px;
        padding-left: 5px;
        
    }

    h3 {
        font-size: 12px;
    }

    p{
        font-size: 10.5px;
        margin-left: 1em;
    }

    em {
        color: blue;
    }

    strong {
        color: red;
    }
</style>
";

        /// <summary>
        /// HTMLフッタ部分（固定）
        /// </summary>
        readonly string _htmlFooter = @"</BODY>
</HTML>
";

        /// <summary>
        /// HTMLつなぎの部分
        /// </summary>
        readonly string _htmlMiddle = @"<section>
<p>
<hr>
機種ごとの詳細については以下の通りです。
</p>
</section>
";

    }


}
