using System;
using System.IO;
using NLog.Web;

namespace AssignmentMovieLibraryEhinners
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();


            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            
            Console.WriteLine("NLOG Loaded");
        }
    }
}
