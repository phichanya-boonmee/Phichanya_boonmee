using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.button1.MouseDown += button1_MouseDown;
            this.button1.MouseUp += button1_MouseUp;


        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Text = "up";
            Button butt = (Button)sender;
            butt.Text = "bbb";
            butt.BackColor = Color.Yellow;


        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = "down";
            Button butt = (Button)sender;
            butt.Text = "aaa";
            butt.BackColor = Color.Red;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = "Move";
            Button butt = (Button)sender;
            butt.Text = "ccc";
            Point p = butt.Location;
            butt.Location = new Point(e.X - 1, e.Y - 1);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
