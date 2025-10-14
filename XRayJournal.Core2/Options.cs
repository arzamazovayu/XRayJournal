using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRayJournal.Core2
{
    public static class Options
    {
        public static string ConnectionString
        {
            get
            {
                //return "Server = localhost; Port = 5432; User Id = postgres; Password = Flvby1; Database = XRayJournal;";
                return Environment.GetEnvironmentVariable("postgres");
            }
        }
    }
}
