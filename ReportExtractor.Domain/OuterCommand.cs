using System;
using System.Diagnostics;
using System.Text;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("UnitTestProject1")]

namespace ReportExtractor.Domain
{
    /// <summary>
    /// 外部プログラムへ指令するためのクラス
    /// </summary>
    public class OuterCommand
    {
        /// <summary>
        /// 受信文字列格納バッファ
        /// </summary>
        private static StringBuilder _output;

        /// <summary>
        /// コマンド実行
        /// </summary>
        /// <param name="command">コマンド名</param>
        /// <param name="argument">引数</param>
        /// <returns></returns>
        internal String ExeCommand(string command, string argument)
        {
            Process process = new Process();
            _output = new StringBuilder();

            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = argument;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            //出力を受取るためのイベント登録
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // 受信したデータを1行ずつ改行して保存
                if (!String.IsNullOrEmpty(e.Data))
                {
                    _output.Append($"{e.Data}\r\n");
                }
            });

            process.Start();

            // 非同期制御
            process.BeginOutputReadLine();

            // 非同期制御
            process.WaitForExit();

            //プログラムが実行完了するまで待つ
            process.WaitForExit();
            process.Close();

            return _output.ToString();
        }
    }
}
