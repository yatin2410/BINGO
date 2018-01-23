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
    public partial class Form5 : Form
    {


        Socket sck;
        EndPoint epLocal, epRemort;
        byte[] buffer;

        bool ishismove = false;
        int id; 

        string my_ip,my_port,friend_ip,friend_port;

        bool win = false;
        int winCount = 0;



        int[] index1 = new int[] { 1, 2, 3, 4, 5 };
        int[] index2 = new int[] { 6, 7, 8, 9, 10 };
        int[] index3 = new int[] { 11, 12, 13, 14, 15 };
        int[] index4 = new int[] { 16, 17, 18, 19, 20 };
        int[] index5 = new int[] { 21, 22, 23, 24, 25 };
        int[] index6 = new int[] { 1, 6, 11, 16, 21 };
        int[] index7 = new int[] { 2, 7, 12, 17, 22 };
        int[] index8 = new int[] { 3, 8, 13, 18, 23 };
        int[] index9 = new int[] { 4, 9, 14, 19, 24 };
        int[] index10 = new int[] { 5, 10, 15, 20, 25 };
        int[] index11 = new int[] { 1, 7, 13, 19, 25 };
        int[] index12 = new int[] { 5, 9, 13, 17, 21 };


        HashSet<int> hSet1;
        HashSet<int> hSet2;
        HashSet<int> hSet3;
        HashSet<int> hSet4;
        HashSet<int> hSet5;
        HashSet<int> hSet6;
        HashSet<int> hSet7;
        HashSet<int> hSet8;
        HashSet<int> hSet9;
        HashSet<int> hSet10;
        HashSet<int> hSet11;
        HashSet<int> hSet12;




        public Form5()
        {
            InitializeComponent();
        }

        public Form5(FlowLayoutPanel fl, string str1,string str2,string str3,string str4)
        {
            InitializeComponent();

            listMessages.Hide();
            textBox1.Hide();
            textBox3.Hide();
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Hide();

            
            hSet1 = new HashSet<int>(index1);
            hSet2 = new HashSet<int>(index2);
            hSet3 = new HashSet<int>(index3);
            hSet4 = new HashSet<int>(index4);
            hSet5 = new HashSet<int>(index5);
            hSet6 = new HashSet<int>(index6);
            hSet7 = new HashSet<int>(index7);
            hSet8 = new HashSet<int>(index8);
            hSet9 = new HashSet<int>(index9);
            hSet10 = new HashSet<int>(index10);
            hSet11 = new HashSet<int>(index11);
            hSet12 = new HashSet<int>(index12);




            int n = 25;
            for (int i = 1; i <= n; i++)
            {
                Control[] c = fl.Controls.Find(i.ToString(), false);

                flowLayoutPanel1.Controls.Add(c[0]);

            }



            this.my_ip = str1;
            this.my_port = str2;
            this.friend_ip = str3;
            this.friend_port = str4;




           // MessageBox.Show(my_ip + " " + my_port + " " + friend_ip + " " + friend_port);


            //set up socket 
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            //get user IP

            //textRemortIP.Text = GetLocalIP();
            my_ip = GetLocalIP();


            try
            { 

            epLocal = new IPEndPoint(IPAddress.Parse(my_ip), Convert.ToInt32(my_port.Length));
                sck.Bind(epLocal);

                //connecting to remort IP
                epRemort = new IPEndPoint(IPAddress.Parse(friend_ip), Convert.ToInt32(friend_port.Length));
                sck.Connect(epRemort);

                //Listning to specific port

                buffer = new byte[1500];

                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemort, new AsyncCallback(MessageCallBack), buffer);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't connect");
                return;

            }

        //MessageBox.Show("Connected");


            //MessageBox.Show(flowLayoutPanel1.Size.ToString());

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!ishismove)
            {
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                byte[] sendingMessage = new byte[1500];
                sendingMessage = aEncoding.GetBytes(textMessage.Text);

                //sending the Encoded Message 

                sck.Send(sendingMessage);

                //ading to the listbox
                listMessages.Items.Add("Me: " + textMessage.Text);

                
                    ishismove = true;
                    id = Int32.Parse(textMessage.Text);
                    
                    if(id>=26 || id<=0)
                    {

                    MessageBox.Show("please enter number between 1 to 25 or not connected!!!");
                    textMessage.Text = "";

                    return;
                }

                textBox3.Show();

                     Move(id);


                
                
                textMessage.Text = "";
            }
            else
                textMessage.Text = "";

            SendKeys.Send("{TAB}");

        }

        private void Move(int id)
        {

            for (int i = 1; i <= 25; i++)
            {
                Control[] c = flowLayoutPanel1.Controls.Find(i.ToString(), false);

                if(c[0].Text==id.ToString())
                {
                   // MessageBox.Show(c[0].Text);
                    c[0].BackColor = Color.Red;
                    checkfun(i);
                    return;
                }

            }


        }

        private void checkfun(int currentPosition)
        {

            if (hSet1.Contains(currentPosition) && hSet1.Count != 0)
            {
                hSet1.Remove(currentPosition);
                if (hSet1.Count == 0) winCount++;
            }

            if (hSet2.Contains(currentPosition) && hSet2.Count != 0)
            {
                hSet2.Remove(currentPosition);
                if (hSet2.Count == 0) winCount++;
            }

            if (hSet3.Contains(currentPosition) && hSet3.Count != 0)
            {
                hSet3.Remove(currentPosition);
                if (hSet3.Count == 0) winCount++;
            }

            if (hSet4.Contains(currentPosition) && hSet4.Count != 0)
            {
                hSet4.Remove(currentPosition);
                if (hSet4.Count == 0) winCount++;
            }

            if (hSet5.Contains(currentPosition) && hSet5.Count != 0)
            {
                hSet5.Remove(currentPosition);
                if (hSet5.Count == 0) winCount++;
            }

            if (hSet6.Contains(currentPosition) && hSet6.Count != 0)
            {
                hSet6.Remove(currentPosition);
                if (hSet6.Count == 0) winCount++;
            }

            if (hSet7.Contains(currentPosition) && hSet7.Count != 0)
            {
                hSet7.Remove(currentPosition);
                if (hSet7.Count == 0) winCount++;
            }

            if (hSet8.Contains(currentPosition) && hSet8.Count != 0)
            {
                hSet8.Remove(currentPosition);
                if (hSet8.Count == 0) winCount++;
            }

            if (hSet9.Contains(currentPosition) && hSet9.Count != 0)
            {
                hSet9.Remove(currentPosition);
                if (hSet9.Count == 0) winCount++;
            }

            if (hSet10.Contains(currentPosition) && hSet10.Count != 0)
            {
                hSet10.Remove(currentPosition);
                if (hSet10.Count == 0) winCount++;
            }

            if (hSet11.Contains(currentPosition) && hSet11.Count != 0)
            {
                hSet11.Remove(currentPosition);
                if (hSet11.Count == 0) winCount++;
            }

            if (hSet12.Contains(currentPosition) && hSet12.Count != 0)
            {
                hSet12.Remove(currentPosition);
                if (hSet12.Count == 0) winCount++;
            }

            if(winCount>=1)
            {
                button1.Show();
            }

            if (winCount >= 2)
            {
                button2.Show();
            }

            if (winCount >= 3)
            {
                button3.Show();
            }

            if (winCount >= 4)
            {
                button4.Show();
            }

            if (winCount >= 5)
            {
                button5.Show();
            }

            if (winCount == 5)
            {

                MessageBox.Show("Yayy! BINGO");

            }



        }


        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                byte[] receivedData = new byte[1500];
                receivedData = (byte[])aResult.AsyncState;

                //covrting byte[] to string

                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receivedMessage = aEncoding.GetString(receivedData);


                //Adding this message into listBox

                

                textBox1.Text = receivedMessage;

                try
                {
                    ishismove = false;
                    textBox3.Hide();
                    id = Int32.Parse(receivedMessage);

                    Move(id);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("please enter number between 1 to 25 or not connected!!!");
                    
                }

                listMessages.Items.Add("Friend: " + receivedMessage);

                buffer = new byte[1500];

                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemort, new AsyncCallback(MessageCallBack), buffer);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }




        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "Your Turn";
            
            if(ishismove)
            {
                textBox2.Hide();
            }
            else
            {
                textBox2.Show();
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = "Your Frinds Turn";

            if (!ishismove)
            {
                textBox3.Hide();
            }

            else
            {
                textBox3.Show();
            }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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
            b.Click += b_Click;
            return b;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3(my_ip,my_port,friend_ip,friend_port);
            ss.Show();

            this.Hide();
            
        }

        private void b_Click(object sender, EventArgs e)
        {

            Button b = (Button)sender;

            textMessage.Text = b.Text;
           // MessageBox.Show(b.Text);

            buttonSend_Click();
           // textMessage.Text = "";

            return;
            
        }

        private void buttonSend_Click()
        {
            if (!ishismove)
            {
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                byte[] sendingMessage = new byte[1500];
                sendingMessage = aEncoding.GetBytes(textMessage.Text);

                //sending the Encoded Message 

                sck.Send(sendingMessage);

                //ading to the listbox
                listMessages.Items.Add("Me: " + textMessage.Text);


                ishismove = true;
                id = Int32.Parse(textMessage.Text);

                if (id >= 26 || id <= 0)
                {

                    MessageBox.Show("please enter number between 1 to 25");
                    textMessage.Text = "";
                    return;
                }
                textBox3.Show();

                Move(id);




                textMessage.Text = "";
            }
            else
                textMessage.Text = "";


        }

        private void Form5_Load(object sender, EventArgs e)
        {

            
        }

        private string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }

            }
            return "127.0.0.1";
        }


         

 
    }
}
