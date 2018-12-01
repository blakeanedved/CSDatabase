using System;
using System.Collections.Generic;

namespace CSDatabase
{
    public class Shell
    {
        internal ScreenBuffer ScreenBuffer;
        internal CommandInterpreter CommandInterpreter;
        internal Dictionary<String, String> config;

        internal String prompt;
        internal String database;

        internal Int32 cursorX;
        internal Int32 cursorY;
        internal Int32 relativeCursorX;

        public Shell(Dictionary<String, String> config)
        {
            ScreenBuffer = new ScreenBuffer(Console.WindowWidth, Console.WindowHeight);
            CommandInterpreter = new CommandInterpreter(ScreenBuffer, config);
            this.config = config;

            prompt = "CSDB> ";
            database = "";

            cursorX = prompt.Length;
            cursorY = 0;
            relativeCursorX = 0;
        }

        internal void Start()
        {
            List<String> previousCommands = new List<String>();
            Int32 previousCommandIndex = 0;

            Boolean running = true;

            String currentCommand = "";
            String archivedCommand = "";
            Int32 commandLine = 0;

            while (running)
            {
                ScreenBuffer.Draw(prompt + currentCommand, 0, commandLine, true);
                ScreenBuffer.Render(cursorX, cursorY);

                // Main driver for the program, will brick here until any character is pressed, save shift and fn
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                ConsoleKey key = keyInfo.Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (previousCommandIndex - 1 >= 0)
                        {
                            if (previousCommandIndex == previousCommands.Count)
                                archivedCommand = currentCommand;
                            previousCommandIndex--;
                            currentCommand = previousCommands[previousCommandIndex];
                            cursorX = prompt.Length + currentCommand.Length;
                            relativeCursorX = currentCommand.Length;
                            cursorY += cursorX / ScreenBuffer.Width;
                            cursorX %= ScreenBuffer.Width;
                        }
                        else Console.Beep();
                        break;
                    case ConsoleKey.DownArrow:
                        if (previousCommandIndex < previousCommands.Count - 1)
                        {
                            previousCommandIndex++;
                            currentCommand = previousCommands[previousCommandIndex];
                            cursorX = prompt.Length + currentCommand.Length;
                            relativeCursorX = currentCommand.Length;
                            cursorY += cursorX / ScreenBuffer.Width;
                            cursorX %= ScreenBuffer.Width;
                        }
                        else if (previousCommandIndex == previousCommands.Count - 1)
                        {
                            previousCommandIndex++;
                            currentCommand = archivedCommand;
                            cursorX = prompt.Length + currentCommand.Length;
                            relativeCursorX = currentCommand.Length;
                            cursorY += cursorX / ScreenBuffer.Width;
                            cursorX %= ScreenBuffer.Width;
                            archivedCommand = "";
                        }
                        else Console.Beep();
                        break;
                    case ConsoleKey.RightArrow:
                        if (relativeCursorX < currentCommand.Length)
                        {
                            cursorX++;
                            relativeCursorX++;
                            if (cursorX == ScreenBuffer.Width)
                            {
                                cursorX -= ScreenBuffer.Width;
                                cursorY++;
                            }
                        }
                        else Console.Beep();
                        break;
                    case ConsoleKey.LeftArrow:
                        if (relativeCursorX > 0)
                        {
                            cursorX--;
                            relativeCursorX--;
                            if (cursorX == -1)
                            {
                                cursorX += ScreenBuffer.Width;
                                cursorY--;
                            }
                        }
                        else Console.Beep();
                        break;
                    case ConsoleKey.Tab:
                        // TODO: tab completion
                        break;
                    case ConsoleKey.Enter:
                        CommandInterpreter.Interpret(currentCommand);
                        cursorY = ScreenBuffer.CurrentLine;
                        commandLine = cursorY;
                        previousCommands.Add(currentCommand);
                        previousCommandIndex = previousCommands.Count;
                        archivedCommand = "";
                        currentCommand = "";
                        break;
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Delete:
                        if (relativeCursorX > 0)
                        {
                            currentCommand = new String(
                                currentCommand.Substring(
                                    0,
                                    relativeCursorX - 1
                                ) + currentCommand.Substring(
                                    relativeCursorX,
                                    currentCommand.Length - relativeCursorX
                                )
                            );
                            relativeCursorX--;
                            cursorX--;
                            if (cursorX == -1)
                            {
                                cursorX += ScreenBuffer.Width;
                                cursorY--;
                            }
                        }
                        else Console.Beep();
                        break;
                    default:
                        currentCommand = new String(
                            currentCommand.Substring(
                                0,
                                relativeCursorX
                            ) + keyInfo.KeyChar + currentCommand.Substring(
                                relativeCursorX,
                                currentCommand.Length - relativeCursorX
                            )
                        );
                        relativeCursorX++;
                        cursorX++;
                        if (cursorX == ScreenBuffer.Width)
                        {
                            cursorX -= ScreenBuffer.Width;
                            cursorY++;
                            if (cursorY > ScreenBuffer.Height)
                            {
                                ScreenBuffer.ShiftBufferUp(1);
                                cursorY--;
                                commandLine--;
                            }
                        }
                        break;
                }
            }
        }
    }
}
