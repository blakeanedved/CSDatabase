using System;

namespace CSDatabase
{
    public class ScreenBuffer
    {
        internal Int32 Width;
        internal Int32 Height;
        Char[] buffer;

        Int32 _CurrentLine;

        public Int32 CurrentLine
        {
            get => _CurrentLine;
            set => _CurrentLine = value > this.Height ? this.Height : value;
        }

        public ScreenBuffer(Int32 Width, Int32 Height)
        {
            this.Width = Width;
            this.Height = Height;

            this.CurrentLine = 0;

            // Initialize the buffer as a rank 1 character array that is the size of the area of the parameters Width and Height
            buffer = new Char[Width * Height];

            // Initialize the buffer with spaces (or null characters)
            for (Int32 index = 0; index < Width * Height; index++)
                buffer[index] = ' ';
        }

        public void Render(Int32 cursorX, Int32 cursorY)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(new String(buffer));
            Console.SetCursorPosition(cursorX, cursorY);
        }

        public void Draw(Char c, Int32 x, Int32 y) => buffer[x + y * Width] = c;

        public void Draw(String s, Int32 x, Int32 y, Boolean clearLine)
        {
            Int32 pos;
            for (var i = 0; i < s.Length; i++)
            {
                pos = x + i + y * Width;
                if (pos > Width * Height - 1)
                {
                    Int32 newLines = (pos - Width * Height) / Width + 1;
                    ShiftBufferUp(newLines);
                    y -= newLines;
                    pos = x + i + y * Width;
                }
                buffer[pos] = s[i];
            }
            if (clearLine)
            {
                Int32 charsToClear = Width - (s.Length % Width);
                for (var i = 0; i < charsToClear; i++)
                    buffer[x + s.Length + i + y * Width] = ' ';
            }
        }

        public void Draw(String s, Int32 x, Int32 y) => Draw(s, x, y, false);

        public void Draw(String s) => Draw(s, 0, CurrentLine, true);

        public void ShiftBufferUp(Int32 lines)
        {
            for (var j = 0; j < Height - lines; j++)
                for (var k = 0; k < Width; k++)
                    buffer[k + j * Width] = buffer[k + (j + lines) * Width];

            for (var j = 0; j < Width; j++)
                buffer[((Height - 1) * Width) + j] = ' ';
        }
    }
}
