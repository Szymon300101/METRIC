﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.IO
{
    public static class FileIO
    {
        public static readonly string appPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string ReadTxt(string filePath)
        {
            string text;
            using (StreamReader sr = new StreamReader(filePath))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
        public static void WriteTxt(string filePath, string text)
        {
            DirectoryInfo outputDir = (new FileInfo(filePath)).Directory;
            if (!outputDir.Exists)
                outputDir.Create();

            File.WriteAllText(filePath, text);
        }
    }
}
