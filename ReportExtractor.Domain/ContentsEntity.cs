using System.Collections.Generic;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ReportExtractorTest")]

namespace ReportExtractor.Domain
{
    internal sealed class ContentsEntity
    {
        List<Content> _items;

        /// <summary>
        /// コンストラクタ
        /// 1行ごとに読み込んだリストからコンテンツリストを作成する
        /// </summary>
        /// <param name="reportList">行に分けられた報告書</param>
        public ContentsEntity(List<string> reportList)
        {
            _items = new List<Content>();

            foreach (var item in reportList)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// アイテム
        /// </summary>
        public List<Content> Items { get { return _items; } }


        /// <summary>
        /// アイテムを格納しレベルも判定
        /// </summary>
        /// <param name="str">判定するテキスト</param>
        private void AddItem(string str)
        {
            var content = new Content(str);
            content.Level = GetLevel(str);
            _items.Add(content);
        }

        /// <summary>
        /// ヘッダなどの階層を設定
        /// </summary>
        /// <param name="str">判定するテキスト</param>
        /// <returns>レベルEnum</returns>
        private LevelEnum GetLevel(string str)
        {
            if(RegexOp.IsHeader1(str))
            {
                return LevelEnum.Header1;
            }
            else if (RegexOp.IsHeader2(str))
            {
                return LevelEnum.Header2;
            }
            else if (RegexOp.IsHeader3(str))
            {
                return LevelEnum.Header3;
            }

            return LevelEnum.None;
        }

        /// <summary>
        /// IsStrong(強調表示)を設定
        /// </summary>
        /// <param name="list">強調行番号リスト</param>
        public void SetStrong(List<int> list)
        {
            int j = 0;
            for (int i = 0; i < _items.Count; i++)
            {
                if (list[j] == i + 1)
                {
                    _items[i].EmphasisLevel = 1;
                    if (j < list.Count - 1)
                    {
                        j++;
                    }
                }
            }
        }
        

        List<int> _PList; //Pタグの行番号を記憶するためのリスト
        bool _isPStart = false;  //Pタグ開始記憶
        bool _isPSectionWrite = false;  //Pタグ開始後にIsWriteがあったかを記憶
        
        int _h1 = 0;  //H1タグの行番号を記憶
        int _h2 = 0;  //H2タグの行番号を記憶
        int _h3 = 0;  //H3タグの行番号を記憶

        /// <summary>
        /// IsWrite(記載するかどうか)を設定。
        /// Pタグ内は1行でもIsWriteなら上位のH1,H2,H3もIsWriteとする。
        /// Pタグ内は1行でもIsWriteならほかの行もIsWriteとする。
        /// </summary>
        public void SetWrite()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                // 空行か判定（スペースのみ、タブのみも含む）
                bool isBlankRow = RegexOp.IsBlankRow(_items[i].Item);

                if (isBlankRow)
                {
                    //Pタグ以外が来たらPタグを終わらせる
                    PEndProcess();

                    // 空行はIsWriteとする。（HTML見た目のため）
                    _items[i].IsWrite = true;
                }
                
                // H1タグの場合
                if (_items[i].Level == LevelEnum.Header1)
                {
                    //Pタグ以外が来たらPタグを終わらせる
                    PEndProcess();
                    //H1タグの行番号を記憶
                    _h1 = i;
                }
                // H2タグの場合
                else if (_items[i].Level == LevelEnum.Header2)
                {
                    //Pタグ以外が来たらPタグを終わらせる
                    PEndProcess();
                    //H2タグの行番号を記憶
                    _h2 = i;
                }
                // H3タグの場合
                else if (_items[i].Level == LevelEnum.Header3)
                {
                    //Pタグ以外が来たらPタグを終わらせる
                    PEndProcess();
                    //H3タグの行番号を記憶
                    _h3 = i;
                }
                // Pタグ（上記以外）の場合
                else
                {
                    //空行以外
                    if (!isBlankRow)
                    {
                        if (!_isPStart)
                        {
                            PStartProcess();
                        }
                        _PList.Add(i);
                    }
                }

                //行がIsStrongの場合
                if (_items[i].EmphasisLevel>0)
                {
                    //Pタグ内のみ
                    if (_isPStart)
                    {
                        _isPSectionWrite = true;
                    }
                }

                
            }
        }

        /// <summary>
        /// SetWrite()でPタグ開始時の処理
        /// </summary>
        private void PStartProcess()
        {
            _PList = new List<int>();
            _isPStart = true;
            _isPSectionWrite = false;

        }

        /// <summary>
        /// SetWrite()でPタグ終了時の処理
        /// </summary>
        private void PEndProcess()
        {
            if (_isPStart)
            {
                if (_isPSectionWrite)
                {
                    _items[_h1].IsWrite = true;
                    _items[_h2].IsWrite = true;
                    _items[_h3].IsWrite = true;

                    foreach (var i in _PList)
                    {
                        _items[i].IsWrite = true;
                    }
                    
                }
                _isPStart = false;
                _isPSectionWrite = false;
            }
        }
    }
}
