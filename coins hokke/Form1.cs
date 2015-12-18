using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace coins_hockey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Paint += drawingr;
            this.KeyDown += Program.klik;
            //checkBox1.BackColor = System.Drawing.Color.Transparent;
            //this.pictureBox1.MouseClick += Program.mklik;
            this.pictureBox1.MouseDown += Program.mdklik;
            this.pictureBox1.MouseUp += Program.muklik;
            this.pictureBox1.MouseMove += Program.mmklik;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void drawoll()
        {
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    
    }
}
