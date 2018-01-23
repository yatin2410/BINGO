using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace BingoSoft
{
    public partial class Form1 : Form
    {
        Socket sck;
        EndPoint epLocal, epRemort;
        byte[] buffer;

        string mip = "";
        string fip = "";
        string mport = "";
        string fport = "";

        public Form1()
        {
            InitializeComponent();
            
            listMessages.Hide();
            textMessage.Hide();
            buttonSend.Hide();

            buttonConnect.MouseHover += buttonConnect_MouseHover;
            buttonConnect.MouseLeave += buttonConnect_MouseLeave;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set up socket 
            sck = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);

            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            //get user IP
            mip = GetLocalIP();

             
            my_ip.Text = Func(mip);

            //textRemortIP.Text = GetLocalIP();

        }


        private string Func(string mip)
        {
            string s = mip;

            int i = 0, count = 0;
            string output = "";
            while (i < s.Length)
            {
                if (s[i] == '.' && count > 0)
                {
                    i++;
                    while (i < s.Length)
                    {

                        if (s[i] == '.')
                            output += getChar(10);
                        else
                            output += getChar(s[i] - '0');
                        i++;
                    }
                }
                else if (s[i] == '.')
                {
                    count++;
                }
                i++;

            }
            return output;
        }

        private string getChar(int i)
        {

            if (i == 0) return "a";
            if (i == 1) return "b";
            if (i == 2) return "c";
            if (i == 3) return "d";
            if (i == 4) return "e";
            if (i == 5) return "f";
            if (i == 6) return "g";
            if (i == 7) return "h";
            if (i == 8) return "i";
            if (i == 9) return "j";
            return "o";

        }

        private string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }

            }
            return "127.0.0.1";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

            fip = decode(friend_ip.Text);
            mport = getport(my_port.Text);
            fport = getport(friend_port.Text);

             

            //binding Socket
            try
            {
                epLocal = new IPEndPoint(IPAddress.Parse(mip), Convert.ToInt32(mport));
                sck.Bind(epLocal);

                //connecting to remort IP
                epRemort = new IPEndPoint(IPAddress.Parse(fip), Convert.ToInt32(fport));
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
            MessageBox.Show("Connected");

            

            
            Form3 ss = new Form3(mip,mport,fip,fport);
            ss.Show();

            this.Hide();




        }

        private string getport(string text)
        {
            string s = text;
            int port = 0;
            for (int i = 0; i < s.Length; i++)
            {
                port = port + (int)s[i] * (i + 1);
            }
            port = port % 10000;

            return port.ToString();
        }

        private string decode(string text)
        {
            string str = "";
            int count = 0;
            int i = 0;
            while(true)
            {
                if (mip[i] == '.')
                    count++;
                if (count == 2)
                    break;
                
                str = str + mip[i];
                i++;
            }

            str = str + ".";
            string input = text;
            string output = "";
            int j = 0;
            while (j < input.Length)
            {
                output += getNumber((char)input[j]);
                j++;
            }

            output = str + output;

            return output;
        }

        private string getNumber(char i)
        {
            if (i == 'a') return "0";
            if (i == 'b') return "1";
            if (i == 'c') return "2";
            if (i == 'd') return "3";
            if (i == 'e') return "4";
            if (i == 'f') return "5";
            if (i == 'g') return "6";
            if (i == 'h') return "7";
            if (i == 'i') return "8";
            if (i == 'j') return "9";
            return ".";
        }

        private void buttonConnect_MouseHover(object sender, EventArgs e)
        {
            this.buttonConnect.BackColor = Color.SlateGray;
        }
        private void buttonConnect_MouseLeave(object sender, EventArgs e)
        {
            this.buttonConnect.BackColor = Color.PaleTurquoise;
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            //convert string message to byte array

            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aEncoding.GetBytes(textMessage.Text);

            //sending the Encoded Message 

            sck.Send(sendingMessage);

            //ading to the listbox
            listMessages.Items.Add("Me: " + textMessage.Text);
            textMessage.Text = "";



        }

        private void listMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed By:- \n Yatin Patel \n Milan Dungarani \n Pratik Rajani ");
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

                listMessages.Items.Add("Friend: " + receivedMessage);

                buffer = new byte[1500];

                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemort, new AsyncCallback(MessageCallBack), buffer);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }




        }
    }
}
