using System;
using System.IO;
using NLog.Web;
using System.Collections.Generic;

namespace AssignmentMovieLibraryEhinners
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();
            Console.WriteLine("");

            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            logger.Info("NLOG Loaded");

            string file = "movies.csv";

            List<string> csvs = new List<string>();
            string temp;

            if(File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                logger.Info("Movies Loaded");
                

                while(!sr.EndOfStream)
                {
                    temp = sr.ReadLine();

                    csvs.Add(temp);
                }

                sr.Close();
            }
            else
            {
                logger.Warn("File does not exists. {file}", file);
            }
            
        }
    }
}
