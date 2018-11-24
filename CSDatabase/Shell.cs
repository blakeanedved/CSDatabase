using System;
namespace CSDatabase
{
    public class Shell
    {
        internal ScreenBuffer ScreenBuffer;
        internal CommandInterpreter CommandInterpreter;

        public Shell()
        {
            ScreenBuffer = new ScreenBuffer(Console.WindowWidth, Console.WindowHeight);
            CommandInterpreter = new CommandInterpreter(ScreenBuffer);
        }

        internal void Start()
        {
            throw new NotImplementedException();
        }
    }
}
