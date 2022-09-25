using System;
using System.Diagnostics;
using System.Text;

using System.Runtime.CompilerServices;
using System.IO;

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
        public string ExeCommand(string command, string argument)
        {
            Process process = new Process();

            // コマンド名
            process.StartInfo.FileName = command;
            
            // 引数
            process.StartInfo.Arguments = argument;
            
            // StandardOutputで結果を受け取るために必要な設定
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            
            // StandardOutputのエンコード設定
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            
            // 実行
            process.Start();
            
            //プログラムが実行完了するまで待つ
            process.WaitForExit();
            
            //結果格納
            var str = process.StandardOutput.ReadToEnd();
            
            //プロセス終了
            process.Close();

            return str;
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        /// <param name="command">コマンド名</param>
        /// <param name="argument">引数</param>
        /// <returns></returns>
        public string ExeCommandSync(string command, string argument)
        {
            Process process = new Process();
            _output = new StringBuilder();

            // コマンド名
            process.StartInfo.FileName = command;
            // 引数
            process.StartInfo.Arguments = argument;
            // StandardOutputで結果を受け取るために必要な設定
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            // StandardOutputのエンコード設定
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

            //出力を受取るためのイベント登録
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // 受信したデータを1行ずつ改行して保存
                if (!String.IsNullOrEmpty(e.Data))
                {
                    _output.Append($"{e.Data}\r\n");
                }
            });

            // 実行
            process.Start();

            // 非同期制御
            process.BeginOutputReadLine();

            //プログラムが実行完了するまで待つ
            process.WaitForExit();
            //プロセス終了
            process.Close();

            return _output.ToString();
        }
    }
}
