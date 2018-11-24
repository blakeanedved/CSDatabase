using System;
namespace CSDatabase
{
    public class ScreenBuffer
    {
        Int32 Width;
        Int32 Height;
        Char[] buffer;

        public ScreenBuffer(Int32 Width, Int32 Height)
        {
            this.Width = Width;
            this.Height = Height;

            // Initialize the buffer as a rank 1 character array that is the size of the area of the parameters Width and Height
            buffer = new Char[Width * Height];

            // Initialize the buffer with spaces (or null characters)
            for (Int32 index = 0; index < Width * Height; index++)
            {
                buffer[index] = ' ';
            }
        }
    }
}
