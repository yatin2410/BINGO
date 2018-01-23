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
    public partial class Form4 : Form
    {
        public int index = 1;
        private Socket sck;
        EndPoint epLocal, epRemort;
        string str1, str2, str3, str4;
        byte[] buffer;
        public Form4()
        {
            InitializeComponent();

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

        public Form4(Socket sck,string str1,string str2,String str3,string str4)
        {
            InitializeComponent();

            int n = 25;
            for (int i = 1; i <= n; i++)
            {
                flowLayoutPanel1.Controls.Add(btn(i));
            }

            this.sck = sck;
            this.str1 = str1;
            this.str2 = str2;
            this.str3 = str3;
            this.str4 = str4;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(flowLayoutPanel1.Size.ToString());

            if(index!=26)
            {
                MessageBox.Show("Please fill all boxes");
                return;
            }

            Form5 ss = new Form5(flowLayoutPanel1,str1,str2,str3,str4);
            ss.Show();

            this.Hide();



        }

        Button btn(int i)
        {
            Button b = new Button();
            b.Name = i.ToString();
            b.Font = new Font(FontFamily.GenericSansSerif,
            14.0F);
            b.Width = 50;
            b.Height = 50;
            b.BackColor = Color.Transparent;
            b.TabStop = false;
            b.Click += b_Click;
            return b;
        }

        private void b_Click(object sender, EventArgs e)
        {
            
            Button b = (Button)sender;
            if (b.Text != "")
                return;
            b.Text = index.ToString();
            index++;

        }

        private void button2_Click(object sender, EventArgs e)
        {


            flowLayoutPanel1.Controls.Clear();
            int n = 25;
            for(int i=1;i<=25;i++)
            {
                flowLayoutPanel1.Controls.Add(btn(i));
            }

        }

        private void buttonConnect_MouseHover(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.SlateGray;
        }
        private void buttonConnect_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.PaleTurquoise;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button_MouseHover(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.SlateGray;
        }
        private void button_MouseLeave(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.PaleTurquoise;
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
