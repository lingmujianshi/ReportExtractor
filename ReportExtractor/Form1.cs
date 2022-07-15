using ReportExtractor.Domain;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace ReportExtractor
{
    public partial class Form1 : Form
    {
        Form1ViewModel vm;

        public Form1()
        {
            InitializeComponent();
            vm = new Form1ViewModel();
        }

        /// <summary>
        /// 文字色変更（カーソルがある行を青色に変更）
        /// </summary>
        private void BlueButton_Click(object sender, EventArgs e)
        {
            SelectionInfo initSelection = GetSelection();
            SelectionInfo line = GetCurrentLine();

            label1.Text = $"Start:{line.Start}, Length:{line.Length}";

            richTextBox1.Select(line.Start, line.Length);
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.Select(initSelection.Start, initSelection.Length);
        }

        /// <summary>
        /// 文字色変更（カーソルがある行を黒色に変更）
        /// </summary>
        private void BlackButton_Click(object sender, EventArgs e)
        {
            SelectionInfo initSelection = GetSelection();
            SelectionInfo line = GetCurrentLine();

            label1.Text = $"Start:{line.Start}, Length:{line.Length}";

            richTextBox1.Select(line.Start, line.Length);
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.Select(initSelection.Start, initSelection.Length);
        }

        /// <summary>
        /// フォント変更（カーソルがある行を太字、18ポイント）
        /// </summary>
        private void Title1Button_Click(object sender, EventArgs e)
        {
            SelectionInfo initSelection = GetSelection();
            SelectionInfo line = GetCurrentLine();

            label1.Text = $"Start:{line.Start}, Length:{line.Length}";

            richTextBox1.Select(line.Start, line.Length);

            Font baseFont = richTextBox1.SelectionFont;
            Font fnt = new Font(baseFont.FontFamily, 18, baseFont.Style | FontStyle.Bold);
            richTextBox1.SelectionFont = fnt;
            baseFont.Dispose();
            fnt.Dispose();
            
            richTextBox1.Select(initSelection.Start, initSelection.Length);

        }

        /// <summary>
        /// フォント変更（カーソルがある行を太字、14ポイント）
        /// </summary>
        private void Title2Button_Click(object sender, EventArgs e)
        {
            SelectionInfo initSelection = GetSelection();
            SelectionInfo line = GetCurrentLine();

            label1.Text = $"Start:{line.Start}, Length:{line.Length}";

            richTextBox1.Select(line.Start, line.Length);

            Font baseFont = richTextBox1.SelectionFont;
            Font fnt = new Font(baseFont.FontFamily, 14, baseFont.Style | FontStyle.Bold);
            richTextBox1.SelectionFont = fnt;
            baseFont.Dispose();
            fnt.Dispose();

            richTextBox1.Select(initSelection.Start, initSelection.Length);
        }

        /// <summary>
        /// カーソルがある行のフォントを初期状態に戻す
        /// </summary>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            SelectionInfo initSelection = GetSelection();
            SelectionInfo line = GetCurrentLine();

            label1.Text = $"Start:{line.Start}, Length:{line.Length}";

            richTextBox1.Select(line.Start, line.Length);
            richTextBox1.SelectionFont = richTextBox1.Font;
            richTextBox1.Select(initSelection.Start, initSelection.Length);
        }

        /// <summary>
        /// 範囲情報（開始位置と文字数）
        /// </summary>
        struct SelectionInfo
        {
            public int Start;
            public int Length;
        }

        /// <summary>
        /// 選択範囲の先頭位置と文字数を取得
        /// </summary>
        /// <returns>SelectionInfo</returns>
        SelectionInfo GetSelection()
        {
            return new SelectionInfo { Start = richTextBox1.SelectionStart, Length = richTextBox1.SelectionLength };
        }

        /// <summary>
        /// カーソルのある行の先頭と文字数を取得
        /// </summary>
        /// <returns>SelectionInfo</returns>
        SelectionInfo GetCurrentLine()
        {
            string str = richTextBox1.Text;
            int start = richTextBox1.SelectionStart;
            int end = start + richTextBox1.SelectionLength;
            int startPos = str.GetTopPos(start);
            int endPos = str.GetEndPos(end);
            int width = endPos - startPos;
            return new SelectionInfo { Start = startPos, Length = width };
        }


        /// <summary>
        /// 選択範囲の先頭行番号を取得
        /// </summary>
        /// <returns>行番号</returns>
        int GetCurrentRow()
        {
            string str = richTextBox1.Text;
            int selectPos = richTextBox1.SelectionStart;
            int row = 1;
            int startPos = 0;
            int endPos = 0;

            while (true)
            {
                endPos = str.IndexOf('\n', startPos);
                if (endPos < selectPos && endPos > -1)
                {
                    row++;
                    startPos = endPos + 1;
                    continue;
                }

                break;
            }

            return row;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetGitDiff diff = new GetGitDiff();
            string inStr = diff.Run();

            System.Text.Encoding enc1 = System.Text.Encoding.GetEncoding("Shift-JIS");
            System.Text.Encoding enc2 = System.Text.Encoding.UTF8;

            byte[] temp = enc1.GetBytes(inStr);
            string str1 = enc2.GetString(temp);

            richTextBox1.Text = str1;
            
            Console.WriteLine("");
        }
    }
}
