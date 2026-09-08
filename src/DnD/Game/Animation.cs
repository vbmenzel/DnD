using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static void SpellAnimation()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Console.WriteLine("");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("         ▓▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("▓   ▓   ▓  ▓   ▓   ▓");
            Console.WriteLine("▓   ▓   ▓  ▓   ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("         ▓▓");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("        ▓  ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("▓   ▓   ▓  ▓   ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓   ");
            Console.WriteLine("   ▓   ▓    ▓   ▓   ");
            Console.WriteLine("▓   ▓   ▓  ▓   ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("        ▓  ▓");
            Thread.Sleep(50);
            Console.Clear();

            Console.WriteLine("       ▓    ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("▓   ▓   ▓  ▓   ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓   ");
            Console.WriteLine("  ▓   ▓      ▓   ▓   ");
            Console.WriteLine("  ▓   ▓      ▓   ▓   ");
            Console.WriteLine("   ▓   ▓    ▓   ▓   ▓");
            Console.WriteLine("▓   ▓   ▓  ▓   ▓   ▓");
            Console.WriteLine(" ▓   ▓   ▓▓   ▓   ▓");
            Console.WriteLine("  ▓   ▓      ▓   ▓");
            Console.WriteLine("   ▓   ▓    ▓   ▓");
            Console.WriteLine("    ▓   ▓  ▓   ▓");
            Console.WriteLine("     ▓   ▓▓   ▓");
            Console.WriteLine("      ▓      ▓");
            Console.WriteLine("       ▓    ▓");
            Thread.Sleep(50);
            Console.Clear();
        }

        public static void DoubleAttackAnimation()
        {
            AttackAnimation();
            AttackAnimation();
        }

        public static void SneakAttackAnimation()
        {
            string[] frames =
                {
                   "             \\/",

                   "              | |" +
                   "             \\/",


                   "              | |" +
                   "             | |" +
                   "            \\/",

                   "              | |" +
                   "             | |" +
                   "            | |" +
                   "           \\/",

                   "              | |" +
                   "             | |" +
                   "            | |" +
                   "           \\/",

                   "              | |" +
                   "              | |" +
                   "             | |" +
                   "            | |" +
                   "           \\/",

                   "            o-| |-o" +
                   "              | |" +
                   "              | |" +
                   "             | |" +
                   "            | |" +
                   "           \\/" +
                   "           (0)",

                   "               |" +
                   "            o-| |-o" +
                   "              | |" +
                   "              | |" +
                   "             | |" +
                   "            | |" +
                   "          (\\/)",

                   "               o" +
                   "               |" +
                   "            o-| |-o" +
                   "              | |" +
                   "              | |" +
                   "             | |" +
                   "            (| |)"

                };
            foreach (string frame in frames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                System.Threading.Thread.Sleep(100);
            }
        }

        public static void MissAnimation()
        {
            string[] frames =
                {
                "   " +
                   "\n   M   M   I    SSS    SSS" +
                   "\n   MM MM   I   SS     SS" +
                   "\n   M M M   I     SS     SS" +
                   "\n   M   M   I   SSS    SSS",

                   "   M" +
                   "\n   M   M   I    SSS    SSS" +
                   "\n   MM MM   I   SS     SS" +
                   "\n   M M M   I     SS     SS" +
                   "\n       M   I   SSS    SSS",

                   "   M" +
                   "\n   MM  M   I    SSS    SSS" +
                   "\n   M  MM   I   SS     SS" +
                   "\n   M M M   I     SS     SS" +
                   "\n       M   I   SSS    SSS",

                "   M" +
                   "\n   MM  M   I    SSS    SSS" +
                   "\n   M MMM   I   SS     SS" +
                   "\n   M   M   I     SS     SS" +
                   "\n       M   I   SSS    SSS",

                "   M" +
                   "\n   MM MM   I    SSS    SSS" +
                   "\n   M M M   I   SS     SS" +
                   "\n   M   M   I     SS     SS" +
                   "\n       M   I   SSS    SSS",

                "       M" +
                   "\n   MM MM   I    SSS    SSS" +
                   "\n   M M M   I   SS     SS" +
                   "\n   M   M   I     SS     SS" +
                   "\n   M       I   SSS    SSS",

                "       M   I" +
                   "\n   M  MM   I    SSS    SSS" +
                   "\n   MMM M   I   SS     SS" +
                   "\n   M   M   I     SS     SS" +
                   "\n   M           SSS    SSS",

                "       M   I   " +
                   "\n   M  MM   I   SSSS    SSS" +
                   "\n   MM  M   I    S     SS" +
                   "\n   M M M   I   S SS     SS" +
                   "\n   M            SS    SSS",

                "       M   I    S" +
                   "\n   M   M   I   SSSS    SSS" +
                   "\n   MM MM   I          SS" +
                   "\n   M M M   I   SSSS     SS" +
                   "\n   M             S    SSS",

                "           I    SS" +
                   "\n   M   M   I   SS S    SSS" +
                   "\n   MM MM   I     S    SS" +
                   "\n   M M M   I   SSSS     SS" +
                   "\n   M   M              SSS",

                "                SSS" +
                   "\n   M   M   I   SS      SSS" +
                   "\n   MM MM   I     SS   SS" +
                   "\n   M M M   I   SSS      SS" +
                   "\n   M   M   I          SSS",

                "                SSS   " +
                   "\n   M   M   I    S     SSSS" +
                   "\n   MM MM   I   S SS    S" +
                   "\n   M M M   I    SS    S SS" +
                   "\n   M   M   I   S       SS",

                "                 SS    S" +
                   "\n   M   M   I    S     SSSS" +
                   "\n   MM MM   I   SSSS     " +
                   "\n   M M M   I     S    SSSS" +
                   "\n   M   M   I   SS       S",

                "                  S    SS" +
                   "\n   M   M   I    SS    SS S" +
                   "\n   MM MM   I   SS S     S" +
                   "\n   M M M   I     S    SSSS" +
                   "\n   M   M   I   SSS       ",

                "                       SSS" +
                   "\n   M   M   I    SSS   SS  " +
                   "\n   MM MM   I   SS       SS" +
                   "\n   M M M   I     SS   SSS" +
                   "\n   M   M   I   SSS       "
            };
            foreach (string frame in frames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                System.Threading.Thread.Sleep(50);
            }






        }
    }
}
