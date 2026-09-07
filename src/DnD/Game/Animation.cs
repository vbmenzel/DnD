using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Game
{
    internal class Animation
    {

        public static void AttackAnimation() 
        {
            string[] frames =
                {
                    "    /\\" +
                    "\n   / /" +
                    "\n  / /" +
                    "\n / /" +
                    "\n/ /" +
                    "\n /" +
                    "\n/" +
                    "\n" +
                    "\n" +
                    "\n" +
                    "\n",

                    "    -/\\" +
                    "\n   -/ /" +
                    "\n  -/ /" +
                    "\n -/ /" +
                    "\n-/ /" +
                    "\n/ /" +
                    "\n /" +
                    "\n/" +
                    "\n" +
                    "\n" +
                    "",

                    "    =-/\\" +
                    "\n   =-/ /" +
                    "\n  =-/ /" +
                    "\n =-/ /" +
                    "\n=-/ /" +
                    "\n-/ /" +
                    "\n/ /" +
                    "\n /" +
                    "\n/" +
                    "" +
                    "",

                    "    ~=-/\\" +
                    "\n   ~=-/ /" +
                    "\n  ~=-/ /" +
                    "\n ~=-/ /" +
                    "\n~=-/ /" +
                    "\n=-/ /" +
                    "\n-/ /" +
                    "\n/ /" +
                    "\n /" +
                    "\n/" +
                    "" +
                    "",


                    "" +
                    "\n" +
                    "\n" +
                    "\n" +
                    "\n" +
                    "\n       \\ | /" +
                    "\n     --- * ---" +
                    "\n       / | \\  ",

                    "" +
                    "\n" +
                    "\n" +
                    "\n     \\   |   /" +
                    "\n      \\  |  /" +
                    "\n       \\ | /" +
                    "\n  ------ * ------" +
                    "\n       / | \\  " +
                    "\n      /  |  \\" +
                    "\n     /   |   \\",

                    ""

                    

                };
            foreach (string frame in frames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
