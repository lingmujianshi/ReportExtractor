using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExtractorTest
{
    public static class MyFile
    {
        static string _folder = @".\TestData\";

        public static string ReadTextFile(string filename)
        {
            string text = "";
            try
            {
                using (var sr = new StreamReader(_folder + filename, Encoding.GetEncoding("UTF-8")))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return text;
        }

        public static List<string> ReadTextLines(string filename)
        {
            List<string> text=new List<string>();
            try
            {
                using (var sr = new StreamReader(_folder + filename, Encoding.GetEncoding("UTF-8")))
                {
                    while(sr.Peek() >= 0)
                    {
                        text.Add(sr.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return text;
        }

        public static void WriteTexts(string filename,string text)
        {
            try
            {
                File.WriteAllText(Path.Combine(_folder, filename), text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
