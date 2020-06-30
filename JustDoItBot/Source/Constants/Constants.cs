using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BotControllerProj.Source.Constants
{
    public class Constants
    {
        public static string DIRECTORY
        {
            get { return Directory.GetCurrentDirectory(); }
        }

        public static string CONNECTION_STRING_FILEPATH
        {
            get { return DIRECTORY + "\\" + "connection_database.txt"; }
        }




    }
}
