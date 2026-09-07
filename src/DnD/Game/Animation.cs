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

        public static void PotionAnimation()
        {
            string[] frames =
                {
                    "  _ _" +
                    "\n /   \\" +
                    "\n|     |" +
                    "\n \\   /" +
                    "\n  | |" +
                    "\n  | |" +
                    "\n /   \\" +
                    "\n/~~~~~\\" +
                    "\n| o     |" +
                    "\n|    o  |" +
                    "\n\\_o___/",

                    "        __" +
                    "\n       /  \\_" +
                    "\n      /    /" +
                    "\n   __/    /" +
                    "\n  /~~~~~~/" +
                    "\n /  o    |" +
                    "\n/     o  |" +
                    "\n\\  o    /" +
                    "\n \\   o /" +
                    "\n  \\___/",

                    "  ___    _" +
                    "\n /~~~\\__/~\\" +
                    "\n| o      o |" +
                    "\n|   o __   |" +
                    "\n \\___/  \\_/",

                    "  ___    _" +
                    "\n /   \\__/ \\" +
                    "\n|~~~~~~~~~~|"  +
                    "\n| o   __ o |"  +
                    "\n \\___/  \\_/",

                    "  ___    _ "+
                    "\n /   \\__/ \\ "+
                    "\n|          | "+
                    "\n|~~~~~__~~~| "+
                    "\n \\_o_/  \\_/"
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
