using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingoSoft
{
    public partial class Form3 : Form
    {
        private Socket sck;

        byte[] buffer;
        EndPoint epLocal, epRemort;
        string str1, str2, str3, str4;

        public Form3()
        {
            InitializeComponent();
        
            flowLayoutPanel1.Hide();

            button1.MouseHover += buttonConnect_MouseHover;
            button1.MouseLeave += buttonConnect_MouseLeave;

            button2.MouseHover += button_MouseHover;
            button2.MouseLeave += button_MouseLeave;

            int n = 25;
            for (int i = 1; i <= n; i++)
            {
                flowLayoutPanel1.Controls.Add(btn(i));
            }

        }

        public Form3(String str1,string str2,string str3,string str4)
        {
            InitializeComponent();

            flowLayoutPanel1.Hide();

            int n = 25;
            for (int i = 1; i <= n; i++)
            {
                flowLayoutPanel1.Controls.Add(btn(i));
            }

            this.str1 = str1;
            this.str2 = str2;
            this.str3 = str3;
            this.str4 = str4;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            selectRandom();
           // MessageBox.Show(str1 + " " + str2 + " " + str3 + " " + str4);
            Form5 ss = new Form5(flowLayoutPanel1,str1,str2,str3,str4);
            ss.Show();

            this.Hide();
           

        }

        private void selectRandom()
        {

            Random rm = new Random();

            bool[] fl = new bool[26];
            for (int i = 1; i <= 25; i++) fl[i] = false;

            int y = 1;

            while(y<=25)
            {
                int z = rm.Next(1, 26);
                //MessageBox.Show(z.ToString() + " " + y.ToString());
                if (!fl[z])
                {
                    
                    fl[z] = true;
                    Control[] c = flowLayoutPanel1.Controls.Find(y.ToString(), false);

                   // MessageBox.Show(y.ToString() + " " + z.ToString());
                    c[0].Text = z.ToString();
                    y++;
                }

            }



        }

        Button btn(int i)
        {
            Button b = new Button();
            b.Name = i.ToString();
            b.Font = new Font(FontFamily.GenericSansSerif,
            14.0F);
            b.TabStop = false;
            b.Width = 50;
            b.Height = 50;
            b.Text = i.ToString();
            b.Click += b_Click;
            b.BackColor = Color.Transparent;
            return b;
        }


        private void b_Click(object sender, EventArgs e)
        {
                 return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 ss = new Form4(sck,str1,str2,str3,str4);
            ss.Show();
            this.Hide();
        }

        private void buttonConnect_MouseHover(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.SlateGray;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonConnect_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.PaleTurquoise;
        }

        private void button_MouseHover(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.SlateGray;
        }
        private void button_MouseLeave(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.PaleTurquoise;
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
