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

            //////////////////////////////
            //      NLOG Instantiation  //
            //////////////////////////////

            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            logger.Info("NLOG Loaded");

            //////////////////////////////
            //   Vars For File Loading  //
            //////////////////////////////

            string file = "movies.csv";

            string[] csvsplit;
            List<string> csvs = new List<string>();
            string temp;

            int numMovies = 0; // Movie Counter
            int idMaxLength = 0;
            int titleMaxLength = 0;
            int genresMaxLength = 0;

            //////////////////////////////
            //       Vars For Menu      //
            //////////////////////////////

            bool validInput = false;
            int userChoice = 0;
            string welcome = "Welcome To The Movie Database";
            string almostHRtag = "==================================";
            string optionsPrompt = "Please Enter One Of The Following Options:";
            string optionNotFoundMessage = "Option Not Found!";
            string exitMessage = "Thanks For Visiting, Good-Bye!";
            List<string> options = new List<string>();
            options.Add("View All Movies");
            options.Add("Add A Movie");
            options.Add("Exit"); // Exit Must Always Be Last Option
            int optionsLowerBound = 0;
            int optionsUpperBound = options.Count;
            int i = 0;

            //////////////////////////////
            //  Vars For Viewing Movies //
            //////////////////////////////

            string titleHR = "-";
            int titleHRlength = 0;
            string genreHR = "-";
            string seperatorHR = "/";

            //////////////////////////////
            //    Loading Movie File    //
            //////////////////////////////
            if(File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                logger.Info("Movies Loaded");
                

                while(!sr.EndOfStream)
                {
                    temp = sr.ReadLine();

                    csvs.Add(temp);
                    csvsplit = temp.Split(",");

                    if(numMovies != 0)
                    {
                        try
                        {
                            numMovies = int.Parse(csvsplit[0]);
                        }
                        catch
                        {
                            logger.Error("ID Not Found: {0}",numMovies);
                        }
                    }
                    else
                    {
                        numMovies++;
                    }
                    if(csvsplit[0].Length>idMaxLength)
                    {
                        idMaxLength = csvsplit[0].Length;
                    }
                    if(csvsplit[1].Length>titleMaxLength)
                    {
                        titleMaxLength = csvsplit[1].Length;
                    }
                    if(csvsplit[2].Length>genresMaxLength)
                    {                        
                        genresMaxLength = csvsplit[2].Length;
                    }
                    
                    
                }
                Console.WriteLine("Last Entered Movie ID: {0}",numMovies);
                sr.Close();
            }
            else
            {
                logger.Warn("File does not exists. {file}", file);
            }

            //////////////////////////////
            //         User Menu        //
            //////////////////////////////

            Console.WriteLine(almostHRtag);
            Console.WriteLine(welcome);
            Console.WriteLine(almostHRtag);   

            while(userChoice != optionsUpperBound)       
            {
                while(!validInput)
                {
                    Console.WriteLine(optionsPrompt);
                    i = 1;
                    foreach (string prompt in options)
                    {
                        Console.Write("{0}: ", i);
                        Console.WriteLine(prompt);
                        i++;
                    }
                    try
                    {
                        userChoice = int.Parse(Console.ReadLine());
                        if(userChoice==0)
                        {
                            logger.Error("Please Enter A Value Greater Than 0");
                        }
                        else if(userChoice>optionsUpperBound)
                        {
                            logger.Error(optionNotFoundMessage);
                        }
                        else
                        {
                            validInput = true;
                        }
                        
                    }
                    catch
                    {
                        logger.Error("Not Valid Input");
                    }
                }
                Console.WriteLine("Input Accepted");
                validInput = false; // Must reset flag value for next iteration of loop

                //////////////////////////////
                //       User Options       //
                //////////////////////////////

                if(userChoice==1)
                {
                    // See All Movies
                    logger.Info(options[userChoice-1]);

                    Console.Clear();
                    // New Line
                    Console.WriteLine();

                    // calculate titleHR
                    titleHRlength = idMaxLength + titleMaxLength;
                    for(int j = 0; j<titleHRlength; j++)
                    {
                        titleHR += "-";
                    }

                    // calculate seperatorHR
                    for(int j = 0; j<titleHRlength; j++)
                    {
                        seperatorHR +="/";
                    }

                    // calculate genreHR
                    for(int j = 0; j<genresMaxLength; j++)
                    {
                        genreHR += "-";
                    }

                    // converting max lengths to negative values for left justification
                    idMaxLength = 0-idMaxLength;
                    idMaxLength--;
                    genresMaxLength = 0-genresMaxLength;

                    //Console.WriteLine("Max Length of ID = {0}", idMaxLength);
                    //Console.WriteLine("Max Length of Titles = {0}", titleMaxLength);
                    //Console.WriteLine("Max Length of Genres = {0}", genresMaxLength);

                    /* Max Length of ID = -7
                    Max Length of Titles = -158
                    Max Length of Genres = -96*/

                    /*

                    // Output Horizontal Rule
                    
                    Console.WriteLine(titleHR);

                    /* movieId  title  genres
                    csvsplit = csvs[0].Split(",");
                    // Output Movie ID
                    //csvsplit[0]
                    //Console.Write($"{csvsplit[0],idMaxLength}");
                    Console.Write($"{csvsplit[0].ToUpper(),-8}");
                    
                    

                    
                    // Output Movie Title
                    //csvsplit[1]
                    Console.Write($"{csvsplit[1].ToUpper(),158}");

                    // New Line
                    Console.WriteLine();

                    // Output Horizontal Rule
                    
                    Console.WriteLine(titleHR);


                    // Output Movie Genre(s)
                    //csvsplit[2]
                    
                    Console.Write($"{csvsplit[2].ToUpper(),96}");
                    // New Line
                    Console.WriteLine();
                    // Output Horizontal Rule
                    
                    Console.WriteLine(titleHR); */

                    foreach (string movie in csvs)
                    {
                        csvsplit = movie.Split(",");

                        // Output Horizontal Rule                    
                        Console.WriteLine(titleHR);
                        
                        // Output Movie ID
                        //Console.Write($"{csvsplit[0],idMaxLength}");
                        Console.Write($"{csvsplit[0].ToUpper(),-8}");                      
                        
                        // Output Movie Title
                        Console.Write($"{csvsplit[1].ToUpper(),158}");

                        // New Line
                        Console.WriteLine();
                        // Output Horizontal Rule                        
                        Console.WriteLine(titleHR);

                        // Output Movie Genre(s)                   
                        Console.Write($"{csvsplit[2].ToUpper(),-96}");

                        // New Line
                        Console.WriteLine();
                        // Output Horizontal Rule                        
                        Console.WriteLine(titleHR);
                        // Output Horizontal Rule                        
                        Console.WriteLine(seperatorHR);
                    }
                }
                else if(userChoice==2)
                {
                    // Add Movie
                    logger.Info(options[userChoice-1]);
                }
                else if(userChoice==optionsUpperBound)
                {
                    Console.WriteLine(exitMessage);   
                }
                else
                {
                    logger.Error(optionNotFoundMessage);
                }
                
            }  

            

            
            
        }
    }
}
