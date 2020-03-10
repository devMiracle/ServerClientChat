using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client client = new Client("127.0.0.1", 1024);
            //client.SendMessageToServer(textBox1.Text);
            //listBox1.Text = client.SendMessageToServer(textBox1.Text);
            listBox1.Items.Add(client.SendMessageToServer(textBox1.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
