using System;
using System.Collections.Generic;

namespace CSDatabase
{
    public class CommandInterpreter
    {
        internal ScreenBuffer ScreenBuffer;
        internal Dictionary<String, String> config;

        public CommandInterpreter(ScreenBuffer ScreenBuffer, Dictionary<String, String> config)
        {
            this.ScreenBuffer = ScreenBuffer;
            this.config = config;
        }

        public void Interpret(String command)
        {
            throw new NotImplementedException();
        }
    }
}
