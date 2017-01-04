using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace coins_hockey
{
    class Z
    {
        public static cointype[] tcoin = { 
            new cointype(15, 150, 1000000, "1 копейка","1ko","1kr", "r"), 
            new cointype(19, 260, 5, "5 копеек","5ko","5kr", "r"), 
            new cointype(18, 185, 10, "10 копеек","10ko","10kr", "r"),
            new cointype(20, 275, 50, "50 копеек","50ko","50kr", "r"),
            new cointype(21, 300, 100, "1 рубль","1ro","1rr", "r"),
            new cointype(23, 500, 200, "2 рубля","2ro","2rr", "r"),
            new cointype(25, 600, 500, "5 рублей","5ro","5rr", "r"),
            new cointype(22, 563, 1000, "10 рублей","10ro","10rr", "r"),
            new cointype(19, 250, 1, "1 cent","1co","1cr", "s"),
            new cointype(21, 500, 5, "5 cents","5co","5cr", "s"),
            new cointype(18, 226, 10, "10 cents","10co","10cr", "s"),
            new cointype(24, 567, 25, "25 cents","25co","25cr", "s"),
            new cointype(27, 810, 100, "1 dollar","1so","1sr", "s"),
            new cointype(16, 230, 1, "1 euro cent","1eco","1ecr", "e"),
            new cointype(19, 306, 2, "2 euro cents","2eco","2ecr", "e"),
            new cointype(20, 410, 10, "10 euro cents","10eco","10ecr", "e"),
            new cointype(22, 574, 20, "20 euro cents","20eco","20ecr", "e"),
            new cointype(24, 780, 50, "50 euro cents","50eco","50ecr", "e"),
            new cointype(23, 750, 100, "1 euro","1eo","1er", "e"),
            new cointype(26, 850, 200, "2 euros","2eo","2er", "e"),
            new cointype(20, 356, 1, "penny","1po","1pr", "f"),
            new cointype(26, 712, 2, "2 pence","2po","2pr", "f"),
            new cointype(18, 325, 5, "5 pence","5po","5pr", "f"),
            new cointype(25, 650, 10, "10 pence","10po","10pr", "f"),
            new cointype(23, 950, 100, "1 pound","1fo","1fr", "f"),
            new cointype(28, 1200, 200, "2 pounds","2fo","2fr", "f")
        };
        public const int mspd = 400;
        public const int vismspd = 50;
        public static Form1 MainForm;
        public static int sit = 0;
        public static int clwidth, clheight;
        public static double usd, eur, gbp;
        public static double koofa, koofb;
        public static int movewindowx, movewindowy;
        public static bool movewindow = false;
        public static bool closeon = false, mininmon = false;
        public static int radangl = 100;
        public static bool use_internet;
    }

    class rec
    {
        public static long timerec = 0;
        public static System.IO.StreamWriter oup;
        public static bool record = false;
        public static System.Diagnostics.Stopwatch rectim = new System.Diagnostics.Stopwatch();
    }

    class valut//class for JSON desurs
    {
        public string Rate;
    }
    
}
