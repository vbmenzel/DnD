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

        public static void HeavyAttackAnimation()
        {
            string[] frames =
                {
                   "   \\__/" +
                   "" +
                   "" +
                   "",

                   
                   "   |   |" +
                   "\n   \\__/" +
                   "" +
                   "",

                   "   |   |" +
                   "   |   |" +
                   "   \\__/" +
                   "",

                   "===    |" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n   \\__/" +
                   "",

                   "   |   |" +
                   "\n===    |" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n   \\__/" +
                   "",

                   "   |   |" +
                   "\n   |   |" +
                   "\n===    |" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n   \\__/" +
                   "",

                   "   /   \\" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n===    |" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n   \\__/" +
                   "",

                   "    ___" +
                   "\n   /   \\" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n===    |" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n   \\__/" +
                   "",

                   "    ___" +
                   "\n   /   \\" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n===    |" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n  -\\__/" +
                   "\n /_______\\",

                   "    ___" +
                   "\n   /   \\" +
                   "\n   |   |" +
                   "\n   |   |" +
                   "\n===    |" +
                   "\n   |   |" +
                   "\n   |x*x|" +
                   "\n  _______" +
                   "\n /_______\\",

                   "" +
                   "\n    _" +
                   "\n   / \\ " +
                   "\n  | * |" +
                   "\n   \\_/" +
                   "" +
                   "",

                   "   ___" +
                   "\n  / _ \\" +
                   "\n / / \\ \\ " +
                   "\n| | * | |" +
                   "\n \\ \\_/ /" +
                   "\n  \\___/" +
                   ""


                };
            foreach (string frame in frames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                System.Threading.Thread.Sleep(100);
            }
            Console.Clear();
        }
    }
}
