using System.Collections.Generic;

namespace ReportExtractor.Domain
{
    public class ContentsEntity
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
                    _items[i].IsStrong = true;
                    if (j < list.Count - 1)
                    {
                        j++;
                    }
                }
            }
        }
        
        List<int> _PList;
        bool _isP = false;
        bool _isPSectionWrite = false;
        int _h1 = 0, _h2 = 0, _h3 = 0;

        /// <summary>
        /// IsWrite(記載するかどうか)を設定
        /// </summary>
        public void SetWrite()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                bool isBlankRow = RegexOp.IsBlankRow(_items[i].Item);

                // スペースやタブだけの行も記載
                if (isBlankRow)
                {
                    PEndProcess();
                    _items[i].IsWrite = true;
                }
                
                if (_items[i].Level == LevelEnum.Header1)
                {
                    PEndProcess();
                    _h1 = i;
                }
                else if (_items[i].Level == LevelEnum.Header2)
                {
                    PEndProcess();
                    _h2 = i;
                }
                else if (_items[i].Level == LevelEnum.Header3)
                {
                    PEndProcess();
                    _h3 = i;
                }
                else
                {
                    if (!_isP && !isBlankRow)
                    {
                        PStartProcess();
                    }
                    if (!isBlankRow)
                    {
                        _PList.Add(i);
                    }
                }

                if (_items[i].IsStrong)
                {
                    //_items[i].IsWrite = true;
                    if (_isP)
                    {
                        _isPSectionWrite = true;
                    }
                }

                
            }
        }

        void PStartProcess()
        {
            _PList = new List<int>();
            _isP = true;
            _isPSectionWrite = false;

        }

        void PEndProcess()
        {
            if (_isP)
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
                _isP = false;
                _isPSectionWrite = false;
            }
        }
    }
}
