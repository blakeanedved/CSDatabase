using System;
namespace CSDatabase
{
    public class CommandInterpreter
    {
        internal ScreenBuffer ScreenBuffer;

        public CommandInterpreter(ScreenBuffer screenBuffer)
        {
            ScreenBuffer = screenBuffer;
        }
    }
}
