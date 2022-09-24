using System.Collections.Generic;

namespace ReportExtractor.Domain
{
    public class ContentsEntity
    {
        List<Content> _items;
        //int _strongListCounter;

        public ContentsEntity(string[] array)
        {
            _items = new List<Content>();

            foreach (var item in array)
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
            var level = new LevelCheck();
            if(level.IsHeader1(str))
            {
                return LevelEnum.Header1;
            }
            else if (level.IsHeader2(str))
            {
                return LevelEnum.Header2;
            }
            else if (level.IsHeader3(str))
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

        /// <summary>
        /// IsWrite(記載するかどうか)を設定
        /// </summary>
        public void SetWrite()
        {
            int h1=0, h2=0, h3=0;
            for (int i = 0; i < _items.Count; i++)
            {
                // 下位レベルで記載がある場合は上位レベルも記載とする。
                if (_items[i].Level == LevelEnum.Header1)
                {
                    h1 = i;
                    h2 = i;
                    h3 = i;
                }
                else if (_items[i].Level == LevelEnum.Header2)
                {
                    h2 = i;
                    h3 = i;
                }
                else if (_items[i].Level == LevelEnum.Header3)
                {
                    h3 = i;
                }

                if (_items[i].IsStrong)
                {
                    _items[i].IsWrite = true;
                    _items[h1].IsWrite = true;
                    _items[h2].IsWrite = true;
                    _items[h3].IsWrite = true;
                }

                // スペースやタブだけの行も記載
                var level = new LevelCheck();
                if (level.IsBlankRow(_items[i].Item))
                {
                    _items[i].IsWrite = true;
                }
            }
        }
    }
}
