using System;
using System.Collections.Generic;

namespace CSDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<String, String> config = new Dictionary<String, String>()
            {
                { "db_dir", "/csdb" }
            };
            // TODO: read a file that allows config changes

            Shell shell = new Shell(config);
            shell.Start();
        }
    }
}
