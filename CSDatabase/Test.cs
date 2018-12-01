using System;
namespace CSDatabase
{
    class Test
    {
        Int32 _Thing;

        public Int32 Thing
        {
            get
            { return _Thing; }
            set
            { _Thing = value > 10 ? 10 : value; }
        }

        public Test()
        {
            Thing = 0;
            for (Int32 i = 0; i < 20; i++)
            {
                Thing++;
                Console.WriteLine(Thing);
            }
        }
    }
}