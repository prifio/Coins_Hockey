using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace coins_hockey
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //[STAThread]
        public static game MainGame = new game();
        public const int countcoin = 26;

        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Z.MainForm = new Form1();
            Z.sit = -1;
            Z.MainForm.Show();
            Z.MainForm.draw_all();
            set_rate();
            Z.MainForm.draw_all();
            if (Z.sit == -2)
            {
                Z.use_internet = false;
                Z.eur = 85;
                Z.gbp = 100;
                Z.usd = 70;
                apply_rate();
            }
            else
                Z.use_internet = true;
            Z.sit = 1;
            Z.clwidth = Z.MainForm.ClientSize.Width;
            Z.clheight = Z.MainForm.ClientSize.Height;
            var ticktim = new System.Diagnostics.Stopwatch();
            ticktim.Start();
            while (Z.MainForm.Created)
            {
                Z.MainForm.draw_all();
                Application.DoEvents();
                if (MainGame.sit == 0)
                    MainGame.update(ticktim.ElapsedMilliseconds);
                if (Z.sit == 1 || Z.sit == 2)
                    MainGame.menu_update();
                ticktim.Restart();
            }
        }
        public static void set_rate()
        {
			try
			{
				WebRequest wrGETURL;
				wrGETURL = WebRequest.Create("https://query1.finance.yahoo.com/v7/finance/quote?&symbols=USDRUB%3DX,EURRUB%3DX,GBPRUB%3DX&fields=regularMarketPrice");
				var objStream = wrGETURL.GetResponse().GetResponseStream();
				var objReader = new StreamReader(objStream);
				string json = objReader.ReadLine();
				var prprp = new { quoteResponse = new { result = new valut[3] } };
				prprp = JsonConvert.DeserializeAnonymousType(json, prprp);
				Z.usd = prprp.quoteResponse.result[0].regularMarketPrice;
				Z.eur = prprp.quoteResponse.result[1].regularMarketPrice;
				Z.gbp = prprp.quoteResponse.result[2].regularMarketPrice;
				apply_rate();
				Z.sit = -1;
			}
			catch
			{
				Z.sit = -2;
			}
        }
        public static void apply_rate()
        {
            for (int i = 1; i < countcoin; i++)
            {
                if (Z.tcoin[i].valut == 2)
                    Z.tcoin[i].cost = (int)(Z.tcoin[i].cost * Z.usd);
                if (Z.tcoin[i].valut == 3)
                    Z.tcoin[i].cost = (int)(Z.tcoin[i].cost * Z.eur);
                if (Z.tcoin[i].valut == 4)
                    Z.tcoin[i].cost = (int)(Z.tcoin[i].cost * Z.gbp);
            }
            double k1 = 20;
            double k2 = (Z.usd * 100 + Z.eur * 150);
            double k3 = (Z.eur * 500);
            double k4 = (Z.eur * 200 + Z.gbp * 300);
            double k5 = (Z.gbp * 200 + Z.eur * 600);
            double got = 100000000;
            double na, nb, ga = 0, gb = 0;
            double ot = 100000000;
            na = 0;
            nb = 0;
            while (na <= 30)
            {
                nb = 0;
                while (nb < 50)
                {
                    ot = 0;
                    ot += Math.Pow(f(na, nb, k1) - 20, 2);
                    ot += Math.Pow(f(na, nb, k2) - k2 / 4 * 6 / 10, 2);
                    ot += Math.Pow(f(na, nb, k3) - k3 / 4 * 6 / 15, 2);
                    ot += Math.Pow(f(na, nb, k4) - k4 / 4 * 6 / 18, 2);
                    ot += Math.Pow(f(na, nb, k5) - k5 / 4 * 6 / 25, 2);
                    if (ot < got)
                    {
                        ga = na;
                        gb = nb;
                        got = ot;
                    }
                    nb += 0.06;
                }
                na += 0.05;
            }
            Z.koofa = ga;
            Z.koofb = gb;
        }
        private static double f(double a, double b, double x)
        {
            return a + Math.Sqrt(x) * b;
        }
        public static void graphic_wait(System.Drawing.Graphics g)
        {
            g.Clear(System.Drawing.Color.Maroon);
            var f = new System.Drawing.Font("Arial", 20);
            g.DrawString("Загрузка и анализ курсов валют...", f, System.Drawing.Brushes.White, 190, 250);
            border(g);
        }
        public static void graphic_nointernet(System.Drawing.Graphics g)
        {
            g.Clear(System.Drawing.Color.Maroon);
            var f = new System.Drawing.Font("Arial", 150);
            g.DrawString(":(", f, System.Drawing.Brushes.White, 20, 30);
            f = new System.Drawing.Font("Courier", 25);
            g.DrawString("Your PC hasn't internet connection", f, System.Drawing.Brushes.White, 20, 360);
            g.DrawString("Wait, while rates are applying defout", f, System.Drawing.Brushes.White, 20, 400);
            border(g);
        }
        public static void border(System.Drawing.Graphics g)
        {
            if (Z.mininmon && Z.sit <= -1)
                g.DrawString("-", new Font("Arial", 15), Brushes.Bisque, Z.clwidth - 35, -3);
            else
                g.DrawString("-", new Font("Arial", 15), Brushes.CornflowerBlue, Z.clwidth - 35, -3);

            if (Z.closeon && Z.sit <= -1)
                g.DrawString("X", new Font("Arial", 15), Brushes.Bisque, Z.clwidth - 18, -3);
            else
                g.DrawString("X", new Font("Arial", 15), Brushes.CornflowerBlue, Z.clwidth - 18, -3);

        }
        public static void klik(object sender, KeyEventArgs e)
        {
            MainGame.klik(sender, e);
        }
        public static void mdklik(object sender, MouseEventArgs e)
        {
            MainGame.mdklik(sender, e);
        }
        public static void mmklik(object sender, MouseEventArgs e)
        {
            MainGame.mmklik(sender, e);
        }
        public static void muklik(object sender, MouseEventArgs e)
        {
            MainGame.muklik(sender, e);
        }
        public static long replay_add(long time, user[] us, coins[] coins, string filename = "replay.chrpl")
        {
            if (time < 21)
                return time;
            rec.oup.WriteLine(us[0].defince + " " + ((int)coins[0].x) + " " + ((int)coins[0].y));
            rec.oup.WriteLine(us[0].atac + " " + ((int)coins[1].x) + " " + ((int)coins[1].y));
            rec.oup.WriteLine(us[0].atac + " " + ((int)coins[2].x) + " " + ((int)coins[2].y));
            rec.oup.WriteLine(us[0].atac + " " + ((int)coins[3].x) + " " + ((int)coins[3].y));
            rec.oup.WriteLine(us[1].defince + " " + ((int)coins[4].x) + " " + ((int)coins[4].y));
            rec.oup.WriteLine(us[1].atac + " " + ((int)coins[5].x) + " " + ((int)coins[5].y));
            rec.oup.WriteLine(us[1].atac + " " + ((int)coins[6].x) + " " + ((int)coins[6].y));
            rec.oup.WriteLine(us[1].atac + " " + ((int)coins[7].x) + " " + ((int)coins[7].y));
            rec.oup.WriteLine("0 " + ((int)coins[8].x) + " " + ((int)coins[8].y));
            rec.oup.WriteLine("0 " + ((int)coins[9].x) + " " + ((int)coins[9].y));
            rec.oup.WriteLine("0 " + ((int)coins[10].x) + " " + ((int)coins[10].y));
            return time - 21;
        }
        public static void Close(object sender, System.EventArgs e)
        {
            MainGame.close();
        }
    }

    class coins
    {
        public int r { get; set; }
        public int m { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public int nu { get; set; }
        public double speed_slowdone { get; set; }
        public Point vec { get; set; }
        Point prov { get; set; }
        bool or = false;
        public int otb { get; set; }
        int typecoin = 0;
        public int is_gool { get; set; }
        public double accuracy { get; private set; }

        public bool stop()
        {
            if (vec.x == 0 && vec.y == 0)
                return true;
            return false;
        }

        public void Draw(Graphics g)
        {
            if (or)
                g.DrawImage(Z.tcoin[typecoin].picob, (int)x - r, (int)y - r, r * 2, r * 2);
            else
                g.DrawImage(Z.tcoin[typecoin].picre, (int)x - r, (int)y - r, r * 2, r * 2);
        }

        public static coins get_number_coin(int nus, int sx, int sy, bool orl, int num, double slowdone = 100.0, double spx = 0, double spy = 0)
        {
            var an = new coins();
            an.nu = nus;
            an.r = Z.tcoin[num].r;
            an.m = Z.tcoin[num].m;
            an.x = sx;
            an.y = sy;
            an.speed_slowdone = slowdone;
            an.or = orl;
            an.typecoin = num;
            an.otb = -1;
            an.vec = new Point();
            an.prov = new Point();
            an.accuracy = Z.tcoin[num].accuracy;
            return an;
        }

        public static coins get_coin(int nus, int rad, int mass, int sx, int sy, double zams, double spx, double spy, bool orl, int tp)
        {
            var an = new coins();
            an.nu = nus;
            an.r = rad;
            an.m = mass;
            an.x = sx;
            an.y = sy;
            an.speed_slowdone = zams;
            an.or = orl;
            an.typecoin = tp;
            an.otb = -1;
            an.vec = new Point();
            an.prov = new Point();
            return an;
        }

        public void move(double time, coins[] coin)
        {
            double spd = Math.Sqrt(vec.x * vec.x + vec.y * vec.y);

            double spdm = spd - time * speed_slowdone / 1000;
            if (spd <= time * speed_slowdone / 1000)
            {
                vec.x = 0;
                vec.y = 0;
                if (otb != -1)
                {
                    coin[otb].otb = -1;
                    otb = -1;
                }
            }
            else
            {
                vec.x = vec.x * spdm / spd;
                vec.y = vec.y * spdm / spd;
                x += time * vec.x / 1000;
                y += time * vec.y / 1000;
            }


            if (x < r || x > Z.clwidth - r)
            {
                vec.x = -vec.x;
                if (x < r)
                    x = r;
                else
                    x = Z.clwidth - r;
                if (otb != -1)
                {
                    coin[otb].otb = -1;
                    otb = -1;
                }

                if (x <= r && y >= Z.clheight / 2 - 50 && y <= Z.clheight / 2 + 50)
                    is_gool = 1;
                if (x >= Z.clwidth - r && y >= Z.clheight / 2 - 50 && y <= Z.clheight / 2 + 50)
                    is_gool = 2;
            }
            if (y < r || y > Z.clheight - r)
            {
                vec.y = -vec.y;
                if (y < r) y = r; else y = Z.clheight - r;
                if (otb != -1)
                {
                    coin[otb].otb = -1;
                    otb = -1;
                }
            }
            on_board();
        }

        public void global_move(double time, coins[] coins)
        {
            move(time, coins);
            for (int i = nu + 1; i < coins.Length; i++)
            {
                if (otb == i)
                {
                    if (Math.Abs(vec.y * prov.y - vec.x * prov.x) >= 1e-5 || Math.Abs(coins[i].vec.y * coins[i].prov.y - coins[i].vec.x * coins[i].prov.x) >= 1e-5)
                    {
                        otb = -1;
                        coins[i].otb = -1;
                    }
                }
                if (otb != i && Math.Pow(coins[i].x - x, 2) + Math.Pow(coins[i].y - y, 2) < Math.Pow(coins[i].r + r, 2))
                {
                    double skpr = (vec.x - coins[i].vec.x) * (x - coins[i].x) + (vec.y - coins[i].vec.y) * (y - coins[i].y);
                    double dist = (x - coins[i].x) * (x - coins[i].x) + (y - coins[i].y) * (y - coins[i].y);
                    Point hvec1 = new Point();
                    Point hvec2 = new Point();
                    hvec1.x = vec.x - (2 * coins[i].m * skpr) / (m + coins[i].m) / dist * (x - coins[i].x);
                    hvec1.y = vec.y - (2 * coins[i].m * skpr) / (m + coins[i].m) / dist * (y - coins[i].y);
                    hvec2.x = coins[i].vec.x - (2 * m * skpr) / (m + coins[i].m) / dist * (-x + coins[i].x);
                    hvec2.y = coins[i].vec.y - (2 * m * skpr) / (m + coins[i].m) / dist * (-y + coins[i].y);
                    vec.x = hvec1.x;
                    vec.y = hvec1.y;
                    coins[i].vec.x = hvec2.x;
                    coins[i].vec.y = hvec2.y;
                    otb = i;
                    coins[i].otb = nu;
                    coins[i].prov.x = coins[i].vec.x;
                    coins[i].prov.y = coins[i].vec.y;
                    prov.x = vec.x;
                    prov.y = vec.y;
                }
            }
        }

        public void change_for_board(int helpsit)
        {
            if (helpsit == 1 || helpsit == 3)
            {
                x = Z.clwidth - x;
                vec.x = -vec.x;
            }
            if (helpsit == 2 || helpsit == 3)
            {
                y = Z.clheight - y;
                vec.y = -vec.y;
            }
        }

        public void on_board()
        {
            int helpsit = -1;
            if ((Z.radangl - x) * (Z.radangl - x) + (Z.radangl - y) * (Z.radangl - y) > (Z.radangl - r) * (Z.radangl - r) && x < Z.radangl && y < Z.radangl)
                helpsit = 0;
            if ((Z.clwidth - Z.radangl - x) * (Z.clwidth - Z.radangl - x) + (Z.radangl - y) * (Z.radangl - y) > (Z.radangl - r) * (Z.radangl - r) && x > Z.clwidth - Z.radangl && y < Z.radangl)
                helpsit = 1;
            if ((Z.clheight - Z.radangl - y) * (Z.clheight - Z.radangl - y) + (Z.radangl - x) * (Z.radangl - x) > (Z.radangl - r) * (Z.radangl - r) && x < Z.radangl && y > Z.clheight - Z.radangl)
                helpsit = 2;
            if ((Z.clwidth - Z.radangl - x) * (Z.clwidth - Z.radangl - x) + (Z.clheight - Z.radangl - y) * (Z.clheight - Z.radangl - y) > (Z.radangl - r) * (Z.radangl - r) && x > Z.clwidth - Z.radangl && y > Z.clheight - Z.radangl)
                helpsit = 3;
            if (helpsit == -1)
                return;
            change_for_board(helpsit);
            double hx = Z.radangl - x, hy = Z.radangl - y;
            double crpr = vec.x * hx + vec.y * hy, dtpr = vec.x * hy - vec.y * hx;
            double oldv = vec.x * vec.x + vec.y + vec.y;
            vec.x = (-crpr * hx + dtpr * hy) / (hx * hx + hy * hy);
            vec.y = (-dtpr * hx - hy * crpr) / (hx * hx + hy * hy);
            otb = -1;
            double dist = Math.Sqrt((Z.radangl - x) * (Z.radangl - x) + (Z.radangl - y) * (Z.radangl - y));
            x = (x - Z.radangl) * (Z.radangl - r) / dist + Z.radangl;
            y = (y - Z.radangl) * (Z.radangl - r) / dist + Z.radangl;
            change_for_board(helpsit);
        }
    }

    class game
    {
        private coins[] coinarr = new coins[11];
        private bool gool1, gool2;
        private int ch1, ch2, money_pick, now_palyer, immunlast;
        private Point power_kick;
        public int sit { get; private set; }
        private user[] us;
        private const int shopfull = 1000;
        private int shop_pick, shop_palyer, shop_cursor;
        private int pause_cursor;

        private long shopwidthanim = shopfull;
        private bool shopold = false;
        private int shopoldcoin = 1;
        private Stopwatch shoptim = new Stopwatch();
        private int shop_speed = 2;
        private bool shop_animon = false;
        private int mem_sit;
        private string write_replay = "replay";
        private string real_write_repay;

        public game()
        {
            shop_palyer = shop_pick = shop_cursor = 1;
            us = new user[2];
            us[0] = new user();
            us[1] = new user();
            now_palyer = ch1 = ch2 = 0;
            money_pick = -1;
            gool1 = gool2 = false;
            new_game();
            power_kick = new Point();
            sit = 1;
            try
            {
                var file = File.OpenText("./data.txt");
                write_replay = file.ReadLine();
                file.Close();
            }
            catch
            {
                write_replay = "replay";
                try
                {
                    var fl = File.CreateText("./data.txt");
                    fl.WriteLine("replay");
                }
                catch
                { }
            }
        }
        public void new_game(bool f = true)
        {
            coinarr[0] = coins.get_number_coin(0, Z.tcoin[us[0].defince].r + 5, 532 / 2, false, us[0].defince);
            coinarr[1] = coins.get_number_coin(1, 300, 532 / 2, false, us[0].atac);
            coinarr[2] = coins.get_number_coin(2, 200, 532 / 2 - 100, false, us[0].atac);
            coinarr[3] = coins.get_number_coin(3, 200, 532 / 2 + 100, false, us[0].atac);
            coinarr[4] = coins.get_number_coin(4, 788 - Z.tcoin[us[1].defince].r, 532 / 2, true, us[1].defince);
            coinarr[5] = coins.get_number_coin(5, 500, 532 / 2, true, us[1].atac);
            coinarr[6] = coins.get_number_coin(6, 600, 532 / 2 - 100, true, us[1].atac);
            coinarr[7] = coins.get_number_coin(7, 600, 532 / 2 + 100, true, us[1].atac);
            coinarr[8] = coins.get_number_coin(8, 400, 532 / 2 - 50, true, 0);
            coinarr[9] = coins.get_number_coin(9, 400, 532 / 2, true, 0);
            coinarr[10] = coins.get_number_coin(10, 400, 532 / 2 + 50, true, 0);
            if (f) ch1 = 0;
            if (f) ch2 = 0;
            immunlast = 5;
        }
        public void update(long tick)
        {
            if (sit == 3)
                return;
            double hl = tick;
            while (hl > 0)
            {
                for (int i = 0; i < coinarr.Length; i++)
                {
                    coinarr[i].global_move(Math.Min(0.1, hl), coinarr);
                }
                hl -= 0.1;
            }

            if ((!rec.record && rec.rectim.IsRunning) || coin_stop())
            {
                rec.rectim.Reset();
                rec.rectim.Stop();
                rec.timerec = 0;
            }
            else
            {
                rec.timerec += rec.rectim.ElapsedMilliseconds;
                rec.rectim.Restart();
                rec.timerec = Program.replay_add(rec.timerec, us, coinarr, real_write_repay);
            }

            for (int i = coinarr.Length - 3; i < coinarr.Length; i++)
            {
                if (coinarr[i].x < coinarr[i].r && coinarr[i].y >= Z.clheight / 2 - 50 && coinarr[i].y <= Z.clheight / 2 + 50 && immunlast == 0)
                    gool1 = true;
                if (coinarr[i].x > Z.clwidth - coinarr[i].r && coinarr[i].y >= Z.clheight / 2 - 50 && coinarr[i].y <= Z.clheight / 2 + 50 && immunlast == 0)
                    gool2 = true;
            }
            for (int i = coinarr.Length - 3; i < coinarr.Length; i++)
            {
                if (coinarr[i].is_gool == 1)
                {
                    if (immunlast == 0)
                        gool1 = true;
                    coinarr[i].is_gool = 0;
                }
                if (coinarr[i].is_gool == 2)
                {
                    if (immunlast == 0)
                        gool2 = true;
                    coinarr[i].is_gool = 0;
                }
            }

            if (gool1 || gool2)
            {
                new_game(false);
            }
            if (gool1)
            {
                gool1 = false;
                ch2++;
                now_palyer = 0;
            }
            if (gool2)
            {
                gool2 = false;
                ch1++;
                now_palyer = 1;
            }
            if (ch1 >= 2)
            {
                var k1 = (2 * us[0].sen_arm() + us[1].sen_arm()) / 3;
                var k2 = (2 * us[1].sen_arm() + us[0].sen_arm()) / 3;
                us[0].mon += (int)(Z.koofa + Z.koofb * Math.Sqrt(k1));
                if (ch2 > 0)
                    us[1].mon += (int)((Z.koofa + Z.koofb * Math.Sqrt(k2)) * 2 / 3);
                sit = 1;
                shop_palyer = 1;
            }
            else
                if (ch2 >= 2)
            {
                var k1 = (2 * us[0].sen_arm() + us[1].sen_arm()) / 3;
                var k2 = (2 * us[1].sen_arm() + us[0].sen_arm()) / 3;
                us[1].mon += (int)(Z.koofa + Z.koofb * Math.Sqrt(k2));
                if (ch1 > 0)
                    us[0].mon += (int)((Z.koofa + Z.koofb * Math.Sqrt(k1)) * 2 / 3);
                sit = 1;
                shop_palyer = 1;
            }
        }
        public void menu_update()
        {
            if (shop_animon)
            {
                if (shopold)
                {
                    shopwidthanim -= shop_speed * shoptim.ElapsedMilliseconds;
                    shoptim.Restart();
                    if (shopwidthanim <= 0)
                    {
                        shopold = false;
                        shopwidthanim = 0;
                    }
                }
                else
                {
                    shopwidthanim += shop_speed * shoptim.ElapsedMilliseconds;
                    shoptim.Restart();
                    if (shopwidthanim >= shopfull)
                    {
                        shop_animon = false;
                        shopwidthanim = shopfull;
                    }
                }
            }
        }
        public void graphic_play(System.Drawing.Graphics g)
        {
            g.Clear(System.Drawing.Color.White);
            g.DrawLine(new Pen(Color.Maroon), 0, 0, Z.clwidth, 0);
            g.DrawLine(new Pen(Color.Maroon), 0, 0, 0, Z.clheight);
            g.DrawLine(new Pen(Color.Maroon), Z.clwidth - 1, 0, Z.clwidth - 1, Z.clheight - 1);
            g.DrawLine(new Pen(Color.Maroon), 0, Z.clheight - 1, Z.clwidth - 1, Z.clheight - 1);

            g.FillRectangle(System.Drawing.Brushes.Maroon, 0, 0, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, 1, 0, Z.radangl * 2, Z.radangl * 2);
            g.FillRectangle(System.Drawing.Brushes.Maroon, Z.clwidth - Z.radangl, 0, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, Z.clwidth - 2 * Z.radangl - 1, 0, Z.radangl * 2, Z.radangl * 2);
            g.FillRectangle(System.Drawing.Brushes.Maroon, 0, Z.clheight - Z.radangl, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, 1, Z.clheight - 2 * Z.radangl - 1, Z.radangl * 2, Z.radangl * 2);
            g.FillRectangle(System.Drawing.Brushes.Maroon, Z.clwidth - Z.radangl, Z.clheight - Z.radangl, Z.radangl, Z.radangl);
            g.FillEllipse(System.Drawing.Brushes.White, Z.clwidth - 2 * Z.radangl - 1, Z.clheight - 2 * Z.radangl - 1, Z.radangl * 2, Z.radangl * 2);


            Brush gate_color = Brushes.Red;
            if (immunlast > 1 || immunlast == 1 && !coin_stop())
                gate_color = Brushes.Blue;
            g.FillRectangle(gate_color, 0, 532 / 2 - 50, 3, 100);
            g.FillRectangle(gate_color, 789, 532 / 2 - 50, 3, 100);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0 || i == 4)
                {
                    g.FillEllipse(Brushes.Blue, (int)(coinarr[i].x - coinarr[i].r) - 1, (int)(coinarr[i].y - coinarr[i].r) - 1, (int)(2 * coinarr[i].r) + 2, (int)(2 * coinarr[i].r) + 2);
                }
                coinarr[i].Draw(g);
            }

            if (money_pick != -1)
            {
                var spd = power_kick.x * power_kick.x + power_kick.y * power_kick.y;
                if (spd > Z.vismspd * Z.vismspd)
                {
                    g.DrawLine(System.Drawing.Pens.Red, (int)coinarr[money_pick].x, (int)coinarr[money_pick].y, (int)coinarr[money_pick].x + (int)(power_kick.x * Z.vismspd / Math.Sqrt(spd)), (int)coinarr[money_pick].y + (int)(power_kick.y * Z.vismspd / Math.Sqrt(spd)));
                }
                else
                {
                    g.DrawLine(System.Drawing.Pens.Green, (int)coinarr[money_pick].x, (int)coinarr[money_pick].y, (int)coinarr[money_pick].x + (int)(power_kick.x), (int)coinarr[money_pick].y + (int)(power_kick.y));
                }
            }
            var f = new System.Drawing.Font("Arial", 30);
            g.DrawString(ch1.ToString() + " : " + ch2.ToString(), f, System.Drawing.Brushes.Black, 793 / 2 - 40, 5);
            var fn = new System.Drawing.Font("Arial", 10);
            int yk = 0;
            if (rec.record)
                g.DrawString("rec to " + real_write_repay, fn, Brushes.Black, 60, 5 + (yk++) * 13);
            int cnt_immun = immunlast;
            if (!coin_stop())
                ++cnt_immun;
            if (cnt_immun > 1)
                g.DrawString("immun " + (cnt_immun - 1).ToString(), fn, Brushes.Black, 60, 5 + (yk++) * 13);
            border(g);
        }
        public void graphic_shop(System.Drawing.Graphics g)
        {
            g.Clear(Color.Maroon);
            var fn = new System.Drawing.Font("Courier", 15);
            int y_coin = shop_pick * 20 - 7;
            g.DrawLine(Pens.White, 145, 0, 145, y_coin - 2);
            g.DrawLine(Pens.White, 145, Z.MainForm.ClientSize.Height, 145, y_coin + 22);
            g.DrawLine(Pens.White, 135, y_coin - 2, 145, y_coin - 2);
            g.DrawLine(Pens.White, 135, y_coin + 22, 145, y_coin + 22);
            for (int i = 1; i < Program.countcoin; i++)
            {
                g.DrawString(Z.tcoin[i].name, fn, System.Drawing.Brushes.Bisque, 10, i * 20 - 10);
            }
            if (shop_cursor <= Program.countcoin && shop_cursor != -1)
                g.DrawString(Z.tcoin[shop_cursor].name, fn, System.Drawing.Brushes.White, 10, shop_cursor * 20 - 10);
            fn = new System.Drawing.Font("Courier", 30);
            //g.DrawLine(Pens.Black, 200, 0, 200, Z.clheight);
            g.DrawString("Масса: " + Z.tcoin[shop_pick].m.ToString(), fn, System.Drawing.Brushes.White, 260, 30);
            g.DrawString("Диаметр: " + Z.tcoin[shop_pick].r.ToString(), fn, System.Drawing.Brushes.White, 260, 80);
            g.DrawString("Цена: " + Z.tcoin[shop_pick].cost.ToString(), fn, System.Drawing.Brushes.White, 260, 130);
            int procentacc = (int)((1 - Z.tcoin[shop_pick].accuracy * 8 / Math.PI) * 100);
            g.DrawString("Точность: " + procentacc.ToString(), fn, System.Drawing.Brushes.White, 260, 180);
            g.DrawString("Кол-во: " + us[shop_palyer - 1].coin[shop_pick].ToString(), fn, System.Drawing.Brushes.White, 260, 230);
            g.DrawString("Игрок " + shop_palyer, fn, System.Drawing.Brushes.White, 260, Z.clheight - 120);
            g.DrawString("Денег: " + us[shop_palyer - 1].mon.ToString(), fn, System.Drawing.Brushes.White, 260, Z.MainForm.ClientRectangle.Height - 70);
            g.DrawString("Купить", fn, System.Drawing.Brushes.Bisque, 600, 250);
            g.DrawString("Атака", fn, System.Drawing.Brushes.Bisque, 600, 300);
            g.DrawString("Защита", fn, System.Drawing.Brushes.Bisque, 600, 350);
            g.DrawString("Далее", fn, System.Drawing.Brushes.Bisque, 600, 400);
            if (shop_cursor == Program.countcoin + 1)
                g.DrawString("Купить", fn, System.Drawing.Brushes.White, 600, 250);
            if (shop_cursor == Program.countcoin + 2)
            {
                g.DrawString("Атака", fn, System.Drawing.Brushes.White, 600, 300);
                g.DrawImage(Z.tcoin[us[shop_palyer - 1].atac].picob, 640, 80, 80, 80);
            }
            if (shop_cursor == Program.countcoin + 3)
            {
                g.DrawString("Защита", fn, System.Drawing.Brushes.White, 600, 350);
                g.DrawImage(Z.tcoin[us[shop_palyer - 1].defince].picob, 640, 80, 80, 80);
            }
            if (shop_cursor == Program.countcoin + 4)
                g.DrawString("Далее", fn, System.Drawing.Brushes.White, 600, 400);
            if (!shop_animon)
            {
                g.DrawImage(Z.tcoin[shop_pick].picob, 310, 300, 80, 80);
                g.DrawImage(Z.tcoin[shop_pick].picre, 410, 300, 80, 80);
            }
            else if (shopold)
            {
                g.DrawImage(Z.tcoin[shopoldcoin].picob, 310 + 40 - 40 * shopwidthanim / shopfull, 300, 80 * shopwidthanim / shopfull, 80);
                g.DrawImage(Z.tcoin[shopoldcoin].picre, 410 + 40 - 40 * shopwidthanim / shopfull, 300, 80 * shopwidthanim / shopfull, 80);
            }
            else if (!shopold)
            {
                g.DrawImage(Z.tcoin[shop_pick].picob, 310 + 40 - 40 * shopwidthanim / shopfull, 300, 80 * shopwidthanim / shopfull, 80);
                g.DrawImage(Z.tcoin[shop_pick].picre, 410 + 40 - 40 * shopwidthanim / shopfull, 300, 80 * shopwidthanim / shopfull, 80);
            }
            border(g);
        }
        public void graphic_pause(System.Drawing.Graphics g)
        {
            g.Clear(Color.Maroon);
            var fn = new System.Drawing.Font("Courier", 30);
            Brush br = Brushes.Bisque;
            if (pause_cursor == 1)
                br = Brushes.White;
            g.DrawString("Загрузить игру", fn, br, 260, 50);
            br = Brushes.Bisque;
            if (pause_cursor == 2)
                br = Brushes.White;
            //if (!Z.use_internet)
            //    br = Brushes.Gray;
            g.DrawString("Сохранить игру", fn, br, 250, 120);
            br = Brushes.Bisque;
            if (pause_cursor == 3)
                br = Brushes.White;
            if (rec.record)
            {
                g.DrawString("Остановить запись", fn, br, 220, 190);
                br = Brushes.Bisque;
                if (pause_cursor == 4)
                    br = Brushes.White;
                g.DrawString("Удалить запись", fn, br, 253, 260);
            }
            else
                g.DrawString("Начать запись", fn, br, 260, 190);
            fn = new System.Drawing.Font("Courier", 20);
            g.DrawString("Введите имя файла, куда сохранится следующий ваш replay", fn, Brushes.White, 10, 375);
            g.FillRectangle(Brushes.White, 190, 440, 420, 50);
            g.DrawString(write_replay, fn, Brushes.Black, 210, 445);
            border(g);
        }
        public void border(System.Drawing.Graphics g)
        {
            if (Z.mininmon)
                g.DrawString("-", new Font("Arial", 15), Brushes.Bisque, Z.clwidth - 35, -3);
            else
                g.DrawString("-", new Font("Arial", 15), Brushes.CornflowerBlue, Z.clwidth - 35, -3);

            if (Z.closeon)
                g.DrawString("X", new Font("Arial", 15), Brushes.Bisque, Z.clwidth - 18, -3);
            else
                g.DrawString("X", new Font("Arial", 15), Brushes.CornflowerBlue, Z.clwidth - 18, -3);

        }
        public void save_game()
        {
            //if (!Z.use_internet)
            //    return;
            try
            {
                var oup = System.IO.File.CreateText("save.txt");
                oup.WriteLine(us[0].ToString());
                oup.WriteLine(us[1].ToString());
                oup.WriteLine(mem_sit + " " + ch1 + " " + ch2);
                oup.Close();
                Spinet.Spinet.SpiFile("save.txt");
            }
            catch
            {

            }
        }
        public void read_game()
        {
            try
            {
                Spinet.Spinet.UnSpiFile("save.txt");
                var inp = System.IO.File.OpenText("save.txt");
                us[0].ReadSave(inp.ReadLine());
                us[1].ReadSave(inp.ReadLine());
                var sr = inp.ReadLine().Split().Select(x1 => int.Parse(x1)).ToArray();
                inp.Close();
                Spinet.Spinet.SpiFile("save.txt");
                sit = sr[0];
                ch1 = sr[1];
                ch2 = sr[2];
                if (sit > 0)
                    shop_palyer = sit;
                new_game(false);
            }
            catch
            { }
        }
        public void start_record()
        {
            if (write_replay == "")
                write_replay = "replay";
            try
            {
                rec.record = !rec.record;
                if (rec.record)
                {
                    string filename = write_replay + ".chrpl";
                    var files = Directory.GetFiles(".\\").Select(s => s.ToLower());
                    int i = 0;
                    while (files.Contains(".\\" + filename))
                    {
                        ++i;
                        filename = write_replay + i + ".chrpl";
                    }
                    real_write_repay = filename;
                    rec.oup = System.IO.File.CreateText(filename);
                }
            }
            catch
            { }
        }
        public void stop_record(bool delete)
        {
            if (rec.record)
            {
                rec.oup.Close();
                rec.record = false;
                if (delete)
                    File.Delete(real_write_repay);
            }
        }
        public void klik(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (sit == 3)
                {
                    if (write_replay == "")
                        write_replay = "replay";
                    sit = mem_sit;
                }
                else
                {
                    mem_sit = sit;
                    sit = 3;
                }
            }
            if (sit == 1 || sit == 2)
            {
                if (e.KeyCode == Keys.S && !e.Control)
                {
                    shop_pick = (shop_pick + 1) % Program.countcoin;
                    if (shop_pick == 0)
                        shop_pick++;
                }
                if (e.KeyCode == Keys.W)
                {
                    shop_pick = (shop_pick + Program.countcoin - 1) % Program.countcoin;
                    if (shop_pick == 0) shop_pick--;
                    shop_pick = (shop_pick + Program.countcoin) % Program.countcoin;
                }
                if (e.KeyCode == Keys.Enter)
                    buy_coin();
                if (e.KeyCode == Keys.A)
                    set_atac();
                if (e.KeyCode == Keys.D)
                    set_def();
                if (e.KeyCode == Keys.N)
                    next_magaz();
            }
            if (sit == 3)
            {
                int code = (int)e.KeyCode;
                if (code >= (int)Keys.A && code <= (int)System.Windows.Forms.Keys.Z && write_replay.Length <= 10)
                {
                    char c = (char)(code - Keys.A + 'a');
                    write_replay += c;
                }
                if (code == (int)Keys.Back && write_replay.Length > 0)
                    write_replay = write_replay.Substring(0, write_replay.Length - 1);
                return;
            }
            if (e.KeyCode == Keys.S && e.Control)
                save_game();
            if (e.KeyCode == Keys.R && e.Control)
                read_game();
            if (e.KeyCode == Keys.R && !e.Control)
            {
                if (!rec.record)
                    start_record();
                else
                    stop_record(false);
            }
            if (e.KeyCode == Keys.M)
                stop_record(true);
        }
        public bool coin_stop()
        {
            bool an = true;
            for (int i = 0; i < coinarr.Length; i++)
            {
                an = an && coinarr[i].stop();
            }
            return an;
        }
        public void mdklik(object sender, MouseEventArgs e)
        {
            if (e.Y < 20)
            {
                Z.movewindow = true;
                Z.movewindowx = -e.X;
                Z.movewindowy = -e.Y;
            }
            if (e.Y < 20 && e.X > Z.clwidth - 20 && e.Button == MouseButtons.Left)
                Application.Exit();
            if (e.Y < 20 && e.X > Z.clwidth - 35 && e.X <= Z.clwidth - 20)
            {
                Z.MainForm.WindowState = FormWindowState.Minimized;
                Z.movewindow = false;
                Z.mininmon = false;
            }
            if (sit == 0 && coin_stop())
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    money_pick = -1;
                    return;
                }
                for (int i = now_palyer * 4 + 1; i < now_palyer * 4 + 4; i++)
                {

                    if ((coinarr[i].x - e.X) * (coinarr[i].x - e.X) +
                        (coinarr[i].y - e.Y) * (coinarr[i].y - e.Y) <
                        coinarr[i].r * coinarr[i].r)
                    {
                        money_pick = i;
                    }
                }
                return;
            }
            if (sit == 1 || sit == 2)
            {
                if (e.Y > 20 && e.X < 300 && e.Y < Program.countcoin * 20 && shop_pick != e.Y / 20)
                {
                    if (!shopold)
                        shopoldcoin = shop_pick;
                    shop_animon = true;
                    shoptim.Restart();
                    shopold = true;
                    shop_pick = e.Y / 20;
                }
                if (e.Y > 250 && e.Y < 300 && e.X > 600)
                    buy_coin();
                if (e.Y > 300 && e.Y < 350 && e.X > 600)
                    set_atac();
                if (e.Y > 350 && e.Y < 400 && e.X > 600)
                    set_def();
                if (e.Y > 400 && e.Y < 450 && e.X > 600)
                    next_magaz();
                return;
            }
            if (sit == 3)
            {
                if (pause_cursor == 1)
                    read_game();
                if (pause_cursor == 2)
                    save_game();
                if (rec.record && pause_cursor == 3)
                    stop_record(false);
                else if (pause_cursor == 3)
                    start_record();
                else if (pause_cursor == 4)
                    stop_record(true);
            }
        }
        public void mmklik(object sender, MouseEventArgs e)
        {
            if (Z.movewindow && e.Button != MouseButtons.None)
            {
                Z.MainForm.Left += e.X + Z.movewindowx;
                Z.MainForm.Top += e.Y + Z.movewindowy;
            }
            if (MouseButtons.None == e.Button)
                Z.movewindow = false;
            if (e.Y < 20 && e.X > Z.clwidth - 20)
                Z.closeon = true;
            else
                Z.closeon = false;
            if (e.Y < 20 && e.X > Z.clwidth - 35 && e.X <= Z.clwidth - 20)
                Z.mininmon = true;
            else
                Z.mininmon = false;
            if (sit == 1 || sit == 2)
            {
                if (e.Y > 20 && e.X < 300 && e.Y < Program.countcoin * 20)
                    shop_cursor = e.Y / 20;
                else if (e.Y > 250 && e.Y < 450 && e.X > 600)
                    shop_cursor = Program.countcoin + (e.Y - 250) / 50 + 1;
                else
                    shop_cursor = -1;
            }
            if (sit == 0)
            {
                if (e.Button != MouseButtons.None && money_pick != -1)
                {
                    power_kick.x = -coinarr[money_pick].x + e.X;
                    power_kick.y = -coinarr[money_pick].y + e.Y;
                }
            }
            if (sit == 3)
            {
                if (e.X >= 200 && e.X <= Z.clwidth - 200)
                {
                    if (e.Y >= 45 && e.Y < 115)
                        pause_cursor = 1;
                    if (e.Y >= 115 && e.Y < 185)
                        pause_cursor = 2;
                    if (e.Y >= 185 && e.Y < 255)
                        pause_cursor = 3;
                    if (rec.record && e.Y >= 225 && e.Y < 295)
                        pause_cursor = 4;
                    if (e.Y < 45 || e.Y >= 295)
                        pause_cursor = -1;
                }
                else
                    pause_cursor = -1;
            }
        }
        public void muklik(object sender, MouseEventArgs e)
        {
            Z.movewindow = false;
            if (sit == 0)
            {
                if (money_pick != -1)
                {
                    var otn = 1.0 * Z.mspd / Z.vismspd;
                    var spd = (power_kick.x * power_kick.x + power_kick.y * power_kick.y) * otn * otn;
                    Point speed = new Point();
                    if (spd > Z.mspd * Z.mspd)
                    {
                        speed.x = -(power_kick.x * otn * Z.mspd / Math.Sqrt(spd));
                        speed.y = -(power_kick.y * otn * Z.mspd / Math.Sqrt(spd));
                    }
                    else
                    {
                        speed.x = -power_kick.x * otn;
                        speed.y = -power_kick.y * otn;
                    }
                    Point rot = geom.RandomAngel(-coinarr[money_pick].accuracy, coinarr[money_pick].accuracy);
                    speed = geom.Rotate(speed, rot);
                    coinarr[money_pick].vec = speed;
                    money_pick = -1;
                    now_palyer = (now_palyer + 1) % 2;
                    immunlast = Math.Max(0, immunlast - 1);
                }
            }
        }
        public void set_def()
        {
            if (us[shop_palyer - 1].coin[shop_pick] >= 1 &&
                       (Z.tcoin[shop_pick].m >= Z.tcoin[us[shop_palyer - 1].atac].m ||
                        Z.tcoin[shop_pick].r >= Z.tcoin[us[shop_palyer - 1].atac].r))
            {
                us[shop_palyer - 1].coin[us[shop_palyer - 1].defince] += 1;
                us[shop_palyer - 1].coin[shop_pick] -= 1;
                us[shop_palyer - 1].defince = shop_pick;
            }
        }
        public void set_atac()
        {
            if (us[shop_palyer - 1].coin[shop_pick] >= 3 &&
               (Z.tcoin[shop_pick].m <= Z.tcoin[us[shop_palyer - 1].defince].m ||
                Z.tcoin[shop_pick].r <= Z.tcoin[us[shop_palyer - 1].defince].r))
            {

                us[shop_palyer - 1].coin[us[shop_palyer - 1].atac] += 3;
                us[shop_palyer - 1].coin[shop_pick] -= 3;
                us[shop_palyer - 1].atac = shop_pick;
            }

        }
        public void buy_coin()
        {
            if (us[shop_palyer - 1].mon >= Z.tcoin[shop_pick].cost)
            {
                us[shop_palyer - 1].mon -= Z.tcoin[shop_pick].cost;
                us[shop_palyer - 1].coin[shop_pick]++;// -= Z.tcoin[magaz.vb].stoim;
            }
        }
        public void next_magaz()
        {
            if (sit == 1)
            {
                sit++;
                shop_palyer = 2;
            }
            else
            {
                sit = 0;
                new_game();
            }
        }
        public void close()
        {
            if (rec.record)
                stop_record(false);
            if (write_replay != " ")
            {
                try
                {
                    var fl = File.CreateText("./data.txt");
                    fl.WriteLine(write_replay);
                    fl.Close();
                }
                catch { }
            }
        }
    }

    class user
    {
        public int[] coin { get; set; }
        public int atac { get; set; }
        public int defince { get; set; }
        public int mon { get; set; }
        public user()
        {
            coin = new int[Program.countcoin];
            atac = 1;
            defince = 1;
            mon = 0;
        }
        public override string ToString()
        {
            var an = coin[1].ToString();
            for (int i = 2; i < Program.countcoin; i++)
            {
                an = an + " " + coin[i];
            }
            return an + " " + atac.ToString() + " " + defince.ToString() + " " + mon;
        }
        public void ReadSave(string s)
        {
            var sr = s.Split().Select(x1 => int.Parse(x1)).ToArray();
            for (int i = 0; i < Program.countcoin; i++)
            {
                coin[i] = 0;
            }
            for (int i = 1; i < Math.Min(sr.Length - 3, Program.countcoin); i++)
            {
                coin[i] = sr[i - 1];
            }

            atac = sr[sr.Length - 3];
            defince = sr[sr.Length - 2];
            mon = sr[sr.Length - 1];
        }
        public int sen_arm()
        {
            return Z.tcoin[atac].cost * 3 + Z.tcoin[defince].cost;
        }
    }

    class cointype
    {
        public int r { get; private set; }
        public int m { get; private set; }
        public int cost { get; set; }
        public string name { get; private set; }
        public System.Drawing.Image picob { get; private set; }
        public Image picre { get; private set; }
        public double accuracy { get; private set; }
        public int valut = 0;
        public cointype(int r1, int m1, int s1, string n, string fileor, string filere, string vlt, string ras = ".png")
        {
            m = m1;
            r = r1;
            cost = s1;
            name = n;
            picob = Image.FromFile("textures\\" + fileor + ras);
            picre = Image.FromFile("textures\\" + filere + ras);
            int fine = m1 - 300 + (r1 - 21) * 120;//what bigger than 1 ruble
            if (fine <= 0)
                accuracy = 0;
            else
                accuracy = Math.PI / 8 * fine / (900 + 7 * 120);//accuracy for 2 pounds = PI / 8
            if (vlt == "r")
                valut = 1;
            if (vlt == "s")
                valut = 2;
            if (vlt == "e")
                valut = 3;
            if (vlt == "f")
                valut = 4;
            if (vlt == "cd")
                valut = 5;
        }
    }
}
